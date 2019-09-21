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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace miniproject
{
    /// <summary>
    /// Interaction logic for splashscreen.xaml
    /// </summary>
    public partial class splashscreen : Window
    {
        DispatcherTimer dpt = new DispatcherTimer();
        public splashscreen()
        {
            InitializeComponent();
            dpt.Tick += new EventHandler(Dpt_tick);
            dpt.Interval = new TimeSpan(0,0,3);
            dpt.Start();
        }

        private void Dpt_tick(object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            dpt.Stop();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
