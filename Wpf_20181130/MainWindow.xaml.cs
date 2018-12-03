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
using System.IO.Ports;       

namespace Wpf_20181130
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        

        public MainWindow()
        {
          

            InitializeComponent();
            
        }
        

        private void OpenSerialButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseSerialButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendCommandTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BaudRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PortName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SerialPort serialPort = new SerialPort();
        }

       
    }
}
