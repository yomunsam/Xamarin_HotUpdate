using System;
using Konata;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp.Services;
using XFApp.ViewModels;

namespace XFApp
{
    public partial class App : Application
    {
        public static NavigationPage NavigationPage { get; private set; }

        public App()
        {
            InitializeComponent();

        }

        protected async override void OnStart()
        {
            IKCore core = KCore.New()
                .ConfigureServices(services =>
                {
                    services.Singleton<IHotUpdateService, HotUpdateService>();

                    //ViewModels
                    services.Bind<MainPageViewModel>();
                });
            await core.RunAsync();
            var hotupdate = core.Services.Get<IHotUpdateService>();
            await hotupdate.StartAsync();

            Console.WriteLine("ILRuntime 准备完毕");

            NavigationPage = new NavigationPage(new MainPage());
            MainPage = NavigationPage;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
