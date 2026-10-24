namespace QuanLyNhanSu.Forms;
partial class FrmMain
{
    private Button btnHoSo=null!,btnTuyenDung=null!,btnCheDo=null!,btnChamCong=null!,btnThuongPhat=null!,btnBangLuong=null!,btnThoat=null!;
    private void InitializeComponent()
    {
        Text="Quản lý nhân sự – Công ty sơn Đông Á";Font=new Font("Segoe UI",10);
        ClientSize=new Size(1060,650);MinimumSize=new Size(1050,650);
        StartPosition=FormStartPosition.CenterScreen;BackColor=Color.White;ForeColor=Color.Black;
        var layout=new TableLayoutPanel{Width=980,Height=420,ColumnCount=3,RowCount=4};
        for(int i=0;i<3;i++)layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100f/3));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,110));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute,110));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,90));
        var heading=new Label{Text="HỆ THỐNG QUẢN LÝ NHÂN SỰ",Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleCenter,Font=new Font("Segoe UI",21,FontStyle.Bold)};
        layout.Controls.Add(heading,0,0);layout.SetColumnSpan(heading,3);
        Button Make(string name,string text)=>new Button{Name=name,Text=text,Dock=DockStyle.Fill,Margin=new Padding(12),Font=new Font("Segoe UI",12),UseVisualStyleBackColor=false,BackColor=Color.WhiteSmoke,ForeColor=Color.Black,FlatStyle=FlatStyle.Flat};
        btnHoSo=Make("btnHoSo",Groups[0].Title);btnTuyenDung=Make("btnTuyenDung",Groups[1].Title);
        btnCheDo=Make("btnCheDo",Groups[2].Title);btnChamCong=Make("btnChamCong",Groups[3].Title);
        btnThuongPhat=Make("btnThuongPhat",Groups[4].Title);btnBangLuong=Make("btnBangLuong",Groups[5].Title);
        Button[] buttons=[btnHoSo,btnTuyenDung,btnCheDo,btnChamCong,btnThuongPhat,btnBangLuong];
        for(int i=0;i<buttons.Length;i++){buttons[i].FlatAppearance.BorderColor=Color.Black;layout.Controls.Add(buttons[i],i%3,1+i/3);}
        btnThoat=Make("btnThoat","Thoát");btnThoat.Margin=new Padding(45,18,45,15);layout.Controls.Add(btnThoat,1,3);
        Controls.Add(layout);void CenterMenu()=>layout.Location=new Point(Math.Max(0,(ClientSize.Width-layout.Width)/2),Math.Max(0,(ClientSize.Height-layout.Height)/2));
        Resize+=(_,_)=>CenterMenu();CenterMenu();
    }
}
