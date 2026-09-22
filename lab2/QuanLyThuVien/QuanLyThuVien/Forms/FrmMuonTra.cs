using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms
{
    public partial class FrmMuonTra : Form
    {
        public FrmMuonTra()
        {
            InitializeComponent();
        }

        private void FrmMuonTra_Load(object sender, EventArgs e)
        {
            LoadDocGia();
            LoadNhanVien();
            LoadSach();
            LoadPhieuMuon();
        }

        // ==============================
        // LOAD ĐỘC GIẢ
        // ==============================
        private void LoadDocGia()
        {
            try
            {
                DataTable dt = Db.Query(
                    @"SELECT MaDocGia,
                             Ho + N' ' + Ten AS HoTen
                      FROM DocGia
                      ORDER BY Ho, Ten");

                cboDocGia.DataSource = dt;
                cboDocGia.DisplayMember = "HoTen";
                cboDocGia.ValueMember = "MaDocGia";
                cboDocGia.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải độc giả: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==============================
        // LOAD NHÂN VIÊN
        // ==============================
        private void LoadNhanVien()
        {
            try
            {
                DataTable dt = Db.Query(
                    @"SELECT MaNhanVien,
                             Ho + N' ' + Ten AS HoTen
                      FROM NhanVien
                      ORDER BY Ho, Ten");

                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "MaNhanVien";
                cboNhanVien.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải nhân viên: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==============================
        // LOAD SÁCH
        // ==============================
        private void LoadSach()
        {
            try
            {
                dgvSach.DataSource = Db.Query(
                    @"SELECT
                        MaDauSach,
                        TenSach,
                        NamXuatBan,
                        SoLuongHienCo,
                        MaTheLoai,
                        MaNhaXuatBan
                      FROM DauSach
                      WHERE SoLuongHienCo > 0
                      ORDER BY TenSach");

                dgvSach.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvSach.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvSach.MultiSelect = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải sách: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==============================
        // LOAD PHIẾU MƯỢN
        // ==============================
        private void LoadPhieuMuon()
        {
            try
            {
                dgvPhieuMuon.DataSource = Db.Query(
                    @"SELECT
                        pm.MaPhieuMuon,
                        pm.MaDocGia,
                        dg.Ho + N' ' + dg.Ten AS HoTenDocGia,
                        pm.MaNhanVien,
                        nv.Ho + N' ' + nv.Ten AS HoTenNhanVien,
                        pm.NgayMuon,
                        pm.NgayHenTra,
                        COUNT(ct.MaChiTiet) AS SoLuongSach
                      FROM PhieuMuon pm
                      INNER JOIN DocGia dg
                          ON pm.MaDocGia = dg.MaDocGia
                      INNER JOIN NhanVien nv
                          ON pm.MaNhanVien = nv.MaNhanVien
                      LEFT JOIN ChiTietPhieuMuon ct
                          ON pm.MaPhieuMuon = ct.MaPhieuMuon
                      GROUP BY
                        pm.MaPhieuMuon,
                        pm.MaDocGia,
                        dg.Ho,
                        dg.Ten,
                        pm.MaNhanVien,
                        nv.Ho,
                        nv.Ten,
                        pm.NgayMuon,
                        pm.NgayHenTra
                      ORDER BY pm.NgayMuon DESC");

                dgvPhieuMuon.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvPhieuMuon.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải phiếu mượn: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==============================
        // LẬP PHIẾU MƯỢN
        // ==============================
        private void btnLapPhieuMuon_Click(object sender, EventArgs e)
        {
            if (cboDocGia.SelectedValue == null ||
                cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn độc giả và nhân viên.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (dgvSach.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn ít nhất một cuốn sách.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string maDocGia = cboDocGia.SelectedValue.ToString();
            string maNhanVien = cboNhanVien.SelectedValue.ToString();

            DateTime ngayMuon = dtpNgayMuon.Value.Date;
            DateTime ngayHenTra = dtpNgayHenTra.Value.Date;

            if (ngayHenTra < ngayMuon)
            {
                MessageBox.Show(
                    "Ngày hẹn trả không được nhỏ hơn ngày mượn.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tran = cn.BeginTransaction())
            {
                try
                {
                    string maPhieuMuon =
                        "PM" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                    string sqlPhieu =
                        @"INSERT INTO PhieuMuon
                          (
                              MaPhieuMuon,
                              MaDocGia,
                              MaNhanVien,
                              NgayMuon,
                              NgayHenTra
                          )
                          VALUES
                          (
                              @MaPhieuMuon,
                              @MaDocGia,
                              @MaNhanVien,
                              @NgayMuon,
                              @NgayHenTra
                          )";

                    using (SqlCommand cmd = new SqlCommand(
                        sqlPhieu, cn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaPhieuMuon", maPhieuMuon);

                        cmd.Parameters.AddWithValue(
                            "@MaDocGia", maDocGia);

                        cmd.Parameters.AddWithValue(
                            "@MaNhanVien", maNhanVien);

                        cmd.Parameters.AddWithValue(
                            "@NgayMuon", ngayMuon);

                        cmd.Parameters.AddWithValue(
                            "@NgayHenTra", ngayHenTra);

                        cmd.ExecuteNonQuery();
                    }

                    foreach (DataGridViewRow row in dgvSach.SelectedRows)
                    {
                        if (row.Cells["MaDauSach"].Value == null)
                            continue;

                        string maDauSach =
                            row.Cells["MaDauSach"].Value.ToString();

                        string maChiTiet =
                            "CT" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
                            + new Random().Next(100, 999);

                        string sqlChiTiet =
                            @"INSERT INTO ChiTietPhieuMuon
                              (
                                  MaChiTiet,
                                  MaPhieuMuon,
                                  MaDauSach
                              )
                              VALUES
                              (
                                  @MaChiTiet,
                                  @MaPhieuMuon,
                                  @MaDauSach
                              )";

                        using (SqlCommand cmd = new SqlCommand(
                            sqlChiTiet, cn, tran))
                        {
                            cmd.Parameters.AddWithValue(
                                "@MaChiTiet", maChiTiet);

                            cmd.Parameters.AddWithValue(
                                "@MaPhieuMuon", maPhieuMuon);

                            cmd.Parameters.AddWithValue(
                                "@MaDauSach", maDauSach);

                            cmd.ExecuteNonQuery();
                        }

                        string sqlGiamSach =
                            @"UPDATE DauSach
                              SET SoLuongHienCo =
                                  SoLuongHienCo - 1
                              WHERE MaDauSach = @MaDauSach
                                AND SoLuongHienCo > 0";

                        using (SqlCommand cmd = new SqlCommand(
                            sqlGiamSach, cn, tran))
                        {
                            cmd.Parameters.AddWithValue(
                                "@MaDauSach", maDauSach);

                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                throw new Exception(
                                    "Sách " + maDauSach +
                                    " không còn trong kho.");
                            }
                        }
                    }

                    tran.Commit();

                    MessageBox.Show(
                        "Lập phiếu mượn thành công.\nMã phiếu: "
                        + maPhieuMuon,
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadSach();
                    LoadPhieuMuon();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch
                    {
                    }

                    MessageBox.Show(
                        "Không thể lập phiếu mượn:\n" + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // ==============================
        // TRẢ SÁCH
        // ==============================
        private void btnTraSach_Click(object sender, EventArgs e)
        {
            if (dgvPhieuMuon.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một phiếu mượn.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string maPhieuMuon =
                dgvPhieuMuon.SelectedRows[0]
                .Cells["MaPhieuMuon"]
                .Value
                .ToString();

            try
            {
                DataTable dt = Db.Query(
                    @"SELECT MaChiTiet, MaDauSach
                      FROM ChiTietPhieuMuon
                      WHERE MaPhieuMuon = @MaPhieuMuon
                        AND NgayTraThucTe IS NULL",
                    new SqlParameter(
                        "@MaPhieuMuon",
                        maPhieuMuon));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Phiếu này đã được trả sách.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string maChiTiet =
                        row["MaChiTiet"].ToString();

                    string maDauSach =
                        row["MaDauSach"].ToString();

                    Db.Execute(
                        @"UPDATE ChiTietPhieuMuon
                          SET NgayTraThucTe = @NgayTra,
                              TinhTrangTra = N'Đã trả'
                          WHERE MaChiTiet = @MaChiTiet",
                        new SqlParameter(
                            "@NgayTra",
                            DateTime.Now.Date),
                        new SqlParameter(
                            "@MaChiTiet",
                            maChiTiet));

                    Db.Execute(
                        @"UPDATE DauSach
                          SET SoLuongHienCo =
                              SoLuongHienCo + 1
                          WHERE MaDauSach = @MaDauSach",
                        new SqlParameter(
                            "@MaDauSach",
                            maDauSach));
                }

                MessageBox.Show(
                    "Trả sách thành công.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadSach();
                LoadPhieuMuon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi trả sách:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ==============================
        // ĐÓNG FORM
        // ==============================
        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}