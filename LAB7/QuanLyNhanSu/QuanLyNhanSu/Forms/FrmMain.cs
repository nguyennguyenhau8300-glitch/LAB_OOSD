namespace QuanLyNhanSu.Forms;

public partial class FrmMain : Form
{
    public static readonly (string Title,string[] Tables)[] Groups = [
        ("Hồ sơ nhân viên và danh mục",["NhanVien","PhongBan","ChucVu","CaLam"]),
        ("Tuyển dụng – thử việc – hợp đồng",["TuyenDung","HopDong"]),
        ("Quyết định lương – chức vụ – bảo hiểm",["QDLuong","QDChucVu","BaoHiem"]),
        ("Phân công tuần – chấm công – phụ trội",["PhanCong","ChamCong","PhieuPhuTroi","ChiTietPhuTroi"]),
        ("Khen thưởng – kỷ luật – thu chi",["ThuongPhat","ThuChi"]),
        ("Tính và chốt lương tháng",["BangLuong"])
    ];
    public FrmMain()
    {
        InitializeComponent();
        btnHoSo.Click+=(_,_)=>OpenFunction(0);
        btnTuyenDung.Click+=(_,_)=>OpenFunction(1);
        btnCheDo.Click+=(_,_)=>OpenFunction(2);
        btnChamCong.Click+=(_,_)=>OpenFunction(3);
        btnThuongPhat.Click+=(_,_)=>OpenFunction(4);
        btnBangLuong.Click+=(_,_)=>OpenFunction(5);
        btnThoat.Click+=(_,_)=>Close();
    }
    public static FrmChucNang CreateFunction(int index)=>index switch
    {
        0=>new FrmHoSoNhanVien(),1=>new FrmTuyenDung(),2=>new FrmCheDo(),
        3=>new FrmChamCong(),4=>new FrmThuongPhat(),5=>new FrmBangLuong(),
        _=>throw new ArgumentOutOfRangeException(nameof(index))
    };
    void OpenFunction(int index)
    {
        try { using var form=CreateFunction(index);form.ShowDialog(this); }
        catch(Exception error) { FormHelper.Error(error); }
    }
}
