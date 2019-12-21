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
    public partial class Form1 : Form
    {
        int panelWidth;
        bool Hidden;

        // Sql Connection Variables
        SqlConnection con;
        SqlDataAdapter adp;
        SqlCommand cb;
        SqlCommand command;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        SqlDataReader da;
        int ItemCounter;
        int cartItemID;
        int countProds;
        int countItems;
        int cartItemsCount;
        int SumOfProducts;
        string DateToday;
        string TimeToday;

        public Form1()
        {
            
            InitializeComponent();
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss");
            panelWidth = 265;
            Hidden = false;
           

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=DESKTOP-9DVROAL\SQLEXPRESS;Initial Catalog=myWorkSpace;Integrated Security=True");
            adp = new SqlDataAdapter();
            ds = new DataSet();

            string prodCounts = "SELECT COUNT(*) FROM Products"; //Access All the products from the Database
            string itemCounts = "SELECT COUNT(*) FROM Cart where UserID = '"+ Program.Username +"' AND SaleType = 'InCart'"; // To Count Items Associated with the User
            string SumQuery = "select SUM(price) from Cart where UserID = '" + Program.Username + "' And Saletype = 'InCart'";
            string items = "select * from Cart where UserID = '" + Program.Username + "' And Saletype = 'InCart'";
            countProds = 0; // count of all Products
            countItems = 0; // Count of all Items in Cart
            ItemCounter = 1;
            cartItemsCount = 0;

           
            ProfToolTip.SetToolTip(button1, "Click to view your Profile");
            ProfToolTip.ToolTipTitle = "Your Profile : "+ Program.Username +"";

            if (Program.Username == "Anonymous")
            {
                LogoutTip.SetToolTip(button4, "Click To Login");
                LogoutTip.ToolTipTitle = "Join the Community!";
            }
            else
            {
                LogoutTip.SetToolTip(button4, "Click To Logout");
                LogoutTip.ToolTipTitle = "You are Logged in!";
            }
            
            CartTip.SetToolTip(button2, "Press To View Cart");
            CartTip.ToolTipTitle = "Cart";

            con.Open();

            adp = new SqlDataAdapter(items, con);
            dt = new DataTable();
            adp.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                using (command = new SqlCommand(SumQuery, con))
                {
                    SumOfProducts = (int)command.ExecuteScalar();
                    label7.Text = SumOfProducts.ToString();
                }
            }
            dt.Clear();
            // Count of records in database
            using (command = new SqlCommand(prodCounts, con))
            {
                countProds = (int)command.ExecuteScalar();
            }
            // Count of Items in Cart database
            using (command = new SqlCommand(itemCounts, con))
            {
                countItems = (int)command.ExecuteScalar();
            }

            informText.Text = "Items: " + countItems;

            // Creating Dynamic Products from Database
            command = new SqlCommand("Select ProdID, ProdName, ProdDesc, ProdPrice, PicName, Stock from Products", con);
            da = command.ExecuteReader();   
            for (int i = 1; i <= countProds; i++)
            {
                Product myControl = AddmyControl(i);
                flowLayoutPanel1.Controls.Add(myControl);
                myControl.myClick += new EventHandler(ProdBuyClick);
            }
            da.Close();

            // Creating dynamic Items from database
            command = new SqlCommand("Select ProdID, ItemName, ProdQuantity, Price, ID from Cart where UserID='" + Program.Username + "' AND SaleType = 'InCart' ", con);
            da = command.ExecuteReader();
            for (int i = 1; i <= countItems; i++)
            {
                CartItem myItem = AddCartItem(i);
                PanelSlide.Controls.Add(myItem);
                myItem.DeleteClick += new EventHandler(ItemDeleteClick);
            }
            con.Close();
            if (Program.AdminUser != "Null")
            {
                button5.Visible = true;
            }
           

        }
        // Product Buy Button Click Method
        private void ProdBuyClick(object sender, EventArgs e)
        {  

            Product myProduct = (Product)sender;
            CartItem myItem = new CartItem();
          
            string insertionQuery = "Insert into Cart values('" + myProduct.prodName + "','" + ItemCounter  + "','" + myProduct.prodPrice + "','" + Program.Username + "', 'InCart' , '"+ myProduct.prodID +"')";
            string lastItem = "Select ProdID, ItemName, ProdQuantity, Price, ID from Cart where id=(Select max(ID) from Cart)";
            string itemCounts = "SELECT COUNT(*) FROM Cart where UserID = '" + Program.Username + "' AND SaleType = 'InCart'";
            string SumQuery = "select SUM(price) from Cart where UserID = '"+ Program.Username +"' And Saletype = 'InCart'";
            con.Open();
            
            command = new SqlCommand(insertionQuery, con);
            command.ExecuteNonQuery();

            using (command = new SqlCommand(itemCounts, con))
            {
                countItems = (int)command.ExecuteScalar();
            }

            using (command = new SqlCommand(SumQuery, con))
            {
                SumOfProducts = (int)command.ExecuteScalar();
            }
            label7.Text = SumOfProducts.ToString();
            
            command = new SqlCommand(lastItem, con);
            da = command.ExecuteReader();
            da.Read();
            myItem.Name = da.GetValue(4).ToString();
            myItem.ItemID = da.GetValue(0).ToString();
            myItem.ItemName = da.GetValue(1).ToString();
            myItem.ItemQuantity = da.GetValue(2).ToString();
            myItem.ItemPrice = da.GetValue(3).ToString();
            myItem.DeleteClick += new EventHandler(ItemDeleteClick);
            informText.Text = "Items: " + countItems;
            PanelSlide.Controls.Add(myItem);
            da.Close();

            con.Close();
            informText.ForeColor = Color.Green;
            
        }
        //Item Delete Click Method
        private void ItemDeleteClick(object sender, EventArgs e)
        {
            CartItem cartItem = (CartItem)sender;
            string ItemName = cartItem.Name;
            foreach (Control item in PanelSlide.Controls.OfType<Control>())
            {
                if (item.Name == cartItem.Name)
                    PanelSlide.Controls.Remove(item);
            }
            string DeleteQuery = "Delete from Cart where UserID = '"+ Program.Username +"' AND ID = '"+ cartItem.Name +"'";
            string SumQuery = "select SUM(price) from Cart where UserID = '" + Program.Username + "' And Saletype = 'InCart'";
            string itemCount = "select * from Cart where UserID = '" + Program.Username + "' And Saletype = 'InCart'"; 
            con.Open();
            command = new SqlCommand(DeleteQuery, con);
            command.ExecuteNonQuery();
            adp = new SqlDataAdapter(itemCount, con);
            dt = new DataTable();
            adp.Fill(dt);
            informText.Text = "Items: " + dt.Rows.Count.ToString();
            if(dt.Rows.Count > 0)
            {
                command = new SqlCommand(SumQuery, con);
                SumOfProducts = (int)command.ExecuteScalar();
                label7.Text = SumOfProducts.ToString();
            }
            else
            {
                label7.Text = dt.Rows.Count.ToString();
            }
            informText.ForeColor = Color.Red;
            con.Close();
        }

        // My Control Properties
        Product AddmyControl(int i)
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
                BuyToolTip.SetToolTip(myControl, "Click Empty Space To Add To Cart");
             //  myControl.ButtonClick += new EventHandler(product3_ButtonClick);
            }
            return myControl;
        }

        // Cart Item Properties
        CartItem AddCartItem(int i)
        {
            CartItem myItem = new CartItem();
            if (da.Read())
            {
                myItem.Name = da.GetValue(4).ToString();
                myItem.ItemID = da.GetValue(0).ToString();
                myItem.ItemName = da.GetValue(1).ToString();
                myItem.ItemQuantity= da.GetValue(2).ToString();
                myItem.ItemPrice = da.GetValue(3).ToString();
                RemoveItem.SetToolTip(myItem, "Click To Remove From Cart");
            }
            return myItem;
        }
        // Message box about the info for Application
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this app is under Development");
        }

        // Button to buy items from Cart
        private void button8_Click(object sender, EventArgs e)
        {
            DateToday = DateTime.Now.ToString("dd/MM/yyyy");
            TimeToday = DateTime.Now.ToString("HH:MM:ss");
            con.Open();
            string BuyingQuery = "Update Cart Set SaleType = 'Purchased' where UserID = '" + Program.Username + "' AND SaleType = 'InCart'";
            string BuyingInsertion = "insert into Delivery values('"+Program.UserID+ "','" + Program.Username + "','" + countItems + "','" + SumOfProducts + "','" + DateToday + "','" + TimeToday + "')";
            string itemCount = "select * from Cart where UserID = '" + Program.Username + "' And Saletype = 'InCart'";

            adp = new SqlDataAdapter(itemCount, con);
            dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                command = new SqlCommand(BuyingInsertion, con);
                command.ExecuteNonQuery();

                using (command = new SqlCommand(BuyingQuery, con))
                {
                    command.ExecuteNonQuery();
                }

                PanelSlide.Controls.Clear();
                PanelSlide.Controls.Add(pictureBox1);
                PanelSlide.Controls.Add(panel3);
                PanelSlide.Controls.Add(panel4);
                informText.Text = "Items : 0";
                label7.Text = 0.ToString();
            }
            else
            {
                MessageBox.Show("There are no Items in the cart");
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Program.Username == "Anonymous")
            {
                User userPanel = new User();
                this.Hide();
                userPanel.Show();
            }
            else
            {
                User Logout = new User();
                this.Hide();
                Logout.Show();
                Program.Username = "Anonymous";
                Program.AdminUser = "Null";
            }
        }
        // to view your profile form
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminBoard backToDashboard = new AdminBoard();
            this.Hide();
            backToDashboard.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy HH:MM:ss");
        }

       

        private void ProfButton_Tick(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cartTimer.Start();
        }

        private void cartTimer_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                PanelSlide.Width = PanelSlide.Width + 5;
                if (PanelSlide.Width >= panelWidth)
                {
                    cartTimer.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                PanelSlide.Width = PanelSlide.Width - 5;
                if (PanelSlide.Width <= 0)
                {
                    cartTimer.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        private void product1_Click(object sender, EventArgs e)
        {

        }

        private void product1_Load(object sender, EventArgs e)
        {

        }

        private void product1_Load_1(object sender, EventArgs e)
        {

        }

        private void product3_ButtonClick(object sender, EventArgs e)
        {
            
        }

        private void PanelSlide_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void product3_Load(object sender, EventArgs e)
        {

        }

        private void product3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
