using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ILAppDomain = ILRuntime.Runtime.Enviorment.AppDomain;
using AppDomain = System.AppDomain;
using ILRuntime.Mono.Cecil.Pdb;
using ILRuntime.Runtime.Enviorment;
using XFApp.ILWorks;

namespace XFApp.Services
{
    public class HotUpdateService : IHotUpdateService
    {
        private bool m_Inited = false;

        public ILAppDomain ILRuntimeAppDomain => m_AppDomain;
        public DelegateManager DelegateManager => m_AppDomain?.DelegateManager;

        private ILAppDomain m_AppDomain;

        private Dictionary<string, Stream> m_LoadedAssemblies = new Dictionary<string, Stream>();
        private Dictionary<string, Stream> m_LoadedSymbols = new Dictionary<string, Stream>();

        public HotUpdateService()
        {
            m_AppDomain = new ILAppDomain();
        }

        public async Task StartAsync()
        {
            if (m_Inited) return;

            this.registerCLRMethodRedirections();

            this.registerDelegates();

            this.registerCrossBindingAdaptors();

            await this.LoadAssemblies();

            m_Inited = true;
        }


        public IHotUpdateService RegisterCrossBindingAdaptor(CrossBindingAdaptor adaptor)
        {
            m_AppDomain?.RegisterCrossBindingAdaptor(adaptor);
            return this;
        }

        private async Task LoadAssemblies()
        {
            var asm = IntrospectionExtensions.GetTypeInfo(typeof(HotUpdateService)).Assembly;
            var resources = asm.GetManifestResourceNames();
            Stream stream_dll = asm.GetManifestResourceStream("XFApp.HotDlls.XFApp.HotUpdate.dll");
            Stream stream_pdb = asm.GetManifestResourceStream("XFApp.HotDlls.XFApp.HotUpdate.pdb");

            m_AppDomain.LoadAssembly(stream_dll, stream_pdb, new PdbReaderProvider());

            await Task.CompletedTask;
        }

        private void registerCrossBindingAdaptors()
        {
            ILWorkRegisters.RegisterCrossBindingAdaptors(this);
        }

        private void registerDelegates()
        {

            ILWorkRegisters.RegisterDelegates(this);
        }

        private void registerCLRMethodRedirections()
        {
            ILWorkRegisters.RegisterCLRMethodRedirections(this);
        }

    }
}
