using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTyper
{
    /// <summary>
    /// Code partially inspired by http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx
    /// </summary>
    /// </summary>
    class AutoTyper
    {
        #region Declarations
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookId = IntPtr.Zero;
        #endregion

        #region Public constructor and methods
        /// <summary>
        /// Default constructor
        /// </summary>
        public AutoTyper()
        {
            _proc = new LowLevelKeyboardProc(HookCallback);
        }

        /// <summary>
        /// Start auto-typing text
        /// </summary>
        /// <param name="text">The text that will replace typing</param>
        public void StartTyping(string text)
        {
            this.Text = text;
            this._replIndex = 0;
            this._intercept = true;
            _hookId = SetHook(_proc);
        }

        /// <summary>
        /// Stop auto-typing
        /// </summary>
        public void StopTyping()
        {
            UnhookWindowsHookEx(_hookId);
            this._replIndex = 0;
            this._intercept = true;
        }

        /// <summary>
        /// The replacement text
        /// </summary>
        public string Text { get; private set; }

        #endregion

        #region Private methods
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// Intercepting and replacing typing ?
        /// </summary>
        private bool _intercept;

        /// <summary>
        /// Index in the replacement text
        /// </summary>
        private int _replIndex;

        /// <summary>
        /// The callback called when a key has been typed
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (_replIndex < this.Text.Length && _intercept)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    switch ((Keys)vkCode)
                    {
                        case Keys.Escape:
                            this.StopTyping();
                            break;

                        case Keys.F1:
                            _replIndex = 0;
                            break;

                        default:
                            _intercept = false;
                            string keys = string.Empty;
                            switch (this.Text[_replIndex])
                            {
                                case '{':
                                case '}':
                                case '(':
                                case ')':
                                case '%':
                                case '+':
                                case '^':
                                    keys = string.Format("{{{0}}}", this.Text[_replIndex]);
                                    break;

                                default:
                                    keys = this.Text[_replIndex].ToString();
                                    break;
                            }
                            Console.WriteLine(keys);

                            SendKeys.Send(keys);
                            _replIndex++;
                            _intercept = true;
                            break;
                    }
                }
                return (IntPtr)1;
            }
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
        #endregion

        #region Dlls Imports
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hk, int ncode, IntPtr wparam, IntPtr lparam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string l);
        #endregion
    }
}
