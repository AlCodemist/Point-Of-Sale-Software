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
    public partial class CartItem : UserControl
    {

        public event EventHandler DeleteClick;
        public CartItem()
        {
            InitializeComponent();
        }

        public int cartItemID { get; set; }
        public string ItemID
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public string ItemName
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }
        public string ItemQuantity
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }
        public string ItemPrice
        {
            get { return label4.Text; }
            set { label4.Text = value; }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CartItem_Load(object sender, EventArgs e)
        {

        }

        private void CartItem_Click(object sender, EventArgs e)
        {
            DeleteClick?.Invoke(sender, e);
        }
    }
}
