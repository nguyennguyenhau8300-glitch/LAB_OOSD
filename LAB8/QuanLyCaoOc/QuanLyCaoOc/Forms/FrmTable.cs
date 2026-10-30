using System.Data;
namespace QuanLyCaoOc.Forms;
public partial class FrmTable:Form
{
    public FrmTable(){InitializeComponent();FormHelper.Style(this,"Chi tiết");pnlButtons.Controls.Add(FormHelper.Button("Xuất CSV",()=>FormHelper.Export(dgvData,Text)));pnlButtons.Controls.Add(FormHelper.Button("Đóng",Close));}
    public FrmTable(string title,DataTable data):this(){Text=title;FormHelper.Bind(dgvData,data);}
}
