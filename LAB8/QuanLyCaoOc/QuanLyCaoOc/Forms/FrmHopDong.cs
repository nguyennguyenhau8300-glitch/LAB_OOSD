using System.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public partial class FrmHopDong:Form
{
    readonly HopDongService service=new();
    public FrmHopDong()
    {
        InitializeComponent();FormHelper.Style(this,"Khách hàng và hợp đồng");
        pnlFilters.Controls.Add(new Label{Text="Hợp đồng giữ lịch từ lúc ký; chỉ kích hoạt sau khi thu đủ 6 tháng đầu. Mỗi hợp đồng có thể chọn nhiều phòng.",AutoSize=true,Padding=new Padding(0,8,0,0)});
        FormHelper.Page(tabs,"Hợp đồng",Reports.Contracts,("Ký hợp đồng",_=>CreateContract()),
            ("Danh sách phòng",g=>{using var f=new FrmTable("Phòng thuộc hợp đồng",Reports.Details((string)FormHelper.Current(g)["MaHD"]));f.ShowDialog(this);}),
            ("Lịch thu",g=>{using var f=new FrmTable("Lịch thu và công nợ",Reports.Charges((string)FormHelper.Current(g)["MaHD"]));f.ShowDialog(this);}),
            ("Hủy chờ trả trước",g=>{string id=(string)FormHelper.Current(g)["MaHD"];if(FormHelper.Confirm("Hủy hợp đồng "+id+"?"))service.Huy(id);}),
            ("Kết thúc hợp đồng",g=>{using var f=new FrmEditor("Kết thúc và đối soát",[new("Ngay","Ngày kết thúc đối soát","date")]);string id=(string)FormHelper.Current(g)["MaHD"];f.SaveAction=e=>service.KetThuc(id,e.D("Ngay"));f.ShowDialog(this);}));
        FormHelper.Page(tabs,"Khách hàng",()=>new DanhMucService().List("KhachHang"),("Thêm khách",_=>CatalogEditor.Show(this,"KhachHang")),("Sửa khách",g=>CatalogEditor.Show(this,"KhachHang",FormHelper.Current(g))),("Xóa khách",g=>CatalogEditor.Delete("KhachHang",g)));
        Load+=(_,_)=>{if(!FormHelper.IsDesigner(this))FormHelper.Run(()=>FormHelper.LoadAll(tabs));};
    }
    void CreateContract()
    {
        var catalog=new DanhMucService();var rooms=FormHelper.Selectable(catalog.List("VanPhong").DefaultView.ToTable(false,"MaPhong","DienTich","GiaThamKhao"));
        rooms.Columns["GiaThamKhao"]!.ColumnName="Gia";
        Guid request=Guid.NewGuid();
        using var dialog=new FrmEditor("Ký hợp đồng — chọn phòng và đơn giá",[
            FormHelper.Choice("MaKH","Khách hàng",FormHelper.Choices(catalog.List("KhachHang"),"MaKH","TenCongTy")),
            new("NgayKy","Ngày ký","date"),new("BatDau","Ngày bắt đầu","date"),new("Thang","Số tháng gốc (≥ 6)","number",6)
        ],rooms,["Chon","Gia"]);
        dialog.SaveAction=e=>
        {
            var lines=FormHelper.Chosen(rooms).Select(r=>new PhongThue((string)r["MaPhong"],(decimal)r["Gia"])).ToList();
            var id=service.Ky(new(e.S("MaKH"),e.D("NgayKy"),e.D("BatDau"),e.I("Thang"),lines,request));FormHelper.Info("Đã ký "+id+". Chuyển sang Thanh toán để thu trước.");
        };
        dialog.ShowDialog(this);
    }
}
