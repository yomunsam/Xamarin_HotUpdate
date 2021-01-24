using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ILRuntime.Runtime.Enviorment;

namespace XFApp.Services
{
    public interface IHotUpdateService
    {
        ILRuntime.Runtime.Enviorment.AppDomain ILRuntimeAppDomain { get; }
        DelegateManager DelegateManager { get; }

        IHotUpdateService RegisterCrossBindingAdaptor(CrossBindingAdaptor adaptor);
        Task StartAsync();
    }
}
