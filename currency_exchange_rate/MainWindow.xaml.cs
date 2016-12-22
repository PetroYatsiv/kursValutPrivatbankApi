using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.IO;
using System.Web.Script.Serialization;

namespace currency_exchange_rate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        static List<Valute> getjson()
        {
            string url = "https://api.privatbank.ua/p24api/pubinfo?exchange&json&coursid=11";
            string jsonString;
            string data = "";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            

            using (StreamReader streamReader = new StreamReader(dataStream))
            {
                data = streamReader.ReadToEnd();
                streamReader.Close();
            }
            jsonString = data;

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<Valute> listvalute = (List<Valute>)javaScriptSerializer.Deserialize(jsonString, typeof(List<Valute>));

            return listvalute;
        }
         
        private void LeftGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Valute usd = new Valute();
            Valute eur = new Valute();
            Valute rub = new Valute();
            List<Valute> listvalute = getjson();
            
            usd = listvalute[0];
            eur = listvalute[1];
            rub = listvalute[2];

            textBox3_Copy8.Text = usd.ccy;
            textBox3_Copy2.Text = usd.buy;
            textBox3_Copy3.Text = usd.sale;

            textBox3_Copy.Text = eur.ccy;
            textBox3_Copy4.Text = eur.buy;
            textBox3_Copy5.Text = eur.sale;

            textBox3_Copy1.Text = rub.ccy;
            textBox3_Copy6.Text = rub.buy;
            textBox3_Copy7.Text = rub.sale;

        }

       
        private void button_Click(object sender, RoutedEventArgs e)
        {
            double summa;
            if (textBoxSuma.Text == "")
            {
                summa = 0.0;
            }
            else
            {
                summa = Convert.ToDouble(textBoxSuma.Text);
            }

            List<Valute> listvalutes = getjson();

            Valute usd = new Valute();
            Valute eur = new Valute();
            Valute rub = new Valute();

            usd = listvalutes[0];
            eur = listvalutes[1];
            rub = listvalutes[2];
            
            //купити
            if (radioButton.IsChecked == true)
            {
                switch (comboBox.Text)
                {
                    case "EUR":
                        textBox.Text = Convert.ToString(summa * Convert.ToDouble(eur.buy.Replace(".", ",")) / Convert.ToDouble(usd.sale.Replace(".", ","))).Remove(6);//usd
                        textBox1.Text = Convert.ToString(summa);//eur
                        textBox2.Text = Convert.ToString(summa * Convert.ToDouble(eur.sale.Replace(".", ",")) / Convert.ToDouble(rub.sale.Replace(".", ","))).Remove(6);//rub
                        textBox4.Text = Convert.ToString(summa * Convert.ToDouble(eur.sale.Replace(".", ",").Remove(6)));//uah
                        break;
                    case "USD":
                        textBox.Text = textBoxSuma.Text;
                        textBox2.Text = Convert.ToString(summa * Convert.ToDouble(usd.sale.Replace(".", ",")) / Convert.ToDouble(rub.buy.Replace(".", ","))).Remove(6);
                        textBox1.Text = Convert.ToString(summa * Convert.ToDouble(usd.sale.Replace(".", ",")) / Convert.ToDouble(eur.buy.Replace(".", ","))).Remove(6);
                        textBox4.Text = Convert.ToString(summa * Convert.ToDouble(usd.sale.Replace(".", ","))).Remove(6);
                        break;
                    case "RUB":
                        textBox.Text = Convert.ToString((summa * Convert.ToDouble(rub.sale.Replace(".", ","))) / (Convert.ToDouble(usd.sale.Replace(".", ",")))).Remove(6);
                        textBox1.Text = Convert.ToString((summa * Convert.ToDouble(rub.sale.Replace(".", ","))) / (Convert.ToDouble(eur.sale.Replace(".", ",")))).Remove(6);
                        textBox2.Text = textBoxSuma.Text;
                        textBox4.Text = Convert.ToString(summa * Convert.ToDouble(rub.sale.Replace(".",",")));
                        break;
                    case "UAH":
                        textBox.Text = Convert.ToString(summa/Convert.ToDouble(usd.sale.Replace(".", ","))).Remove(6);
                        textBox1.Text = Convert.ToString(summa/Convert.ToDouble(eur.sale.Replace(".", ","))).Remove(6);
                        textBox2.Text = Convert.ToString(summa/Convert.ToDouble(rub.sale.Replace(".", ","))).Remove(6);
                        textBox4.Text = textBoxSuma.Text;//uah
                        break;
                }
            }
            //продати
            else if (radioButton1.IsChecked == true)
            {
                switch (comboBox.Text)
                {
                    case "EUR":
                        textBox.Text = Convert.ToString(summa * Convert.ToDouble(eur.sale.Replace(".", ",")) / Convert.ToDouble(usd.buy.Replace(".", ","))).Remove(6);//usd
                        textBox1.Text = Convert.ToString(summa);//eur
                        textBox2.Text = Convert.ToString(summa * Convert.ToDouble(eur.buy.Replace(".", ",")) / Convert.ToDouble(rub.buy.Replace(".", ","))).Remove(6);//rub
                        textBox4.Text = Convert.ToString(summa * Convert.ToDouble(eur.buy.Replace(".", ",").Remove(6)));//uah
                        break;
                    case "USD":
                        textBox.Text = textBoxSuma.Text;
                        textBox2.Text = Convert.ToString(summa * Convert.ToDouble(usd.buy.Replace(".", ",")) / Convert.ToDouble(rub.sale.Replace(".", ","))).Remove(6);
                        textBox1.Text = Convert.ToString(summa * Convert.ToDouble(usd.buy.Replace(".", ",")) / Convert.ToDouble(eur.sale.Replace(".", ","))).Remove(6);
                        textBox4.Text = Convert.ToString(summa * Convert.ToDouble(usd.buy.Replace(".", ",")));

                        break;
                    case "RUB":
                        textBox.Text = Convert.ToString((summa * Convert.ToDouble(rub.buy.Replace(".", ","))) / (Convert.ToDouble(usd.buy.Replace(".", ",")))).Remove(6);
                        textBox1.Text = Convert.ToString((summa * Convert.ToDouble(rub.buy.Replace(".", ","))) / (Convert.ToDouble(eur.buy.Replace(".", ",")))).Remove(6);
                        textBox2.Text = textBoxSuma.Text;
                        textBox4.Text = Convert.ToString(summa * Convert.ToDouble(rub.buy.Replace(".", ",")));
                        break;
                    case "UAH":
                        textBox.Text = Convert.ToString(summa / Convert.ToDouble(usd.buy.Replace(".", ","))).Remove(6);
                        textBox1.Text = Convert.ToString(summa / Convert.ToDouble(eur.buy.Replace(".", ","))).Remove(6);
                        textBox2.Text = Convert.ToString(summa / Convert.ToDouble(rub.buy.Replace(".", ",")));
                        textBox4.Text = textBoxSuma.Text;//uah
                        break;
                }
            }
        }
    }



    class Valute
        {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public string buy { get; set; }
        public string sale { get; set; }
    }

    
    
    
}
