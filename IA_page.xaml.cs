using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Shapes;

namespace miniproject
{
    /// <summary>
    /// Interaction logic for IA_page.xaml
    /// </summary>
    public partial class IA_page : Page
    {
        public IA_page()
        {
            InitializeComponent();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8"
            };
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecteditem = sender as ComboBox;
            string sem = selecteditem.SelectedItem as string;
        }

        private void ComboBox_Loaded1(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>
            {
                "1",
                "2",
                "3"
            };
            var combo1 = sender as ComboBox;
            combo1.ItemsSource = data;
            combo1.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            var selecteditem = sender as ComboBox;
            string ia = selecteditem.SelectedItem as string;
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nithinkrishna\Desktop\miniproject\miniproject\Database2.mdf;Integrated Security=True;Connect Timeout=30Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nithinkrishna\Desktop\miniproject\miniproject\Database2.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand str = new SqlCommand("Select Count(*) from IAA where USN = '" + usn.Text + "' and Sem = " + int.Parse(sem.Text) + " and IA=" + IA.Text + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(str);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (usn.Text.Equals(""))
                {
                    MessageBox.Show("Please enter the USN");
                }
                else
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        SqlCommand cmd = new SqlCommand("Update IAA set Subject1 = " + int.Parse(textbox1.Text) + ",Subject2 = " + int.Parse(textbox2.Text) + ",Subject3 = " + int.Parse(textbox3.Text) + ",Subject4 = " + int.Parse(textbox4.Text) + ",Subject5 =" + int.Parse(textbox5.Text) + ",Subject6 =" + int.Parse(textbox6.Text) + " where USN='" + usn.Text + "' and sem=" + int.Parse(sem.Text) + " and IA=" + int.Parse(IA.Text) + " ;", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Updated Sucessfully");
                        con.Close();

                    }
                    else
                    {
                        SqlCommand cmd2 = new SqlCommand("Insert into IAA (USN,Subject1,Subject2,Subject3,Subject4,Subject5,Subject6,IA,sem) values('" + usn.Text + "'," + int.Parse(textbox1.Text) + "," + int.Parse(textbox2.Text) + "," + int.Parse(textbox3.Text) + "," + int.Parse(textbox4.Text) + "," + int.Parse(textbox5.Text) + "," + int.Parse(textbox6.Text) + "," + int.Parse(IA.Text) + "," + int.Parse(sem.Text) + ");", con);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Inserted Sucessfully");
                        con.Close();
                    }
                    textbox1.Text = "";
                    textbox2.Text = "";
                    textbox3.Text = "";
                    textbox4.Text = "";
                    textbox5.Text = "";
                    textbox6.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Connecting Database" + ex.ToString());
            }
        }

        private void View_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nithinkrishna\Desktop\miniproject\miniproject\Database2.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand str = new SqlCommand("Select Count(*) from IAA where USN = '" + usn.Text + "' and Sem = " +int.Parse( sem.Text) + " and IA="+IA.Text+";", con);
                SqlDataAdapter sda = new SqlDataAdapter(str);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd = new SqlCommand("Select * From IAA where USN = '" + usn.Text + "' and Sem=" + (int.Parse(sem.Text)) + ";", con);
               // cmd.ExecuteNonQuery();
                if (usn.Text == "")
                {
                    MessageBox.Show("Please Enter your USN");
                }
                else
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        view_IA va = new view_IA();
                       // va.Show();
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        va.datagrid.ItemsSource = dt1.DefaultView;
                        sda1.Update(dt1);
                        va.Show();
                    }                    
                    else
                    {
                        MessageBox.Show("Invalid USN and Semester");
                        usn.Text = "";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Connecting Database" + ex.ToString());
            }
        }
    
    }
}
