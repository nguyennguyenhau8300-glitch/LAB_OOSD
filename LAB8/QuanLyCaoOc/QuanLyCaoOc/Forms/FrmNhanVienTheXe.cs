using System.Data;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public partial class FrmNhanVienTheXe:Form
{
    readonly AnNinhService service=new();
    readonly PictureBox picAnh=new(){Name="picAnh",Dock=DockStyle.Right,Width=180,SizeMode=PictureBoxSizeMode.Zoom,BackColor=Color.WhiteSmoke};
    public FrmNhanVienTheXe()
    {
        InitializeComponent();FormHelper.Style(this,"Nhân viên công ty và thẻ giữ xe");
        pnlFilters.Controls.Add(new Label{Text="Tìm theo mã thẻ, biển số hoặc họ tên trong từng bảng. Nhân viên phải làm tại phòng công ty có quyền thuê.",AutoSize=true,Padding=new Padding(0,8,0,0)});
        var profile=FormHelper.Page(tabs,"Nhân viên",Reports.Employees,("Thêm nhân viên",_=>EditEmployee(null)),("Sửa hồ sơ",g=>EditEmployee(FormHelper.Current(g))),
            ("Ghi nhận nghỉ",g=>{string id=(string)FormHelper.Current(g)["MaNV"];using var d=new FrmEditor("Nghỉ việc và thu hồi tất cả thẻ",[new("Ngay","Ngày nghỉ","date")]);d.SaveAction=e=>service.NghiViec(id,e.D("Ngay"));d.ShowDialog(this);}));
        profile.Tab.Controls.Add(picAnh);profile.Grid.BringToFront();profile.Grid.SelectionChanged+=(_,_)=>ShowPhoto(profile.Grid);
        picAnh.Paint+=(_,e)=>{if(picAnh.Image==null)TextRenderer.DrawText(e.Graphics,"Ảnh nhân viên",Font,picAnh.ClientRectangle,Color.DimGray,TextFormatFlags.HorizontalCenter|TextFormatFlags.VerticalCenter);};
        FormHelper.Page(tabs,"Thẻ giữ xe",Reports.Cards,("Cấp thẻ",_=>IssueCard()),("Thu hồi thẻ",g=>
        {string id=(string)FormHelper.Current(g)["MaThe"];using var d=new FrmEditor("Thu hồi thẻ "+id,[new("Ngay","Ngày thu hồi","date")]);d.SaveAction=e=>service.ThuHoi(id,e.D("Ngay"));d.ShowDialog(this);}));
        Load+=(_,_)=>{if(!FormHelper.IsDesigner(this))FormHelper.Run(()=>FormHelper.LoadAll(tabs));};
        FormClosed+=(_,_)=>picAnh.Image?.Dispose();
    }
    void ShowPhoto(DataGridView grid)
    {
        picAnh.Image?.Dispose();picAnh.Image=null;
        if(grid.CurrentRow?.DataBoundItem is not DataRowView row)return;
        string? path=row.Row["Anh"] as string;if(string.IsNullOrWhiteSpace(path))return;
        path=Path.IsPathRooted(path)?path:Path.Combine(AppContext.BaseDirectory,path);
        if(!File.Exists(path))return;
        try{using var image=Image.FromFile(path);picAnh.Image=new Bitmap(image);}catch(ArgumentException){}catch(IOException){}
    }
    void EditEmployee(DataRow? row)
    {
        var catalog=new DanhMucService();
        using var dialog=new FrmEditor("Hồ sơ nhân viên công ty",[
            new("MaNV","Mã nhân viên","text",row?["MaNV"],ReadOnly:row!=null),
            FormHelper.Choice("MaKH","Công ty",FormHelper.Choices(catalog.List("KhachHang"),"MaKH","TenCongTy"),row?["MaKH"]) with{ReadOnly=row!=null},
            FormHelper.Choice("MaPhong","Phòng làm việc",FormHelper.Choices(catalog.List("VanPhong"),"MaPhong","Huong"),row?["MaPhong"]),
            new("HoTen","Họ tên","text",row?["HoTen"]),new("ChucVu","Chức vụ","text",row?["ChucVu"]),new("NgayVao","Ngày bố trí","date",row?["NgayVao"]),new("Anh","Ảnh nhân viên (tùy chọn)","file",row?["Anh"])
        ]);
        dialog.SaveAction=e=>service.LuuNhanVien(e.S("MaNV"),e.S("MaKH"),e.S("MaPhong"),e.S("HoTen"),FormHelper.SavePhoto(e.S("Anh")),e.S("ChucVu"),e.D("NgayVao"),row!=null);dialog.ShowDialog(this);
    }
    void IssueCard()
    {
        var employees=Reports.Employees();employees.DefaultView.RowFilter="NgayNghi IS NULL";
        using var d=new FrmEditor("Cấp thẻ giữ xe",[
            new("MaThe","Mã thẻ","text",Rules.Id("THE")),FormHelper.Choice("MaNV","Nhân viên",FormHelper.Choices(employees.DefaultView.ToTable(),"MaNV","HoTen")),
            new("BienSo","Biển số"),new("LoaiXe","Loại xe","text","Xe máy"),new("MauXe","Màu xe","text","Đen"),new("Ngay","Ngày cấp","date")]);
        d.SaveAction=e=>{service.CapThe(e.S("MaThe"),e.S("MaNV"),e.S("BienSo"),e.S("LoaiXe"),e.S("MauXe"),e.D("Ngay"));};d.ShowDialog(this);
    }
}
