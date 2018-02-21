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
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                
                loadQuote();
                return true;
            });
        }

        async public void loadQuote()
        {
            loading.IsRunning = true;
            await App.Sleep(5000);
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
                txtBTC.Text = String.Format("BTC {0:0.########}", (brlBuy / quote));
                loading.IsRunning = false;
            });
        }
    }
}
