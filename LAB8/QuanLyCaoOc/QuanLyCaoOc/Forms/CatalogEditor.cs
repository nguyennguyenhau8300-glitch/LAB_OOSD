using System.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public static class CatalogEditor
{
    public static void Show(IWin32Window owner,string table,DataRow? row=null)
    {
        var service=new DanhMucService();var fields=new List<Field>();
        foreach(string key in DanhMucService.Columns[table])
        {
            object? value=row?[key];if(value is DBNull)value=null;
            Field field;
            if(key=="MaTang"&&table=="VanPhong")field=FormHelper.Choice(key,"Tầng",FormHelper.Choices(service.List("Tang"),"MaTang","TenTang"),value);
            else if(key=="NgaySuDung")field=new(key,FormHelper.Caption(key),"date",value??new DateTime(2026,1,1));
            else if(key=="NgungSuDung")field=new(key,FormHelper.Caption(key),"bool",value??false);
            else if(key=="SoTang")field=new(key,FormHelper.Caption(key),"number",value??1);
            else if(key is "DienTich" or "GiaThamKhao" or "GiaMacDinh")field=new(key,FormHelper.Caption(key),"money",value??(key=="DienTich"?50M:1000000M));
            else field=new(key,FormHelper.Caption(key),"text",value,ReadOnly:row!=null&&key==DanhMucService.Columns[table][0]);
            fields.Add(field);
        }
        using var dialog=new FrmEditor((row==null?"Thêm ":"Sửa ")+table,fields);
        dialog.SaveAction=e=>service.Save(table,e.Values(),row!=null);dialog.ShowDialog(owner);
    }
    public static void Delete(string table,DataGridView grid)
    {
        string id=Convert.ToString(FormHelper.Current(grid)[DanhMucService.Columns[table][0]])!;
        if(FormHelper.Confirm("Xóa "+id+"? Bản ghi có liên kết sẽ bị chặn."))new DanhMucService().Delete(table,id);
    }
}
