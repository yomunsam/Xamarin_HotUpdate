using System;
using System.Threading.Tasks;
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
            Console.WriteLine("热更层已执行加载UI的操作");
            if(App.NavigationPage == null)
            {
                Console.WriteLine("错误，热更层获取App.NavigationPage为null");
                return;
            }
            await App.NavigationPage.PushAsync(new HotUpdatePage(), true);
        }
    }
}
