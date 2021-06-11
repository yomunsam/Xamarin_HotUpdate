using System;
using System.Collections.Generic;
using System.Text;
using XFApp.ILWorks.Adaptors;
using XFApp.ILWorks.Adaptors.Buttons;
using XFApp.ILWorks.Adaptors.Pages;
using XFApp.Services;

namespace XFApp.ILWorks
{
    public unsafe static class ILWorkRegisters
    {
        public static void RegisterDelegates(HotUpdateService hotUpdateService)
        {
            hotUpdateService.DelegateManager.RegisterMethodDelegate<object, EventArgs>();
            hotUpdateService.DelegateManager.RegisterMethodDelegate<EventHandler<EventArgs>>();
            hotUpdateService.DelegateManager.RegisterMethodDelegate<EventArgs>();
            hotUpdateService.DelegateManager.RegisterDelegateConvertor<EventHandler<EventArgs>>((action) =>
            {
                return action;
            });
        }

        internal static void RegisterCLRMethodRedirections(HotUpdateService hotUpdateService)
        {

        }

        public static void RegisterCrossBindingAdaptors(HotUpdateService hotUpdateService)
        {
            hotUpdateService.RegisterCrossBindingAdaptor(new IAsyncStateMachineClassInheritanceAdaptor()); //用于Async/await
            hotUpdateService.RegisterCrossBindingAdaptor(new ContentPageAdaptors());
            hotUpdateService.RegisterCrossBindingAdaptor(new ButtonAdaptors());
        }
    }
}
