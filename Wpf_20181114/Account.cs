using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_20181114
{
    class Account
    {
        Account(CardType type , string name)    //卡类型
        {
            Type = type;
            Name = name;            
        }
        public enum CardType  //贷记卡
        {
            DebitCard
        }
        public enum Currency   // 币种枚举
        {
            RMB,
            Dollar
        }
        public CardType Type { get; }      //属性
        public string Name { get; }
        public double RMB { get;  }
        public double Dollar { get; }

        private double rmb = 0;      
        private double dollar = 0;           
        public void Deposit(Currency currency, double amount)  //存
        {
            
            
        }
        public void Withdraw(Currency currency, double amount) //取
        {

        }

            

     
    }

}
