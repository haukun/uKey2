using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace uKey2
{
    class uCore
    {
        private uJson uJson;
        private uKey uKey;
        private uNotifyIconWrapper mNotifyIcon;

        public enum ListenerState
        {
            NONE,
            MIC_MUTE,
            _MAX
        }

        public uCore(ref uNotifyIconWrapper _uNotifyIconWrapper)
        {
            uJson = new uJson();
            uKey = new uKey(Process.GetCurrentProcess().Handle, hotkeyListener);

            uJson.Retrieve();
            foreach(uKeyJson.Setting setting in uJson.json.Settings)
            {
                uKey.Bind(setting.key, setting.shift, setting.control, setting.alt, setting.win, setting.message, setting.wParam, setting.lParam);
            }

            mNotifyIcon = _uNotifyIconWrapper;
        }

        public int hotkeyListener(ListenerState state, int result)
        {
            switch (state)
            {
                case ListenerState.MIC_MUTE:
                    if (result != 0)
                    {
                        mNotifyIcon.SetMicOn();
                    }
                    else
                    {
                        mNotifyIcon.SetMicOn(false);
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }

    }
}
