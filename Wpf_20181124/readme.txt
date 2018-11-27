



CNYBalance.Content =  Convert.ToDouble(CNYBalance.Content) + Convert.ToDouble(InputAmount.Text);   是对的
          
CNYBalance.Text = Convert.ToString(Convert.ToDouble(CNYBalance.Text) + Convert.ToDouble(InputAmount.Text));    会报错
                  
