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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace miniproject
{
    /// <summary>
    /// Interaction logic for Attendance_page.xaml
    /// </summary>
    public partial class Attendance_page : Page
    {
        DataSet ds = new DataSet();
        public Attendance_page()
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nithinkrishna\Desktop\miniproject\miniproject\Database2.mdf;Integrated Security=True;Connect Timeout=30");
                SqlCommand str1 = new SqlCommand("Select Count(*) from Attendance where USN = '" + usn.Text + "' and Sem = '" + sem.Text + "'",conn);
                SqlDataAdapter sda = new SqlDataAdapter(str1);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                conn.Open();
                if (usn.Text.Equals(""))
                {
                    MessageBox.Show("Please enter your usn");
                }
                else 
                {
                    if (dt2.Rows[0][0].ToString() == "1")
                    {
                        SqlCommand cmd3 = new SqlCommand("Update Attendance set Subject1 = Subject1+" + int.Parse(textbox1.Text) + ",Subject2 = Subject2+" + int.Parse(textbox2.Text )+ ",Subject3 = Subject3+" + int.Parse(textbox3.Text) + ",Subject4 = Subject4+" + int.Parse(textbox4.Text )+ ",Subject5 =Subject5+" + int.Parse(textbox5.Text) + ",Subject6 = Subject6+" + int.Parse(textbox6.Text) + " where USN='"+usn.Text+"' ;", conn);
                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("Updated Successfully");
                        conn.Close();
                    }
                    else
                    {
                        SqlCommand cmd4 = new SqlCommand("Insert into Attendance (USN,Sem,Subject1,Subject2,Subject3,Subject4,Subject5,Subject6) values('" + usn.Text + "'," +int.Parse( sem.Text) + "," + int.Parse(textbox1.Text) + "," + int.Parse(textbox2.Text )+ "," + int.Parse(textbox3.Text )+ "," + int.Parse(textbox4.Text )+ "," + int.Parse(textbox5.Text )+ "," + int.Parse(textbox6.Text) + ");", conn);
                        cmd4.ExecuteNonQuery();
                        MessageBox.Show("Inserted Successfully");
                        conn.Close();
                    }
                }
                textbox1.Text = "";
                textbox2.Text = "";
                textbox3.Text = "";
                textbox4.Text = "";
                textbox5.Text = "";
                textbox6.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error connecting to database");
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nithinkrishna\Desktop\miniproject\miniproject\Database2.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlDataAdapter str = new SqlDataAdapter("Select Count(*) from Attendance where USN = '" + usn.Text + "' and Sem = '" + sem.Text + "'",con);
                
                DataTable dt2 = new DataTable();
                str.Fill(dt2);
                SqlCommand cmd = new SqlCommand("Select * From Attendance where USN='"+usn.Text+"'and Sem='"+sem.Text+"'",con);
                if (usn.Text.Equals(""))
                {
                    MessageBox.Show("Please enter your usn");
                }
                else 
                {
                    if (dt2.Rows[0][0].ToString() == "1")
                    {
                        view_attendance va = new view_attendance();
                        va.Show();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        va.datagridview.ItemsSource = dt.DefaultView;
                        sda.Update(dt);
                    }
                    else
                    {
                        MessageBox.Show("Invalid USN and Semester");
                        usn.Text = "";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error connecting to database");
            }
        }
    }
}
