using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public record Field(string Key,string Label,string Kind="text",object? Value=null,DataTable? Options=null,string? Display=null,string? Id=null,bool ReadOnly=false);
public sealed record ModulePage(TabPage Tab,DataGridView Grid,FlowLayoutPanel Buttons,TextBox Search);

public static class FormHelper
{
    static readonly Dictionary<string,string> Names=new()
    {
        ["MaTang"]="Mã tầng",["SoTang"]="Số tầng",["TenTang"]="Tên tầng",["MaPhong"]="Mã phòng",["DienTich"]="Diện tích (m²)",["Huong"]="Hướng",["GiaThamKhao"]="Giá tham khảo",["NgaySuDung"]="Ngày sử dụng",["NgungSuDung"]="Ngừng sử dụng",["ConTrong"]="Còn trống",
        ["MaKH"]="Mã khách",["TenCongTy"]="Công ty",["MaSoThue"]="Mã số thuế",["DaiDien"]="Đại diện",["DienThoai"]="Điện thoại",["MaHD"]="Mã hợp đồng",["NgayKy"]="Ngày ký",["NgayBatDau"]="Bắt đầu",["SoThangGoc"]="Tháng gốc",["TrangThai"]="Trạng thái",["NgayKetThuc"]="Hạn hiệu lực",
        ["MaCTHD"]="ID phòng thuê",["DonGiaThang"]="Giá / tháng",["MaKhoan"]="Mã khoản",["Loai"]="Loại",["KyTu"]="Kỳ từ",["KyDen"]="Kỳ đến (không gồm)",["HanThu"]="Hạn thu",["SoTien"]="Số tiền",["DaThu"]="Đã thu",["ConNo"]="Còn nợ",
        ["MaHDDV"]="Mã hóa đơn DV",["MaDV"]="Mã dịch vụ",["TenDV"]="Dịch vụ",["DonViTinh"]="Đơn vị",["GiaMacDinh"]="Giá mặc định",["SoLuong"]="Số lượng",["DonGia"]="Đơn giá",["Thang"]="Tháng",["Nam"]="Năm",["NgayChot"]="Chốt đến",["TongTien"]="Tổng tiền",["MaPT"]="Mã phiếu thu",["NgayThu"]="Ngày thu",["PhuongThuc"]="Phương thức",["RequestId"]="Mã yêu cầu",
        ["MaNV"]="Mã nhân viên",["HoTen"]="Họ tên",["Anh"]="Ảnh",["ChucVu"]="Chức vụ",["NgayVao"]="Ngày vào",["NgayNghi"]="Ngày nghỉ",["MaThe"]="Mã thẻ",["MaXe"]="Mã xe",["BienSo"]="Biển số",["LoaiXe"]="Loại xe",["MauXe"]="Màu xe",["NgayCap"]="Ngày cấp",["NgayThuHoi"]="Ngày thu hồi",
        ["MaPGH"]="Mã gia hạn",["NgayLap"]="Ngày lập",["NguoiLap"]="Người lập",["SoThang"]="Tháng thêm",["HanCu"]="Hạn cũ",["HanMoi"]="Hạn mới",["MaCTKT"]="ID kiểm tra",["MaPKT"]="Mã biên bản",["NgayKiemTra"]="Ngày kiểm tra",["NguoiKiemTra"]="Người kiểm tra",["TinhTrang"]="Hiện trạng",["ThayDoiKetCau"]="Thay đổi kết cấu",["DeXuat"]="Đề xuất tu sửa",["GhiChu"]="Ghi chú",["LanGanNhat"]="Mốc kiểm tra gần nhất",["HanKiemTra"]="Đến hạn",["MaPhat"]="Mã phạt",["LyDo"]="Lý do",["HanNop"]="Hạn nộp",["DaNop"]="Đã nộp",["Chon"]="Chọn",["Gia"]="Giá tháng"
    };
    public static string Caption(string key)=>Names.GetValueOrDefault(key,key);
    public static void Style(Form form,string title)
    { form.Text=title;form.StartPosition=FormStartPosition.CenterScreen;form.BackColor=Color.White;form.ForeColor=Color.Black;form.Font=new Font("Segoe UI",10F);form.AutoScaleMode=AutoScaleMode.Dpi;form.MinimumSize=new Size(1000,640);form.Size=new Size(1320,800); }
    public static void Error(Exception e)
    {
        string message=e is SqlException sql?sql.Number switch {547=>"Dữ liệu có liên kết với chứng từ khác hoặc không đúng ràng buộc. Không thể xóa/lưu.",2601 or 2627=>"Mã, kỳ hóa đơn, biển số hoặc bản ghi đã tồn tại. Vui lòng kiểm tra lại.",_=>sql.Message}:e.Message;
        MessageBox.Show(message,"Không thể thực hiện",MessageBoxButtons.OK,MessageBoxIcon.Warning);
    }
    public static void Run(Action action)
    { Cursor.Current=Cursors.WaitCursor;try{action();}catch(Exception e){Error(e);}finally{Cursor.Current=Cursors.Default;} }
    public static void Info(string text)=>MessageBox.Show(text,"Kết quả",MessageBoxButtons.OK,MessageBoxIcon.Information);
    public static bool Confirm(string text)=>MessageBox.Show(text,"Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes;
    public static bool IsDesigner(Form f)=>f.DesignModeForHelper() || System.ComponentModel.LicenseManager.UsageMode==System.ComponentModel.LicenseUsageMode.Designtime;
    static bool DesignModeForHelper(this Form f)=>f.Site?.DesignMode==true;
    public static Button Button(string text,Action action)=>MakeButton(text,()=>Run(action));
    static Button MakeButton(string text,Action action)
    { var b=new Button{Text=text,AutoSize=true,MinimumSize=new Size(95,36),Margin=new Padding(5),Padding=new Padding(7,0,7,0)};b.Click+=(_,_)=>action();return b; }
    public static DataGridView Grid(bool edit=false)
    { return new DataGridView{BindingContext=new BindingContext(),Dock=DockStyle.Fill,AutoGenerateColumns=true,ReadOnly=!edit,AllowUserToAddRows=false,AllowUserToDeleteRows=false,BackgroundColor=Color.White,BorderStyle=BorderStyle.FixedSingle,RowHeadersVisible=false,MultiSelect=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.DisplayedCells,ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize,AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.AllCells}; }
    public static void Bind(DataGridView grid,DataTable data)
    {
        grid.DataSource=data;
        foreach(DataGridViewColumn col in grid.Columns)
        {
            col.HeaderText=Caption(col.Name);col.MinimumWidth=80;
            if(col.ValueType==typeof(DateTime))col.DefaultCellStyle.Format="dd/MM/yyyy";
            if(col.ValueType==typeof(decimal)){col.DefaultCellStyle.Format="N2";col.DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleRight;}
            if(col.Name is "RequestId" or "Anh")col.Visible=false;
        }
    }
    public static DataRow Current(DataGridView grid)=>grid.CurrentRow?.DataBoundItem is DataRowView view?view.Row:throw new InvalidOperationException("Chọn một dòng dữ liệu trước.");
    public static ModulePage Page(TabControl tabs,string title,Func<DataTable> load,params (string Text,Action<DataGridView> Click)[] commands)
    {
        var tab=new TabPage(title);tabs.TabPages.Add(tab);var buttons=new FlowLayoutPanel{Dock=DockStyle.Top,AutoSize=true,WrapContents=true,Padding=new Padding(4)};
        var search=new TextBox{Width=220,PlaceholderText="Tìm trong bảng...",Margin=new Padding(6,10,6,6)};buttons.Controls.Add(search);
        var grid=Grid();grid.Name="dgv"+tabs.TabPages.Count;tab.Controls.Add(grid);tab.Controls.Add(buttons);
        void Reload(){string key=search.Text;Bind(grid,load());ApplyFilter(grid,key);}
        buttons.Controls.Add(Button("Tải lại",Reload));
        foreach(var cmd in commands)buttons.Controls.Add(Button(cmd.Text,()=>{cmd.Click(grid);Reload();}));
        buttons.Controls.Add(Button("Xuất CSV",()=>Export(grid,title)));
        search.TextChanged+=(_,_)=>ApplyFilter(grid,search.Text);
        grid.DataError+=(_,e)=>{e.ThrowException=false;Info("Giá trị nhập chưa đúng kiểu dữ liệu.");};
        tab.Tag=(Action)Reload;return new(tab,grid,buttons,search);
    }
    public static void LoadAll(TabControl tabs)
    { foreach(TabPage tab in tabs.TabPages)if(tab.Tag is Action load)load(); }
    public static void ApplyFilter(DataGridView grid,string key)
    {
        if(grid.DataSource is not DataTable dt)return;
        string s=key.Replace("'","''").Replace("[","[[]").Replace("%","[%]").Replace("*","[*]");
        dt.DefaultView.RowFilter=string.IsNullOrWhiteSpace(key)?"":string.Join(" OR ",dt.Columns.Cast<DataColumn>().Select(c=>$"CONVERT([{c.ColumnName}], 'System.String') LIKE '%{s}%'"));
    }
    public static void Export(DataGridView grid,string title)
    {
        using var dialog=new SaveFileDialog{Filter="CSV UTF-8|*.csv",FileName=string.Concat(title.Select(c=>Path.GetInvalidFileNameChars().Contains(c)?'_':c))+".csv"};
        if(dialog.ShowDialog()!=DialogResult.OK)return;
        string Quote(object? v)=>"\""+Convert.ToString(v,CultureInfo.GetCultureInfo("vi-VN"))?.Replace("\"","\"\"")+"\"";
        var columns=grid.Columns.Cast<DataGridViewColumn>().Where(c=>c.Visible).ToArray();var text=new StringBuilder();
        text.AppendLine(string.Join(",",columns.Select(c=>Quote(c.HeaderText))));
        foreach(DataGridViewRow row in grid.Rows)text.AppendLine(string.Join(",",columns.Select(c=>Quote(row.Cells[c.Index].Value))));
        File.WriteAllText(dialog.FileName,text.ToString(),new UTF8Encoding(true));Info("Đã xuất CSV.");
    }
    public static DataTable Selectable(DataTable input,params (string Name,Type Type,object Default)[] extra)
    {
        var table=input.Copy();table.Columns.Add("Chon",typeof(bool));foreach(DataRow row in table.Rows)row["Chon"]=false;
        table.Columns["Chon"]!.SetOrdinal(0);
        foreach(var item in extra){table.Columns.Add(item.Name,item.Type);foreach(DataRow row in table.Rows)row[item.Name]=item.Default;}
        return table;
    }
    public static IEnumerable<DataRow> Chosen(DataTable table)=>table.AsEnumerable().Where(r=>r.Field<bool>("Chon"));
    public static DataTable Choices(DataTable table,string id,string display)
    {
        var data=new DataTable();data.Columns.Add("Id",table.Columns[id]!.DataType);data.Columns.Add("Text");
        foreach(DataRow row in table.Rows)data.Rows.Add(row[id],row[id]+" — "+row[display]);return data;
    }
    public static Field Choice(string key,string label,DataTable options,object? value=null)=>new(key,label,"choice",value,options,"Text","Id");
    public static string SavePhoto(string path)
    {
        if(string.IsNullOrWhiteSpace(path))return "";
        if(!Path.IsPathRooted(path))return path;
        using var image=Image.FromFile(path);
        if(image.Width>10000||image.Height>10000)throw new InvalidOperationException("Ảnh quá lớn.");
        string directory=Path.Combine(AppContext.BaseDirectory,"Photos");Directory.CreateDirectory(directory);
        string name=Guid.NewGuid().ToString("N")+".png";image.Save(Path.Combine(directory,name),System.Drawing.Imaging.ImageFormat.Png);return Path.Combine("Photos",name);
    }
}
