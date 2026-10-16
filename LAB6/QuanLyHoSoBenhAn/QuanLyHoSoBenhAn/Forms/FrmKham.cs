using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

public partial class FrmKham : Form
{
    readonly BenhAnService service = new();
    void Nap() => grid.DataSource = service.Kham(FormHelper.Gia(hs));
    public FrmKham() { InitializeComponent(); dinhKy.Text = "Định kỳ"; tien.Value = 150000; btnLap.Click += (s, e) => FormHelper.Lam(() => { FormHelper.Bao(service.LapKham(FormHelper.Gia(hs), FormHelper.Gia(bs), dinhKy.Checked, tc.Text, tien.Value, hen.Value == 0 ? null : (int)hen.Value)); Nap(); }); btnTai.Click += (s, e) => FormHelper.Lam(Nap); hs.SelectedIndexChanged += (s, e) => FormHelper.Lam(() => { tc.Clear(); hen.Value=0; dinhKy.Checked=false; Nap(); }); Load += (s, e) => FormHelper.Lam(() => { FormHelper.Nap(hs, service.HoSo(), "HoTen", "MaHS"); FormHelper.Nap(bs, service.BacSi(), "HoTen", "MaBS"); Nap(); }); }
}
