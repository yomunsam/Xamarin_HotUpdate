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

            //注册CLR重定向
            this.registerCLRMethodRedirections();

            //注册委托适配器
            this.registerDelegates();

            //注册跨域适配器
            this.registerCrossBindingAdaptors();
            //注册CLR绑定

            //加载Assemblies
            await this.LoadAssemblies();

            m_Inited = true;
        }


        public IHotUpdateService RegisterCrossBindingAdaptor(CrossBindingAdaptor adaptor)
        {
            m_AppDomain?.RegisterCrossBindingAdaptor(adaptor);
            return this;
        }

        /// <summary>
        /// 加载热更工程，这里是技术尝试demo，所以特别简单，直接从嵌入资源里加载了
        /// </summary>
        /// <returns></returns>
        private async Task LoadAssemblies()
        {
            var asm = IntrospectionExtensions.GetTypeInfo(typeof(HotUpdateService)).Assembly;
            var resources = asm.GetManifestResourceNames();
            Stream stream_dll = asm.GetManifestResourceStream("XFApp.HotDlls.XFApp.HotUpdate.dll");
            Stream stream_pdb = asm.GetManifestResourceStream("XFApp.HotDlls.XFApp.HotUpdate.pdb");

            m_AppDomain.LoadAssembly(stream_dll, stream_pdb, new PdbReaderProvider());

            await Task.CompletedTask;
        }

        /// <summary>
        /// 注册跨域继承适配器
        /// </summary>
        private void registerCrossBindingAdaptors()
        {
            ILWorkRegisters.RegisterCrossBindingAdaptors(this);
        }

        /// <summary>
        /// 注册委托适配器
        /// </summary>
        private void registerDelegates()
        {
            //内部委托适配器
            ILWorkRegisters.RegisterDelegates(this);
        }

        /// <summary>
        /// 注册CLR重定向
        /// </summary>
        private void registerCLRMethodRedirections()
        {
            ILWorkRegisters.RegisterCLRMethodRedirections(this);
        }

    }
}
