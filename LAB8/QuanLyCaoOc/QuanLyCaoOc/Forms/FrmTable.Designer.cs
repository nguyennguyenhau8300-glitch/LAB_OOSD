#nullable enable
namespace QuanLyCaoOc.Forms;
public partial class FrmTable
{
    private System.ComponentModel.IContainer? components;
    private DataGridView dgvData=null!;
    private FlowLayoutPanel pnlButtons=null!;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        components=new System.ComponentModel.Container();dgvData=FormHelper.Grid();pnlButtons=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=54};
        Controls.Add(dgvData);Controls.Add(pnlButtons);
    }
}

