using System.Data;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms;

internal static class FormHelper
{
    public static void Nap(ComboBox combo, DataTable data, string display, string value)
    {
        combo.DataSource = null;
        combo.DisplayMember = display;
        combo.ValueMember = value;
        combo.DataSource = data;
    }

    public static string Gia(ComboBox combo)
    {
        return combo.SelectedItem is DataRowView row && row.Row.Table.Columns.Contains(combo.ValueMember)
            ? Convert.ToString(row[combo.ValueMember]) ?? "" : "";
    }

    public static string O(DataGridView grid, string column)
    {
        if (grid.CurrentRow == null || grid.CurrentRow.IsNewRow || !grid.Columns.Contains(column)) return "";
        return Convert.ToString(grid.CurrentRow.Cells[column].Value) ?? "";
    }

    public static bool Bao(KetQuaXuLy result)
    {
        MessageBox.Show(result.ThongBao, result.ThanhCong ? "Thành công" : "Không thể thực hiện",
            MessageBoxButtons.OK, result.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        return result.ThanhCong;
    }
}
