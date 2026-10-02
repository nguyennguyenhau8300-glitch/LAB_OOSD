namespace Shopping.Forms
{
    partial class FrmDatHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblrecipient;
        private System.Windows.Forms.TextBox recipient;
        private System.Windows.Forms.Label lbladdress;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label lblphone;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label lblregion;
        private System.Windows.Forms.ComboBox region;
        private System.Windows.Forms.Label lbldelivery;
        private System.Windows.Forms.ComboBox delivery;
        private System.Windows.Forms.Label lblcard;
        private System.Windows.Forms.ComboBox card;
        private System.Windows.Forms.Label lblnumber;
        private System.Windows.Forms.TextBox number;
        private System.Windows.Forms.Label lblholder;
        private System.Windows.Forms.TextBox holder;
        private System.Windows.Forms.Label lblexpiry;
        private System.Windows.Forms.DateTimePicker expiry;
        private System.Windows.Forms.Label lblcsv;
        private System.Windows.Forms.TextBox csv;
        private System.Windows.Forms.Label lblscenario;
        private System.Windows.Forms.ComboBox scenario;
        private System.Windows.Forms.Label lblfailEmail;
        private System.Windows.Forms.CheckBox failEmail;
        private System.Windows.Forms.Label quoteLabel;
        private System.Windows.Forms.Button btnRequote;
        private System.Windows.Forms.Button place;
        private System.Windows.Forms.Button lookup;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblrecipient = new System.Windows.Forms.Label();
            this.recipient = new System.Windows.Forms.TextBox();
            this.lbladdress = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.lblphone = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.TextBox();
            this.lblregion = new System.Windows.Forms.Label();
            this.region = new System.Windows.Forms.ComboBox();
            this.lbldelivery = new System.Windows.Forms.Label();
            this.delivery = new System.Windows.Forms.ComboBox();
            this.lblcard = new System.Windows.Forms.Label();
            this.card = new System.Windows.Forms.ComboBox();
            this.lblnumber = new System.Windows.Forms.Label();
            this.number = new System.Windows.Forms.TextBox();
            this.lblholder = new System.Windows.Forms.Label();
            this.holder = new System.Windows.Forms.TextBox();
            this.lblexpiry = new System.Windows.Forms.Label();
            this.expiry = new System.Windows.Forms.DateTimePicker();
            this.lblcsv = new System.Windows.Forms.Label();
            this.csv = new System.Windows.Forms.TextBox();
            this.lblscenario = new System.Windows.Forms.Label();
            this.scenario = new System.Windows.Forms.ComboBox();
            this.lblfailEmail = new System.Windows.Forms.Label();
            this.failEmail = new System.Windows.Forms.CheckBox();
            this.quoteLabel = new System.Windows.Forms.Label();
            this.btnRequote = new System.Windows.Forms.Button();
            this.place = new System.Windows.Forms.Button();
            this.lookup = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(1110, 40);
            this.lblTitle.Text = "Đặt hàng và thanh toán";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 86, 139);
            this.Controls.Add(this.lblTitle);
            // lblrecipient
            this.lblrecipient.Name = "lblrecipient";
            this.lblrecipient.Location = new System.Drawing.Point(25, 85);
            this.lblrecipient.Size = new System.Drawing.Size(180, 28);
            this.lblrecipient.Text = "Người nhận *";
            this.Controls.Add(this.lblrecipient);
            // recipient
            this.recipient.Name = "recipient";
            this.recipient.Location = new System.Drawing.Point(210, 85);
            this.recipient.Size = new System.Drawing.Size(400, 28);
            this.Controls.Add(this.recipient);
            // lbladdress
            this.lbladdress.Name = "lbladdress";
            this.lbladdress.Location = new System.Drawing.Point(25, 130);
            this.lbladdress.Size = new System.Drawing.Size(180, 28);
            this.lbladdress.Text = "Địa chỉ nhận *";
            this.Controls.Add(this.lbladdress);
            // address
            this.address.Name = "address";
            this.address.Location = new System.Drawing.Point(210, 130);
            this.address.Size = new System.Drawing.Size(400, 28);
            this.Controls.Add(this.address);
            // lblphone
            this.lblphone.Name = "lblphone";
            this.lblphone.Location = new System.Drawing.Point(25, 175);
            this.lblphone.Size = new System.Drawing.Size(180, 28);
            this.lblphone.Text = "Điện thoại nhận *";
            this.Controls.Add(this.lblphone);
            // phone
            this.phone.Name = "phone";
            this.phone.Location = new System.Drawing.Point(210, 175);
            this.phone.Size = new System.Drawing.Size(400, 28);
            this.Controls.Add(this.phone);
            // lblregion
            this.lblregion.Name = "lblregion";
            this.lblregion.Location = new System.Drawing.Point(25, 220);
            this.lblregion.Size = new System.Drawing.Size(180, 28);
            this.lblregion.Text = "Khu vực";
            this.Controls.Add(this.lblregion);
            // region
            this.region.Name = "region";
            this.region.Location = new System.Drawing.Point(210, 220);
            this.region.Size = new System.Drawing.Size(400, 28);
            this.region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.region.SelectedIndexChanged += this.QuoteChanged;
            this.Controls.Add(this.region);
            // lbldelivery
            this.lbldelivery.Name = "lbldelivery";
            this.lbldelivery.Location = new System.Drawing.Point(25, 265);
            this.lbldelivery.Size = new System.Drawing.Size(180, 28);
            this.lbldelivery.Text = "Loại giao";
            this.Controls.Add(this.lbldelivery);
            // delivery
            this.delivery.Name = "delivery";
            this.delivery.Location = new System.Drawing.Point(210, 265);
            this.delivery.Size = new System.Drawing.Size(400, 28);
            this.delivery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.delivery.SelectedIndexChanged += this.QuoteChanged;
            this.Controls.Add(this.delivery);
            // lblcard
            this.lblcard.Name = "lblcard";
            this.lblcard.Location = new System.Drawing.Point(25, 310);
            this.lblcard.Size = new System.Drawing.Size(180, 28);
            this.lblcard.Text = "Loại thẻ";
            this.Controls.Add(this.lblcard);
            // card
            this.card.Name = "card";
            this.card.Location = new System.Drawing.Point(210, 310);
            this.card.Size = new System.Drawing.Size(400, 28);
            this.card.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.card.SelectedIndexChanged += this.QuoteChanged;
            this.Controls.Add(this.card);
            // lblnumber
            this.lblnumber.Name = "lblnumber";
            this.lblnumber.Location = new System.Drawing.Point(25, 355);
            this.lblnumber.Size = new System.Drawing.Size(180, 28);
            this.lblnumber.Text = "Số thẻ *";
            this.Controls.Add(this.lblnumber);
            // number
            this.number.Name = "number";
            this.number.Location = new System.Drawing.Point(210, 355);
            this.number.Size = new System.Drawing.Size(400, 28);
            this.number.UseSystemPasswordChar = true;
            this.Controls.Add(this.number);
            // lblholder
            this.lblholder.Name = "lblholder";
            this.lblholder.Location = new System.Drawing.Point(25, 400);
            this.lblholder.Size = new System.Drawing.Size(180, 28);
            this.lblholder.Text = "Tên chủ thẻ *";
            this.Controls.Add(this.lblholder);
            // holder
            this.holder.Name = "holder";
            this.holder.Location = new System.Drawing.Point(210, 400);
            this.holder.Size = new System.Drawing.Size(400, 28);
            this.Controls.Add(this.holder);
            // lblexpiry
            this.lblexpiry.Name = "lblexpiry";
            this.lblexpiry.Location = new System.Drawing.Point(25, 445);
            this.lblexpiry.Size = new System.Drawing.Size(180, 28);
            this.lblexpiry.Text = "Hết hạn";
            this.Controls.Add(this.lblexpiry);
            // expiry
            this.expiry.Name = "expiry";
            this.expiry.Location = new System.Drawing.Point(210, 445);
            this.expiry.Size = new System.Drawing.Size(400, 28);
            this.expiry.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.expiry.CustomFormat = "MM/yyyy";
            this.expiry.ShowUpDown = true;
            this.Controls.Add(this.expiry);
            // lblcsv
            this.lblcsv.Name = "lblcsv";
            this.lblcsv.Location = new System.Drawing.Point(25, 490);
            this.lblcsv.Size = new System.Drawing.Size(180, 28);
            this.lblcsv.Text = "CSV *";
            this.Controls.Add(this.lblcsv);
            // csv
            this.csv.Name = "csv";
            this.csv.Location = new System.Drawing.Point(210, 490);
            this.csv.Size = new System.Drawing.Size(400, 28);
            this.csv.UseSystemPasswordChar = true;
            this.Controls.Add(this.csv);
            // lblscenario
            this.lblscenario.Name = "lblscenario";
            this.lblscenario.Location = new System.Drawing.Point(25, 535);
            this.lblscenario.Size = new System.Drawing.Size(180, 28);
            this.lblscenario.Text = "Kịch bản lab";
            this.Controls.Add(this.lblscenario);
            // scenario
            this.scenario.Name = "scenario";
            this.scenario.Location = new System.Drawing.Point(210, 535);
            this.scenario.Size = new System.Drawing.Size(400, 28);
            this.scenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scenario.Items.AddRange(new object[] { "Thành công", "Từ chối", "Timeout / đối soát" });
            this.Controls.Add(this.scenario);
            // lblfailEmail
            this.lblfailEmail.Name = "lblfailEmail";
            this.lblfailEmail.Location = new System.Drawing.Point(25, 580);
            this.lblfailEmail.Size = new System.Drawing.Size(180, 28);
            this.lblfailEmail.Text = "Giả lập email lỗi";
            this.Controls.Add(this.lblfailEmail);
            // failEmail
            this.failEmail.Name = "failEmail";
            this.failEmail.Location = new System.Drawing.Point(210, 580);
            this.failEmail.Size = new System.Drawing.Size(400, 28);
            this.Controls.Add(this.failEmail);
            // quoteLabel
            this.quoteLabel.Name = "quoteLabel";
            this.quoteLabel.Location = new System.Drawing.Point(660, 85);
            this.quoteLabel.Size = new System.Drawing.Size(460, 580);
            this.quoteLabel.BackColor = System.Drawing.Color.FromArgb(230, 239, 248);
            this.quoteLabel.Padding = new System.Windows.Forms.Padding(15);
            this.Controls.Add(this.quoteLabel);
            // btnRequote
            this.btnRequote.Name = "btnRequote";
            this.btnRequote.Location = new System.Drawing.Point(20, 695);
            this.btnRequote.Size = new System.Drawing.Size(135, 38);
            this.btnRequote.Text = "Tính lại";
            this.btnRequote.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRequote.Click += this.RequoteClick;
            this.Controls.Add(this.btnRequote);
            // place
            this.place.Name = "place";
            this.place.Location = new System.Drawing.Point(165, 695);
            this.place.Size = new System.Drawing.Size(135, 38);
            this.place.Text = "Xác nhận đặt hàng";
            this.place.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.place.Click += this.PlaceClick;
            this.Controls.Add(this.place);
            // lookup
            this.lookup.Name = "lookup";
            this.lookup.Location = new System.Drawing.Point(310, 695);
            this.lookup.Size = new System.Drawing.Size(135, 38);
            this.lookup.Text = "Đối soát";
            this.lookup.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lookup.Click += this.LookupClick;
            this.Controls.Add(this.lookup);
            // btnBack
            this.btnBack.Name = "btnBack";
            this.btnBack.Location = new System.Drawing.Point(455, 695);
            this.btnBack.Size = new System.Drawing.Size(135, 38);
            this.btnBack.Text = "Quay lại";
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnBack.Click += this.CloseClick;
            this.Controls.Add(this.btnBack);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1150, 760);
            this.MinimumSize = new System.Drawing.Size(1166, 799);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmDatHang";
            this.Text = "e-SHOPPING - Đặt hàng và thanh toán";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
