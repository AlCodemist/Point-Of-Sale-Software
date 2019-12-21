using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DairyECommerce
{
    public partial class AdminBoard : Form
    {
       
        // Connection Variables
        SqlConnection con;
        SqlDataAdapter adp;   
        SqlCommand command;
        DataTable dt;
        DataRow dr;
        SqlDataReader da;
        string userPicPath;
        string updateProduct;
        string createProduct;
        string defaultPic;
        bool userStatus;
        bool createStatus;
        bool updateStatus;
        
        int prodID;
        int custID;
        string createCust;
        string updateCust;

        public AdminBoard()
        {
            InitializeComponent();
            string ConnectionQuery = @"Data Source=DESKTOP-9DVROAL\SQLEXPRESS;Initial Catalog=myWorkSpace;Integrated Security=True";
            con = new SqlConnection(ConnectionQuery);
            adp = new SqlDataAdapter();
        }


        private void AdminBoard_Load(object sender, EventArgs e)

        {
            Customers customerDisp = new Customers();

            string customerCount = "SELECT COUNT(*) FROM UserData"; //Access All the Customers from the Database
            int custCount = 0;                                      //Storing the count of Customers

            string productCount = "SELECT COUNT(*) FROM Products";
            int prodCount = 0;

            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss");
            defaultPic = @"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_instagram_50px.png";

            con.Open();

            // Count of Customer records in database
            using (command = new SqlCommand(customerCount, con))
            {
                custCount = (int)command.ExecuteScalar();
            }

            // Count of Product records in database
            using (command = new SqlCommand(productCount, con))
            {
                prodCount = (int)command.ExecuteScalar();
            }

            // Creating Dynamic Products from Database
            command = new SqlCommand("Select ProdID, ProdName, ProdDesc, ProdPrice, PicName, Stock from Products", con);
            da = command.ExecuteReader();
            for (int i = 1; i <= prodCount; i++)
            {
                Product myControl = AddmyControl(i);
                ProductPanelSlide.Controls.Add(myControl);
                myControl.myClick += new EventHandler(ProdEditClick);
            }
            da.Close();

            // Creating Dynamic Customers from Database
            string selectCusts = "Select LastLogin,Username,isAdmin,PicPath from UserData";
            command = new SqlCommand(selectCusts, con);
            da = command.ExecuteReader();
            for (int i = 1; i <= custCount; i++)
            {
                Customers customerList = createCustomerList(i);
                CustPanelSlide.Controls.Add(customerList);
                customerList.DeleteCustClick += new EventHandler(DeleteCustomer);
                customerList.updateCustomer += new EventHandler(updateCustomer);
            }
            da.Close();

        }

        // Method to Add Products to List
        private Product AddmyControl(int i)
        {
            Product myControl = new Product();
            if (da.Read())
            {
                myControl.Name = "Product" + i.ToString();
                myControl.prodID = "Product ID: " + da.GetValue(0).ToString();
                myControl.prodName = da.GetValue(1).ToString();
                myControl.prodDesc = da.GetValue(2).ToString();
                myControl.prodPrice = da.GetValue(3).ToString();
                myControl.addImage = Image.FromFile(da.GetValue(4).ToString());
                myControl.addImageLayout = ImageLayout.Stretch;
                ProdToolTip.SetToolTip(myControl, "Click Empty Space to Edit");
                //  myControl.BackColor = Color.FromArgb(192, 192, 255);
                //  myControl.ButtonClick += new EventHandler(product3_ButtonClick);
            }
            return myControl;
        }


        //Method to add product to Edit Section
        private void ProdEditClick(object sender, EventArgs e)
        {
            
            Product productList = (Product)sender;
            string prodName = productList.prodName;
            string dataFetch = "Select * from Products where prodName='"+ prodName +"'"; // Query to get Relevant data to Edit Section
            command = new SqlCommand(dataFetch, con);
            da = command.ExecuteReader();
            da.Read();
            prodID = da.GetInt32(0);
            textBox1.Text = da.GetString(2);
            textBox2.Text = da.GetValue(1).ToString();
            textBox3.Text = da.GetValue(4).ToString();
            textBox4.Text = da.GetValue(6).ToString();
            richTextBox1.Text = da.GetString(3);
            userPicPath = da.GetString(5);
            pictureBox1.BackgroundImage = Image.FromFile(userPicPath); 
            da.Close();
        }

        // Button to Update Product
        private void button5_Click(object sender, EventArgs e)
        {
            string productName = textBox1.Text;
            string stock = textBox4.Text;
            string productPrice = textBox3.Text;
            string productID = textBox2.Text;
            string productDesc = richTextBox1.Text;
            string picPath = userPicPath;
            //MessageBox.Show(stock);
            if (stock != null && productPrice != null && productName != null && productID != null && productDesc != null && picPath != null)
            {
                string dataUpdate = "update Products set ProdID = '"+ productID +"', ProdName = '"+productName+"', ProdDesc = '"+productDesc+"', ProdPrice = '"+productPrice+"', PicName = '"+picPath+"', Stock = '"+stock+"' where ID = "+ prodID +"";
                using (command = new SqlCommand(dataUpdate, con))
                {
                    command.ExecuteNonQuery();

                }
                textBox1.Text = "";
                textBox4.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
                richTextBox1.Text = "";
                userPicPath = "";
                pictureBox1.BackgroundImage = Image.FromFile(defaultPic); 
            }
            else
            {
                MessageBox.Show("You can't update with a null field");
            }

        }

        // Button to Change Update Product Picture
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog myDialouge = new OpenFileDialog();

            if (myDialouge.ShowDialog() == DialogResult.OK)
            {
                userPicPath = myDialouge.FileName;
                pictureBox1.BackgroundImage = Image.FromFile(userPicPath);
            }
        }

        //Method to add Customers to List
        private Customers createCustomerList(int i)
        {
            string adminStatus;
            Customers myCustomer = new Customers();
            if (da.Read())
            {
                adminStatus = da.GetValue(2).ToString();
                myCustomer.Name = "Customer" + i.ToString();
                myCustomer.CustomerLastLogin = da.GetValue(0).ToString();
                myCustomer.CustomerName = da.GetValue(1).ToString();
                if (adminStatus == "True")
                {
                    myCustomer.Status = "Admin";
                    myCustomer.BackColor = Color.FromArgb(255, 255, 192);
                }
                else
                {
                    myCustomer.Status = "Customer";
                    myCustomer.BackColor = Color.FromArgb(192,192,255);
                }
                myCustomer.addImage = Image.FromFile(da.GetValue(3).ToString());
                CustDelete.SetToolTip(myCustomer, "(i) Double Click to Delete Customer \n(ii) Single Click to Update Data");
            }
            return myCustomer;
        }

        // Update Method for Customer Details
        private void updateCustomer(object sender, EventArgs e)
        {
            Customers customerList = (Customers)sender;
            string name = customerList.CustomerName;
            string dataFetch = "Select * from UserData where Username='"+ name +"'"; // Query to get Relevant data to Edit Section
            command = new SqlCommand(dataFetch, con);
            da = command.ExecuteReader();
            da.Read();
            custID = da.GetInt32(0);
            textBox13.Text = da.GetString(1);
            textBox14.Text = da.GetString(3);
            textBox15.Text = da.GetString(4);
            textBox16.Text = da.GetString(2);
            userStatus = da.GetBoolean(5);
            if(userStatus == true)
            {
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                comboBox2.SelectedIndex = 1;
            }
            richTextBox4.Text = da.GetString(8);
            string userPicPath = da.GetString(6);
            pictureBox3.BackgroundImage = Image.FromFile(userPicPath); 
            da.Close();
        }

        // Update User Button
        private void button10_Click(object sender, EventArgs e)
        {
            string Username = textBox13.Text;
            string Password = textBox14.Text;
            string Contactno = textBox15.Text;
            string Email = textBox16.Text;
            string UserAddress = richTextBox4.Text;
            //MessageBox.Show(stock);
            if ((Username != "" && Password != "" && Contactno != "" && Email != "" && UserAddress != ""))
            {
                string custUpdate = "update UserData set Username = '"+Username+"', Email = '"+Email+"', Password = '"+Password+"', Contact = '"+Contactno+"', isAdmin = '"+updateStatus+"', PicPath = '"+ updateCust + "'," +
                    " UserAddress = '"+ UserAddress +"' where ID = " + custID + "";
                using (command = new SqlCommand(custUpdate, con))
                {
                    command.ExecuteNonQuery();

                }
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
                textBox16.Text = "";
                richTextBox4.Text = "";
                updateStatus = false;
                updateCust = "";
                comboBox2.SelectedItem = null;
                pictureBox3.BackgroundImage = Image.FromFile(@"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_instagram_50px.png");
            }
            else
            {
                MessageBox.Show("You can't update with a null field");
            }
        }

        //Updating Customer Status
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                updateCust = @"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_administrator_male_50px.png";
                updateStatus = true;
                pictureBox3.BackgroundImage = Image.FromFile(@"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_administrator_male_50px.png");
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                updateCust = @"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_user_50px.png";
                updateStatus = false;
                pictureBox3.BackgroundImage = Image.FromFile(@"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_user_50px.png");
            }
        }

        // Delete Customer Method   
        private void DeleteCustomer(object sender, EventArgs e)
        {
            Customers customerList = (Customers)sender;
            string ItemName = customerList.CustomerName;
            foreach (Control item in CustPanelSlide.Controls.OfType<Control>())
            {
                if (item.Name == customerList.Name)
                    CustPanelSlide.Controls.Remove(item);
            }
            string deleteCustomer = "delete from UserData where Username = '"+ ItemName +"'";
            using (command = new SqlCommand(deleteCustomer, con))
            {
                command.ExecuteNonQuery();
            }
        }

        // Method to Create Product
        private void button9_Click(object sender, EventArgs e)
        {
            string stock = textBox5.Text;
            string productPrice = textBox10.Text;
            string productName = textBox11.Text;
            string productID = textBox12.Text;
            string productDesc = richTextBox3.Text;
            string picPath = createProduct;
            if (stock != null && productPrice != null && productName != null && productID != null && productDesc != null && picPath != null)
            {
                string dataInsert = "insert into Products values('"+ productID +"','"+ productName +"','"+ productDesc +"','"+ productPrice +"','"+ picPath +"','"+ stock +"')";
                using (command = new SqlCommand(dataInsert, con))
                {
                    command.ExecuteNonQuery();
                }
                textBox5.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                richTextBox3.Text = "";
                pictureBox2.BackgroundImage = Image.FromFile(@"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_instagram_50px.png");
            }
            else
            {
                MessageBox.Show("Fill all the available options please!");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
           
        }

        // Create Product Picture
        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog myDialouge = new OpenFileDialog();

            if (myDialouge.ShowDialog() == DialogResult.OK)
            {
                createProduct = myDialouge.FileName;
                pictureBox2.BackgroundImage = Image.FromFile(createProduct);
            }
        }
        // Set Create customer picture
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                createCust = @"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_administrator_male_50px.png";
                createStatus = true;
                pictureBox4.BackgroundImage = Image.FromFile(@"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_administrator_male_50px.png");
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                createCust = @"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_user_50px.png";
                createStatus = false;
                pictureBox4.BackgroundImage = Image.FromFile(@"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_user_50px.png");
            }
           
        }

        // Method to Create Customer
        private void button7_Click(object sender, EventArgs e)
        {
            string Username = textBox9.Text;
            string Password = textBox8.Text;
            string Contactno = textBox7.Text;
            string Email = textBox6.Text;
            string UserAddress = richTextBox2.Text;
            if (Username != "" && Password != "" && Contactno != "" && Email != "" && UserAddress != "")
            {
                if(comboBox1.SelectedIndex >= 0)
                {
                    string dataInsert = "insert into UserData values('"+Username+"','"+Email+"','"+Password+"','"+Contactno+"','"+ createStatus +"','"+createCust+"','"+ null +"','"+ UserAddress +"')";
                    using (command = new SqlCommand(dataInsert, con))
                    {
                        command.ExecuteNonQuery();
                    }
                    textBox9.Text = "";
                    textBox8.Text = "";
                    textBox7.Text = "";
                    textBox6.Text = "";
                    richTextBox2.Text = "";
                    createStatus = false;
                    createCust = "";
                    comboBox1.SelectedItem = null;
                    pictureBox4.BackgroundImage = Image.FromFile(@"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_instagram_50px.png");
                } 
            }
            else
            {
                MessageBox.Show("Fill all the available options please!");
            }
        }

        // Button that generates the Information Regarding Application
        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("this app is under Development");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminTimer_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 BackToForm = new Form1();
            this.Hide();
            BackToForm.Show(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        private void ProductPanel_Tick(object sender, EventArgs e)
        {
           
        }
        private void CustomerPanel_Tick(object sender, EventArgs e)
        {
           
        }

        private void customers1_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
             
        }

        

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void CustPanelSlide_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
