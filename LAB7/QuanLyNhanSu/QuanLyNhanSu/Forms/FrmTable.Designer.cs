namespace QuanLyNhanSu.Forms;
partial class FrmTable
{
    DataGridView grid=null!;
    void InitializeComponent(){Font=new Font("Segoe UI",10);ClientSize=new Size(1100,650);StartPosition=FormStartPosition.CenterParent;grid=new DataGridView{Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,AllowUserToDeleteRows=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.AllCells,SelectionMode=DataGridViewSelectionMode.FullRowSelect,BackgroundColor=Color.White,RowHeadersVisible=false,EnableHeadersVisualStyles=false};grid.ColumnHeadersDefaultCellStyle.BackColor=Color.Gainsboro;grid.ColumnHeadersDefaultCellStyle.ForeColor=Color.Black;grid.DefaultCellStyle.SelectionBackColor=Color.Gainsboro;grid.DefaultCellStyle.SelectionForeColor=Color.Black;Controls.Add(grid);}
}
