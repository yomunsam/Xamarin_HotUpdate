using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp.HotUpdate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotUpdatePage : ContentPage
    {
        public HotUpdatePage()
        {
            InitializeComponent();
        }
    }

    public class TestButton : Button
    {
        public static string ButtonClick(object sender, EventArgs e)
        {
            (sender as Button).Text = Guid.NewGuid().ToString();
            return (sender as Button).Text;
        }
    }
}