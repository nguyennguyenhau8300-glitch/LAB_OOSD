using System.Data;
namespace QuanLyNhanSu.Forms;
public partial class FrmTable:Form
{
    public FrmTable(string title,DataTable data){InitializeComponent();Text=title;grid.DataSource=data;FormHelper.FormatGrid(grid);}
}
