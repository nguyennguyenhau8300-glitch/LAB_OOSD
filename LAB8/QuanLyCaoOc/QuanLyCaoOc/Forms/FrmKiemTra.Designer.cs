#nullable enable
namespace QuanLyCaoOc.Forms;
partial class FrmKiemTra
{
    private System.ComponentModel.IContainer? components;
    private TabControl tabs = null!;
    private FlowLayoutPanel pnlFilters = null!;
    protected override void Dispose(bool disposing)
    {
        if(disposing) components?.Dispose();
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        tabs = new TabControl { Name="tabs", Dock=DockStyle.Fill };
        pnlFilters = new FlowLayoutPanel { Name="pnlFilters", Dock=DockStyle.Top, AutoSize=true, Padding=new Padding(10), WrapContents=true };
        SuspendLayout();
        AutoScaleMode=AutoScaleMode.Dpi;
        ClientSize=new Size(1320,800);
        Controls.Add(tabs);
        Controls.Add(pnlFilters);
        Name="FrmKiemTra";
        StartPosition=FormStartPosition.CenterParent;
        ResumeLayout(false);
        PerformLayout();
    }
}


