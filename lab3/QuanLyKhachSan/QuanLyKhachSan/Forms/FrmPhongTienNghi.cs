using Microsoft.Data.SqlClient;
using QuanLyKhachSan.Data;
using System.Data;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmPhongTienNghi : Form
    {
        public FrmPhongTienNghi()
        {
            InitializeComponent();
        }

        private void FrmPhongTienNghi_Load(
            object sender,
            EventArgs e)
        {
            LoadKhuVuc();
            LoadLoaiTienNghi();

            LoadPhong();
            LoadTienNghi();

            LoadComboPhong();
            LoadComboTienNghi();

            LoadLapDat();

            chkChuaThaoDo_CheckedChanged(
                sender,
                e);
        }

        // =========================================================
        // PHÒNG
        // =========================================================

        private void LoadKhuVuc()
        {
            DataTable dt = Db.Query(@"
                SELECT MaKhuVuc, TenKhuVuc
                FROM KhuVuc
                ORDER BY MaKhuVuc");

            cboKhuVuc.DataSource = dt;
            cboKhuVuc.DisplayMember = "TenKhuVuc";
            cboKhuVuc.ValueMember = "MaKhuVuc";

            cboKhuVuc.SelectedIndex = -1;
        }

        private void LoadPhong()
        {
            dgvPhong.DataSource = Db.Query(@"
                SELECT
                    P.SoPhong AS [Số phòng],
                    P.MaKhuVuc AS [Mã khu vực],
                    KV.TenKhuVuc AS [Khu vực],
                    P.LoaiPhong AS [Loại phòng],
                    P.SoNguoiToiDa AS [Số người tối đa],
                    P.DonGiaNgay AS [Đơn giá/ngày],
                    P.TrangThai AS [Trạng thái]

                FROM Phong P

                INNER JOIN KhuVuc KV
                    ON P.MaKhuVuc = KV.MaKhuVuc

                ORDER BY P.SoPhong");
        }

        private void btnThemPhong_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập số phòng.");

                return;
            }

            if (cboKhuVuc.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn khu vực.");

                return;
            }

            if (string.IsNullOrWhiteSpace(txtLoaiPhong.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập loại phòng.");

                return;
            }

            try
            {
                Db.Execute(@"
                    INSERT INTO Phong
                    (
                        SoPhong,
                        MaKhuVuc,
                        LoaiPhong,
                        SoNguoiToiDa,
                        DonGiaNgay,
                        TrangThai
                    )

                    VALUES
                    (
                        @SoPhong,
                        @KhuVuc,
                        @Loai,
                        @SoNguoi,
                        @Gia,
                        @TrangThai
                    )",

                    new SqlParameter(
                        "@SoPhong",
                        txtSoPhong.Text.Trim()),

                    new SqlParameter(
                        "@KhuVuc",
                        cboKhuVuc.SelectedValue),

                    new SqlParameter(
                        "@Loai",
                        txtLoaiPhong.Text.Trim()),

                    new SqlParameter(
                        "@SoNguoi",
                        nudSoNguoi.Value),

                    new SqlParameter(
                        "@Gia",
                        nudDonGiaPhong.Value),

                    new SqlParameter(
                        "@TrangThai",
                        cboTrangThaiPhong.Text)
                );

                MessageBox.Show(
                    "Thêm phòng thành công.");

                LoadPhong();
                LoadComboPhong();

                ClearPhong();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnSuaPhong_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text))
            {
                MessageBox.Show(
                    "Hãy chọn phòng cần sửa.");

                return;
            }

            try
            {
                int result = Db.Execute(@"
                    UPDATE Phong

                    SET
                        MaKhuVuc = @KhuVuc,
                        LoaiPhong = @Loai,
                        SoNguoiToiDa = @SoNguoi,
                        DonGiaNgay = @Gia,
                        TrangThai = @TrangThai

                    WHERE SoPhong = @SoPhong",

                    new SqlParameter(
                        "@KhuVuc",
                        cboKhuVuc.SelectedValue),

                    new SqlParameter(
                        "@Loai",
                        txtLoaiPhong.Text.Trim()),

                    new SqlParameter(
                        "@SoNguoi",
                        nudSoNguoi.Value),

                    new SqlParameter(
                        "@Gia",
                        nudDonGiaPhong.Value),

                    new SqlParameter(
                        "@TrangThai",
                        cboTrangThaiPhong.Text),

                    new SqlParameter(
                        "@SoPhong",
                        txtSoPhong.Text.Trim())
                );

                if (result == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy phòng.");

                    return;
                }

                MessageBox.Show(
                    "Cập nhật phòng thành công.");

                LoadPhong();
                LoadComboPhong();

                ClearPhong();
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
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text))
                return;

            if (MessageBox.Show(
                    $"Xóa phòng {txtSoPhong.Text}?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Db.Execute(@"
                    DELETE FROM Phong
                    WHERE SoPhong = @SoPhong",

                    new SqlParameter(
                        "@SoPhong",
                        txtSoPhong.Text.Trim()));

                MessageBox.Show(
                    "Xóa phòng thành công.");

                LoadPhong();
                LoadComboPhong();

                ClearPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa phòng.\n\n" +
                    "Phòng có thể đã có đặt phòng hoặc tiện nghi.\n\n" +
                    ex.Message,
                    "Lỗi");
            }
        }

        private void dgvPhong_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvPhong.Rows[e.RowIndex];

            txtSoPhong.Text =
                row.Cells["Số phòng"].Value?.ToString();

            cboKhuVuc.SelectedValue =
                row.Cells["Mã khu vực"].Value?.ToString();

            txtLoaiPhong.Text =
                row.Cells["Loại phòng"].Value?.ToString();

            nudSoNguoi.Value =
                Convert.ToDecimal(
                    row.Cells["Số người tối đa"].Value);

            nudDonGiaPhong.Value =
                Convert.ToDecimal(
                    row.Cells["Đơn giá/ngày"].Value);

            cboTrangThaiPhong.Text =
                row.Cells["Trạng thái"].Value?.ToString();
        }

        private void btnMoiPhong_Click(
            object sender,
            EventArgs e)
        {
            ClearPhong();
        }

        private void ClearPhong()
        {
            txtSoPhong.Clear();

            cboKhuVuc.SelectedIndex = -1;

            txtLoaiPhong.Clear();

            nudSoNguoi.Value = 1;
            nudDonGiaPhong.Value = 0;

            cboTrangThaiPhong.SelectedIndex = 0;

            txtSoPhong.Focus();
        }

        // =========================================================
        // LOẠI TIỆN NGHI
        // =========================================================

        private void LoadLoaiTienNghi()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    MaLoaiTN,
                    TenLoaiTN

                FROM LoaiTienNghi

                ORDER BY MaLoaiTN");

            cboLoaiTienNghi.DataSource = dt;

            cboLoaiTienNghi.DisplayMember =
                "TenLoaiTN";

            cboLoaiTienNghi.ValueMember =
                "MaLoaiTN";

            cboLoaiTienNghi.SelectedIndex = -1;
        }

        // =========================================================
        // TIỆN NGHI
        // =========================================================

        private void LoadTienNghi()
        {
            dgvTienNghi.DataSource =
                Db.Query(@"
                    SELECT
                        TN.MaTienNghi AS [Mã tiện nghi],
                        TN.MaLoaiTN AS [Mã loại],
                        L.TenLoaiTN AS [Loại tiện nghi],
                        TN.TenTienNghi AS [Tên tiện nghi],
                        TN.TinhTrang AS [Tình trạng]

                    FROM TienNghi TN

                    INNER JOIN LoaiTienNghi L
                        ON TN.MaLoaiTN = L.MaLoaiTN

                    ORDER BY TN.MaTienNghi");
        }

        private void btnThemTN_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtMaTienNghi.Text))
            {
                MessageBox.Show(
                    "Nhập mã tiện nghi.");

                return;
            }

            if (cboLoaiTienNghi.SelectedValue == null)
            {
                MessageBox.Show(
                    "Chọn loại tiện nghi.");

                return;
            }

            try
            {
                Db.Execute(@"
                    INSERT INTO TienNghi
                    (
                        MaTienNghi,
                        MaLoaiTN,
                        TenTienNghi,
                        TinhTrang
                    )

                    VALUES
                    (
                        @Ma,
                        @Loai,
                        @Ten,
                        @TinhTrang
                    )",

                    new SqlParameter(
                        "@Ma",
                        txtMaTienNghi.Text.Trim()),

                    new SqlParameter(
                        "@Loai",
                        cboLoaiTienNghi.SelectedValue),

                    new SqlParameter(
                        "@Ten",
                        txtTenTienNghi.Text.Trim()),

                    new SqlParameter(
                        "@TinhTrang",
                        cboTinhTrangTN.Text)
                );

                MessageBox.Show(
                    "Thêm tiện nghi thành công.");

                LoadTienNghi();
                LoadComboTienNghi();

                ClearTienNghi();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnSuaTN_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                Db.Execute(@"
                    UPDATE TienNghi

                    SET
                        MaLoaiTN = @Loai,
                        TenTienNghi = @Ten,
                        TinhTrang = @TinhTrang

                    WHERE MaTienNghi = @Ma",

                    new SqlParameter(
                        "@Loai",
                        cboLoaiTienNghi.SelectedValue),

                    new SqlParameter(
                        "@Ten",
                        txtTenTienNghi.Text.Trim()),

                    new SqlParameter(
                        "@TinhTrang",
                        cboTinhTrangTN.Text),

                    new SqlParameter(
                        "@Ma",
                        txtMaTienNghi.Text.Trim())
                );

                MessageBox.Show(
                    "Cập nhật tiện nghi thành công.");

                LoadTienNghi();
                LoadComboTienNghi();

                ClearTienNghi();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnXoaTN_Click(
            object sender,
            EventArgs e)
        {
            if (MessageBox.Show(
                    "Xóa tiện nghi này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Db.Execute(@"
                    DELETE FROM TienNghi
                    WHERE MaTienNghi = @Ma",

                    new SqlParameter(
                        "@Ma",
                        txtMaTienNghi.Text.Trim()));

                MessageBox.Show(
                    "Xóa tiện nghi thành công.");

                LoadTienNghi();
                LoadComboTienNghi();

                ClearTienNghi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa tiện nghi vì đã có dữ liệu liên quan.\n\n"
                    + ex.Message);
            }
        }

        private void dgvTienNghi_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvTienNghi.Rows[e.RowIndex];

            txtMaTienNghi.Text =
                row.Cells["Mã tiện nghi"].Value?.ToString();

            cboLoaiTienNghi.SelectedValue =
                row.Cells["Mã loại"].Value?.ToString();

            txtTenTienNghi.Text =
                row.Cells["Tên tiện nghi"].Value?.ToString();

            cboTinhTrangTN.Text =
                row.Cells["Tình trạng"].Value?.ToString();
        }

        private void btnMoiTN_Click(
            object sender,
            EventArgs e)
        {
            ClearTienNghi();
        }

        private void ClearTienNghi()
        {
            txtMaTienNghi.Clear();

            cboLoaiTienNghi.SelectedIndex = -1;

            txtTenTienNghi.Clear();

            cboTinhTrangTN.SelectedIndex = 0;

            txtMaTienNghi.Focus();
        }

        // =========================================================
        // COMBO LẮP ĐẶT
        // =========================================================

        private void LoadComboPhong()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    SoPhong,
                    SoPhong + N' - ' + LoaiPhong AS HienThi

                FROM Phong

                ORDER BY SoPhong");

            cboPhongLapDat.DataSource = dt;

            cboPhongLapDat.DisplayMember =
                "HienThi";

            cboPhongLapDat.ValueMember =
                "SoPhong";

            cboPhongLapDat.SelectedIndex = -1;
        }

        private void LoadComboTienNghi()
        {
            DataTable dt = Db.Query(@"
                SELECT
                    MaTienNghi,
                    MaTienNghi + N' - ' + TenTienNghi AS HienThi

                FROM TienNghi

                ORDER BY MaTienNghi");

            cboTienNghiLapDat.DataSource = dt;

            cboTienNghiLapDat.DisplayMember =
                "HienThi";

            cboTienNghiLapDat.ValueMember =
                "MaTienNghi";

            cboTienNghiLapDat.SelectedIndex = -1;
        }

        // =========================================================
        // LẮP ĐẶT
        // =========================================================

        private void LoadLapDat()
        {
            dgvLapDat.DataSource =
                Db.Query(@"
                    SELECT
                        LD.MaLapDat AS [Mã lắp đặt],
                        LD.MaTienNghi AS [Mã tiện nghi],
                        TN.TenTienNghi AS [Tên tiện nghi],
                        LD.SoPhong AS [Số phòng],
                        LD.NgayLap AS [Ngày lắp],
                        LD.NgayThaoDo AS [Ngày tháo]

                    FROM LapDatTienNghi LD

                    INNER JOIN TienNghi TN
                        ON LD.MaTienNghi = TN.MaTienNghi

                    ORDER BY LD.MaLapDat DESC");
        }

        private void btnLapDat_Click(
            object sender,
            EventArgs e)
        {
            if (cboTienNghiLapDat.SelectedValue == null ||
                cboPhongLapDat.SelectedValue == null)
            {
                MessageBox.Show(
                    "Chọn tiện nghi và phòng.");

                return;
            }

            try
            {
                // Không cho thiết bị đang lắp ở phòng khác
                int dangSuDung = Convert.ToInt32(
                    Db.Scalar(@"
                        SELECT COUNT(*)

                        FROM LapDatTienNghi

                        WHERE MaTienNghi = @MaTN
                          AND NgayThaoDo IS NULL",

                        new SqlParameter(
                            "@MaTN",
                            cboTienNghiLapDat.SelectedValue))
                );

                if (dangSuDung > 0)
                {
                    MessageBox.Show(
                        "Tiện nghi này đang được lắp tại một phòng khác.");

                    return;
                }

                Db.Execute(@"
                    INSERT INTO LapDatTienNghi
                    (
                        MaTienNghi,
                        SoPhong,
                        NgayLap,
                        NgayThaoDo
                    )

                    VALUES
                    (
                        @MaTN,
                        @Phong,
                        @NgayLap,
                        NULL
                    )",

                    new SqlParameter(
                        "@MaTN",
                        cboTienNghiLapDat.SelectedValue),

                    new SqlParameter(
                        "@Phong",
                        cboPhongLapDat.SelectedValue),

                    new SqlParameter(
                        "@NgayLap",
                        dtpNgayLap.Value.Date)
                );

                MessageBox.Show(
                    "Lắp đặt tiện nghi thành công.");

                LoadLapDat();
                ClearLapDat();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // =========================================================
        // THÁO DỠ
        // =========================================================

        private void btnThaoDo_Click(
            object sender,
            EventArgs e)
        {
            if (dgvLapDat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Chọn thiết bị cần tháo.");

                return;
            }

            int maLapDat =
                Convert.ToInt32(
                    dgvLapDat.CurrentRow
                        .Cells["Mã lắp đặt"].Value);

            DateTime ngayLap =
                Convert.ToDateTime(
                    dgvLapDat.CurrentRow
                        .Cells["Ngày lắp"].Value);

            if (dtpNgayThaoDo.Value.Date < ngayLap.Date)
            {
                MessageBox.Show(
                    "Ngày tháo không được nhỏ hơn ngày lắp.");

                return;
            }

            try
            {
                Db.Execute(@"
                    UPDATE LapDatTienNghi

                    SET NgayThaoDo = @NgayThao

                    WHERE MaLapDat = @Ma",

                    new SqlParameter(
                        "@NgayThao",
                        dtpNgayThaoDo.Value.Date),

                    new SqlParameter(
                        "@Ma",
                        maLapDat)
                );

                MessageBox.Show(
                    "Tháo dỡ tiện nghi thành công.");

                LoadLapDat();
                ClearLapDat();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void dgvLapDat_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvLapDat.Rows[e.RowIndex];

            cboTienNghiLapDat.SelectedValue =
                row.Cells["Mã tiện nghi"].Value?.ToString();

            cboPhongLapDat.SelectedValue =
                row.Cells["Số phòng"].Value?.ToString();

            dtpNgayLap.Value =
                Convert.ToDateTime(
                    row.Cells["Ngày lắp"].Value);

            object? ngayThao =
                row.Cells["Ngày tháo"].Value;

            if (ngayThao == null ||
                ngayThao == DBNull.Value)
            {
                chkChuaThaoDo.Checked = true;
            }
            else
            {
                chkChuaThaoDo.Checked = false;

                dtpNgayThaoDo.Value =
                    Convert.ToDateTime(ngayThao);
            }
        }

        private void chkChuaThaoDo_CheckedChanged(
            object sender,
            EventArgs e)
        {
            dtpNgayThaoDo.Enabled =
                !chkChuaThaoDo.Checked;
        }

        private void btnMoiLapDat_Click(
            object sender,
            EventArgs e)
        {
            ClearLapDat();
        }

        private void ClearLapDat()
        {
            cboTienNghiLapDat.SelectedIndex = -1;
            cboPhongLapDat.SelectedIndex = -1;

            dtpNgayLap.Value = DateTime.Today;
            dtpNgayThaoDo.Value = DateTime.Today;

            chkChuaThaoDo.Checked = true;
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