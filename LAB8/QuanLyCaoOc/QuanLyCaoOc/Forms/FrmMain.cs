namespace QuanLyCaoOc.Forms;

public partial class FrmMain:Form
{
    public FrmMain()
    {
        InitializeComponent();FormHelper.Style(this,"LAB8 — Quản lý cho thuê văn phòng cao ốc");
        btnVanPhong.Click+=(_,_)=>Open(0);btnHopDong.Click+=(_,_)=>Open(1);btnThanhToan.Click+=(_,_)=>Open(2);
        btnGiaHan.Click+=(_,_)=>Open(3);btnNhanVienTheXe.Click+=(_,_)=>Open(4);btnKiemTra.Click+=(_,_)=>Open(5);btnThoat.Click+=(_,_)=>Close();
    }
    public static Form CreateFunction(int index)=>index switch
    {0=>new FrmVanPhong(),1=>new FrmHopDong(),2=>new FrmThanhToan(),3=>new FrmGiaHan(),4=>new FrmNhanVienTheXe(),5=>new FrmKiemTra(),_=>throw new ArgumentOutOfRangeException(nameof(index))};
    void Open(int index)=>FormHelper.Run(()=>{using var form=CreateFunction(index);form.ShowDialog(this);});
}
