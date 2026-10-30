using System.Data;

namespace QuanLyCaoOc.Forms;

public partial class FrmEditor:Form
{
    readonly Dictionary<string,Control> editors=[];
    DataGridView? detailGrid;
    public DataTable? Lines {get;private set;}
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public Action<FrmEditor>? SaveAction {get;set;}
    public FrmEditor(){InitializeComponent();FormHelper.Style(this,"Nhập dữ liệu");btnSave.Click+=Save;}
    public FrmEditor(string title,IEnumerable<Field> fields,DataTable? lines=null,IEnumerable<string>? editable=null):this()
    {
        Text=title;
        foreach(var field in fields)
        {
            var panel=new Panel{Width=370,Height=78,Margin=new Padding(5)};
            panel.Controls.Add(new Label{Text=field.Label,Location=new Point(0,0),Width=360,Height=25});
            Control input;
            if(field.Kind=="date")input=new DateTimePicker{Format=DateTimePickerFormat.Custom,CustomFormat="dd/MM/yyyy",Value=field.Value is DateTime dt?dt:DateTime.Today};
            else if(field.Kind is "money" or "number")input=new NumericUpDown{Maximum=999999999999M,Minimum=0,DecimalPlaces=field.Kind=="money"?2:0,ThousandsSeparator=true,Value=Convert.ToDecimal(field.Value??0)};
            else if(field.Kind=="bool")input=new CheckBox{Text="Có / bật",Checked=field.Value is bool flag&&flag};
            else if(field.Kind=="choice")
            {
                var combo=new ComboBox{BindingContext=new BindingContext(),DropDownStyle=ComboBoxStyle.DropDownList,DataSource=field.Options,DisplayMember=field.Display??"Text",ValueMember=field.Id??"Id"};
                if(field.Value!=null)combo.SelectedValue=field.Value;
                input=combo;
            }
            else input=new TextBox{Text=Convert.ToString(field.Value)??"",MaxLength=field.Key.StartsWith("Ma")?20:500};
            input.Name=field.Key;input.Location=new Point(0,28);input.Width=field.Kind=="file"?250:355;input.Enabled=!field.ReadOnly;
            panel.Controls.Add(input);editors.Add(field.Key,input);pnlFields.Controls.Add(panel);
            if(field.Kind=="file")
            {
                var browse=new Button{Text="Chọn ảnh",Location=new Point(255,26),Size=new Size(105,34)};
                browse.Click+=(_,_)=>{using var fd=new OpenFileDialog{Filter="Ảnh|*.png;*.jpg;*.jpeg"};if(fd.ShowDialog(this)==DialogResult.OK)input.Text=fd.FileName;};panel.Controls.Add(browse);
            }
        }
        if(lines!=null)
        {
            Lines=lines;detailGrid=FormHelper.Grid(true);FormHelper.Bind(detailGrid,lines);detailGrid.SelectionMode=DataGridViewSelectionMode.CellSelect;
            var allowed=(editable??[]).ToHashSet();foreach(DataGridViewColumn col in detailGrid.Columns)col.ReadOnly=!allowed.Contains(col.Name);
            detailGrid.DataError+=(_,e)=>{e.ThrowException=false;FormHelper.Info("Nhập số hoặc chọn dữ liệu đúng kiểu.");};pnlDetail.Controls.Add(detailGrid);
        }
        else {MinimumSize=new Size(850,400);Size=new Size(900,Math.Min(750,200+((editors.Count+1)/2)*88));}
    }
    public object? Value(string key)=>editors[key] switch
    {ComboBox c=>c.SelectedValue,DateTimePicker d=>d.Value.Date,NumericUpDown n=>n.Value,CheckBox b=>b.Checked,_=>editors[key].Text.Trim()};
    public string S(string key)=>Convert.ToString(Value(key))??"";
    public decimal M(string key)=>Convert.ToDecimal(Value(key));
    public int I(string key)=>Convert.ToInt32(Value(key));
    public long L(string key)=>Convert.ToInt64(Value(key));
    public DateTime D(string key)=>Convert.ToDateTime(Value(key)).Date;
    public bool B(string key)=>Convert.ToBoolean(Value(key));
    public Dictionary<string,object?> Values()=>editors.Keys.ToDictionary(k=>k,Value);
    void Save(object? sender,EventArgs e)
    {
        FormHelper.Run(()=>
        {
            if(detailGrid!=null){detailGrid.EndEdit();(BindingContext?[Lines!] as CurrencyManager)?.EndCurrentEdit();}
            btnSave.Enabled=false;
            try{SaveAction?.Invoke(this);DialogResult=DialogResult.OK;Close();}finally{btnSave.Enabled=true;}
        });
    }
}

