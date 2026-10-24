namespace QuanLyNhanSu.Forms;
partial class FrmEditor
{
    TableLayoutPanel fields=null!;Button save=null!,cancel=null!;
    void InitializeComponent()
    {
        Font=new Font("Segoe UI",10);ClientSize=new Size(820,650);MinimumSize=new Size(750,450);StartPosition=FormStartPosition.CenterParent;ShowInTaskbar=false;
        fields=new TableLayoutPanel{Dock=DockStyle.Top,AutoSize=true,ColumnCount=2,Padding=new Padding(12)};fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,300));fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        var scroll=new Panel{Dock=DockStyle.Fill,AutoScroll=true};scroll.Controls.Add(fields);
        var bottom=new FlowLayoutPanel{Dock=DockStyle.Bottom,Height=60,FlowDirection=FlowDirection.RightToLeft,Padding=new Padding(8)};
        cancel=new Button{Text="Hủy",Width=100,Height=36,DialogResult=DialogResult.Cancel};save=new Button{Text="Lưu",Width=110,Height=36};bottom.Controls.AddRange([cancel,save]);Controls.Add(scroll);Controls.Add(bottom);AcceptButton=save;CancelButton=cancel;
    }
}
