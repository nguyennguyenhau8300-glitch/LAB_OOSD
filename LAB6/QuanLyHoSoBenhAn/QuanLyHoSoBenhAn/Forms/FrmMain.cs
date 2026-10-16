using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

public partial class FrmMain : Form
{
    readonly BenhAnService service = new();

    public FrmMain() { InitializeComponent(); btnDanhMuc.Click += (s,e) => new FrmDanhMuc().ShowDialog(); btnHoSo.Click += (s, e) => new FrmHoSo().ShowDialog(); btnHen.Click += (s, e) => new FrmLichHen().ShowDialog(); btnKham.Click += (s, e) => new FrmKham().ShowDialog(); btnXN.Click += (s, e) => new FrmXetNghiem().ShowDialog(); btnToa.Click += (s, e) => new FrmToaThuoc().ShowDialog(); btnThongKe.Click += (s, e) => FormHelper.Lam(() => grid.DataSource = service.ThongKe()); Load += (s, e) => FormHelper.Lam(() => { grid.DataSource = service.ThongKe(); }); }
}
