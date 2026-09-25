using Microsoft.Data.SqlClient;
using QuanLyKhachSan.Data;
using System.Data;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDatNhanPhong : Form
    {
        public FrmDatNhanPhong()
        {
            InitializeComponent();
        }

        private void FrmDatNhanPhong_Load(
            object sender,
            EventArgs e)
        {
            LoadKhachHang();
            LoadNhanVien();
            LoadPhong();

            LoadPhieuDat();
            LoadComboPhieu();

            dtpNgayNhan.Value =
                DateTime.Today;

            dtpNgayTra.Value =
                DateTime.Today.AddDays(1);

            dtpNgayNhanThucTe.Value =
                DateTime.Now;
        }

        // =========================================================
        // COMBO
        // =========================================================

        private void LoadKhachHang()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    MaKhach,
                    MaKhach + N' - ' + HoTen AS HienThi
                FROM KhachHang
                ORDER BY MaKhach");

            cboKhachHang.DataSource = dt;
            cboKhachHang.DisplayMember = "HienThi";
            cboKhachHang.ValueMember = "MaKhach";

            cboKhachHang.SelectedIndex = -1;
        }

        private void LoadNhanVien()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    MaNV,
                    MaNV + N' - ' + HoTen AS HienThi
                FROM NhanVien
                WHERE TrangThai = N'Đang làm'
                ORDER BY MaNV");

            cboNhanVien.DataSource = dt;
            cboNhanVien.DisplayMember = "HienThi";
            cboNhanVien.ValueMember = "MaNV";

            cboNhanVien.SelectedIndex = -1;
        }

        private void LoadPhong()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    SoPhong,
                    SoPhong + N' - ' +
                    LoaiPhong + N' - ' +
                    TrangThai AS HienThi
                FROM Phong
                WHERE TrangThai <> N'Bảo trì'
                ORDER BY SoPhong");

            cboPhong.DataSource = dt;
            cboPhong.DisplayMember = "HienThi";
            cboPhong.ValueMember = "SoPhong";

            cboPhong.SelectedIndex = -1;
        }

        // =========================================================
        // LOAD PHIẾU
        // =========================================================

        private void LoadPhieuDat()
        {
            dgvPhieuDat.DataSource =
                Db.Query(@"
                    SELECT
                        P.SoPhieuDat AS [Số phiếu],
                        P.MaKhach AS [Mã khách],
                        K.HoTen AS [Khách hàng],
                        P.MaNVLeTan AS [Mã NV],
                        NV.HoTen AS [Nhân viên],
                        P.NgayLap AS [Ngày lập],
                        P.NgayNhan AS [Ngày nhận],
                        P.NgayTraDuKien AS [Ngày trả],
                        P.TienCoc AS [Tiền cọc],
                        P.KenhDat AS [Kênh đặt],
                        P.TrangThai AS [Trạng thái]

                    FROM PhieuDatPhong P

                    INNER JOIN KhachHang K
                        ON P.MaKhach = K.MaKhach

                    INNER JOIN NhanVien NV
                        ON P.MaNVLeTan = NV.MaNV

                    ORDER BY P.NgayLap DESC");
        }

        private void LoadComboPhieu()
        {
            DataTable dt1 = Db.Query(@"
                SELECT
                    SoPhieuDat,
                    SoPhieuDat + N' - ' +
                    TrangThai AS HienThi

                FROM PhieuDatPhong

                WHERE TrangThai IN
                (
                    N'Đã đặt',
                    N'Đang ở'
                )

                ORDER BY SoPhieuDat");

            cboPhieuChiTiet.DataSource = dt1;
            cboPhieuChiTiet.DisplayMember = "HienThi";
            cboPhieuChiTiet.ValueMember = "SoPhieuDat";

            cboPhieuChiTiet.SelectedIndex = -1;

            DataTable dt2 = Db.Query(@"
                SELECT
                    P.SoPhieuDat,
                    P.SoPhieuDat + N' - ' +
                    K.HoTen AS HienThi

                FROM PhieuDatPhong P

                INNER JOIN KhachHang K
                    ON P.MaKhach = K.MaKhach

                WHERE P.TrangThai IN
                (
                    N'Đã đặt',
                    N'Đang ở'
                )

                ORDER BY P.SoPhieuDat");

            cboPhieuNhan.DataSource = dt2;
            cboPhieuNhan.DisplayMember = "HienThi";
            cboPhieuNhan.ValueMember = "SoPhieuDat";

            cboPhieuNhan.SelectedIndex = -1;
        }

        // =========================================================
        // THÊM PHIẾU
        // =========================================================

        private void btnThemPhieu_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtSoPhieuDat.Text))
            {
                MessageBox.Show(
                    "Nhập số phiếu đặt.");

                return;
            }

            if (cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show(
                    "Chọn khách hàng.");

                return;
            }

            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show(
                    "Chọn nhân viên lễ tân.");

                return;
            }

            if (dtpNgayTra.Value.Date <=
                dtpNgayNhan.Value.Date)
            {
                MessageBox.Show(
                    "Ngày trả phải lớn hơn ngày nhận.");

                return;
            }

            try
            {
                Db.Execute(@"
                    INSERT INTO PhieuDatPhong
                    (
                        SoPhieuDat,
                        MaKhach,
                        MaNVLeTan,
                        NgayNhan,
                        NgayTraDuKien,
                        TienCoc,
                        KenhDat,
                        TrangThai
                    )

                    VALUES
                    (
                        @SoPhieu,
                        @Khach,
                        @NhanVien,
                        @Nhan,
                        @Tra,
                        @Coc,
                        @Kenh,
                        N'Đã đặt'
                    )",

                    new SqlParameter(
                        "@SoPhieu",
                        txtSoPhieuDat.Text.Trim()),

                    new SqlParameter(
                        "@Khach",
                        cboKhachHang.SelectedValue),

                    new SqlParameter(
                        "@NhanVien",
                        cboNhanVien.SelectedValue),

                    new SqlParameter(
                        "@Nhan",
                        dtpNgayNhan.Value.Date),

                    new SqlParameter(
                        "@Tra",
                        dtpNgayTra.Value.Date),

                    new SqlParameter(
                        "@Coc",
                        nudTienCoc.Value),

                    new SqlParameter(
                        "@Kenh",
                        cboKenhDat.Text)
                );

                MessageBox.Show(
                    "Tạo phiếu đặt phòng thành công.");

                LoadPhieuDat();
                LoadComboPhieu();

                ClearPhieu();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =========================================================
        // SỬA PHIẾU
        // =========================================================

        private void btnSuaPhieu_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtSoPhieuDat.Text))
                return;

            if (dtpNgayTra.Value.Date <=
                dtpNgayNhan.Value.Date)
            {
                MessageBox.Show(
                    "Ngày trả phải lớn hơn ngày nhận.");

                return;
            }

            try
            {
                Db.Execute(@"
                    UPDATE PhieuDatPhong

                    SET
                        MaKhach = @Khach,
                        MaNVLeTan = @NV,
                        NgayNhan = @Nhan,
                        NgayTraDuKien = @Tra,
                        TienCoc = @Coc,
                        KenhDat = @Kenh

                    WHERE SoPhieuDat = @SoPhieu
                      AND TrangThai = N'Đã đặt'",

                    new SqlParameter(
                        "@Khach",
                        cboKhachHang.SelectedValue),

                    new SqlParameter(
                        "@NV",
                        cboNhanVien.SelectedValue),

                    new SqlParameter(
                        "@Nhan",
                        dtpNgayNhan.Value.Date),

                    new SqlParameter(
                        "@Tra",
                        dtpNgayTra.Value.Date),

                    new SqlParameter(
                        "@Coc",
                        nudTienCoc.Value),

                    new SqlParameter(
                        "@Kenh",
                        cboKenhDat.Text),

                    new SqlParameter(
                        "@SoPhieu",
                        txtSoPhieuDat.Text.Trim())
                );

                MessageBox.Show(
                    "Cập nhật phiếu thành công.");

                LoadPhieuDat();
                LoadComboPhieu();

                ClearPhieu();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =========================================================
        // HỦY PHIẾU
        // =========================================================

        private void btnHuyPhieu_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtSoPhieuDat.Text))
            {
                MessageBox.Show(
                    "Chọn phiếu cần hủy.");

                return;
            }

            if (MessageBox.Show(
                    $"Hủy phiếu {txtSoPhieuDat.Text}?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int result = Db.Execute(@"
                    UPDATE PhieuDatPhong

                    SET TrangThai = N'Hủy'

                    WHERE SoPhieuDat = @SoPhieu
                      AND TrangThai = N'Đã đặt'",

                    new SqlParameter(
                        "@SoPhieu",
                        txtSoPhieuDat.Text.Trim())
                );

                if (result == 0)
                {
                    MessageBox.Show(
                        "Chỉ có thể hủy phiếu đang ở trạng thái Đã đặt.");

                    return;
                }

                MessageBox.Show(
                    "Đã hủy phiếu đặt phòng.");

                LoadPhieuDat();
                LoadComboPhieu();

                ClearPhieu();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void dgvPhieuDat_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvPhieuDat.Rows[e.RowIndex];

            txtSoPhieuDat.Text =
                row.Cells["Số phiếu"].Value?.ToString();

            cboKhachHang.SelectedValue =
                row.Cells["Mã khách"].Value?.ToString();

            cboNhanVien.SelectedValue =
                row.Cells["Mã NV"].Value?.ToString();

            dtpNgayNhan.Value =
                Convert.ToDateTime(
                    row.Cells["Ngày nhận"].Value);

            dtpNgayTra.Value =
                Convert.ToDateTime(
                    row.Cells["Ngày trả"].Value);

            nudTienCoc.Value =
                Convert.ToDecimal(
                    row.Cells["Tiền cọc"].Value);

            cboKenhDat.Text =
                row.Cells["Kênh đặt"].Value?.ToString();

            cboTrangThai.Text =
                row.Cells["Trạng thái"].Value?.ToString();
        }

        private void btnMoiPhieu_Click(
            object sender,
            EventArgs e)
        {
            ClearPhieu();
        }

        private void ClearPhieu()
        {
            txtSoPhieuDat.Clear();

            cboKhachHang.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;

            dtpNgayNhan.Value = DateTime.Today;
            dtpNgayTra.Value =
                DateTime.Today.AddDays(1);

            nudTienCoc.Value = 0;

            cboKenhDat.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;

            txtSoPhieuDat.Focus();
        }

        // =========================================================
        // CHI TIẾT PHÒNG
        // =========================================================

        private void cboPhieuChiTiet_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            LoadChiTietPhong();
        }

        private void LoadChiTietPhong()
        {
            if (cboPhieuChiTiet.SelectedValue == null ||
                cboPhieuChiTiet.SelectedValue is DataRowView)
            {
                dgvChiTiet.DataSource = null;
                return;
            }

            string soPhieu =
                cboPhieuChiTiet.SelectedValue.ToString()!;

            dgvChiTiet.DataSource =
                Db.Query(@"
                    SELECT
                        CT.SoPhieuDat AS [Số phiếu],
                        CT.SoPhong AS [Số phòng],
                        P.LoaiPhong AS [Loại phòng],
                        CT.SoNguoi AS [Số người],
                        P.SoNguoiToiDa AS [Sức chứa],
                        P.DonGiaNgay AS [Đơn giá]

                    FROM ChiTietDatPhong CT

                    INNER JOIN Phong P
                        ON CT.SoPhong = P.SoPhong

                    WHERE CT.SoPhieuDat = @SoPhieu

                    ORDER BY CT.SoPhong",

                    new SqlParameter(
                        "@SoPhieu",
                        soPhieu));
        }

        private void btnThemPhong_Click(
            object sender,
            EventArgs e)
        {
            if (cboPhieuChiTiet.SelectedValue == null ||
                cboPhong.SelectedValue == null)
            {
                MessageBox.Show(
                    "Chọn phiếu đặt và phòng.");

                return;
            }

            try
            {
                using SqlConnection conn =
                    Db.GetConnection();

                conn.Open();

                using SqlCommand cmd =
                    new SqlCommand(
                        "sp_ThemPhongVaoPhieuDat",
                        conn);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@SoPhieuDat",
                    cboPhieuChiTiet.SelectedValue);

                cmd.Parameters.AddWithValue(
                    "@SoPhong",
                    cboPhong.SelectedValue);

                cmd.Parameters.AddWithValue(
                    "@SoNguoi",
                    Convert.ToInt32(
                        nudSoNguoi.Value));

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "Thêm phòng vào phiếu thành công.");

                LoadChiTietPhong();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnXoaPhong_Click(
            object sender,
            EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null)
                return;

            string soPhieu =
                dgvChiTiet.CurrentRow
                    .Cells["Số phiếu"]
                    .Value.ToString()!;

            string soPhong =
                dgvChiTiet.CurrentRow
                    .Cells["Số phòng"]
                    .Value.ToString()!;

            if (MessageBox.Show(
                    $"Xóa phòng {soPhong} khỏi phiếu {soPhieu}?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Db.Execute(@"
                    DELETE FROM ChiTietDatPhong

                    WHERE SoPhieuDat = @SoPhieu
                      AND SoPhong = @Phong",

                    new SqlParameter(
                        "@SoPhieu",
                        soPhieu),

                    new SqlParameter(
                        "@Phong",
                        soPhong)
                );

                LoadChiTietPhong();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnMoiChiTiet_Click(
            object sender,
            EventArgs e)
        {
            cboPhong.SelectedIndex = -1;
            nudSoNguoi.Value = 1;
        }

        // =========================================================
        // NHẬN PHÒNG
        // =========================================================

        private void cboPhieuNhan_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            LoadThongTinNhanPhong();
        }

        private void LoadThongTinNhanPhong()
        {
            if (cboPhieuNhan.SelectedValue == null ||
                cboPhieuNhan.SelectedValue is DataRowView)
                return;

            string soPhieu =
                cboPhieuNhan.SelectedValue.ToString()!;

            DataTable dt = Db.Query(@"
                SELECT
                    P.SoPhieuDat,
                    K.HoTen,
                    P.NgayNhan,
                    P.NgayTraDuKien,
                    P.TrangThai

                FROM PhieuDatPhong P

                INNER JOIN KhachHang K
                    ON P.MaKhach = K.MaKhach

                WHERE P.SoPhieuDat = @SoPhieu",

                new SqlParameter(
                    "@SoPhieu",
                    soPhieu));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblThongTinNhan.Text =
                    $"Khách: {row["HoTen"]}   |   " +
                    $"Nhận: {Convert.ToDateTime(row["NgayNhan"]):dd/MM/yyyy}   |   " +
                    $"Trả: {Convert.ToDateTime(row["NgayTraDuKien"]):dd/MM/yyyy}   |   " +
                    $"Trạng thái: {row["TrangThai"]}";
            }

            LoadPhongNguoiLuuTru();
            LoadNguoiLuuTru();
        }

        private void LoadPhongNguoiLuuTru()
        {
            if (cboPhieuNhan.SelectedValue == null ||
                cboPhieuNhan.SelectedValue is DataRowView)
                return;

            DataTable dt = Db.Query(@"
                SELECT
                    SoPhong

                FROM ChiTietDatPhong

                WHERE SoPhieuDat = @SoPhieu

                ORDER BY SoPhong",

                new SqlParameter(
                    "@SoPhieu",
                    cboPhieuNhan.SelectedValue));

            cboPhongNguoiLT.DataSource = dt;
            cboPhongNguoiLT.DisplayMember = "SoPhong";
            cboPhongNguoiLT.ValueMember = "SoPhong";

            cboPhongNguoiLT.SelectedIndex = -1;
        }

        private void btnNhanPhong_Click(
            object sender,
            EventArgs e)
        {
            if (cboPhieuNhan.SelectedValue == null)
            {
                MessageBox.Show(
                    "Chọn phiếu cần nhận phòng.");

                return;
            }

            string soPhieu =
                cboPhieuNhan.SelectedValue.ToString()!;

            try
            {
                object? count = Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM ChiTietDatPhong
                    WHERE SoPhieuDat = @SoPhieu",

                    new SqlParameter(
                        "@SoPhieu",
                        soPhieu));

                if (Convert.ToInt32(count) == 0)
                {
                    MessageBox.Show(
                        "Phiếu chưa có phòng. Không thể nhận phòng.");

                    return;
                }

                using SqlConnection conn =
                    Db.GetConnection();

                conn.Open();

                using SqlTransaction tran =
                    conn.BeginTransaction();

                try
                {
                    using SqlCommand cmd1 =
                        new SqlCommand(@"
                            UPDATE PhieuDatPhong

                            SET
                                TrangThai = N'Đang ở',
                                NgayNhanThucTe = @Ngay

                            WHERE SoPhieuDat = @SoPhieu
                              AND TrangThai = N'Đã đặt'",
                            conn,
                            tran);

                    cmd1.Parameters.AddWithValue(
                        "@Ngay",
                        dtpNgayNhanThucTe.Value);

                    cmd1.Parameters.AddWithValue(
                        "@SoPhieu",
                        soPhieu);

                    int updated =
                        cmd1.ExecuteNonQuery();

                    if (updated == 0)
                    {
                        throw new Exception(
                            "Phiếu này không ở trạng thái Đã đặt.");
                    }

                    using SqlCommand cmd2 =
                        new SqlCommand(@"
                            UPDATE P

                            SET TrangThai = N'Đang ở'

                            FROM Phong P

                            INNER JOIN ChiTietDatPhong CT
                                ON P.SoPhong = CT.SoPhong

                            WHERE CT.SoPhieuDat = @SoPhieu",
                            conn,
                            tran);

                    cmd2.Parameters.AddWithValue(
                        "@SoPhieu",
                        soPhieu);

                    cmd2.ExecuteNonQuery();

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

                MessageBox.Show(
                    "Nhận phòng thành công.");

                LoadPhieuDat();
                LoadComboPhieu();
                LoadPhong();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =========================================================
        // NGƯỜI LƯU TRÚ
        // =========================================================

        private void btnThemNguoiLT_Click(
            object sender,
            EventArgs e)
        {
            if (cboPhieuNhan.SelectedValue == null ||
                cboPhongNguoiLT.SelectedValue == null)
            {
                MessageBox.Show(
                    "Chọn phiếu và phòng.");

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    txtHoTenNguoiLT.Text))
            {
                MessageBox.Show(
                    "Nhập họ tên người lưu trú.");

                return;
            }

            try
            {
                string soPhieu =
                    cboPhieuNhan.SelectedValue.ToString()!;

                string soPhong =
                    cboPhongNguoiLT.SelectedValue.ToString()!;

                int soNguoiDaCo =
                    Convert.ToInt32(
                        Db.Scalar(@"
                            SELECT COUNT(*)

                            FROM NguoiLuuTru

                            WHERE SoPhieuDat = @SoPhieu
                              AND SoPhong = @Phong",

                            new SqlParameter(
                                "@SoPhieu",
                                soPhieu),

                            new SqlParameter(
                                "@Phong",
                                soPhong)));

                int soNguoiDat =
                    Convert.ToInt32(
                        Db.Scalar(@"
                            SELECT SoNguoi

                            FROM ChiTietDatPhong

                            WHERE SoPhieuDat = @SoPhieu
                              AND SoPhong = @Phong",

                            new SqlParameter(
                                "@SoPhieu",
                                soPhieu),

                            new SqlParameter(
                                "@Phong",
                                soPhong)));

                if (soNguoiDaCo >= soNguoiDat)
                {
                    MessageBox.Show(
                        "Đã đủ số người đăng ký cho phòng này.");

                    return;
                }

                Db.Execute(@"
                    INSERT INTO NguoiLuuTru
                    (
                        SoPhieuDat,
                        SoPhong,
                        HoTen,
                        CCCD,
                        QuocTich
                    )

                    VALUES
                    (
                        @SoPhieu,
                        @Phong,
                        @HoTen,
                        @CCCD,
                        @QuocTich
                    )",

                    new SqlParameter(
                        "@SoPhieu",
                        soPhieu),

                    new SqlParameter(
                        "@Phong",
                        soPhong),

                    new SqlParameter(
                        "@HoTen",
                        txtHoTenNguoiLT.Text.Trim()),

                    new SqlParameter(
                        "@CCCD",
                        txtCCCDNguoiLT.Text.Trim()),

                    new SqlParameter(
                        "@QuocTich",
                        txtQuocTichNguoiLT.Text.Trim())
                );

                MessageBox.Show(
                    "Thêm người lưu trú thành công.");

                txtHoTenNguoiLT.Clear();
                txtCCCDNguoiLT.Clear();
                txtQuocTichNguoiLT.Text = "Việt Nam";

                LoadNguoiLuuTru();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void LoadNguoiLuuTru()
        {
            if (cboPhieuNhan.SelectedValue == null ||
                cboPhieuNhan.SelectedValue is DataRowView)
            {
                dgvNguoiLuuTru.DataSource = null;
                return;
            }

            dgvNguoiLuuTru.DataSource =
                Db.Query(@"
                    SELECT
                        MaNguoiLT AS [Mã],
                        SoPhong AS [Phòng],
                        HoTen AS [Họ tên],
                        CCCD,
                        QuocTich AS [Quốc tịch]

                    FROM NguoiLuuTru

                    WHERE SoPhieuDat = @SoPhieu

                    ORDER BY SoPhong, HoTen",

                    new SqlParameter(
                        "@SoPhieu",
                        cboPhieuNhan.SelectedValue));
        }

        // =========================================================
        // CHUNG
        // =========================================================

        private void btnDong_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(
                "Có lỗi xảy ra:\n\n" +
                ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}