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
    public partial class User : Form
    {

        SqlConnection con;
        SqlDataAdapter adp;
        SqlCommand command;
        SqlCommand timeInsert;
        SqlDataReader da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        string customerDefault;
        public User()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-9DVROAL\SQLEXPRESS;Initial Catalog=myWorkSpace;Integrated Security=True");
            adp = new SqlDataAdapter();
            ds = new DataSet();
            customerDefault = @"E:\C# Learning\UrwaProject\DairyECommerce\DairyECommerce\Images\icons8_user_50px.png";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime userLoginTime = DateTime.Now;
            string updatingTime = "update Userdata Set LastLogin = '" + userLoginTime + "' where Username = '" + Program.Username + "'";
            con.Open();
            timeInsert = new SqlCommand(updatingTime, con);
            timeInsert.ExecuteNonQuery();
            con.Close();
            Form1 reOpen = new Form1();
            this.Hide();
            reOpen.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime userLoginTime = DateTime.Now;

            Form1 MainForm = new Form1();
            con.Open();
            string LoginQuery = "SELECT Username,Password,isAdmin,ID FROM UserData WHERE username='" + textBox1.Text + "' AND password='" + textBox2.Text + "'";
            string LoginValiditiyQuery = "SELECT COUNT(*) FROM UserData WHERE username='" + textBox1.Text + "' AND password='" + textBox2.Text + "'";
            adp = new SqlDataAdapter(LoginValiditiyQuery, con);
            /* in above line the program is selecting the whole data from table and the matching it with the user name and password provided by user. */
            dt = new DataTable(); //this is creating a virtual table  
            adp.Fill(dt);
            // MessageBox.Show(adminChecker);
            if (dt.Rows[0][0].ToString() == "1")
            {
                command = new SqlCommand(LoginQuery, con);
                da = command.ExecuteReader();
                da.Read();
                Program.Username = da.GetValue(0).ToString();
                Program.UserID = da.GetInt32(3);
                string adminChecker = da.GetValue(2).ToString();

                if (adminChecker == "True")
                {
                    Program.AdminUser = da.GetValue(0).ToString();
                    AdminBoard logToBoard = new AdminBoard();
                    this.Hide();
                    logToBoard.Show();
                }
                else
                {
                    this.Hide();
                    MainForm.Show();
                }
                da.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
            dt.Clear();
            string updatingTime = "update Userdata Set LastLogin = '" + userLoginTime + "' where Username = '" + Program.Username + "'";
            timeInsert = new SqlCommand(updatingTime, con);
            timeInsert.ExecuteNonQuery();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ValidityQuery = "SELECT COUNT(*) FROM UserData WHERE username='" + textBox4.Text + "'";
            adp = new SqlDataAdapter(ValidityQuery, con);
            dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This username is registered, use a different username!");
            }
            else
            {
            var isAdmin = 0;
            string DataInsertionQuery = "insert UserData (Username,Email,Password,Contact,isAdmin,PicPath,UserAddress) values ('" + textBox4.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox7.Text + "','" + isAdmin + "', '"+ customerDefault +"','Default Address')";
            if (textBox4.Text == "" || textBox3.Text == "" || textBox5.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("Please fill the relevant data!");
            }
            else
            {
                    if (textBox5.Text != textBox6.Text)
                    {
                        MessageBox.Show("Password Doesn't Match");
                    }
                    else
                    {
                        con.Open();
                        command = new SqlCommand(DataInsertionQuery, con);
                        command.ExecuteNonQuery();
                        con.Close();
                        panel3.Visible = true;
                        panel2.Visible = false;
                        MessageBox.Show("Successful registration");

                    }
            }
            dt.Clear();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel2.Visible = false;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
