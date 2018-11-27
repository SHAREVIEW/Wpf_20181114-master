using System;
using System.Collections.Generic;
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

namespace Wpf_20181114
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string[] accountTypeArr = new string[] { "借记卡", "贷记卡" };
            accountTypeBox.ItemsSource = accountTypeArr; //设置下拉菜单的可选项
            accountTypeBox.SelectedIndex = 0;  //默认选中第0项

        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {

            if (CheckBox.IsChecked == true)
            {
                MessageBox.Show("打印凭条");
            }
            if (InputAmount.Text == "")
            {
                MessageBox.Show("请输入操作金额！");
                // return;
            }

        }

        private void InputAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //操作金额限制为数字输入
            TextBox temptbox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);                     //得到Change的内容
            int offset = change[0].Offset;                    //得到Change的偏置值(offset) 可理解为变化的起始位置
            if (change[0].AddedLength > 0)                                  //如果是内容增加 则执行
            {
                int num;                                                    //其实没啥用 但是没这个变量TryParse函数不能用
                if (temptbox.Text.IndexOf(' ') != -1 || !int.TryParse(temptbox.Text, out num))  //Text.IndexOf检测某字符首次出现的位置，此处用来检测是否有空格
                {                                                                           //int.TryParse返回是字符串是否转为数字，此处用来检测字符串是纯数字


                    temptbox.Text = temptbox.Text.Remove(offset, change[0].AddedLength);//去除change
                    temptbox.Select(offset, 0);　　　　　　　　　　　　　　　　　　　　　　　　//恢复原状
                }
            }
        }


        class Account
        {
            Account(CardType type)
                
            {
                Type = type;
            }

            public enum CardType
            {
                DebitCard
            }

            public CardType Type { get; }           
            public double RMB { get; set; }
            public double Dollar { get; set; }
            public string Name { get; }

            public void Balance()
            {

            }
        }

        



        public class Bank
        {
            public string account;
            private double balance = 0;

            public int MyProperty { get; set; }
            public double Balance   //字段(变量)
            {
                get { return Balance; }   //属性
            }
            public void Save(double money) //存钱
            {
                balance += money; 
            }
            public void Draw(double money)//取钱
            {
                if(account=="借记卡")
                {
                    if (balance > money)
                    {
                        balance -= money;                        
                    }
                    else
                    {
                        MessageBox.Show("借记卡余额透支！");//return false;
                     }
                }

                else
                {
                        balance -= money;
                }              
                
            }

        }
    }

}
        


        //  private void operation()
        //{
        //  if(accountTypeBox.SelectedIndex != 0);








        /*
        private void MoneyLogic()
        {

            string strMsg = accountTypeBox.SelectedValue.ToString();
            ComboBoxItem item = (ComboBoxItem)accountTypeBox.SelectedItem;
            if (item.Content.ToString() == "借记卡")
            {
                //存款
                if (rdb_ck.IsChecked == true)
                {
                    if (item.Content.ToString() == "人民币")
                    {
                        cnyTotalBalance.Text += Convert.ToDouble(InputAmount.Text);
                    }
                    else
                    {
                        usdTotalBalance.Text += Convert.ToDouble(InputAmount.Text);
                    }
                    if (rdb_qk.IsChecked == true)
                    {
                        if (item.Content.ToString() == "人民币")
                        {
                           // cnyTotalBalance.Text -= Convert.ToDouble(InputAmount.Text);
                        }
                    }
                }
            }

        }

        private void rdb_ck_Checked(object sender, RoutedEventArgs e)
        {
            MoneyLogic();
        }

        private void rdb_qk_Checked(object sender, RoutedEventArgs e)
        {
            MoneyLogic();
        }
        */
   
