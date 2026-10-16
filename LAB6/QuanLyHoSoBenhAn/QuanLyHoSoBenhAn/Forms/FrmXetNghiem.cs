using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

public partial class FrmXetNghiem : Form
{
    readonly BenhAnService service = new();
    void Nap() => grid.DataSource = service.XN(FormHelper.Gia(pk)); void NapPK() => FormHelper.Nap(pk, service.Kham(FormHelper.Gia(hs)), "MaPK", "MaPK");
    public FrmXetNghiem() { InitializeComponent(); grid.SelectionChanged += (_,_) => kq.Text = grid.CurrentRow == null ? "" : Convert.ToString(grid.CurrentRow.Cells["KetQua"].Value); btnThem.Click += (s, e) => FormHelper.Lam(() => { FormHelper.Bao(service.ChiDinh(FormHelper.Gia(pk), ten.Text, phi.Value)); Nap(); }); btnLuu.Click += (s, e) => FormHelper.Lam(() => { FormHelper.Bao(service.KetQua(FormHelper.Id(grid, "MaXN"), kq.Text)); Nap(); }); hs.SelectedIndexChanged += (s, e) => FormHelper.Lam(NapPK); pk.SelectedIndexChanged += (s, e) => FormHelper.Lam(() => { ten.Clear(); phi.Value=0; kq.Clear(); Nap(); }); Load += (s, e) => FormHelper.Lam(() => { FormHelper.Nap(hs, service.HoSo(), "HoTen", "MaHS"); NapPK(); Nap(); }); }
}
