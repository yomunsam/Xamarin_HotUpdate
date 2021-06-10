using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace XFApp.ILWorks.Adaptors.Buttons
{
    public class ButtonAdaptors: CrossBindingAdaptor
    {
        public override Type BaseCLRType => typeof(Button);

        public override Type AdaptorType => typeof(MyButton);

        public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new MyButton(appdomain, instance);
        }

        class MyButton : Button, CrossBindingAdaptorType
        {
            ILTypeInstance instance;
            AppDomain appdomain;
            public ILTypeInstance ILInstance => this.instance;


            IMethod mOnAppearing;
            bool mOnAppearingGot;
            bool mOnAppearingInvoking;

            public MyButton() { }
            public MyButton(AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
                this.Clicked += MyButton_Clicked;
            }

            private void MyButton_Clicked(object sender, EventArgs e)
            {
                var result = this.appdomain.Invoke("XFApp.HotUpdate.Views.TestButton", "ButtonClick", null, sender, e);
                if (result is string)
                {
                    var a = result as string;
                }
            }
        }
    }
}
