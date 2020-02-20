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

namespace Ex_3_Part_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = sender.ToString() + " was clicked.";
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            //MessageTextBlock.Text = "Mouse on: " + sender.ToString();
            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;
            MessageTextBlock.Text = "Current position: " + x + " : " + y;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            MessageTextBlock.Text = string.Empty;
        }

        private void Grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            MessageTextBlock.Text = "PREVIEW from: " + sender.ToString();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text += "\nGRID was clicked";
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text += "\nSTACKPANEL was clicked";
        }
    }
}
