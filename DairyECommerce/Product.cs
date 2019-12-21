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
    public partial class Product : UserControl
    {
        // public EventHandler ProductClick;
        public event EventHandler myClick;
        public Product()
        {
            InitializeComponent();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void prodPicture_Click(object sender, EventArgs e)
        {
            
        }

        public ImageLayout addImageLayout
        {
            get { return addPicture.BackgroundImageLayout ; }
            set { addPicture.BackgroundImageLayout = value ; }
        }
        public Image addImage 
        { 
            get { return addPicture.BackgroundImage ; }
            set { addPicture.BackgroundImage = value ; }
        }

        public string prodPrice
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
      

        public string prodDesc
        {
            get { return richTextBox1.Text   ; }
            set { richTextBox1.Text = value; }
        }

        public string prodName
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        public string prodID
        {
            get { return label4.Text; }
            set { label4.Text = value; }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
          //  MessageBox.Show(this.prodID);
            // if (this.ButtonClick != null)
            //     this.ButtonClick(this, e);
        }

        private void Product_Click(object sender, EventArgs e)
        {
            myClick?.Invoke(sender, e);

        }

        private void Product_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }
    }
}
