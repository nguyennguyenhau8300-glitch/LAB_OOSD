using QuanLyHoSoBenhAn.Services;
namespace QuanLyHoSoBenhAn.Forms;

public partial class FrmDanhMuc : Form
{
    readonly DanhMucService service = new();
    int? currentId;
    bool loading;
    LoaiDanhMuc Loai => (LoaiDanhMuc)Math.Max(0, loai.SelectedIndex);
    public FrmDanhMuc()
    {
        InitializeComponent();
        loai.SelectedIndexChanged += (_,_) => FormHelper.Lam(DoiLoai);
        grid.SelectionChanged += (_,_) => { if (!loading) ChonDong(); };
        btnMoi.Click += (_,_) => Moi();
        btnTai.Click += (_,_) => FormHelper.Lam(Tai);
        btnTim.Click += (_,_) => FormHelper.Lam(Tai);
        btnLuu.Click += (_,_) => FormHelper.Lam(() => {
            var r=service.Luu(Loai,currentId,ten.Text,donVi.Text,Loai==LoaiDanhMuc.BacSi ? FormHelper.Gia(khoa) : null);
            FormHelper.Bao(r);if(r.ThanhCong) Tai();
        });
        btnXoa.Click += (_,_) => FormHelper.Lam(() => {
            if(!currentId.HasValue) { MessageBox.Show("Chọn danh mục cần xóa.");return; }
            if(MessageBox.Show($"Xóa danh mục mã {currentId}: {ten.Text}?", "Xác nhận xóa",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)return;
            var r=service.Xoa(Loai,currentId.Value);FormHelper.Bao(r);if(r.ThanhCong)Tai();
        });
        Load += (_,_) => { loai.SelectedIndex=0; };
    }
    void DoiLoai()
    {
        loading=true;
        try
        {
        tenLabel.Text=Loai==LoaiDanhMuc.BacSi ? "Họ tên bác sĩ" : Loai==LoaiDanhMuc.Thuoc ? "Tên thuốc" : "Tên khoa";
        ten.MaxLength=Loai==LoaiDanhMuc.Thuoc ? 150 : 100;
        khoaPanel.Visible=Loai==LoaiDanhMuc.BacSi;donViPanel.Visible=Loai==LoaiDanhMuc.Thuoc;
        if(Loai==LoaiDanhMuc.BacSi) FormHelper.Nap(khoa,service.DanhSach(LoaiDanhMuc.Khoa),"TenKhoa","MaKhoa");
        tuKhoa.Clear();
        } finally { loading=false; }
        Tai();
    }
    void Tai()
    {
        loading=true;try { grid.DataSource=service.DanhSach(Loai,tuKhoa.Text); } finally { loading=false; }
        if(grid.Rows.Count>0)ChonDong();else Moi();
    }
    void ChonDong()
    {
        if(grid.CurrentRow==null || !grid.CurrentRow.Selected)return;
        var r=grid.CurrentRow;currentId=Convert.ToInt32(r.Cells[0].Value);
        ten.Text=Convert.ToString(r.Cells[Loai==LoaiDanhMuc.BacSi ? "HoTen" : Loai==LoaiDanhMuc.Thuoc ? "TenThuoc" : "TenKhoa"].Value);
        if(Loai==LoaiDanhMuc.Thuoc)donVi.Text=Convert.ToString(r.Cells["DonVi"].Value);
        if(Loai==LoaiDanhMuc.BacSi)khoa.SelectedValue=Convert.ToInt32(r.Cells["MaKhoa"].Value);
        trangThai.Text=$"Đang sửa mã {currentId} • {grid.Rows.Count} bản ghi";
    }
    void Moi()
    {
        loading=true;currentId=null;ten.Clear();donVi.Clear();grid.ClearSelection();loading=false;
        trangThai.Text="Thêm mới • mã được cấp tự động";ten.Focus();
    }
}
