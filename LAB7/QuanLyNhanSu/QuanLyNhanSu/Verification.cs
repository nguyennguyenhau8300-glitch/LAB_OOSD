using System.Data;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Forms;
using QuanLyNhanSu.Services;
namespace QuanLyNhanSu;
public static class Verification
{
    static readonly List<string> results=[];
    static void Check(bool pass,string name){results.Add((pass?"PASS ":"FAIL ")+name);if(!pass)throw new InvalidOperationException(name);}
    static void Reject(Action action,string name){try{action();}catch(Exception e) when(e is InvalidOperationException or Microsoft.Data.SqlClient.SqlException){Check(true,name);return;}Check(false,name);}
    static int Save(string table,params object?[] v){var m=Modules.Get(table);return NhanSuService.Save(m,0,m.Fields.Select((f,i)=>(f.Name,v[i])).ToDictionary(x=>x.Name,x=>x.Item2));}
    public static int Run()
    {
        results.Clear();
        // Only the disposable verification database is used by this entry point.
        string testDb="QuanLyNhanSuLAB7_Verify_"+Guid.NewGuid().ToString("N");
        string master=@"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=true;TrustServerCertificate=true";
        Db.ConnectionString=master;
        try{
            using(var c=Db.Open()){
                string source=File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"Data","QuanLyNhanSu.sql")).Replace("QuanLyNhanSuLAB7",testDb);
                foreach(string batch in System.Text.RegularExpressions.Regex.Split(source,@"^\s*GO\s*$",System.Text.RegularExpressions.RegexOptions.Multiline|System.Text.RegularExpressions.RegexOptions.IgnoreCase))if(!string.IsNullOrWhiteSpace(batch))Db.Exec(c,null,batch);
            }
            Db.ConnectionString=master.Replace("Database=master","Database="+testDb);
            using(var c=Db.Open())Check(Convert.ToString(Db.Scalar(c,null,"SELECT DB_NAME()"))==testDb,"Kết nối CSDL kiểm thử riêng");
            string key="T"+DateTime.Now.ToString("yyyyMMddHHmmssfff");
            int pb=Save("PhongBan",key),role=Save("ChucVu",key),shift=Save("CaLam",key,new TimeSpan(8,0,0),new TimeSpan(16,0,0));
            Check(pb>0&&role>0&&shift>0,"Thêm danh mục");Reject(()=>Save("PhongBan",key),"Chặn trùng tên phòng ban");
            int nv=Save("NhanVien","Người kiểm thử",new DateTime(1998,1,1),"0900000000","TP.HCM",pb);
            Check(nv>0,"Đăng ký nhân viên");
            var a=new DateTime(2026,7,1);var b=new DateTime(2026,7,31);var day=new DateTime(2026,9,7);
            Reject(()=>Save("TuyenDung",nv,key+"bad",b,a,7800000m,"Thử việc"),"Chặn thời gian thử việc ngược");
            int td=Save("TuyenDung",nv,key+"td",a,b,7800000m,"Điều khoản thử việc");Check(td>0,"Lập quyết định tuyển dụng");
            Reject(()=>Save("HopDong",nv,key+"hd0","Dài hạn",new DateTime(2026,8,1),new DateTime(2026,8,1),null,"Điều khoản"),"Chặn hợp đồng trước khi đạt thử việc");
            NhanSuService.Evaluate(td,true);Check(true,"Đánh giá đạt thử việc");Reject(()=>NhanSuService.Evaluate(td,false),"Chặn đánh giá lại");
            int hd=Save("HopDong",nv,key+"hd","Dài hạn",new DateTime(2026,8,1),new DateTime(2026,8,1),null,"Điều khoản");NhanSuService.SignContract(hd);Check(true,"Lập và ký hợp đồng");
            Reject(()=>NhanSuService.SignContract(hd),"Chặn ký hợp đồng hai lần");
            Save("QDLuong",nv,key+"l",new DateTime(2026,8,1),10400000m,1040000m,0m,"Lương chính thức");
            Save("QDChucVu",nv,role,key+"cv",new DateTime(2026,8,1),520000m,"Bổ nhiệm");
            Save("BaoHiem",nv,"BHYT",key+"yt",new DateTime(2026,8,1),null,1.5m);
            Reject(()=>Save("BaoHiem",nv,"BHYT",key+"yt2",new DateTime(2026,8,1),null,1.5m),"Chặn bảo hiểm trùng thời gian");
            int pc=Save("PhanCong",nv,shift,day,2m);int cc=Save("ChamCong",pc,8m,"");Check(cc>0,"Phân công và ghi 8 giờ công");
            Reject(()=>Save("ChamCong",pc,9m,""),"Chặn công vượt ca");
            int overlapping=Save("CaLam",key+"overlap",new TimeSpan(12,0,0),new TimeSpan(20,0,0));
            Reject(()=>Save("PhanCong",nv,overlapping,day,0m),"Chặn phân công chồng ca");
            int pt=Save("PhieuPhuTroi",key+"pt",day,"Tăng ca",2m);Save("ChiTietPhuTroi",pt,nv,2m);
            Reject(()=>Save("ChiTietPhuTroi",pt,nv,3m),"Chặn phụ trội vượt phiếu");
            int reward=Save("ThuongPhat",key+"kt","Khen thưởng",nv,null,day,"Hoàn thành công việc",200000m,"Qua lương");NhanSuService.Approve(reward);
            int direct=Save("ThuongPhat",key+"kt2","Khen thưởng",nv,null,day,"Thưởng trực tiếp",100000m,"Trực tiếp");NhanSuService.Approve(direct);NhanSuService.PayDirect(direct);
            Reject(()=>NhanSuService.PayDirect(direct),"Chặn thu chi hai lần");
            Reject(()=>Save("ThuongPhat",key+"xor","Khen thưởng",nv,pb,day,"Sai đối tượng",1m,"Trực tiếp"),"Chặn chọn đồng thời cá nhân và tập thể");
            Reject(()=>Save("ThuongPhat",key+"group","Khen thưởng",null,pb,day,"Tập thể",1m,"Qua lương"),"Không tự phân bổ thưởng tập thể vào lương");
            int collective=Save("ThuongPhat",key+"tapthe","Khen thưởng",null,pb,day,"Khen thưởng phòng ban",300000m,"Trực tiếp");NhanSuService.Approve(collective);NhanSuService.PayDirect(collective);Check(true,"Khen thưởng và chi tiền tập thể");
            Reject(()=>NhanSuService.Payroll(9,2026,208m),"Chặn tính lương khi thiếu BHXH");
            Save("BaoHiem",nv,"BHXH",key+"xh",new DateTime(2026,8,1),null,8m);
            int payroll=NhanSuService.Payroll(9,2026,208m);var row=NhanSuService.Details(payroll).Rows.Cast<DataRow>().Single(r=>Convert.ToInt32(r["NhanVienId"])==nv);
            Check(Convert.ToDecimal(row["LuongTheoCong"])==400000m,"Lương 8 giờ = 400000");Check(Convert.ToDecimal(row["PhuCap"])==60000m,"Phụ cấp theo công = 60000");
            Check(Convert.ToDecimal(row["TienPhuTroi"])==150000m,"Phụ trội 2 giờ hệ số 1,5 = 150000");Check(Convert.ToDecimal(row["BaoHiem"])==988000m,"Bảo hiểm theo tỷ lệ cấu hình = 988000");
            Check(Convert.ToDecimal(row["Thuong"])==200000m,"Chỉ cộng thưởng qua lương, loại khoản đã chi trực tiếp");
            Check(Convert.ToDecimal(row["ThucLinh"])==-178000m,"Công thức thực lĩnh giữ cả số âm để đối soát");
            NhanSuService.Payroll(9,2026,208m);Check(NhanSuService.Details(payroll).Rows.Cast<DataRow>().Count(r=>Convert.ToInt32(r["NhanVienId"])==nv)==1,"Tính lại không nhân đôi dòng lương");
            NhanSuService.Payroll(9,2026,208m,true);Check(true,"Chốt lương");Reject(()=>NhanSuService.Payroll(9,2026,208m),"Chặn tính lại tháng đã chốt");
            Reject(()=>Save("PhanCong",nv,shift,day.AddDays(1),0m),"Chặn thêm công tháng đã chốt");Reject(()=>Save("QDLuong",nv,key+"retro",new DateTime(2026,9,15),12000000m,0m,0m,"Hồi tố"),"Chặn quyết định lương hồi tố vào tháng đã chốt");
            Reject(()=>NhanSuService.PayDirect(reward),"Không chi trực tiếp quyết định đã qua lương");
            Check(NhanSuService.History(nv).Rows.Count>=5,"Tra cứu lịch sử công tác");
            Reject(()=>NhanSuService.Delete(Modules.Get("PhongBan"),pb),"Không xóa phòng ban đang được sử dụng");
            Check(true,"Hoàn tất kiểm thử nghiệp vụ");
        }catch(Exception e){results.Add("ERROR "+e);Write("KiemThu_LAB7.txt");return 1;}
        finally{
            Microsoft.Data.SqlClient.SqlConnection.ClearAllPools();Db.ConnectionString=master;
            try{using var cleanup=Db.Open();Db.Exec(cleanup,null,$"IF DB_ID(N'{testDb}') IS NOT NULL DROP DATABASE [{testDb}]");}catch(Exception e){results.Add("CLEANUP "+e.Message);}
        }
        Write("KiemThu_LAB7.txt");return 0;
    }
    public static int Ui()
    {
        results.Clear();try{
            using var main=new FrmMain();main.Show();Application.DoEvents();
            Check(FrmMain.Groups.Length==6,"Màn hình chính có đúng 6 nhóm chức năng");
            for(int i=0;i<FrmMain.Groups.Length;i++){
                using var function=FrmMain.CreateFunction(i);function.Show(main);Application.DoEvents();
                Check(function.ScreenTables.SequenceEqual(FrmMain.Groups[i].Tables),"Form CN"+(i+1).ToString("00")+" có đúng các màn hình con");
                foreach(string screen in function.ScreenTables){function.SelectScreen(screen);Application.DoEvents();Check(function.VisibleRecordCount>0,"Có dữ liệu hiển thị: "+Modules.Get(screen).Title);}
                function.Close();Application.DoEvents();Check(main.Visible,"Đóng Form CN"+(i+1).ToString("00")+" trở về màn hình chính");
            }
            using(var snapshot=new Bitmap(main.Width,main.Height)){main.DrawToBitmap(snapshot,new Rectangle(Point.Empty,main.Size));snapshot.Save(Path.Combine(AppContext.BaseDirectory,"FrmMain.png"));}
            foreach(var module in Modules.All.Where(x=>x.Editable)){
                using(var editor=new FrmEditor(module)){
                    editor.Show(main);Application.DoEvents();Check(editor.Controls.Count>0,"Mở màn hình "+module.Title);
                    if(module.Table=="ThuongPhat"){using var shot=new Bitmap(editor.Width,editor.Height);editor.DrawToBitmap(shot,new Rectangle(Point.Empty,editor.Size));shot.Save(Path.Combine(AppContext.BaseDirectory,"FrmKhenThuong.png"));}
                    editor.Close();
                }
                var rows=NhanSuService.List(module);
                if(rows.Rows.Count>0){using var edit=new FrmEditor(module,rows.Rows[0]);edit.Show(main);Application.DoEvents();Check(edit.Text.StartsWith("Cập nhật"),"Nạp bản ghi để sửa "+module.Title);edit.Close();}
            }
            main.Close();
        }catch(Exception e){results.Add("ERROR "+e);Write("KiemThu_GiaoDien_LAB7.txt");return 1;}
        Write("KiemThu_GiaoDien_LAB7.txt");return 0;
    }
    static void Write(string name){var directory=Path.Combine(AppContext.BaseDirectory,"Verification");Directory.CreateDirectory(directory);File.WriteAllLines(Path.Combine(directory,name),results);}
}
