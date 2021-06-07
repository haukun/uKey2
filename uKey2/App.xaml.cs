using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace uKey2
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private uNotifyIconWrapper uNotifyIcon;
        private uCore uCore;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            uNotifyIcon = new uNotifyIconWrapper();
            uCore = new uCore(ref uNotifyIcon);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            uNotifyIcon.Dispose();
        }
    }
}
