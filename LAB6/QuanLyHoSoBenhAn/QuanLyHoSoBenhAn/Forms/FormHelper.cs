using System.Data;
using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

internal static class FormHelper
{
    public static void Nap(ComboBox c, DataTable d, string text, string id)
    {
        if (id == "MaHS" && d.Columns.Contains("HoTen"))
        {
            d = d.Copy(); d.Columns.Add("NhanHoSo", typeof(string));
            foreach (DataRow row in d.Rows) row["NhanHoSo"] = $"HS{row["MaHS"]} - {row["HoTen"]}";
            text = "NhanHoSo";
        }
        if (id == "MaBS" && d.Columns.Contains("HoTen"))
        {
            d = d.Copy(); d.Columns.Add("NhanBacSi", typeof(string));
            foreach (DataRow row in d.Rows) row["NhanBacSi"] = $"BS{row["MaBS"]} - {row["HoTen"]}";
            text = "NhanBacSi";
        }
        c.DropDownWidth = Math.Max(c.Width, 380); c.DisplayMember = text; c.ValueMember = id; c.DataSource = d;
    }
    public static int Gia(ComboBox c) => c.SelectedValue is int n ? n : 0;
    public static int Id(DataGridView g, string col) => g.CurrentRow == null ? 0 : Convert.ToInt32(g.CurrentRow.Cells[col].Value);
    public static void Bao(KetQuaXuLy k) => MessageBox.Show(k.ThongBao, k.ThanhCong ? "Thành công" : "Không thể thực hiện", MessageBoxButtons.OK, k.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
    public static void Lam(Action a) { try { a(); } catch (Exception e) { MessageBox.Show(e.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); } }
    public static void TrangTri(Form form, TableLayoutPanel layout, FlowLayoutPanel bar, DataGridView grid)
    {
        form.BackColor = Color.White;
        layout.BackColor = Color.White;
        layout.RowStyles[1] = new RowStyle(SizeType.AutoSize);
        bar.BackColor = Color.White; bar.AutoSize = true;
        bar.AutoSizeMode = AutoSizeMode.GrowAndShrink; bar.AutoScroll = false;
        bar.Padding = new Padding(10, 4, 10, 4);
        var controls = bar.Controls.Cast<Control>().ToArray();
        bar.Controls.Clear();
        for (var i = 0; i < controls.Length; i++)
        {
            var control = controls[i];
            if (control is Label && i + 1 < controls.Length && controls[i + 1] is not Button)
            {
                var field = controls[++i];
                var cell = new Panel { Width = 218, Height = 65, Margin = new Padding(4) };
                control.Location = new Point(0, 0); control.Margin = Padding.Empty;
                control.Width = 214; field.Location = new Point(0, 25); field.Width = 214;
                cell.Controls.Add(control); cell.Controls.Add(field); bar.Controls.Add(cell);
            }
            else
            {
                control.Margin = new Padding(4, 8, 4, 4);
                if (control is Button button) { button.Height = 36; button.MinimumSize = new Size(110,36); button.FlatStyle = FlatStyle.Flat; button.BackColor = Color.White; }
                bar.Controls.Add(control);
            }
        }
        grid.BackgroundColor = Color.White; grid.BorderStyle = BorderStyle.FixedSingle;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font(form.Font, FontStyle.Bold);
        grid.DefaultCellStyle.BackColor = Color.White;
        grid.RowHeadersVisible = false;
        grid.DataBindingComplete += (_, _) => {
            foreach (DataGridViewColumn column in grid.Columns) {
                column.MinimumWidth = 65;
                if (column.Name == "Anh" || (form is FrmKham && new[] { "MaHS", "MaBS", "MaKhoa", "MaHen" }.Contains(column.Name))) column.Visible = false;
                if (column.Name is "TrieuChung" or "ChanDoan" or "LieuDung") column.FillWeight = 180;
                if (column.Name.StartsWith("Ma")) column.FillWeight = 55;
            }
        };
    }
}
