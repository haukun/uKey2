using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using NAudio.CoreAudioApi;

namespace uKey2
{
    class uKey:Form
    {
        protected class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
            [DllImport("user32.dll")]
            public static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);
            [DllImport("user32.dll")]
            public static extern int UnregisterHotKey(IntPtr hWnd, int id);
        }

        private IntPtr mhWnd;
        ParameterizedThreadStart proc;

        private static uint MOD_ALT = 0x0001;
        private static uint MOD_CONTROL = 0x0002;
        private static uint MOD_SHIFT = 0x0004;
        private static uint MOD_WIN = 0x0008;
        private static uint WM_HOTKEY = 0x0312;

        static int ID_BASE = 10000;
        private int mRegistCount = 0;
        private MMDevice mMMCaptureDevice;

        Func<uCore.ListenerState, int, int> mLIstener;

        private class KeyBind
        {
            public KeyBind(int _id, uint _message, uint _wParam, uint _lParam)
            {
                id = _id;
                message = _message;
                wParam = _wParam;
                lParam = _lParam;
            }
            public int id { get; set; }
            public uint message { get; set; }
            public uint wParam { get; set; }
            public uint lParam { get; set; }
        }
        List<KeyBind> keyBinds = new List<KeyBind>();

        public uKey(IntPtr _hWnd, Func<uCore.ListenerState, int, int> listener)
        {
            this.proc = new ParameterizedThreadStart(raiseHotKeyPush);
            mhWnd = _hWnd;

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            mMMCaptureDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);

            mLIstener = listener;
        }


        ~uKey()
        {
            UnregisterHotKey();
        }

        private void raiseHotKeyPush(object o)
        {
            hotKeyPush(this, EventArgs.Empty, o);
        }

        private void hotKeyPush(object sender, EventArgs e, object o)
        {
            int targetId = int.Parse(o.ToString());
            KeyBind targetBind = keyBinds.Single(el => el.id == targetId);
            if(targetBind != null)
            {
                SendMessage(targetBind.message, targetBind.wParam, targetBind.lParam);

                //  マイク状態の変更
                if(targetBind.message == 0x0319)
                {
                    if(targetBind.lParam == 0x180000)
                    {
                        mLIstener(uCore.ListenerState.MIC_MUTE, mMMCaptureDevice.AudioEndpointVolume.Mute ? 1 : 0);
                    }
                }
            }
        }

        public void Bind(string key, bool isShift, bool isControl, bool isAlt, bool isWin, uint message, uint wParam, uint lParam)
        {
            uint modifier = 0;
            modifier += isShift ? MOD_SHIFT : 0;
            modifier += isControl ? MOD_CONTROL : 0;
            modifier += isAlt ? MOD_ALT : 0;
            modifier += isWin ? MOD_WIN : 0;

            Keys vk = Keys.None;
            foreach (System.Reflection.FieldInfo info in typeof(Keys).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                if (key.Equals(info.Name))
                {
                    vk = (Keys)info.GetValue(null);
                }
            }

            if (vk != Keys.None) {
                NativeMethods.RegisterHotKey(this.Handle, ID_BASE + mRegistCount, modifier, vk);
                keyBinds.Add(new KeyBind(mRegistCount, message, wParam, lParam));
                mRegistCount++;
            }

        }


        public void UnregisterHotKey()
        {
            foreach(KeyBind bind in keyBinds)
            {
                NativeMethods.UnregisterHotKey(this.Handle, ID_BASE + bind.id);
            }
        }

        public int SendMessage(uint _Msg, uint _wParam, uint _lParam)
        {
            return NativeMethods.SendMessage(mhWnd, _Msg, _wParam, _lParam);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if(m.Msg == WM_HOTKEY)
            {
                proc(m.WParam - ID_BASE);
            }
        }

    }
}
