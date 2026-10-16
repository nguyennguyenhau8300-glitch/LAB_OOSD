using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

public partial class FrmHoSo : Form
{
    readonly BenhAnService service = new();
    byte[] anh; void Nap() => grid.DataSource = service.HoSo(txtTim.Text);
    public FrmHoSo() { InitializeComponent(); phi.Value = 200000; ngaySinh.Value = DateTime.Today.AddYears(-20); btnAnh.Click += (s, e) => { using var d = new OpenFileDialog { Filter = "Ảnh|*.jpg;*.jpeg;*.png" }; if (d.ShowDialog() == DialogResult.OK) FormHelper.Lam(() => { using var im = Image.FromFile(d.FileName); if (new FileInfo(d.FileName).Length > 5000000) throw new Exception("Ảnh tối đa 5 MB."); anh = File.ReadAllBytes(d.FileName); btnAnh.Text = "Đã chọn ảnh"; }); }; btnThem.Click += (s, e) => { var result = service.DangKy(txtTen.Text, ngaySinh.Value, txtDiaChi.Text, txtDT.Text, anh, phi.Value); FormHelper.Bao(result); if (result.ThanhCong) { txtTen.Clear(); txtDiaChi.Clear(); txtDT.Clear(); anh = null; btnAnh.Text = "Chọn ảnh 3x4"; } FormHelper.Lam(Nap); }; btnTim.Click += (s, e) => FormHelper.Lam(Nap); btnGiaHan.Click += (s, e) => { FormHelper.Bao(service.GiaHan(FormHelper.Id(grid, "MaHS"), phi.Value)); }; btnPhi.Click += (s, e) => FormHelper.Lam(() => { var d = service.Phi(FormHelper.Id(grid, "MaHS")); MessageBox.Show(string.Join("\n", d.Rows.Cast<System.Data.DataRow>().Select(r => $"{r["TuNgay"]:d} - {r["DenNgay"]:d}: {r["SoTien"]:N0}")), "Lịch sử lệ phí"); }); Load += (s, e) => FormHelper.Lam(() => { Nap(); }); }
}
