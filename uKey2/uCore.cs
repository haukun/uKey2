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

        public uCore()
        {
            uJson = new uJson();
            uKey = new uKey(Process.GetCurrentProcess().Handle);

            uJson.Retrieve();

            foreach(uKeyJson.Setting setting in uJson.json.Settings)
            {
                uKey.Bind(setting.key, setting.shift, setting.control, setting.alt, setting.win, setting.message, setting.wParam, setting.lParam);
            }
        }

    }
}
