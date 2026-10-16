namespace QuanLyHoSoBenhAn.Forms;
partial class FrmMain {
 private System.ComponentModel.IContainer components=null;
 protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
 private Button btnHoSo; private Button btnHen; private Button btnKham; private Button btnXN; private Button btnToa; private Button btnThongKe; private Button btnDanhMuc; private DataGridView grid;
 private void InitializeComponent(){
 Text="QUẢN LÝ HỒ SƠ BỆNH ÁN"; Font=new Font("Segoe UI",10); AutoScaleMode=AutoScaleMode.Dpi; ClientSize=new Size(1180,740); MinimumSize=new Size(1000,680); StartPosition=FormStartPosition.CenterParent;
 var layout=new TableLayoutPanel{Dock=DockStyle.Fill,RowCount=3,ColumnCount=1};
 layout.RowStyles.Add(new RowStyle(SizeType.Absolute,55));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,225));layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));
 layout.Controls.Add(new Label{Text="QUẢN LÝ HỒ SƠ BỆNH ÁN",Dock=DockStyle.Fill,Font=new Font("Segoe UI",20,FontStyle.Bold),ForeColor=Color.FromArgb(0,51,102),TextAlign=ContentAlignment.MiddleCenter},0,0);
 var bar=new FlowLayoutPanel{Dock=DockStyle.Fill,AutoScroll=true,Padding=new Padding(12)};layout.Controls.Add(bar,0,1);
 var area=new Panel{Dock=DockStyle.Fill,Padding=new Padding(10)};layout.Controls.Add(area,0,2);
 btnHoSo=new Button(); btnHoSo.Name="btnHoSo"; btnHoSo.Text="Đăng ký và hồ sơ"; btnHoSo.AutoSize=true; bar.Controls.Add(btnHoSo); btnHen=new Button(); btnHen.Name="btnHen"; btnHen.Text="Lịch hẹn"; btnHen.AutoSize=true; bar.Controls.Add(btnHen); btnKham=new Button(); btnKham.Name="btnKham"; btnKham.Text="Khám bệnh"; btnKham.AutoSize=true; bar.Controls.Add(btnKham); btnXN=new Button(); btnXN.Name="btnXN"; btnXN.Text="Xét nghiệm"; btnXN.AutoSize=true; bar.Controls.Add(btnXN); btnToa=new Button(); btnToa.Name="btnToa"; btnToa.Text="Lập toa và thanh toán"; btnToa.AutoSize=true; bar.Controls.Add(btnToa); btnThongKe=new Button(); btnThongKe.Name="btnThongKe"; btnThongKe.Text="Thống kê"; btnThongKe.AutoSize=true; bar.Controls.Add(btnThongKe); btnDanhMuc=new Button{Name="btnDanhMuc",Text="Danh mục",AutoSize=true};bar.Controls.Add(btnDanhMuc); grid=new DataGridView(); grid.Name="grid"; grid.Dock=DockStyle.Fill; grid.ReadOnly=true; grid.AllowUserToAddRows=false; grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.DisplayedCells; grid.SelectionMode=DataGridViewSelectionMode.FullRowSelect; grid.MultiSelect=false; area.Controls.Add(grid); Controls.Add(layout); FormHelper.TrangTri(this, layout, bar, grid);
 }
}
