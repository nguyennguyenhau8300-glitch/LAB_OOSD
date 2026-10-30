using System.Data;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public partial class FrmKiemTra:Form
{
    readonly KiemTraService service=new();
    readonly DateTimePicker dtpNgay=new(){Name="dtpNgay",Width=150,Format=DateTimePickerFormat.Custom,CustomFormat="dd/MM/yyyy"};
    public FrmKiemTra()
    {
        InitializeComponent();FormHelper.Style(this,"Kiểm tra phòng và xử phạt");
        pnlFilters.Controls.AddRange([new Label{Text="Xem phòng đến hạn ngày",AutoSize=true,Padding=new Padding(0,8,0,0)},dtpNgay,FormHelper.Button("Tải danh sách",()=>FormHelper.LoadAll(tabs))]);
        FormHelper.Page(tabs,"Đến hạn 12 tháng",()=>Reports.DueChecks(dtpNgay.Value),("Lập biên bản",_=>NewCheck()));
        FormHelper.Page(tabs,"Biên bản kiểm tra",Reports.Checks,("Lập biên bản",_=>NewCheck()),("Lập phiếu phạt",g=>NewFine(FormHelper.Current(g))));
        FormHelper.Page(tabs,"Phiếu phạt",Reports.Fines,("Ghi nhận nộp phạt",g=>
        {
            var r=FormHelper.Current(g);using var d=new FrmEditor("Nộp tiền phạt",[new("Tien","Số tiền nộp","money",(decimal)r["SoTien"]-(decimal)r["DaNop"])]);
            d.SaveAction=e=>service.NopPhat((string)r["MaPhat"],e.M("Tien"));d.ShowDialog(this);
        }));
        Load+=(_,_)=>{if(!FormHelper.IsDesigner(this))FormHelper.Run(()=>FormHelper.LoadAll(tabs));};
    }
    void NewCheck()
    {
        var rooms=FormHelper.Selectable(new DanhMucService().List("VanPhong").DefaultView.ToTable(false,"MaPhong","MaTang"),
            ("TinhTrang",typeof(string),"Bình thường"),("ThayDoiKetCau",typeof(bool),false),("DeXuat",typeof(string),""));
        using var dialog=new FrmEditor("Biên bản kiểm tra nhiều phòng",[new("Ngay","Ngày kiểm tra","date"),new("Nguoi","Người kiểm tra","text",Environment.UserName),new("GhiChu","Ghi chú")],rooms,["Chon","TinhTrang","ThayDoiKetCau","DeXuat"]);
        dialog.SaveAction=e=>
        {
            var lines=FormHelper.Chosen(rooms).Select(r=>new KiemTraPhong((string)r["MaPhong"],(string)r["TinhTrang"],(bool)r["ThayDoiKetCau"],(string)r["DeXuat"])).ToList();
            string id=service.LapBienBan(e.D("Ngay"),e.S("Nguoi"),e.S("GhiChu"),lines);FormHelper.Info("Đã lưu biên bản "+id);
        };dialog.ShowDialog(this);
    }
    void NewFine(DataRow check)
    {
        var rentals=Db.Query("SELECT ct.MaCTHD,ct.MaPhong+' / '+h.MaKH+' / '+h.MaHD ThongTin FROM ChiTietHopDong ct JOIN HopDong h ON h.MaHD=ct.MaHD WHERE ct.MaPhong=@p0 AND h.NgayBatDau<=@p1 AND @p1<ct.NgayKetThuc AND h.TrangThai IN('HIEU_LUC','KET_THUC')",check["MaPhong"],check["NgayKiemTra"]);
        using var d=new FrmEditor("Lập phiếu phạt theo biên bản",[
            FormHelper.Choice("MaCTHD","Bên thuê chịu trách nhiệm",FormHelper.Choices(rentals,"MaCTHD","ThongTin")),new("LyDo","Lý do / căn cứ phạt"),new("Tien","Mức phạt","money",500000),new("Han","Hạn nộp","date")]);
        d.SaveAction=e=>{string id=service.LapPhat((long)check["MaCTKT"],e.L("MaCTHD"),e.S("LyDo"),e.M("Tien"),e.D("Han"));FormHelper.Info("Đã lập "+id);};d.ShowDialog(this);
    }
}
