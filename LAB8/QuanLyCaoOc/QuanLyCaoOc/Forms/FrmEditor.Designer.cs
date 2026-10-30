#nullable enable
namespace QuanLyCaoOc.Forms;

public partial class FrmEditor
{
    private System.ComponentModel.IContainer? components;
    private FlowLayoutPanel pnlFields=null!;
    private Panel pnlDetail=null!;
    private FlowLayoutPanel pnlActions=null!;
    private Button btnSave=null!,btnCancel=null!;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        components=new System.ComponentModel.Container();
        pnlFields=new FlowLayoutPanel{Dock=DockStyle.Top,AutoSize=true,WrapContents=true,Padding=new Padding(10)};
        pnlDetail=new Panel{Dock=DockStyle.Fill,Padding=new Padding(12)};
        pnlActions=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=58,FlowDirection=FlowDirection.RightToLeft,Padding=new Padding(8)};
        btnSave=new Button{Text="Lưu",Width=120,Height=36};btnCancel=new Button{Text="Hủy",Width=120,Height=36,DialogResult=DialogResult.Cancel};
        pnlActions.Controls.AddRange([btnCancel,btnSave]);Controls.Add(pnlDetail);Controls.Add(pnlFields);Controls.Add(pnlActions);
        AcceptButton=btnSave;CancelButton=btnCancel;
    }
}

