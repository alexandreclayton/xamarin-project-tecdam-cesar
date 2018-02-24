using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;

namespace BitcoinSimulator
{
    public partial class BitcoinSimulatorPage : ContentPage
    {
        WebRequest request;
        Blinktrade blinktrade;

        public BitcoinSimulatorPage()
        {
            InitializeComponent();
            loadQuote();

            if (Application.Current.Properties.ContainsKey("vlrbrl"))
            {
                txtBrl.Text = Application.Current.Properties["vlrbrl"].ToString();
                slResume.IsVisible = true;
            }

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {   
                loadQuote();
                return true;
            });
        }

        public void loadQuote()
        {
            loading.IsRunning = true;
            Uri uri = new Uri("https://api.blinktrade.com/api/v1/BRL/ticker");
            request = WebRequest.Create(uri);
            request.BeginGetResponse(WebRequestCallback, null);
        }

        void WebRequestCallback(IAsyncResult result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Stream stream = request.EndGetResponse(result).GetResponseStream();
                var jsonSerializer = new DataContractJsonSerializer(typeof(Blinktrade));
                blinktrade = (Blinktrade)jsonSerializer.ReadObject(stream);
                double quote = blinktrade.Buy;
                double brlBuy = Double.Parse(((txtBrl.Text == null) ? "0.0" : txtBrl.Text));
                double btc = (brlBuy / quote);
                lbQuote.Text = String.Format("R$ {0:0.00}", quote);
                txtBTC.Text = String.Format("Você compra {0:0.########} bitcoins", (brlBuy / quote));
                loading.IsRunning = false;
            });
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            loadQuote();
            slResume.IsVisible = (txtBrl.Text != null);
        }

        public void SalvarVlrBrl()
        {
            Application.Current.Properties["vlrbrl"] = txtBrl.Text;
        }
    }
}
