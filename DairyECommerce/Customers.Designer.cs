namespace DairyECommerce
{
    partial class Customers
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CustPicBox = new System.Windows.Forms.PictureBox();
            this.CustName = new System.Windows.Forms.Label();
            this.CustLastLogin = new System.Windows.Forms.Label();
            this.CustStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CustPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CustPicBox
            // 
            this.CustPicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CustPicBox.Location = new System.Drawing.Point(3, 3);
            this.CustPicBox.Name = "CustPicBox";
            this.CustPicBox.Size = new System.Drawing.Size(82, 84);
            this.CustPicBox.TabIndex = 0;
            this.CustPicBox.TabStop = false;
            this.CustPicBox.Click += new System.EventHandler(this.CustPicBox_Click);
            // 
            // CustName
            // 
            this.CustName.AutoSize = true;
            this.CustName.Location = new System.Drawing.Point(87, 14);
            this.CustName.Name = "CustName";
            this.CustName.Size = new System.Drawing.Size(82, 13);
            this.CustName.TabIndex = 1;
            this.CustName.Text = "Customer Name";
            // 
            // CustLastLogin
            // 
            this.CustLastLogin.AutoSize = true;
            this.CustLastLogin.Location = new System.Drawing.Point(143, 38);
            this.CustLastLogin.Name = "CustLastLogin";
            this.CustLastLogin.Size = new System.Drawing.Size(103, 13);
            this.CustLastLogin.TabIndex = 2;
            this.CustLastLogin.Text = "Customer Last Login";
            this.CustLastLogin.Click += new System.EventHandler(this.label2_Click);
            // 
            // CustStatus
            // 
            this.CustStatus.AutoSize = true;
            this.CustStatus.Location = new System.Drawing.Point(87, 64);
            this.CustStatus.Name = "CustStatus";
            this.CustStatus.Size = new System.Drawing.Size(84, 13);
            this.CustStatus.TabIndex = 3;
            this.CustStatus.Text = "Customer Status";
            this.CustStatus.Click += new System.EventHandler(this.CustStatus_Click);
            this.CustStatus.DoubleClick += new System.EventHandler(this.CustStatus_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Last Login:";
            // 
            // StatusTip
            // 
            this.StatusTip.AutoPopDelay = 5000;
            this.StatusTip.InitialDelay = 200;
            this.StatusTip.ReshowDelay = 100;
            this.StatusTip.ToolTipTitle = "Change Status";
            // 
            // Customers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CustStatus);
            this.Controls.Add(this.CustLastLogin);
            this.Controls.Add(this.CustName);
            this.Controls.Add(this.CustPicBox);
            this.Name = "Customers";
            this.Size = new System.Drawing.Size(265, 90);
            this.Load += new System.EventHandler(this.Customers_Load);
            this.Click += new System.EventHandler(this.Customers_Click);
            this.DoubleClick += new System.EventHandler(this.Customers_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.CustPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CustPicBox;
        private System.Windows.Forms.Label CustName;
        private System.Windows.Forms.Label CustLastLogin;
        private System.Windows.Forms.Label CustStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip StatusTip;
    }
}
