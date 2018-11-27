using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_20181124
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        List<string> expresions = new List<string>();  //初始化字符串存储
        public MainWindow()
        {                
            InitializeComponent();



      
            try                                                                                                     //启动程序余额栏显示最后一次的金额，读取字符串
            {
                FileStream afile = new FileStream("history.txt",  FileMode.OpenOrCreate);   //@"C:\Users\suanjuzi\Desktop\Wpf_20181114-master\Wpf_20181124\bin\Debug\
                StreamReader sr = new StreamReader(afile);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("USD"))   //美元显示
                    {                       
                        string[] sline = line.Trim().Split(' ');    //删除字符串首尾字符，以空格分割（不包含空格）组成数组
                        USDBalance.Content = sline[6];
                        //sr.Close();
                    }
                    if (line.Contains("CNY"))       //人民币显示
                    {   
                        //string s = sr.ReadLine();  这句是读取当前行的下一行                          
                        string[] sline = line.Trim().Split(' ');    //删除字符串首尾字符，以空格分割（不包含空格）组成数组
                        CNYBalance.Content = sline[6];
                        //sr.Close();
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("404");
                MessageBox.Show(e.ToString());
            }


            string[] accountTypeArr = new string[] { "借记卡", "贷记卡" };          //卡型下拉框
            accountTypeBox.ItemsSource = accountTypeArr;                          //设置下拉菜单的可选项
            accountTypeBox.SelectedIndex = 0;                                                //默认选中第0项
        }

    
        private void InputAmount_TextChanged(object sender, TextChangedEventArgs e)                  //操作金额限制为数字输入
        {             
            TextBox temptbox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);                                       //得到Change的内容
            int offset = change[0].Offset;                                            //得到Change的偏置值(offset) 可理解为变化的起始位置
            if (change[0].AddedLength > 0)                                         //如果是内容增加 则执行
            {
                int num;                                                                        //其实没啥用 但是没这个变量TryParse函数不能用
                if (temptbox.Text.IndexOf(' ') != -1 || !int.TryParse(temptbox.Text, out num))                //Text.IndexOf检测某字符首次出现的位置，此处用来检测是否有空格
                {                                                                                                                                     //int.TryParse返回是字符串是否转为数字，此处用来检测字符串是纯数字                    
                    temptbox.Text = temptbox.Text.Remove(offset, change[0].AddedLength);              //去除change
                    temptbox.Select(offset, 0);　　　　　　　　　　　　　　　　　　　　　　　  　//恢复原状
                }
            }
        }
        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {           
            if (InputAmount.Text == "")                                                    //输入金额不为空
            {
                MessageBox.Show("请输入操作金额！");
                return;
            }
            
            if (DepositRadioBtn.IsChecked == true)                                 //存钱
            {
                if (CNYRadioBtn.IsChecked == true )
                {
                    CNYBalance.Content =  Convert.ToDouble(CNYBalance.Content) + Convert.ToDouble(InputAmount.Text);
                    expresions.Add("Operate(deposit CNY ------): "  + "depposit " + InputAmount.Text + " balance " + CNYBalance.Content + "        " + DateTime.Now);   // DateTime.Now.ToLongDateString()
                }
                else
                 {
                     USDBalance.Content =Convert.ToDouble(USDBalance.Content) + Convert.ToDouble(InputAmount.Text);
                    expresions.Add("Operate(deposit USD ------): " + "depposit " + InputAmount.Text + " balance " + USDBalance.Content + "        " + DateTime.Now);

                }
            }
            else                                                                                      //取钱
             {
                if (accountTypeBox.SelectedItem.ToString() == "借记卡")
                {
                    if (CNYRadioBtn.IsChecked == true)
                    {
                        if (Convert.ToDouble(CNYBalance.Content) - Convert.ToDouble(InputAmount.Text) >= 0)
                        {
                            CNYBalance.Content = Convert.ToDouble(CNYBalance.Content) - Convert.ToDouble(InputAmount.Text);
                            expresions.Add("Operate(withdraw CNY Debit): " + "withdraw " + InputAmount.Text + " balance " + CNYBalance.Content + "        " + DateTime.Now);

                        }
                        else
                        {
                            MessageBox.Show("借记卡透支，请重新输入操作金额！"); 
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(USDBalance.Content) - Convert.ToDouble(InputAmount.Text) >= 0)
                        {
                            USDBalance.Content = Convert.ToDouble(USDBalance.Content) - Convert.ToDouble(InputAmount.Text);
                            expresions.Add("Operate(withdraw USD Debit): " + "withdraw " + InputAmount.Text + " balance " + USDBalance.Content + "        " + DateTime.Now);

                        }
                        else
                        {
                            MessageBox.Show("借记卡透支，请重新输入操作金额！");
                        }
                    }
                }
                else
                {
                    if (CNYRadioBtn.IsChecked == true)
                    {
                        CNYBalance.Content = Convert.ToDouble(CNYBalance.Content) - Convert.ToDouble(InputAmount.Text);
                        expresions.Add("Operate(withdraw CNY Credit): " + "withdraw " + InputAmount.Text + " balance " + CNYBalance.Content + "        " + DateTime.Now);

                    }
                    else
                    {
                        USDBalance.Content = Convert.ToDouble(USDBalance.Content) - Convert.ToDouble(InputAmount.Text);
                        expresions.Add("Operate(withdraw USD Credit): " + "withdraw " + InputAmount.Text + " balance " + USDBalance.Content + "        " + DateTime.Now);

                    }
                }
                        
             }
            if (CheckBox.IsChecked == true)
            {
                MessageBox.Show("打印凭条");
            }
        }
        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileStream SaveFile = new FileStream("History.txt", FileMode.Append);   //打开或新建,尾部追加
                StreamWriter streamWriter = new StreamWriter(SaveFile);                // StreamWriter streamWriter = new StreamWriter(history.txt,true);
                foreach (string a in expresions)                     //写入
                {
                    streamWriter.WriteLine(a);
                }
                streamWriter.Close();                  //停止写
            }
            catch (IOException ex)
            {
                MessageBox.Show("An IO exception has been thrown!");
                MessageBox.Show(ex.ToString());
              //  return;
            }
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {                    
            Application.Current.Shutdown();         //退出程序
         }

        
    }
}
