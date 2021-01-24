using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Konata;
using Xamarin.Forms;
using XFApp.ViewModels;

namespace XFApp
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            var context = KCore.MainInstance.Services.Get<MainPageViewModel>();
            this.BindingContext = context;
        }
    }
}
