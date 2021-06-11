using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Konata;
using Xamarin.Forms;
using XFApp.Services;
using XFApp.ViewModels.Base;
using XFApp.Views;

namespace XFApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        [Inject]
        public IHotUpdateService HotUpdateService { get; set; }

        public Command OpenLocalPage { get; set; }

        public Command OpenILPage { get; set; }

        public MainPageViewModel()
        {
            OpenLocalPage = new Command(OnOpenLocalPageClicked);
            OpenILPage = new Command(OnOpenILPageClicked);
        }


        private async void OnOpenLocalPageClicked(object obj)
        {
            await App.NavigationPage.PushAsync(new Home(), true);
        }

        private async void OnOpenILPageClicked(object obj)
        {
            try
            {
                var result = this.HotUpdateService.ILRuntimeAppDomain.Invoke("XFApp.HotUpdate.Main", "OpenUIAsync", null, null);
                if (result is Task)
                {
                    await (result as Task);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
