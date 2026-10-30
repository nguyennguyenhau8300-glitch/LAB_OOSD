using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public partial class FrmVanPhong:Form
{
    readonly DanhMucService service=new();
    readonly ComboBox cboTang=new(){Name="cboTang",Width=180,DropDownStyle=ComboBoxStyle.DropDownList};
    readonly DateTimePicker dtpTu=new(){Name="dtpTu",Width=140,Format=DateTimePickerFormat.Custom,CustomFormat="dd/MM/yyyy"};
    readonly DateTimePicker dtpDen=new(){Name="dtpDen",Width=140,Format=DateTimePickerFormat.Custom,CustomFormat="dd/MM/yyyy",Value=DateTime.Today.AddMonths(6)};
    public FrmVanPhong()
    {
        InitializeComponent();FormHelper.Style(this,"Tầng và văn phòng");
        pnlFilters.Controls.AddRange([new Label{Text="Tầng",AutoSize=true,Padding=new Padding(0,7,0,0)},cboTang,new Label{Text="Tra cứu trống từ",AutoSize=true,Padding=new Padding(0,7,0,0)},dtpTu,new Label{Text="đến",AutoSize=true,Padding=new Padding(0,7,0,0)},dtpDen,FormHelper.Button("Lọc lịch phòng",()=>FormHelper.LoadAll(tabs))]);
        FormHelper.Page(tabs,"Văn phòng",()=>service.TimPhong("",cboTang.SelectedValue as string,dtpTu.Value,dtpDen.Value),
            ("Thêm phòng",_=>CatalogEditor.Show(this,"VanPhong")),("Sửa phòng",g=>CatalogEditor.Show(this,"VanPhong",FormHelper.Current(g))),
            ("Xóa phòng",g=>CatalogEditor.Delete("VanPhong",g)),("Lịch thuê",g=>{using var f=new FrmTable("Lịch thuê phòng",Db.Query("SELECT h.MaHD,h.MaKH,h.NgayBatDau,ct.NgayKetThuc,ct.DonGiaThang,h.TrangThai FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE ct.MaPhong=@p0 ORDER BY h.NgayBatDau",FormHelper.Current(g)["MaPhong"]));f.ShowDialog(this);}));
        FormHelper.Page(tabs,"Tầng",()=>service.List("Tang"),("Thêm tầng",_=>CatalogEditor.Show(this,"Tang")),("Sửa tầng",g=>CatalogEditor.Show(this,"Tang",FormHelper.Current(g))),("Xóa tầng",g=>CatalogEditor.Delete("Tang",g)));
        Load+=(_,_)=>{if(FormHelper.IsDesigner(this))return;FormHelper.Run(()=>
        {
            var data=FormHelper.Choices(service.List("Tang"),"MaTang","TenTang");data.Rows.InsertAt(data.NewRow(),0);data.Rows[0]["Text"]="Tất cả tầng";
            cboTang.BindingContext=new BindingContext();cboTang.DataSource=data;cboTang.DisplayMember="Text";cboTang.ValueMember="Id";FormHelper.LoadAll(tabs);
        });};
    }
}
