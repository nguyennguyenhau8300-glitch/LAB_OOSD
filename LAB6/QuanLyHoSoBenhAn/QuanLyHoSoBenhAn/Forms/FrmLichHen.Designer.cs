namespace QuanLyHoSoBenhAn.Forms;
partial class FrmLichHen {
 private System.ComponentModel.IContainer components=null;
 protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
 private ComboBox hs; private ComboBox bs; private DateTimePicker at; private Button btnDat; private Button btnHuy; private Button btnTai; private DataGridView grid;
 private void InitializeComponent(){
 Text="Đặt lịch khám sức khỏe"; Font=new Font("Segoe UI",10); AutoScaleMode=AutoScaleMode.Dpi; ClientSize=new Size(1180,740); MinimumSize=new Size(1000,680); StartPosition=FormStartPosition.CenterParent;
 var layout=new TableLayoutPanel{Dock=DockStyle.Fill,RowCount=3,ColumnCount=1};
 layout.RowStyles.Add(new RowStyle(SizeType.Absolute,55));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,225));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));
 layout.Controls.Add(new Label{Text="Đặt lịch khám sức khỏe",Dock=DockStyle.Fill,Font=new Font("Segoe UI",20,FontStyle.Bold),ForeColor=Color.FromArgb(0,51,102),TextAlign=ContentAlignment.MiddleCenter},0,0);
 var bar=new FlowLayoutPanel{Dock=DockStyle.Fill,AutoScroll=true,Padding=new Padding(12)};layout.Controls.Add(bar,0,1);
 var area=new Panel{Dock=DockStyle.Fill,Padding=new Padding(10)};layout.Controls.Add(area,0,2);
 hs=new ComboBox(); hs.Name="hs"; bar.Controls.Add(new Label(){Text="Hồ sơ",AutoSize=true,Margin=new Padding(8,8,2,0)}); hs.Width=175; bar.Controls.Add(hs); hs.DropDownStyle=ComboBoxStyle.DropDownList; bs=new ComboBox(); bs.Name="bs"; bar.Controls.Add(new Label(){Text="Bác sĩ",AutoSize=true,Margin=new Padding(8,8,2,0)}); bs.Width=175; bar.Controls.Add(bs); bs.DropDownStyle=ComboBoxStyle.DropDownList; at=new DateTimePicker(); at.Name="at"; bar.Controls.Add(new Label(){Text="Ngày giờ",AutoSize=true,Margin=new Padding(8,8,2,0)}); at.Width=175; bar.Controls.Add(at); at.Format=DateTimePickerFormat.Custom; at.CustomFormat="dd/MM/yyyy HH:mm"; btnDat=new Button(); btnDat.Name="btnDat"; btnDat.Text="Đặt hẹn"; btnDat.AutoSize=true; bar.Controls.Add(btnDat); btnHuy=new Button(); btnHuy.Name="btnHuy"; btnHuy.Text="Hủy lịch chọn"; btnHuy.AutoSize=true; bar.Controls.Add(btnHuy); btnTai=new Button(); btnTai.Name="btnTai"; btnTai.Text="Tải lại"; btnTai.AutoSize=true; bar.Controls.Add(btnTai); grid=new DataGridView(); grid.Name="grid"; grid.Dock=DockStyle.Fill; grid.ReadOnly=true; grid.AllowUserToAddRows=false; grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.DisplayedCells; grid.SelectionMode=DataGridViewSelectionMode.FullRowSelect; grid.MultiSelect=false; area.Controls.Add(grid); Controls.Add(layout); FormHelper.TrangTri(this, layout, bar, grid);
 }
}
