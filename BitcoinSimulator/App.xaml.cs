using System.Threading.Tasks;
using Xamarin.Forms;

namespace BitcoinSimulator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BitcoinSimulatorPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            ((BitcoinSimulatorPage)MainPage).SalvarVlrBrl();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
    }
}
