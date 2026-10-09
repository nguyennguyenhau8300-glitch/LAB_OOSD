using System.Drawing.Imaging;
using QuanLyCongTyDuLich.Forms;

namespace QuanLyCongTyDuLich;

internal static class FormDiagnostics
{
    // Read-only diagnostic: opens every form and tab with the configured database.
    public static void Run(string directory)
    {
        Directory.CreateDirectory(directory);
        var results = new List<string>();
        using var log = new StreamWriter(Path.Combine(directory, "forms.log"), false);
        try
        {
            using (var cn = Data.Db.OpenConnection()) results.Add("Database: " + cn.Database + " connected");
            foreach (var type in new[] { typeof(FrmMain), typeof(FrmDanhMuc), typeof(FrmTour), typeof(FrmChuyenLe),
                         typeof(FrmDangKyLe), typeof(FrmDangKyDoan), typeof(FrmPhanCongHDV), typeof(FrmKetThucKhaoSat), typeof(FrmLuongThongKe) })
            {
                using var form = (Form)Activator.CreateInstance(type)!;
                form.Show();
                Application.DoEvents();
                var tabs = form.Controls.OfType<TabControl>().FirstOrDefault();
                int count = tabs?.TabPages.Count ?? 1;
                for (int i = 0; i < count; i++)
                {
                    if (tabs != null) tabs.SelectedIndex = i;
                    Application.DoEvents();
                    if (form is FrmLuongThongKe && tabs != null)
                    {
                        if (i == 0)
                            ((NumericUpDown)form.Controls.Find("numThang", true)[0]).Value = 9;
                        var name = i == 0 ? "btnLuong" : "btnTongHop";
                        ((Button)form.Controls.Find(name, true)[0]).PerformClick();
                        Application.DoEvents();
                    }
                    using var bitmap = new Bitmap(form.Width, form.Height);
                    form.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    bitmap.Save(Path.Combine(directory, type.Name + "_" + i + ".png"), ImageFormat.Png);
                }
                results.Add(type.Name + ": loaded " + count + " page(s)");
                form.Close();
            }
            var service = new Services.ThongKeService();
            var salary = service.LuongHDV(9, 2026);
            if (salary.Rows.Count != 3) throw new InvalidOperationException("Expected 3 guides in sample salary.");
            foreach (System.Data.DataRow row in salary.Rows)
            {
                decimal expected = Convert.ToString(row["MaHDV"]) == "HDV02" ? 11500000m : 10500000m;
                if (Convert.ToDecimal(row["TongLuong"]) != expected) throw new InvalidOperationException("Unexpected salary for " + row["MaHDV"]);
            }
            if (service.TongHop(new DateTime(2026, 1, 1), new DateTime(2026, 10, 1)).Rows.Count != 5)
                throw new InvalidOperationException("Expected 5 statistics indicators.");
            results.Add("Salary sample and 5 statistics indicators: passed");
            log.WriteLine(string.Join(Environment.NewLine, results));
        }
        catch (Exception ex)
        {
            log.WriteLine(string.Join(Environment.NewLine, results));
            log.WriteLine(ex);
            Environment.ExitCode = 1;
        }
    }
}
