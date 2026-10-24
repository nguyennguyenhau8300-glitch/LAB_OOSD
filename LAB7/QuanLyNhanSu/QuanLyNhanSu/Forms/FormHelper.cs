using Microsoft.Data.SqlClient;
namespace QuanLyNhanSu.Forms;
public static class FormHelper
{
    public static void FormatGrid(DataGridView g)
    {
        var names=new Dictionary<string,string>{{"Id","Mã"},{"NhanVienId","Mã nhân viên"},{"HoTen","Họ tên"},{"Thang","Tháng"},{"Nam","Năm"},{"GioChuan","Giờ chuẩn"},{"TrangThai","Trạng thái"},{"NgayLap","Ngày lập"},{"GioCong","Giờ công"},{"GioPhuTroi","Giờ phụ trội"},{"LuongTheoCong","Lương theo công"},{"PhuCap","Phụ cấp"},{"TienPhuTroi","Tiền phụ trội"},{"BaoHiem","Bảo hiểm"},{"Thuong","Thưởng"},{"Phat","Phạt"},{"ThucLinh","Thực lĩnh"},{"QuyetDinhId","Mã quyết định"},{"Ngay","Ngày"},{"SoTien","Số tiền"},{"Loai","Loại"},{"BangLuongId","Mã bảng lương"},{"KetQua","Kết quả"},{"SoChungTu","Số chứng từ"},{"NoiDung","Nội dung"}};
        foreach(DataGridViewColumn col in g.Columns){if(names.TryGetValue(col.Name,out var label))col.HeaderText=label;if(col.ValueType==typeof(DateTime))col.DefaultCellStyle.Format="dd/MM/yyyy";if(col.ValueType==typeof(decimal)){col.DefaultCellStyle.Format="N2";col.DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleRight;}}
    }
    public static void Error(Exception e)
    {
        var message=e is SqlException sql ? sql.Number switch{
            2601 or 2627=>"Mã, số chứng từ hoặc dữ liệu này đã tồn tại. Kiểm tra bản ghi trùng.",
            547=>"Dữ liệu vi phạm ràng buộc hoặc đang được chứng từ khác sử dụng. Kiểm tra ngày, số tiền và liên kết.",
            4060 or 2 or 53=>"Chưa kết nối được CSDL. Chạy Data\\QuanLyNhanSu.sql và kiểm tra App.config.",
            _=>"Không thể ghi hoặc đọc dữ liệu: "+sql.Message}:e.Message;
        MessageBox.Show(message,"Quản lý nhân sự",MessageBoxButtons.OK,MessageBoxIcon.Warning);
    }
    public static bool Confirm(string text)=>MessageBox.Show(text,"Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes;
}
