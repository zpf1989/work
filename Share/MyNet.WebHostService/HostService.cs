﻿using Microsoft.Owin.Hosting;
using MyNet.Components.Logger;
using System;
using System.Collections.Generic;
using System.ServiceProcess;

namespace MyNet.WebHostService
{
    public partial class HostService : ServiceBase
    {
        private static ILogHelper<HostService> _logHelper = LogHelperFactory.GetLogHelper<HostService>();
        private static List<IDisposable> _instances = new List<IDisposable>();
        public HostService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var app = WebApp.Start<Startup>(url: HostContext.HostUrl);
                _instances.Add(app);
                _logHelper.LogInfo(string.Format("{0}已启动，host：{1}", HostContext.SvrName, HostContext.HostUrl));
            }
            catch (Exception ex)
            {
                _logHelper.LogInfo(string.Format("{0}启动失败，host：{1}", HostContext.SvrName, HostContext.HostUrl), ex);
                Console.WriteLine(string.Format("{0}启动失败，请查看日志", HostContext.SvrName));
                base.Stop();
            }
        }

        protected override void OnStop()
        {
            if (_instances != null && _instances.Count > 0)
            {
                _logHelper.LogInfo("将要释放所有web应用...");
                _instances.ForEach(i => i.Dispose());
                _logHelper.LogInfo("所有web应用释放完毕");
            }
            _logHelper.LogInfo("WebHostService已停止");
        }
    }
}
