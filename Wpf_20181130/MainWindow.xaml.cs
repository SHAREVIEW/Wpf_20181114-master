using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO.Ports;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_serialport20181202
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort mySerialPort = new SerialPort();          // 实例化一个串口
        public MainWindow()
        {         

            InitializeComponent();

            Portsets();             // 设置串口


        }

        private void Portsets()
        {            
            foreach (string str in SerialPort.GetPortNames())    // 遍历串口
            {
                myPortName.Items.Add(str);
            }        
           
            string[] BaudRateArr = new string[] { "9600", "115200" };          // 波特率集合
            BaudRate.ItemsSource = BaudRateArr;                          
            BaudRate.SelectedIndex = 0;                                                
            mySerialPort.Parity = Parity.None;                  // 奇偶校验检查协议
            mySerialPort.StopBits = StopBits.One;          // 每个字节的标准停止位数
            mySerialPort.DataBits = 8;                             // 每个字节的标准数据位长度
            //mySerialPort.Handshake = Handshake.None;            
            mySerialPort.NewLine = "\r\n";
            mySerialPort.RtsEnable = true;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);    // 添加数据到达事件
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();   //以字符串形式，读取串口的所有数据(编码方式取决于	Encoding属性的值)     
            //Read(Byte[], Int32, Int32) --  从 串口输入缓冲区读取一些字节，并将那些字节	写入字节数组中指定的偏移量处

            DataReceived.Content = indata;     // 显示
        }

            private void OpenButton_Click(object sender, RoutedEventArgs e)                      // 打开串口
        {
            if (mySerialPort.IsOpen)
            {
                MessageBox.Show("串口不能重复打开！");
            }
            else
            {
                try
                {
                    mySerialPort.PortName = myPortName.Text;             // 串口
                    mySerialPort.BaudRate = int.Parse(BaudRate.Text);   // 波特率
                    mySerialPort.Open();
                }
                catch (Exception err)
                {
                    mySerialPort = new SerialPort();
                    MessageBox.Show(err.Message);
                }
            }
           
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)    // 关闭串口
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.Close();
            }
            else
            {
                MessageBox.Show("串口已关闭");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            int m = 0,  n = 0; // m: 温度发送至25，n: 湿度发送65

            List<byte> WriteBuffer = new List<byte>();  //实例化一个byte列表用于存储


        }
    }
}
