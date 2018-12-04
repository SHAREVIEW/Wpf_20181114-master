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
 public partial class MainWindow : Window
    {
        SerialPort mySerialPort = new SerialPort();          // 实例化一个串口
        private StringBuilder builder = new StringBuilder();  // 字符串构造器
        private long received_count = 0;//接收计数   
        private long send_count = 0;//发送计数   


        public MainWindow()
        {         

            InitializeComponent();

            PortSets();             // 设置串口


        }

        private void PortSets()
        {            
            foreach (string str in SerialPort.GetPortNames())    // 遍历串口
            {
                myPortName.Items.Add(str);
            }        
           
            string[] BaudRateArr = new string[] { "9600", "115200" };          // 波特率集合
            BaudRate.ItemsSource = BaudRateArr;                          
            BaudRate.SelectedIndex = 1;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);    // 添加数据到达事件
            mySerialPort.NewLine = "\r\n";
        }
            private void OpenButton_Click(object sender, RoutedEventArgs e)                      // 打开串口
            {
               try
               {
                  mySerialPort.PortName = myPortName.Text;             // 串口名
                  mySerialPort.BaudRate = int.Parse(BaudRate.Text);   // 波特率,字符串型数值转换为整数
                  mySerialPort.Open();
                  OpenButton.IsEnabled = false;   // 重复按无效
                  CloseButton.IsEnabled = true;    
                  SendButton.IsEnabled = true;
               }
                catch (Exception err)
               {               
                  MessageBox.Show(err.Message);
               }
             }         
      
        private void CloseButton_Click(object sender, RoutedEventArgs e)    // 关闭串口
        {
            try
            {
                mySerialPort.Close();
                OpenButton.IsEnabled = true;   // 打开串口按键可用
                CloseButton.IsEnabled = false;
                SendButton.IsEnabled = false;
            }
            catch 
            {
               // MessageBox.Show(err.Message);
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] Data = new byte[1];     //一次发送一个字节
            for (int i = 1; i < (SendTextBox.Text.Length - SendTextBox.Text.Length % 2) / 2; i++)      //输入两个等于一个字节
            {
                Data[0] = Convert.ToByte(SendTextBox.Text.Substring(i * 2, 2), 16);            //对输入框中的内容依次取2个，并转换为16进制byte型数值
                mySerialPort.Write(Data, 0, 1);               //对Data数组中的内容从偏移量为0的位置写入指定1个字节长度的内容
            }
            if (SendTextBox.Text.Length % 2 != 0)          //若输入内容长度不为偶数
            {
                MessageBox.Show("Error");
            }
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = mySerialPort.BytesToRead;     //先记录下来，避免缓存不一致   
            byte[] buf = new byte[n];                  //声明一个临时数组存储当前来的串口数据   
            received_count += n;                    //增加接收计数   
            mySerialPort.Read(buf, 0, n);       //读取缓冲数据   


        }
    }
}

