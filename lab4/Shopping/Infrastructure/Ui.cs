using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shopping.Adapters;
using Shopping.Data;
using Shopping.Models;
using Shopping.Services;

namespace Shopping.Forms
{
    public static class Ui
    {
        public static string Money(decimal amount){return amount.ToString("N0",CultureInfo.GetCultureInfo("vi-VN"))+" đ";}
        public static Button Button(string text,EventHandler click,bool primary=false)
        {
            var b=new Button{Text=text,AutoSize=true,MinimumSize=new Size(120,38),FlatStyle=FlatStyle.Flat,BackColor=primary?Color.FromArgb(31,86,139):Color.White,ForeColor=primary?Color.White:Color.FromArgb(30,55,75),Margin=new Padding(6)};
            b.Click+=click;return b;
        }
        public static DataGridView Grid(){return new DataGridView{Dock=DockStyle.Fill,ReadOnly=true,AllowUserToAddRows=false,AllowUserToDeleteRows=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false,RowHeadersVisible=false,BackgroundColor=Color.White,BorderStyle=BorderStyle.None,AutoGenerateColumns=true};}
        public static void Attempt(Action action){try{action();}catch(Exception ex){MessageBox.Show(ex.Message,"e-SHOPPING",MessageBoxButtons.OK,MessageBoxIcon.Information);}}
        public static TableLayoutPanel Fields(){return new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2,AutoScroll=true,Padding=new Padding(14)};}
        public static void Field(TableLayoutPanel panel,string name,Control control)
        {
            int row=panel.RowCount++;panel.RowStyles.Add(new RowStyle(SizeType.Absolute,48));
            panel.ColumnStyles.Clear();panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,185));panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
            var label=new Label{Text=name,AutoSize=true,Anchor=AnchorStyles.Left};control.Anchor=AnchorStyles.Left|AnchorStyles.Right;
            panel.Controls.Add(label,0,row);panel.Controls.Add(control,1,row);
        }
        public static TextBox Text(bool password=false){return new TextBox{UseSystemPasswordChar=password,MaxLength=300};}
        public static ComboBox Choice(string[] items){var c=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList};c.Items.AddRange(items);c.SelectedIndex=0;return c;}
        public static void Headers(DataGridView grid)
        {
            string[] keys={"Code","Name","Group","Maker","Price","Status","Quantity","Total","MaDonHang","ThoiDiemDat","TenNguoiNhan","TongTien","TrangThaiEmail"};
            string[] names={"Mã SP","Sản phẩm","Nhóm","Nhà sản xuất","Đơn giá","Tình trạng","Số lượng","Thành tiền","Mã đơn","Ngày đặt","Người nhận","Tổng tiền","Email"};
            for(int i=0;i<keys.Length;i++)if(grid.Columns.Contains(keys[i]))grid.Columns[keys[i]].HeaderText=names[i];
            foreach(DataGridViewColumn c in grid.Columns)if(c.ValueType==typeof(decimal))c.DefaultCellStyle.Format="N0";
        }
    }
}
