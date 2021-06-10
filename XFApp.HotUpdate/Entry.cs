using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFApp.HotUpdate.Views;

namespace XFApp.HotUpdate
{
    public class Entry
    {
        public static string SayHello()
        {
            Console.WriteLine("hello");
            return "Hello";
        }

        public static async Task OpenUIAsync()
        {
            Console.WriteLine("OpenUIAsync");
            App.Current.MainPage = new HotUpdatePage();
            Console.WriteLine("done OpenUIAsync");
        }
    }
}
