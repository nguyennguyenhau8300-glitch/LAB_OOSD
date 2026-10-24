using QuanLyNhanSu.Services;
namespace QuanLyNhanSu.Forms;
partial class FrmChucNang
{
    ListBox navigation=null!;DataGridView grid=null!;FlowLayoutPanel actions=null!;TextBox search=null!;Label title=null!,status=null!,hint=null!;
    NumericUpDown month=null!,year=null!,standard=null!;
    Panel workspace=null!,sidebar=null!;Label groupTitle=null!;
    void InitializeWorkspace()
    {
        Text="Quản lý nhân sự – Công ty sơn Đông Á";Font=new Font("Segoe UI",10);ClientSize=new Size(1320,780);MinimumSize=new Size(1050,650);StartPosition=FormStartPosition.CenterScreen;BackColor=Color.White;ForeColor=Color.Black;
        navigation=new ListBox{Dock=DockStyle.Fill,BorderStyle=BorderStyle.FixedSingle,ItemHeight=36,IntegralHeight=false,BackColor=Color.White,ForeColor=Color.Black,DrawMode=DrawMode.OwnerDrawFixed};
        navigation.DrawItem+=(_,e)=>{if(e.Index<0)return;bool active=(e.State&DrawItemState.Selected)!=0;using var fill=new SolidBrush(active?Color.Gainsboro:Color.White);e.Graphics.FillRectangle(fill,e.Bounds);TextRenderer.DrawText(e.Graphics,((Module)navigation.Items[e.Index]).Title,Font,e.Bounds,Color.Black,TextFormatFlags.VerticalCenter|TextFormatFlags.Left|TextFormatFlags.EndEllipsis);e.DrawFocusRectangle();};
        sidebar=new Panel{Dock=DockStyle.Left,Width=250,Padding=new Padding(12),BackColor=Color.White};
        groupTitle=new Label{Dock=DockStyle.Top,Height=100,Text="QUẢN LÝ NHÂN SỰ",Font=new Font("Segoe UI",11,FontStyle.Bold)};
        var back=new Button{Text="Đóng – Về màn hình chính",Dock=DockStyle.Bottom,Height=42,Margin=new Padding(0,12,0,0)};back.Click+=(_,_)=>Close();sidebar.Controls.Add(navigation);sidebar.Controls.Add(groupTitle);sidebar.Controls.Add(back);
        workspace=new Panel{Dock=DockStyle.Fill,Padding=new Padding(16)};
        var main=workspace;
        title=new Label{Dock=DockStyle.Top,Height=44,Font=new Font("Segoe UI",18,FontStyle.Bold)};
        search=new TextBox{Width=260,PlaceholderText="Tìm theo tên, số chứng từ…"};
        month=new NumericUpDown{Minimum=1,Maximum=12,Value=DateTime.Today.Month,Width=55};year=new NumericUpDown{Minimum=2000,Maximum=2100,Value=DateTime.Today.Year,Width=75};standard=new NumericUpDown{Minimum=1,Maximum=744,Value=208,DecimalPlaces=2,Width=85};
        var filters=new FlowLayoutPanel{Dock=DockStyle.Top,Height=48,WrapContents=false};filters.Controls.AddRange([search,new Label{Text="Tháng",AutoSize=true,Margin=new Padding(12,7,2,0)},month,new Label{Text="Năm",AutoSize=true,Margin=new Padding(8,7,2,0)},year,new Label{Text="Giờ chuẩn",AutoSize=true,Margin=new Padding(8,7,2,0)},standard]);
        actions=new FlowLayoutPanel{Dock=DockStyle.Top,Height=94,AutoScroll=true};
        hint=new Label{Dock=DockStyle.Top,Height=62,ForeColor=Color.Black};
        grid=new DataGridView{Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,AllowUserToDeleteRows=false,MultiSelect=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.AllCells,BackgroundColor=Color.White,RowHeadersVisible=false,BorderStyle=BorderStyle.None};
        grid.EnableHeadersVisualStyles=false;grid.ColumnHeadersDefaultCellStyle.BackColor=Color.Gainsboro;grid.ColumnHeadersDefaultCellStyle.ForeColor=Color.Black;grid.ColumnHeadersDefaultCellStyle.SelectionBackColor=Color.Gainsboro;grid.ColumnHeadersDefaultCellStyle.SelectionForeColor=Color.Black;grid.DefaultCellStyle.SelectionBackColor=Color.Gainsboro;grid.DefaultCellStyle.SelectionForeColor=Color.Black;grid.AlternatingRowsDefaultCellStyle.BackColor=Color.White;grid.RowTemplate.Height=32;grid.ColumnHeadersHeight=38;
        status=new Label{Dock=DockStyle.Bottom,Height=32,Text="Sẵn sàng",Padding=new Padding(4,7,0,0)};
        main.Controls.Add(grid);main.Controls.Add(hint);main.Controls.Add(actions);main.Controls.Add(filters);main.Controls.Add(title);main.Controls.Add(status);Controls.Add(main);Controls.Add(sidebar);
    }
}
