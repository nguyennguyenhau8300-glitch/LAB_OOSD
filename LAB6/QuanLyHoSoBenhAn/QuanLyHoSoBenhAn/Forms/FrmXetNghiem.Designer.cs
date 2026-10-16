namespace QuanLyHoSoBenhAn.Forms;
partial class FrmXetNghiem {
 private System.ComponentModel.IContainer components=null;
 protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
 private ComboBox hs; private ComboBox pk; private TextBox ten; private NumericUpDown phi; private Button btnThem; private TextBox kq; private Button btnLuu; private DataGridView grid;
 private void InitializeComponent(){
 Text="Chỉ định và ghi nhận xét nghiệm"; Font=new Font("Segoe UI",10); AutoScaleMode=AutoScaleMode.Dpi; ClientSize=new Size(1180,740); MinimumSize=new Size(1000,680); StartPosition=FormStartPosition.CenterParent;
 var layout=new TableLayoutPanel{Dock=DockStyle.Fill,RowCount=3,ColumnCount=1};
 layout.RowStyles.Add(new RowStyle(SizeType.Absolute,55));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,225));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));
 layout.Controls.Add(new Label{Text="Chỉ định và ghi nhận xét nghiệm",Dock=DockStyle.Fill,Font=new Font("Segoe UI",20,FontStyle.Bold),ForeColor=Color.FromArgb(0,51,102),TextAlign=ContentAlignment.MiddleCenter},0,0);
 var bar=new FlowLayoutPanel{Dock=DockStyle.Fill,AutoScroll=true,Padding=new Padding(12)};layout.Controls.Add(bar,0,1);
 var area=new Panel{Dock=DockStyle.Fill,Padding=new Padding(10)};layout.Controls.Add(area,0,2);
 hs=new ComboBox(); hs.Name="hs"; bar.Controls.Add(new Label(){Text="Hồ sơ",AutoSize=true,Margin=new Padding(8,8,2,0)}); hs.Width=175; bar.Controls.Add(hs); hs.DropDownStyle=ComboBoxStyle.DropDownList; pk=new ComboBox(); pk.Name="pk"; bar.Controls.Add(new Label(){Text="Phiếu khám",AutoSize=true,Margin=new Padding(8,8,2,0)}); pk.Width=175; bar.Controls.Add(pk); pk.DropDownStyle=ComboBoxStyle.DropDownList; ten=new TextBox(); ten.Name="ten"; bar.Controls.Add(new Label(){Text="Tên xét nghiệm",AutoSize=true,Margin=new Padding(8,8,2,0)}); ten.Width=175; bar.Controls.Add(ten); phi=new NumericUpDown(); phi.Name="phi"; bar.Controls.Add(new Label(){Text="Chi phí",AutoSize=true,Margin=new Padding(8,8,2,0)}); phi.Width=175; bar.Controls.Add(phi); phi.Maximum=100000000; btnThem=new Button(); btnThem.Name="btnThem"; btnThem.Text="Chỉ định"; btnThem.AutoSize=true; bar.Controls.Add(btnThem); kq=new TextBox(); kq.Name="kq"; bar.Controls.Add(new Label(){Text="Kết quả",AutoSize=true,Margin=new Padding(8,8,2,0)}); kq.Width=175; bar.Controls.Add(kq); btnLuu=new Button(); btnLuu.Name="btnLuu"; btnLuu.Text="Lưu kết quả dòng chọn"; btnLuu.AutoSize=true; bar.Controls.Add(btnLuu); grid=new DataGridView(); grid.Name="grid"; grid.Dock=DockStyle.Fill; grid.ReadOnly=true; grid.AllowUserToAddRows=false; grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.DisplayedCells; grid.SelectionMode=DataGridViewSelectionMode.FullRowSelect; grid.MultiSelect=false; area.Controls.Add(grid); Controls.Add(layout); FormHelper.TrangTri(this, layout, bar, grid);
 }
}
