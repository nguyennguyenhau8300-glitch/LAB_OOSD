using System.Data;
using System.Globalization;
using System.Text;
using QuanLyNhanSu.Services;
namespace QuanLyNhanSu.Forms;
public abstract partial class FrmChucNang:Form
{
    Module selected=Modules.All[0];readonly Dictionary<string,Dictionary<int,string>> lookups=[];
    protected void ConfigureGroup(int index)
    {
        InitializeWorkspace();
        var group=FrmMain.Groups[index];
        Text=group.Title;groupTitle.Text=group.Title.Replace(" – ","\n");
        navigation.DisplayMember="Title";
        navigation.DataSource=group.Tables.Select(Modules.Get).ToArray();
        navigation.SelectedIndexChanged+=(_,_)=>SelectModule();
        search.TextChanged+=(_,_)=>Reload();month.ValueChanged+=(_,_)=>Reload();year.ValueChanged+=(_,_)=>Reload();
        grid.CellDoubleClick+=(_,e)=>{if(e.RowIndex>=0 && selected.Editable)Edit();};grid.CellFormatting+=FormatReferences;
        Shown+=(_,_)=>SelectModule();
    }
    public void SelectScreen(string table)
    {
        var module=navigation.Items.Cast<Module>().SingleOrDefault(x=>x.Table==table)
            ?? throw new InvalidOperationException("Màn hình không thuộc nhóm chức năng này.");
        navigation.SelectedItem=module;SelectModule();
    }
    public string[] ScreenTables=>navigation.Items.Cast<Module>().Select(x=>x.Table).ToArray();
    public int VisibleRecordCount=>grid.Rows.Count;
    void Safe(Action action){try{UseWaitCursor=true;action();}catch(Exception e){FormHelper.Error(e);}finally{UseWaitCursor=false;}}
    void Button(string text,Action action){var b=new Button{Text=text,AutoSize=true,Height=36,MinimumSize=new Size(90,36),Padding=new Padding(5,0,5,0)};b.Click+=(_,_)=>Safe(action);actions.Controls.Add(b);}
    int Id(){if(grid.CurrentRow?.DataBoundItem is not DataRowView row)throw new InvalidOperationException("Chọn một dòng dữ liệu.");return Convert.ToInt32(row["Id"]);}
    void SelectModule()
    {
        if(navigation.SelectedItem is not Module module)return;
        selected=module;title.Text=selected.Title;actions.Controls.Clear();
        search.Visible=selected.Table!="BangLuong";month.Visible=year.Visible=standard.Visible=true;
        if(selected.Editable){Button("Thêm mới",()=>{using var f=new FrmEditor(selected);if(f.ShowDialog(this)==DialogResult.OK)Reload();});Button("Sửa",Edit);Button("Xóa",()=>{int id=Id();if(FormHelper.Confirm("Xóa bản ghi đã chọn? Chứng từ đã xử lý hoặc đang được sử dụng sẽ được bảo vệ.")){NhanSuService.Delete(selected,id);Reload();}});}
        Button("Tải lại",Reload);Button("Xuất CSV",Export);
        switch(selected.Table)
        {
            case "NhanVien":Button("Lịch sử công tác",()=>{using var f=new FrmTable("Lịch sử nhân viên",NhanSuService.History(Id()));f.ShowDialog(this);});break;
            case "TuyenDung":Button("Đạt thử việc",()=>Evaluate(true));Button("Không đạt",()=>Evaluate(false));break;
            case "HopDong":Button("Ghi nhận đã ký",()=>{if(FormHelper.Confirm("Ghi nhận hợp đồng đã được hai bên ký?")){NhanSuService.SignContract(Id());Reload();}});break;
            case "ThuongPhat":Button("Ghi nhận đã ký",()=>{if(FormHelper.Confirm("Ghi nhận quyết định đã được người có thẩm quyền ký?")){NhanSuService.Approve(Id());Reload();}});Button("Lập phiếu thu chi",()=>{if(FormHelper.Confirm("Xác nhận đã thu hoặc chi trực tiếp theo quyết định?")){NhanSuService.PayDirect(Id());Reload();}});break;
            case "BangLuong":Button("Tính lại bản nháp",()=>Payroll(false));Button("Chốt lương",()=>Payroll(true));Button("Xem chi tiết",()=>{using var f=new FrmTable("Chi tiết bảng lương",NhanSuService.Details(Id()));f.ShowDialog(this);});Button("Xuất chi tiết CSV",()=>ExportTable(NhanSuService.Details(Id()),"ChiTietLuong"));break;
        }
        hint.Text=selected.Table switch {
            "PhanCong"=>"Chọn tháng/năm để xem phân công. Mỗi dòng xác định nhân viên, ngày và ca; giờ phụ trội dự kiến chỉ dùng lập kế hoạch.",
            "ChamCong"=>"Ghi công theo phân công. Nhập 0 giờ nếu nghỉ. Hệ thống kiểm tra độ dài ca và bảo vệ tháng đã chốt lương.",
            "PhieuPhuTroi"=>"Lập phiếu trước, sau đó vào Nhân viên phụ trội để thêm người tham gia. Hệ số tính tiền là 1,5 cho các hình thức theo đề.",
            "ChiTietPhuTroi"=>"Mỗi nhân viên xuất hiện một lần trên phiếu; giờ thực tế không vượt giới hạn của phiếu.",
            "BaoHiem"=>"Nhập tỷ lệ người lao động đóng theo cấu hình bài lab. Hợp đồng dài hạn cần đủ BHYT và BHXH để tính lương.",
            "ThuongPhat"=>"Chọn cá nhân hoặc tập thể. Tập thể thu/chi trực tiếp. Cá nhân có thể xử lý qua lương; chỉ quyết định đã ký mới được tính.",
            "BangLuong"=>"Giờ chuẩn mặc định 208 là quy ước bài lab, có thể đổi. Chốt lương sẽ tính lại dữ liệu mới nhất và khóa tháng. Chỉ chốt sau khi hết tháng.",
            "HopDong"=>"Lập hợp đồng sau khi đạt thử việc; nhấn Ghi nhận đã ký sau khi hai bên ký. Sau đó lập quyết định lương và bảo hiểm nếu dài hạn.",
            "TuyenDung"=>"Lưu quyết định tuyển dụng để bắt đầu thử việc. Ghi nhận Đạt hoặc Không đạt sau ngày kết thúc thử việc.",
            _=>"Thêm mới hoặc chọn một dòng để sửa. Lịch sử và chứng từ liên quan được bảo vệ bằng ràng buộc dữ liệu."};
        Reload();
    }
    void Evaluate(bool pass){if(FormHelper.Confirm("Xác nhận kết quả thử việc: "+(pass?"Đạt":"Không đạt")+"?")){NhanSuService.Evaluate(Id(),pass);Reload();}}
    void Edit(){Safe(()=>{if(!selected.Editable)return;_ = Id();var row=((DataRowView)grid.CurrentRow!.DataBoundItem!).Row;using var f=new FrmEditor(selected,row);if(f.ShowDialog(this)==DialogResult.OK)Reload();});}
    void Payroll(bool close){if(close&&!FormHelper.Confirm("Chốt lương tháng đã chọn? Sau khi chốt, dữ liệu nguồn của tháng được khóa."))return;int id=NhanSuService.Payroll((int)month.Value,(int)year.Value,standard.Value,close);Reload();using var f=new FrmTable(close?"Bảng lương đã chốt":"Kết quả tính lương",NhanSuService.Details(id));f.ShowDialog(this);}
    public void Reload()
    {
        Safe(()=>{
            lookups.Clear();foreach(var field in selected.Fields.Where(f=>f.Kind=="ref")){var dt=NhanSuService.Lookup(field.Source);lookups[field.Name]=dt.Rows.Cast<DataRow>().ToDictionary(r=>Convert.ToInt32(r["Id"]),r=>(string)r["Ten"]);}
            var data=NhanSuService.List(selected,search.Text.Trim());
            string? dateField=selected.Table switch{"PhanCong"=>"NgayLam","PhieuPhuTroi" or "ThuongPhat" or "ThuChi"=>"Ngay",_=>null};
            if(dateField!=null)data.DefaultView.RowFilter=$"[{dateField}] >= #{(int)month.Value}/1/{(int)year.Value}# AND [{dateField}] < #{new DateTime((int)year.Value,(int)month.Value,1).AddMonths(1):MM/dd/yyyy}#";
            if(selected.Table=="BangLuong")data.DefaultView.RowFilter=$"Thang={(int)month.Value} AND Nam={(int)year.Value}";
            grid.DataSource=data.DefaultView;FormHelper.FormatGrid(grid);foreach(var f in selected.Fields)if(grid.Columns.Contains(f.Name))grid.Columns[f.Name]!.HeaderText=f.Label;
            status.Text=$"{data.DefaultView.Count} bản ghi • {selected.Title}";
        });
    }
    void FormatReferences(object? sender,DataGridViewCellFormattingEventArgs e)
    {
        string name=grid.Columns[e.ColumnIndex].Name;if(e.Value!=null && e.Value!=DBNull.Value && lookups.TryGetValue(name,out var map)&&map.TryGetValue(Convert.ToInt32(e.Value),out var label)){e.Value=label;e.FormattingApplied=true;}
    }
    void Export()
    {
        using var dialog=new SaveFileDialog{Filter="CSV UTF-8|*.csv",FileName=selected.Table+".csv"};if(dialog.ShowDialog(this)!=DialogResult.OK)return;
        string Escape(string s)=>"\""+s.Replace("\"","\"\"")+"\"";
        var lines=new List<string>{string.Join(",",grid.Columns.Cast<DataGridViewColumn>().Select(c=>Escape(c.HeaderText)))};
        foreach(DataGridViewRow row in grid.Rows)lines.Add(string.Join(",",row.Cells.Cast<DataGridViewCell>().Select(c=>Escape(CsvSafe(Convert.ToString(c.FormattedValue)??"")))));
        File.WriteAllLines(dialog.FileName,lines,new UTF8Encoding(true));status.Text="Đã xuất CSV.";
    }
    static string CsvSafe(string value)=>value.Length>0&&"=+-@\t\r".IndexOf(value[0])>=0?"'"+value:value;
    static void ExportTable(DataTable table,string name)
    {
        using var dialog=new SaveFileDialog{Filter="CSV UTF-8|*.csv",FileName=name+".csv"};if(dialog.ShowDialog()!=DialogResult.OK)return;
        string Escape(string value)=>"\""+CsvSafe(value).Replace("\"","\"\"")+"\"";
        var lines=new List<string>{string.Join(",",table.Columns.Cast<DataColumn>().Select(c=>Escape(c.ColumnName)))};
        lines.AddRange(table.Rows.Cast<DataRow>().Select(r=>string.Join(",",r.ItemArray.Select(v=>Escape(Convert.ToString(v,CultureInfo.InvariantCulture)??"")))));File.WriteAllLines(dialog.FileName,lines,new UTF8Encoding(true));
    }
}

