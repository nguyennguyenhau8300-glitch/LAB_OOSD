#nullable enable
namespace QuanLyCaoOc.Forms;

public partial class FrmMain
{
    private System.ComponentModel.IContainer? components;
    private Button btnVanPhong=null!,btnHopDong=null!,btnThanhToan=null!,btnGiaHan=null!,btnNhanVienTheXe=null!,btnKiemTra=null!,btnThoat=null!;
    protected override void Dispose(bool disposing){if(disposing)components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        components=new System.ComponentModel.Container();
        var title=new Label{Text="QUẢN LÝ CHO THUÊ VĂN PHÒNG CAO ỐC",Dock=DockStyle.Top,Height=110,TextAlign=ContentAlignment.MiddleCenter,Font=new Font("Segoe UI",22,FontStyle.Bold)};
        var subtitle=new Label{Text="Nguyễn Nguyên Hậu  •  1250080049  •  12_ĐH_CNPM1\nLAB8 — C# WinForms .NET 10 và SQL Server",Dock=DockStyle.Top,Height=70,TextAlign=ContentAlignment.MiddleCenter};
        var table=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=3,RowCount=2,Padding=new Padding(40),Name="pnlChucNang"};
        for(int i=0;i<3;i++)table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,33.333F));
        for(int i=0;i<2;i++)table.RowStyles.Add(new RowStyle(SizeType.Percent,50));
        btnVanPhong=CreateButton("Tầng và văn phòng","btnVanPhong");btnHopDong=CreateButton("Khách hàng và hợp đồng","btnHopDong");btnThanhToan=CreateButton("Thanh toán và dịch vụ","btnThanhToan");
        btnGiaHan=CreateButton("Gia hạn hợp đồng","btnGiaHan");btnNhanVienTheXe=CreateButton("Nhân viên và thẻ xe","btnNhanVienTheXe");btnKiemTra=CreateButton("Kiểm tra và xử phạt","btnKiemTra");
        table.Controls.Add(btnVanPhong,0,0);table.Controls.Add(btnHopDong,1,0);table.Controls.Add(btnThanhToan,2,0);table.Controls.Add(btnGiaHan,0,1);table.Controls.Add(btnNhanVienTheXe,1,1);table.Controls.Add(btnKiemTra,2,1);
        btnThoat=new Button{Text="Thoát",Name="btnThoat",Dock=DockStyle.Bottom,Height=50};Controls.Add(table);Controls.Add(subtitle);Controls.Add(title);Controls.Add(btnThoat);
    }
    static Button CreateButton(string text,string name)=>new(){Text=text,Name=name,Dock=DockStyle.Fill,Margin=new Padding(12),Font=new Font("Segoe UI",14,FontStyle.Bold),BackColor=Color.White};
}

