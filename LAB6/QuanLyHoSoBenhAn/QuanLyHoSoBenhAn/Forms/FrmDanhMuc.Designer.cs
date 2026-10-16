namespace QuanLyHoSoBenhAn.Forms;
partial class FrmDanhMuc
{
    private System.ComponentModel.IContainer components=null;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private ComboBox loai,khoa;
    private TextBox ten,donVi,tuKhoa;
    private Label tenLabel,trangThai;
    private Panel khoaPanel,donViPanel;
    private Button btnMoi,btnLuu,btnXoa,btnTai,btnTim;
    private DataGridView grid;
    private void InitializeComponent()
    {
        Text="Quản lý danh mục khoa bác sĩ và thuốc";Font=new Font("Segoe UI",10);AutoScaleMode=AutoScaleMode.Dpi;
        ClientSize=new Size(1180,740);MinimumSize=new Size(1000,680);StartPosition=FormStartPosition.CenterParent;
        var layout=new TableLayoutPanel{Dock=DockStyle.Fill,RowCount=3,ColumnCount=1};
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute,55));layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));
        layout.Controls.Add(new Label{Text="QUẢN LÝ DANH MỤC",Dock=DockStyle.Fill,Font=new Font("Segoe UI",20,FontStyle.Bold),TextAlign=ContentAlignment.MiddleCenter},0,0);
        var bar=new FlowLayoutPanel{Dock=DockStyle.Fill,AutoSize=true,Padding=new Padding(10)};layout.Controls.Add(bar,0,1);
        Panel Field(string text,Control input)
        {
            var p=new Panel{Width=218,Height=65,Margin=new Padding(4)};
            p.Controls.Add(new Label{Text=text,Location=new Point(0,0),AutoSize=true});input.Location=new Point(0,25);input.Width=214;p.Controls.Add(input);bar.Controls.Add(p);return p;
        }
        loai=new ComboBox{Name="loai",DropDownStyle=ComboBoxStyle.DropDownList};loai.Items.AddRange(new object[]{"Khoa","Bác sĩ","Thuốc"});Field("Loại danh mục",loai);
        ten=new TextBox{Name="ten"};var tenPanel=Field("Tên khoa",ten);tenLabel=(Label)tenPanel.Controls[0];
        khoa=new ComboBox{Name="khoa",DropDownStyle=ComboBoxStyle.DropDownList};khoaPanel=Field("Khoa của bác sĩ",khoa);
        donVi=new TextBox{Name="donVi",MaxLength=30};donViPanel=Field("Đơn vị thuốc",donVi);
        tuKhoa=new TextBox{Name="tuKhoa"};Field("Tìm theo mã hoặc tên",tuKhoa);
        Button Action(string name,string text){var b=new Button{Name=name,Text=text,AutoSize=true};bar.Controls.Add(b);return b;}
        btnTim=Action("btnTim","Tìm kiếm");btnMoi=Action("btnMoi","Thêm mới");btnLuu=Action("btnLuu","Lưu");btnXoa=Action("btnXoa","Xóa");btnTai=Action("btnTai","Tải lại");
        trangThai=new Label{AutoSize=true,Margin=new Padding(8,16,0,0)};bar.Controls.Add(trangThai);
        var area=new Panel{Dock=DockStyle.Fill,Padding=new Padding(10)};layout.Controls.Add(area,0,2);
        grid=new DataGridView{Name="grid",Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,MultiSelect=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect};area.Controls.Add(grid);
        Controls.Add(layout);FormHelper.TrangTri(this,layout,bar,grid);
    }
}
