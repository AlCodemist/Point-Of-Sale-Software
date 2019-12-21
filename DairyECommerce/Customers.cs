using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DairyECommerce
{
    public partial class Customers : UserControl
    {
        public event EventHandler DeleteCustClick;
        public event EventHandler updateCustomer;
        public Customers()
        {
            InitializeComponent();
            
        }

        private void Customers_DoubleClick(object sender, EventArgs e)
        {
            DeleteCustClick?.Invoke(sender, e);
        }

        private void Customers_Click(object sender, EventArgs e)
        {
            updateCustomer?.Invoke(sender, e);
        }

        private void CustStatus_DoubleClick(object sender, EventArgs e)
        {
           
        }

        public Image addImage
        {
            get { return CustPicBox.BackgroundImage; }
            set { CustPicBox.BackgroundImage = value; }
        }

        public string CustomerName
        {
            get { return CustName.Text; }
            set { CustName.Text = value; }
        }


        public string Status
        {
            get { return CustStatus.Text; }
            set { CustStatus.Text = value; }
        }
        public string CustomerLastLogin
        {
            get { return CustLastLogin.Text; }
            set { CustLastLogin.Text = value; }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CustStatus_Click(object sender, EventArgs e)
        {
           
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            
        }

        private void CustPicBox_Click(object sender, EventArgs e)
        {

        }
    }
}
