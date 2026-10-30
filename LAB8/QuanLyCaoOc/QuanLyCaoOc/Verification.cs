using System.Data;
using System.Globalization;
using Microsoft.Data.SqlClient;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Forms;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc;

internal static class Verification
{
    static readonly List<string> log=[];
    static void Check(string name,bool condition)
    { if(!condition)throw new Exception("FAIL: "+name);log.Add("PASS: "+name); }
    static void Reject(string name,Action action)
    { bool rejected=false;try{action();}catch(InvalidOperationException){rejected=true;}catch(SqlException){rejected=true;}Check(name,rejected); }
    static object Scalar(string sql,params object?[] args){using var c=Db.Open();return Db.Scalar(c,null,sql,args)!;}
    static string Status(string hd)=>(string)Scalar("SELECT TrangThai FROM HopDong WHERE MaHD=@p0",hd);
    static int Count(string table)=>Convert.ToInt32(Scalar("SELECT COUNT(*) FROM "+table));
    static readonly DateTime Jan=new(2026,1,1), Sep=new(2026,9,30);
    public static int Run()
    {
        string original=Db.ConnectionString, database="QuanLyCaoOcLAB8_TEST_"+Guid.NewGuid().ToString("N");int exit=0;
        try
        {
            var settings=new SqlConnectionStringBuilder(original){InitialCatalog=database};Db.ConnectionString=settings.ConnectionString;
            Db.Initialize();Check("19 domain tables",Count("sys.tables")==19);
            int sampleCharges=Count("KhoanPhaiThu"),sampleReceipts=Count("PhieuThu");Db.Initialize();
            Check("SQL can run twice without duplicate sample charges or receipts",Count("KhoanPhaiThu")==sampleCharges&&Count("PhieuThu")==sampleReceipts);
            // Dedicated fixture rooms avoid collisions with the bundled demonstration records.
            Db.Transaction((c,t)=>{for(int i=1;i<=6;i++)Db.Exec(c,t,"INSERT VanPhong VALUES(@p0,'T1',50,N'Đông',1000000,'2025-01-01',0)","TEST"+i);return true;});
            var rent=new HopDongService();var pay=new ThanhToanService();var renewal=new GiaHanService();
            HopDongRequest Request(string room,int months=12)=>new("CT01",Jan,Jan,months,[new(room,1000000)],Guid.NewGuid());
            Reject("Reject lease shorter than six months",()=>rent.Ky(Request("TEST1",5)));
            int before=Count("HopDong");
            Reject("Invalid second room rolls back whole contract",()=>rent.Ky(new("CT01",Jan,Jan,12,[new("TEST1",1000000),new("MISSING",1000000)],Guid.NewGuid())));
            Check("No orphan contract after rollback",Count("HopDong")==before);
            var req=new HopDongRequest("CT01",Jan,Jan,12,[new("TEST1",1000000),new("TEST2",2000000)],Guid.NewGuid());
            string hd=rent.Ky(req);Check("Contract retry is idempotent",rent.Ky(req)==hd);
            Check("Two rooms generate 24 monthly charges",Reports.Charges(hd).Rows.Count==24);
            Check("Unpaid contract waits for advance",Status(hd)=="CHO_TRA_TRUOC");
            Reject("Reject overlapping reserved room",()=>rent.Ky(Request("TEST1")));
            var advance=pay.TraTruoc(hd);Check("Advance covers first six months of both rooms",advance.Count==12&&advance.Sum(x=>x.SoTien)==18000000);
            var firstHalf=advance.Take(6).ToList();
            pay.Thu(new("CT01",Jan,firstHalf.Sum(x=>x.SoTien),"Test",firstHalf,Guid.NewGuid()));
            Check("Partial advance cannot activate contract",Status(hd)=="CHO_TRA_TRUOC");
            advance=pay.TraTruoc(hd);var payment=new ThuTienRequest("CT01",Jan,advance.Sum(x=>x.SoTien),"Test",advance,Guid.NewGuid());
            string receipt=pay.Thu(payment);Check("Receipt retry cannot double-pay",pay.Thu(payment)==receipt);
            Check("Full advance activates all rooms",Status(hd)=="HIEU_LUC");
            var details=Reports.Details(hd);long ct=(long)details.Rows[0]["MaCTHD"],ct2=(long)details.Rows[1]["MaCTHD"];
            var july=Reports.Charges(hd).AsEnumerable().First(r=>(DateTime)r["KyTu"]==new DateTime(2026,7,1));long charge=(long)july["MaKhoan"];
            Check("Seventh month due at beginning of period",(DateTime)july["HanThu"]==new DateTime(2026,7,1));
            Reject("Reject wrong customer payment",()=>pay.Thu(new("CT02",Sep,1,"Test",[new(charge,1)],Guid.NewGuid())));
            Reject("Reject total/allocation mismatch",()=>pay.Thu(new("CT01",Sep,2,"Test",[new(charge,1)],Guid.NewGuid())));
            Reject("Reject overpayment",()=>pay.Thu(new("CT01",Sep,999999999,"Test",[new(charge,999999999)],Guid.NewGuid())));
            Reject("Reject sub-cent payment precision",()=>pay.Thu(new("CT01",Sep,0.001M,"Test",[new(charge,0.001M)],Guid.NewGuid())));
            GiaHanRequest Renew(params PhongGiaHan[] lines)=>new(hd,Sep,"Tester",lines,Guid.NewGuid());
            Reject("Rent debt blocks renewal",()=>renewal.Lap(Renew(new PhongGiaHan(ct,1,1200000))));
            var due=Reports.Charges(hd).AsEnumerable().Where(r=>(DateTime)r["HanThu"]<=Sep&&(decimal)r["ConNo"]>0).Select(r=>new PhanBo((long)r["MaKhoan"],(decimal)r["ConNo"])).ToList();
            pay.Thu(new("CT01",Sep,due.Sum(x=>x.SoTien),"Test",due,Guid.NewGuid()));
            Reject("Missing service reconciliation blocks renewal",()=>renewal.Lap(Renew(new PhongGiaHan(ct,1,1200000))));
            string invoice=pay.LuuDichVu(ct,9,2026,[new("DIEN",10,3000)],new(2026,9,1));
            Reject("Cannot issue positive service before month end",()=>pay.PhatHanh(invoice,new(2026,9,15)));
            pay.XacNhanKhongPhatSinh(ct,Sep);pay.XacNhanKhongPhatSinh(ct2,Sep);
            Reject("Nonzero draft blocks renewal despite zero confirmations",()=>renewal.Lap(Renew(new PhongGiaHan(ct,1,1200000))));
            pay.PhatHanh(invoice,Sep);int charges=Count("KhoanPhaiThu");pay.PhatHanh(invoice,Sep);
            Check("Publishing twice creates only one charge",Count("KhoanPhaiThu")==charges);
            Check("Services due at month end",(DateTime)Scalar("SELECT HanThu FROM KhoanPhaiThu WHERE MaKhoan=(SELECT MaKhoan FROM HoaDonDichVu WHERE MaHDDV=@p0)",invoice)==Sep);
            Reject("Service debt blocks renewal",()=>renewal.Lap(Renew(new PhongGiaHan(ct,1,1200000))));
            Reject("Issued invoice cannot be overwritten",()=>pay.LuuDichVu(ct,9,2026,[],Sep));
            long dv=(long)Scalar("SELECT MaKhoan FROM HoaDonDichVu WHERE MaHDDV=@p0",invoice);
            pay.Thu(new("CT01",Sep,30000,"Test",[new(dv,30000)],Guid.NewGuid()));
            Reject("Renewal greater than half original term rejected",()=>renewal.Lap(Renew(new PhongGiaHan(ct,7,1200000))));
            before=Count("PhieuGiaHan");DateTime oldEnd=(DateTime)Scalar("SELECT NgayKetThuc FROM ChiTietHopDong WHERE MaCTHD=@p0",ct);
            Reject("Invalid second room rolls back multi-room renewal",()=>renewal.Lap(Renew(new PhongGiaHan(ct,1,1200000),new PhongGiaHan(ct2,7,2000000))));
            Check("Renewal rollback preserves header and room end",Count("PhieuGiaHan")==before&&(DateTime)Scalar("SELECT NgayKetThuc FROM ChiTietHopDong WHERE MaCTHD=@p0",ct)==oldEnd);
            var renew=Renew(new PhongGiaHan(ct,6,1200000));string extension=renewal.Lap(renew);
            Check("Valid six month renewal succeeds",(DateTime)Scalar("SELECT NgayKetThuc FROM ChiTietHopDong WHERE MaCTHD=@p0",ct)==new DateTime(2027,7,1));
            Check("Renewal retry does not extend again",renewal.Lap(renew)==extension);
            Check("Original price snapshot preserved",(decimal)Scalar("SELECT DonGiaThang FROM ChiTietHopDong WHERE MaCTHD=@p0",ct)==1000000);
            Check("New rent uses renewal price",(decimal)Scalar("SELECT SoTien FROM KhoanPhaiThu WHERE MaCTHD=@p0 AND KyTu='2027-01-01'",ct)==1200000);
            string a=rent.Ky(Request("TEST3",6));
            string b=rent.Ky(new("CT02",Jan,new(2026,7,1),6,[new("TEST3",1000000)],Guid.NewGuid()));
            Check("Adjacent half-open leases allowed",a!=b);rent.Huy(b);Check("Waiting contract can be cancelled",Status(b)=="HUY");
            Reject("Active contract cannot be cancelled",()=>rent.Huy(hd));
            var security=new AnNinhService();
            Reject("Employee cannot belong to another company's room",()=>security.LuuNhanVien("TESTNV0","CT02","TEST1","Test",null,"Test",Jan,false));
            security.LuuNhanVien("TESTNV1","CT01","TEST1","Test A",null,"Test",Jan,false);
            security.LuuNhanVien("TESTNV2","CT01","TEST2","Test B",null,"Test",Jan,false);
            security.CapThe("TESTTHE1","TESTNV1","59A 123","Xe máy","Đen",Jan);
            Reject("One vehicle cannot have two active cards",()=>security.CapThe("TESTTHE2","TESTNV2","59A123","Xe máy","Đen",Jan));
            security.NghiViec("TESTNV1",Sep);Check("Employee departure recalls active cards",Scalar("SELECT NgayThuHoi FROM TheGiuXe WHERE MaThe='TESTTHE1'") is DateTime);
            Reject("Former employee cannot receive card",()=>security.CapThe("TESTTHE3","TESTNV1","NEW123","Xe máy","Đen",Sep));
            var inspect=new KiemTraService();before=Count("PhieuKiemTra");
            Reject("Inspection invalid room rolls back all rows",()=>inspect.LapBienBan(Sep,"Test","",[new("TEST1","Bình thường",false,""),new("UNKNOWN","X",false,"")]));
            Check("No orphan inspection",Count("PhieuKiemTra")==before);
            string check=inspect.LapBienBan(Sep,"Test","",[new("TEST1","Tự đổi kết cấu",true,"Khôi phục")]);
            long ck=(long)Scalar("SELECT MaCTKT FROM ChiTietKiemTra WHERE MaPKT=@p0",check);
            string fine=inspect.LapPhat(ck,ct,"Tự đổi kết cấu",500000,Sep);
            inspect.NopPhat(fine,200000);Reject("Fine cannot be overpaid",()=>inspect.NopPhat(fine,400000));
            Check("Fine partial balance retained",(decimal)Scalar("SELECT DaNop FROM PhieuPhat WHERE MaPhat=@p0",fine)==200000);
            Check("Annual inspection schedule excludes recently inspected room",!Reports.DueChecks(Sep).AsEnumerable().Any(r=>(string)r["MaPhong"]=="TEST1"));
            // Competing full payments must result in exactly one receipt and zero balance.
            long concurrent=(long)Scalar("SELECT TOP 1 n.MaKhoan FROM vwCongNo n JOIN ChiTietHopDong ct ON ct.MaCTHD=n.MaCTHD WHERE ct.MaHD=@p0 AND n.ConNo=1000000 ORDER BY n.MaKhoan",hd);
            int wins=0;Task.WaitAll(Enumerable.Range(0,2).Select(_=>Task.Run(()=>{try{pay.Thu(new("CT01",Sep,1000000,"Race",[new(concurrent,1000000)],Guid.NewGuid()));Interlocked.Increment(ref wins);}catch(InvalidOperationException){}})).ToArray());
            Check("Concurrent payments cannot overpay",wins==1&&(decimal)Scalar("SELECT ConNo FROM vwCongNo WHERE MaKhoan=@p0",concurrent)==0);
            string anchored=rent.Ky(new("CT01",Jan,new(2026,1,31),6,[new("TEST4",1000000)],Guid.NewGuid()));
            var prepay=pay.TraTruoc(anchored);pay.Thu(new("CT01",new(2026,1,31),6000000,"Test",prepay,Guid.NewGuid()));
            long anchor=(long)Reports.Details(anchored).Rows[0]["MaCTHD"];
            pay.XacNhanKhongPhatSinh(anchor,new(2026,7,30));
            Reject("Expired room cannot be renewed retroactively after expiry",()=>renewal.Lap(new(anchored,new(2026,8,1),"Test",[new(anchor,1,1000000)],Guid.NewGuid())));
            for(int i=0;i<3;i++)renewal.Lap(new(anchored,new(2026,7,30),"Test",[new(anchor,1,1000000)],Guid.NewGuid()));
            Check("Month-end lease keeps original day anchor across renewals",(DateTime)Scalar("SELECT NgayKetThuc FROM ChiTietHopDong WHERE MaCTHD=@p0",anchor)==new DateTime(2026,10,31));
            string occupied=rent.Ky(Request("TEST5",6));prepay=pay.TraTruoc(occupied);pay.Thu(new("CT01",Jan,6000000,"Test",prepay,Guid.NewGuid()));
            long occupiedCt=(long)Reports.Details(occupied).Rows[0]["MaCTHD"];
            rent.Ky(new("CT02",Jan,new(2026,7,1),6,[new("TEST5",1000000)],Guid.NewGuid()));
            pay.XacNhanKhongPhatSinh(occupiedCt,new(2026,6,30));
            Reject("Renewal cannot overlap next customer's reserved lease",()=>renewal.Lap(new(occupied,new(2026,6,30),"Test",[new(occupiedCt,1,1000000)],Guid.NewGuid())));
            wins=0;Task.WaitAll(Enumerable.Range(0,2).Select(_=>Task.Run(()=>{try{rent.Ky(Request("TEST6",6));Interlocked.Increment(ref wins);}catch(InvalidOperationException){}})).ToArray());
            Check("Concurrent contracts cannot reserve same room",wins==1);
        }
        catch(Exception ex){log.Add(ex.ToString());exit=1;}
        finally
        {
            try
            {
                SqlConnection.ClearAllPools();using var c=Db.Open("master");
                if(database.StartsWith("QuanLyCaoOcLAB8_TEST_")&&System.Text.RegularExpressions.Regex.IsMatch(database,@"^QuanLyCaoOcLAB8_TEST_[a-f0-9]{32}$"))
                    Db.Exec(c,null,$"IF DB_ID(N'{database}') IS NOT NULL BEGIN ALTER DATABASE [{database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{database}]; END");
            }
            catch(Exception ex){log.Add("Cleanup: "+ex.Message);exit=1;}
            Db.ConnectionString=original;
            File.WriteAllLines(Path.Combine(AppContext.BaseDirectory,"verification.txt"),log);Console.WriteLine(string.Join(Environment.NewLine,log));
        }
        return exit;
    }
    static IEnumerable<Control> All(Control parent)
    {foreach(Control c in parent.Controls){yield return c;foreach(var child in All(c))yield return child;}}
    public static int RunUi()
    {
        string folder=Path.Combine(AppContext.BaseDirectory,"Verification","UI");Directory.CreateDirectory(folder);int pages=0;
        try
        {
            var forms=new List<Form>{new FrmMain()};for(int i=0;i<6;i++)forms.Add(FrmMain.CreateFunction(i));
            forms.Add(new FrmEditor("Kiểm tra hộp nhập",[new("Ma","Mã","text","TEST"),new("Ngay","Ngày","date"),FormHelper.Choice("MaKH","Khách hàng",FormHelper.Choices(new DanhMucService().List("KhachHang"),"MaKH","TenCongTy"),"CT02")]));
            forms.Add(new FrmTable("Kiểm tra bảng",Reports.Contracts()));
            foreach(var f in forms)
            {
                using(f)
                {
                    f.Show();Application.DoEvents();
                    foreach(var tab in All(f).OfType<TabControl>())
                    foreach(TabPage page in tab.TabPages)
                    {
                        tab.SelectedTab=page;Application.DoEvents();pages++;
                        Check(f.Name+" / "+page.Text+" visible",page.ClientSize.Width>500&&page.ClientSize.Height>250);
                        foreach(var grid in All(page).OfType<DataGridView>())Check("Grid bound: "+page.Text,grid.DataSource!=null);
                        using var png=new Bitmap(f.Width,f.Height);f.DrawToBitmap(png,new Rectangle(Point.Empty,f.Size));png.Save(Path.Combine(folder,f.GetType().Name+"_"+tab.SelectedIndex+".png"));
                    }
                    if(f is FrmEditor editor)Check("Choice preserves selected customer",editor.S("MaKH")=="CT02");
                    using var shot=new Bitmap(f.Width,f.Height);f.DrawToBitmap(shot,new Rectangle(Point.Empty,f.Size));shot.Save(Path.Combine(folder,f.GetType().Name+".png"));f.Close();
                }
            }
            Check("All 9 forms / 15 module tabs opened",forms.Count==9&&pages==15);
            var rooms=FormHelper.Selectable(new DanhMucService().List("VanPhong"));
            using(var detail=new FrmEditor("Kiểm tra nhập nhiều phòng",[
                FormHelper.Choice("MaKH","Khách hàng",FormHelper.Choices(new DanhMucService().List("KhachHang"),"MaKH","TenCongTy")),
                new("NgayKy","Ngày ký","date"),new("BatDau","Ngày bắt đầu","date"),new("SoThang","Số tháng gốc","number",6)
            ],rooms,["Chon","GiaThamKhao"]))
            {
                detail.Show();Application.DoEvents();var grid=All(detail).OfType<DataGridView>().Single();
                Check("Room identifiers are read-only in detail dialog",grid.Columns["MaPhong"]!.ReadOnly);
                Check("Room selection and price are editable",!grid.Columns["Chon"]!.ReadOnly&&!grid.Columns["GiaThamKhao"]!.ReadOnly);
                Check("Detail grid shows Vietnamese captions",grid.Columns["MaPhong"]!.HeaderText=="Mã phòng");
                using var png=new Bitmap(detail.Width,detail.Height);detail.DrawToBitmap(png,new Rectangle(Point.Empty,detail.Size));png.Save(Path.Combine(folder,"Dialog_HopDong.png"));detail.Close();
            }
            void Dialog(Form owner,string method,params object?[] arguments)
            {
                using(owner)
                {
                    owner.Show();Application.DoEvents();bool opened=false;
                    using var timer=new System.Windows.Forms.Timer{Interval=150};
                    timer.Tick+=(_,_)=>
                    {
                        var modal=Application.OpenForms.Cast<Form>().OfType<FrmEditor>().LastOrDefault();if(modal==null)return;
                        opened=true;timer.Stop();
                        using var png=new Bitmap(modal.Width,modal.Height);modal.DrawToBitmap(png,new Rectangle(Point.Empty,modal.Size));png.Save(Path.Combine(folder,"Dialog_"+method+".png"));
                        modal.DialogResult=DialogResult.Cancel;modal.Close();
                    };
                    timer.Start();owner.GetType().GetMethod(method,System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance)!.Invoke(owner,arguments);
                    Check("Actual action opens dialog: "+method,opened);owner.Close();
                }
            }
            Dialog(new FrmHopDong(),"CreateContract");
            Dialog(new FrmThanhToan(),"EditInvoice",new object?[]{null});
            Dialog(new FrmThanhToan(),"ConfirmZero");
            Dialog(new FrmGiaHan(),"Renew");
            Dialog(new FrmNhanVienTheXe(),"EditEmployee",new object?[]{null});
            Dialog(new FrmNhanVienTheXe(),"EditEmployee",Reports.Employees().Rows[0]);
            Dialog(new FrmNhanVienTheXe(),"IssueCard");
            Dialog(new FrmKiemTra(),"NewCheck");
            Dialog(new FrmKiemTra(),"NewFine",Reports.Checks().AsEnumerable().First(r=>(bool)r["ThayDoiKetCau"]));
            File.WriteAllLines(Path.Combine(folder,"ui-checks.txt"),log);return 0;
        }
        catch(Exception ex){File.WriteAllText(Path.Combine(folder,"ui-error.txt"),ex.ToString());return 1;}
    }
}

