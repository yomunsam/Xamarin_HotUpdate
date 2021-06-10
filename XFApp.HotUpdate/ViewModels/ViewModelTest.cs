using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFApp.HotUpdate.ViewModels
{
    public class ViewModelTest : INotifyPropertyChanged
    {
        string test;
        public string Test
        {
            get => test;
            set
            {
                if (value != test)
                {
                    test = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Test"));
                }
            }
        }
        public ViewModelTest()
        {
            Test = "Test ViewModel";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(this, eventArgs);
        }
    }
}
