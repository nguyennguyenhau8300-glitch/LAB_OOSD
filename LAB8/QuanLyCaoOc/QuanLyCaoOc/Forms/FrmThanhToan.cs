using System.Data;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public partial class FrmThanhToan:Form
{
    readonly ComboBox cboHopDong=new(){Name="cboHopDong",Width=650,DropDownStyle=ComboBoxStyle.DropDownList};
    readonly ThanhToanService service=new();
    string HD=>Convert.ToString(cboHopDong.SelectedValue)??"";
    public FrmThanhToan()
    {
        InitializeComponent();FormHelper.Style(this,"Thanh toán, dịch vụ và công nợ");
        pnlFilters.Controls.AddRange([new Label{Text="Hợp đồng",AutoSize=true,Padding=new Padding(0,8,0,0)},cboHopDong,FormHelper.Button("Tải hợp đồng",LoadContracts)]);
        FormHelper.Page(tabs,"Khoản phải thu",()=>Reports.Charges(HD),
            ("Thu trước 6 tháng",_=>Collect(service.TraTruoc(HD))),
            ("Thu khoản đang chọn",g=>{var row=FormHelper.Current(g);Collect([new((long)row["MaKhoan"],(decimal)row["ConNo"])]);}));
        FormHelper.Page(tabs,"Dịch vụ",()=>Reports.Invoices(HD),
            ("Lập kỳ dịch vụ",_=>EditInvoice(null)),("Xem / sửa kỳ",g=>EditInvoice(FormHelper.Current(g))),
            ("Phát hành cuối tháng",g=>{string id=(string)FormHelper.Current(g)["MaHDDV"];using var e=new FrmEditor("Chốt hóa đơn dịch vụ",[new("Ngay","Ngày chốt (dịch vụ có tiền: cuối tháng)","date")]);e.SaveAction=d=>service.PhatHanh(id,d.D("Ngay"));e.ShowDialog(this);}),
            ("Đối soát không phát sinh",_=>ConfirmZero()));
        FormHelper.Page(tabs,"Phiếu thu",Reports.Receipts,("Chi tiết phân bổ",g=>{using var f=new FrmTable("Phân bổ phiếu thu",Db.Query("SELECT p.MaPT,p.MaKhoan,k.Loai,k.KyTu,p.SoTien FROM PhanBoThu p JOIN KhoanPhaiThu k ON k.MaKhoan=p.MaKhoan WHERE p.MaPT=@p0",FormHelper.Current(g)["MaPT"]));f.ShowDialog(this);}));
        FormHelper.Page(tabs,"Danh mục dịch vụ",()=>new DanhMucService().List("DichVu"),("Thêm",_=>CatalogEditor.Show(this,"DichVu")),("Sửa",g=>CatalogEditor.Show(this,"DichVu",FormHelper.Current(g))),("Xóa",g=>CatalogEditor.Delete("DichVu",g)));
        cboHopDong.SelectedIndexChanged+=(_,_)=>{if(IsHandleCreated)FormHelper.Run(()=>FormHelper.LoadAll(tabs));};
        Load+=(_,_)=>{if(!FormHelper.IsDesigner(this))FormHelper.Run(LoadContracts);};
    }
    void LoadContracts()
    {
        var contracts=Reports.Contracts();cboHopDong.BindingContext=new BindingContext();cboHopDong.DataSource=FormHelper.Choices(contracts,"MaHD","TenCongTy");cboHopDong.DisplayMember="Text";cboHopDong.ValueMember="Id";FormHelper.LoadAll(tabs);
    }
    string Customer()=>Convert.ToString(Db.Query("SELECT MaKH FROM HopDong WHERE MaHD=@p0",HD).Rows.Cast<DataRow>().Single()["MaKH"])!;
    void Collect(IReadOnlyList<PhanBo> lines)
    {
        Rules.Require(lines.Count>0&&lines.Sum(x=>x.SoTien)>0,"Không còn tiền phải thu ở lựa chọn này.");
        decimal max=lines.Sum(x=>x.SoTien);Guid request=Guid.NewGuid();string kh=Customer();
        using var dialog=new FrmEditor("Ghi nhận phiếu thu",[new("Ngay","Ngày thu","date"),new("Tien","Số tiền (tối đa "+max.ToString("N0")+")","money",max),new("Cach","Phương thức","text","Chuyển khoản")]);
        dialog.SaveAction=e=>
        {
            decimal amount=e.M("Tien"),remaining=amount;Rules.Require(amount>0&&amount<=max,"Số tiền phải dương và không vượt số dư.");
            var allocation=new List<PhanBo>();foreach(var line in lines){decimal take=Math.Min(line.SoTien,remaining);if(take>0)allocation.Add(new(line.MaKhoan,take));remaining-=take;}
            string id=service.Thu(new(kh,e.D("Ngay"),amount,e.S("Cach"),allocation,request));FormHelper.Info("Đã ghi phiếu "+id);
        };dialog.ShowDialog(this);
    }
    void EditInvoice(DataRow? invoice)
    {
        var details=Reports.Details(HD);Rules.Require(details.Rows.Count>0,"Chọn hợp đồng có phòng thuê.");
        var lines=new DanhMucService().List("DichVu").DefaultView.ToTable(false,"MaDV","TenDV","DonViTinh","GiaMacDinh");
        lines.Columns["GiaMacDinh"]!.ColumnName="DonGia";lines.Columns.Add("SoLuong",typeof(decimal));foreach(DataRow r in lines.Rows)r["SoLuong"]=0M;
        if(invoice!=null)
        {
            var old=Db.Query("SELECT * FROM ChiTietDichVu WHERE MaHDDV=@p0",invoice["MaHDDV"]);
            foreach(DataRow r in lines.Rows){var match=old.AsEnumerable().FirstOrDefault(x=>(string)x["MaDV"]==(string)r["MaDV"]);if(match!=null){r["SoLuong"]=match["SoLuong"];r["DonGia"]=match["DonGia"];}}
        }
        using var dialog=new FrmEditor("Dịch vụ — lượng dùng tích lũy của kỳ",[
            FormHelper.Choice("MaCTHD","Phòng thuê",FormHelper.Choices(details,"MaCTHD","MaPhong"),invoice?["MaCTHD"]) with{ReadOnly=invoice!=null},
            new("Thang","Tháng","number",invoice?["Thang"]??DateTime.Today.Month,ReadOnly:invoice!=null),
            new("Nam","Năm","number",invoice?["Nam"]??DateTime.Today.Year,ReadOnly:invoice!=null),new("Ngay","Ngày ghi","date")
        ],lines,["SoLuong","DonGia"]);
        dialog.SaveAction=e=>
        {
            if(invoice==null)Rules.Require(Db.Query("SELECT MaHDDV FROM HoaDonDichVu WHERE MaCTHD=@p0 AND Thang=@p1 AND Nam=@p2",e.L("MaCTHD"),e.I("Thang"),e.I("Nam")).Rows.Count==0,"Kỳ đã tồn tại. Chọn dòng hóa đơn và bấm Xem / sửa kỳ để giữ lượng dùng cũ.");
            service.LuuDichVu(e.L("MaCTHD"),e.I("Thang"),e.I("Nam"),lines.AsEnumerable().Select(r=>new DongDichVu((string)r["MaDV"],(decimal)r["SoLuong"],(decimal)r["DonGia"])).ToList(),e.D("Ngay"));
        };dialog.ShowDialog(this);
    }
    void ConfirmZero()
    {
        var details=Reports.Details(HD);using var dialog=new FrmEditor("Xác nhận các kỳ không sử dụng dịch vụ",[
            FormHelper.Choice("MaCTHD","Phòng thuê",FormHelper.Choices(details,"MaCTHD","MaPhong")),new("Ngay","Đối soát đến ngày","date")]);
        dialog.SaveAction=e=>{if(!FormHelper.Confirm("Chỉ xác nhận kỳ thực sự không phát sinh. Kỳ có lượng dùng sẽ giữ nguyên để chốt và thanh toán riêng. Tiếp tục?"))throw new InvalidOperationException("Chưa xác nhận đối soát.");service.XacNhanKhongPhatSinh(e.L("MaCTHD"),e.D("Ngay"));};dialog.ShowDialog(this);
    }
}
