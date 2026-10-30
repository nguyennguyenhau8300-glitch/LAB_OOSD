using System.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms;

public partial class FrmGiaHan:Form
{
    readonly ComboBox cboHopDong=new(){Name="cboHopDong",Width=650,DropDownStyle=ComboBoxStyle.DropDownList};
    readonly GiaHanService service=new();
    string HD=>Convert.ToString(cboHopDong.SelectedValue)??"";
    public FrmGiaHan()
    {
        InitializeComponent();FormHelper.Style(this,"Gia hạn hợp đồng");
        pnlFilters.Controls.AddRange([new Label{Text="Hợp đồng",AutoSize=true,Padding=new Padding(0,8,0,0)},cboHopDong,FormHelper.Button("Tải hợp đồng",LoadContracts)]);
        FormHelper.Page(tabs,"Phòng thuê",()=>Reports.Details(HD),("Lập phiếu gia hạn",_=>Renew()),
            ("Kiểm tra công nợ",g=>{long id=(long)FormHelper.Current(g)["MaCTHD"];var dt=Reports.Charges(HD);dt.DefaultView.RowFilter="MaCTHD="+id;using var f=new FrmTable("Công nợ phòng thuê",dt.DefaultView.ToTable());f.ShowDialog(this);}));
        FormHelper.Page(tabs,"Lịch sử gia hạn",Reports.Renewals);
        cboHopDong.SelectedIndexChanged+=(_,_)=>{if(IsHandleCreated)FormHelper.Run(()=>FormHelper.LoadAll(tabs));};
        Load+=(_,_)=>{if(!FormHelper.IsDesigner(this))FormHelper.Run(LoadContracts);};
    }
    void LoadContracts()
    {
        var table=Reports.Contracts();table.DefaultView.RowFilter="TrangThai='HIEU_LUC'";
        cboHopDong.BindingContext=new BindingContext();cboHopDong.DataSource=FormHelper.Choices(table.DefaultView.ToTable(),"MaHD","TenCongTy");cboHopDong.DisplayMember="Text";cboHopDong.ValueMember="Id";FormHelper.LoadAll(tabs);
    }
    void Renew()
    {
        var rooms=FormHelper.Selectable(Reports.Details(HD),("SoThang",typeof(int),1));Rules.Require(rooms.Rows.Count>0,"Chọn hợp đồng hiệu lực.");Guid request=Guid.NewGuid();string hd=HD;
        using var dialog=new FrmEditor("Gia hạn — chọn phòng, số tháng và đơn giá",[new("Ngay","Ngày lập","date"),new("Nguoi","Người lập","text",Environment.UserName)],rooms,["Chon","SoThang","DonGiaThang"]);
        dialog.SaveAction=e=>
        {
            var lines=FormHelper.Chosen(rooms).Select(r=>new PhongGiaHan((long)r["MaCTHD"],(int)r["SoThang"],(decimal)r["DonGiaThang"])).ToList();
            string id=service.Lap(new(hd,e.D("Ngay"),e.S("Nguoi"),lines,request));FormHelper.Info("Đã lập "+id+"; hạn và các khoản thuê mới đã lưu.");
        };dialog.ShowDialog(this);
    }
}
