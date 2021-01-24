using System;
using System.Collections.Generic;
using System.Text;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using Xamarin.Forms;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace XFApp.ILWorks.Adaptors.Pages
{
    public class ContentPageAdaptors : CrossBindingAdaptor
    {
        public override Type BaseCLRType => typeof(ContentPage);

        public override Type AdaptorType => typeof(MyContentPage);

        public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new MyContentPage(appdomain, instance);
        }

        class MyContentPage : ContentPage, CrossBindingAdaptorType
        {
            ILTypeInstance instance;
            AppDomain appdomain;
            public ILTypeInstance ILInstance => this.instance;


            IMethod mOnAppearing;
            bool mOnAppearingGot;
            bool mOnAppearingInvoking;

            public MyContentPage() { }
            public MyContentPage(AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            protected override void OnAppearing()
            {
                if (!mOnAppearingGot)
                {
                    mOnAppearing = instance.Type.GetMethod("OnAppearing", 0);
                    mOnAppearingGot = true;
                }

                if (mOnAppearing != null && !mOnAppearingInvoking)
                {
                    mOnAppearingInvoking = true;
                    appdomain.Invoke(mOnAppearing, instance);
                    mOnAppearingInvoking = false;
                }
                else
                    base.OnAppearing();
            }

        }

    }
}
