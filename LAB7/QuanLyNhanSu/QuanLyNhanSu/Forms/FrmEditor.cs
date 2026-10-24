using System.Data;
using QuanLyNhanSu.Services;
namespace QuanLyNhanSu.Forms;
public partial class FrmEditor : Form
{
    readonly Module module;readonly int recordId;readonly Dictionary<string,Control> controls=[];
    public FrmEditor(Module m,DataRow? row=null)
    {
        module=m;recordId=row==null?0:Convert.ToInt32(row["Id"]);InitializeComponent();Text=(recordId==0?"Thêm mới — ":"Cập nhật — ")+m.Title;
        int index=0;
        foreach(var f in m.Fields)
        {
            Control input;
            if(f.Kind=="ref")
            {
                var cb=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList,DisplayMember="Ten",ValueMember="Id",BindingContext=new BindingContext()};var items=NhanSuService.Lookup(f.Source);
                if(!f.Required){var r=items.NewRow();r["Id"]=0;r["Ten"]="— Không chọn —";items.Rows.InsertAt(r,0);}
                cb.DataSource=items;input=cb;
                if(row!=null && row[f.Name]!=DBNull.Value)cb.SelectedValue=row[f.Name];else cb.SelectedIndex=items.Rows.Count>0?0:-1;
            }
            else if(f.Kind=="choice")
            {
                var cb=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList};cb.Items.AddRange(f.Source.Split('|'));cb.SelectedIndex=0;if(row!=null)cb.SelectedItem=row[f.Name];input=cb;
            }
            else if(f.Kind is "date" or "time")
            {
                var dt=new DateTimePicker{Format=DateTimePickerFormat.Custom,CustomFormat=f.Kind=="time"?"HH:mm":"dd/MM/yyyy",ShowUpDown=f.Kind=="time",ShowCheckBox=!f.Required};
                dt.Value=f.Name=="NgaySinh"?DateTime.Today.AddYears(-25):DateTime.Today;
                if(row!=null && row[f.Name]!=DBNull.Value)dt.Value=f.Kind=="time"?DateTime.Today+(TimeSpan)row[f.Name]:(DateTime)row[f.Name];else if(!f.Required)dt.Checked=false;
                input=dt;
            }
            else if(f.Kind is "money" or "hours" or "percent")
            {
                var num=new NumericUpDown{Maximum=f.Kind=="percent"?100:f.Kind=="hours"?24:1000000000000m,DecimalPlaces=f.Kind=="percent"?4:2,ThousandsSeparator=true};
                if(row!=null)num.Value=Convert.ToDecimal(row[f.Name]);input=num;
            }
            else input=new TextBox{Text=row==null?"":Convert.ToString(row[f.Name]),Multiline=f.Kind=="long",Height=f.Kind=="long"?90:32,MaxLength=f.Kind=="long"?1000:f.Name is "HoTen" or "Ten"?120:f.Name=="DiaChi"?250:f.Name=="DienThoai"?25:f.Name=="SoBH"?50:f.Name.StartsWith("So")?40:300,ScrollBars=f.Kind=="long"?ScrollBars.Vertical:ScrollBars.None};
            input.Dock=DockStyle.Fill;input.Margin=new Padding(4,7,8,7);input.AccessibleName=f.Label;
            fields.RowStyles.Add(new RowStyle(SizeType.AutoSize));fields.Controls.Add(new Label{Text=f.Label+(f.Required?" *":""),AutoSize=true,Anchor=AnchorStyles.Left,Margin=new Padding(5,10,5,10)},0,index);fields.Controls.Add(input,1,index++);controls[f.Name]=input;
        }
        save.Click+=(_,_)=>SaveRecord();cancel.Click+=(_,_)=>Close();
    }
    void SaveRecord()
    {
        try
        {
            var values=new Dictionary<string,object?>();
            foreach(var f in module.Fields)
            {
                var ctl=controls[f.Name];object? value=ctl switch{
                    ComboBox cb when f.Kind=="ref"=>cb.SelectedValue==null||Convert.ToInt32(cb.SelectedValue)==0?null:cb.SelectedValue,
                    ComboBox cb=>cb.SelectedItem?.ToString(),
                    DateTimePicker dt=>!f.Required&&!dt.Checked?null:f.Kind=="time"?dt.Value.TimeOfDay:dt.Value.Date,
                    NumericUpDown num=>num.Value,
                    _=>ctl.Text.Trim()};values[f.Name]=value;
            }
            NhanSuService.Save(module,recordId,values);DialogResult=DialogResult.OK;Close();
        }
        catch(Exception e){FormHelper.Error(e);}
    }
}
