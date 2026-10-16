using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

public partial class FrmLichHen : Form
{
    readonly BenhAnService service = new();
    void Nap() => grid.DataSource = service.Hen(FormHelper.Gia(hs));
    public FrmLichHen() { InitializeComponent(); at.Value = DateTime.Now.AddDays(1); btnDat.Click += (s, e) => FormHelper.Lam(() => { FormHelper.Bao(service.DatHen(FormHelper.Gia(hs), FormHelper.Gia(bs), at.Value)); Nap(); }); btnHuy.Click += (s, e) => FormHelper.Lam(() => { FormHelper.Bao(service.HuyHen(FormHelper.Id(grid, "MaHen"))); Nap(); }); btnTai.Click += (s, e) => FormHelper.Lam(Nap); hs.SelectedIndexChanged += (s, e) => FormHelper.Lam(Nap); Load += (s, e) => FormHelper.Lam(() => { FormHelper.Nap(hs, service.HoSo(), "HoTen", "MaHS"); FormHelper.Nap(bs, service.BacSi(), "HoTen", "MaBS"); Nap(); }); }
}
