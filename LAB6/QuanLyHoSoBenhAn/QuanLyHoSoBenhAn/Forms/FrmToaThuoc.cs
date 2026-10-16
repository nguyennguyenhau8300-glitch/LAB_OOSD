using System.Data;
using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

public partial class FrmToaThuoc : Form
{
    readonly BenhAnService service = new();
    readonly List<DongThuoc> ds = new();
    void Nap() => grid.DataSource = ds.Select(x => new { x.MaThuoc, x.SoLuong, x.LieuDung }).ToList();
    void ChonPhieu()
    {
        ds.Clear(); Nap(); lieu.Clear(); sl.Value=1;
        cd.Text = pk.SelectedItem is DataRowView row ? Convert.ToString(row["ChanDoan"]) : "";
    }
    void NapPK()
    {
        ds.Clear(); cd.Clear(); Nap();
        FormHelper.Nap(pk, service.Kham(FormHelper.Gia(hs)), "MaPK", "MaPK");
        ChonPhieu();
    }
    void XemToa()
    {
        int maPK = FormHelper.Gia(pk);
        var rows = service.Kham(FormHelper.Gia(hs)).Rows.Cast<DataRow>();
        var phieu = rows.FirstOrDefault(r => Convert.ToInt32(r["MaPK"]) == maPK);
        var toa = service.Toa(maPK);
        ds.Clear(); cd.Text = phieu == null ? "" : Convert.ToString(phieu["ChanDoan"]);
        grid.DataSource = toa;
    }
    public FrmToaThuoc()
    {
        InitializeComponent(); sl.Minimum = 1; sl.Value = 1;
        btnThem.Click += (_, _) => FormHelper.Lam(() => {
            int maThuoc = FormHelper.Gia(thuoc);
            if (maThuoc <= 0 || string.IsNullOrWhiteSpace(lieu.Text)) throw new InvalidOperationException("Chọn thuốc và nhập liều dùng.");
            if (ds.Any(x => x.MaThuoc == maThuoc)) throw new InvalidOperationException("Thuốc đã có trong danh sách. Xóa dòng cũ trước khi thêm lại.");
            ds.Add(new(maThuoc, (int)sl.Value, lieu.Text.Trim())); Nap();
        });
        btnXoa.Click += (_, _) => { var id = FormHelper.Id(grid, "MaThuoc"); ds.RemoveAll(x => x.MaThuoc == id); Nap(); };
        btnLuu.Click += (_, _) => FormHelper.Lam(() => {
            var r = service.LapToa(FormHelper.Gia(pk), cd.Text, ds); FormHelper.Bao(r);
            if (r.ThanhCong) XemToa();
        });
        btnXem.Click += (_, _) => FormHelper.Lam(XemToa);
        btnTT.Click += (_, _) => FormHelper.Bao(service.ThanhToan(FormHelper.Gia(pk)));
        btnXuat.Click += (_, _) => FormHelper.Lam(() => {
            var content = service.XuatToa(FormHelper.Gia(pk));
            using var f = new SaveFileDialog { Filter = "Văn bản|*.txt", FileName = $"ToaThuoc_{FormHelper.Gia(pk)}.txt" };
            if (f.ShowDialog() == DialogResult.OK) File.WriteAllText(f.FileName, content, System.Text.Encoding.UTF8);
        });
        hs.SelectedIndexChanged += (_, _) => FormHelper.Lam(NapPK);
        pk.SelectedIndexChanged += (_, _) => ChonPhieu();
        Load += (_, _) => FormHelper.Lam(() => {
            FormHelper.Nap(hs, service.HoSo(), "HoTen", "MaHS");
            FormHelper.Nap(thuoc, service.Thuoc(), "TenThuoc", "MaThuoc"); NapPK();
        });
    }
}
