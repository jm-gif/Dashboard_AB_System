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

namespace Dashboard_AB_System.Windows
{
    /// <summary>
    /// Interaction logic for Window_3.xaml
    /// </summary>
    public partial class Window_3 : Window
    {
        public Window_3()
        {
            InitializeComponent();
        }

        private void Back_2_DashBoard_Click(object sender, RoutedEventArgs e)
        {
            DashBoard dashBoard = new DashBoard();
            dashBoard.Show();

            this.Close();
        }
    }
}
