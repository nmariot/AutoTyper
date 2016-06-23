#define VERBOSE_DEBUG 
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoTyper
{
    /// <summary>
    /// Code partially inspired by http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx
    /// </summary>
    /// </summary>
    class AutoTyper : IDisposable
    {
        #region Evenements
        /// <summary>
        /// Event raised when a scenario is started (returns the scenario number. 0 for the first one)
        /// </summary>        
        public event EventHandler<int> Started;

        /// <summary>
        /// Event raised when a scenario is stopped (no more key or another scenario to start)
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Event raised when a key is typed (returns the index key number)
        /// </summary>
        public event EventHandler<int> KeyStroke;

        /// <summary>
        /// Event raised when the number of letters typed has changed
        /// </summary>
        public event EventHandler<int> NbOfLettersTypedChanged;
        #endregion

        #region Declarations
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookId = IntPtr.Zero;
        private int _nbOfLettersTyped = 1;
        #endregion

        #region Public constructor and methods
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="autoTypedText">List of autotyped text</param>
        public AutoTyper(string[] autoTypedText)
        {
            for (int i = 0; i < Math.Min(autoTypedText.Length, this.AutoTypedText.Length); i++)
            {
                this.AutoTypedText[i] = autoTypedText[i];
            }
            _proc = new LowLevelKeyboardProc(HookCallback);
            _hookId = SetHook(_proc);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Dispose : remove the key hook
        /// </summary>
        public void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
            Stopped?.Invoke(this, EventArgs.Empty);
        }
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
        /// Index in the replacement text
        /// </summary>
        private int _replIndex;

        /// <summary>
        /// Text index in the array AutoTypedText
        /// </summary>
        private int _textIndex;

        /// <summary>
        /// Special keys
        /// </summary>
        private bool _ctrlPressed = false;
        private bool _shiftPressed = false;
        //private bool _windowsPressed = false;

        /// <summary>
        /// Bool value used to temporarily desactivate interception (for one keystroke to avoid endless loop)
        /// </summary>
        private bool _tmpIntercept = true;

        /// <summary>
        /// replacing text ?
        /// </summary>
        private bool _replaceText = false;

        /// <summary>
        /// The callback called when a key has been typed
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (_tmpIntercept && nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                // Debug($"wParam = {wParam}, lParam = {lParam}, vkCode = {vkCode}");

                if (wParam == (IntPtr)WM_KEYUP)
                {
                    switch ((Keys)vkCode)
                    {
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            _shiftPressed = false;
                            Debug("Shift Key released");
                            break;

                        case Keys.LControlKey:
                        case Keys.RControlKey:
                            _ctrlPressed = false;
                            Debug("Ctrl Key released");
                            break;

                        case Keys.LWin:
                        case Keys.RWin:
                            //_windowsPressed = false;
                            Debug("Windows Key released");
                            break;
                    }
                }
                else if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    switch ((Keys)vkCode)
                    {
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            _shiftPressed = true;
                            Debug("Shift Key pressed");
                            break;

                        case Keys.LControlKey:
                        case Keys.RControlKey:
                            _ctrlPressed = true;
                            Debug("Ctrl Key pressed");
                            break;

                        case Keys.LWin:
                        case Keys.RWin:
                            //_windowsPressed = true;
                            Debug("Windows Key pressed");
                            break;

                        case Keys.Escape:
                            _replaceText = false;
                            Stopped?.Invoke(this, EventArgs.Empty);
                            Debug("Stop replacing text");
                            break;

                        case Keys.F1:
                        case Keys.F2:
                        case Keys.F3:
                        case Keys.F4:
                        case Keys.F5:
                        case Keys.F6:
                        case Keys.F7:
                        case Keys.F8:
                        case Keys.F9:
                        case Keys.F10:
                        case Keys.F11:
                        case Keys.F12:
                            if (_ctrlPressed && _shiftPressed)
                            {
                                _replaceText = true;
                                _textIndex = vkCode - 112;
                                _replIndex = 0;
                                _tmpIntercept = true;
                                Started?.Invoke(this, _textIndex);
                                Debug("Start replacing text : " + AutoTypedText[_textIndex]);
                            }
                            break;

                        default:
                            if (_ctrlPressed && _shiftPressed && (Keys)vkCode == Keys.Up)
                            {
                                // Ctrl-Shift-Up increases the number of letters typeds
                                this.NbOfLettersTyped++;
                            }
                            else if (_ctrlPressed && _shiftPressed && (Keys)vkCode == Keys.Down)
                            {
                                // Ctrl-Shift-Down decreases the number
                                this.NbOfLettersTyped--;
                            }
                            else if (_replaceText)
                            {
                                bool lettersTyped = false;
                                for (int i = 0; i < this.NbOfLettersTyped; i++)
                                {
                                    if (_replIndex < AutoTypedText[_textIndex].Length)
                                    {
                                        lettersTyped = true;
                                        _tmpIntercept = false;
                                        string keys = string.Empty;
                                        switch (AutoTypedText[_textIndex][_replIndex])
                                        {
                                            case '{':
                                            case '}':
                                            case '(':
                                            case ')':
                                            case '%':
                                            case '+':
                                            case '^':
                                            case '~':
                                                keys = string.Format("{{{0}}}", AutoTypedText[_textIndex][_replIndex]);
                                                break;

                                            default:
                                                keys = AutoTypedText[_textIndex][_replIndex].ToString();
                                                break;
                                        }
                                        Debug($"Replace capture by '{keys}'. Index = {_replIndex}");
                                        SendKeys.Send(keys);

                                        _replIndex++;
                                        _tmpIntercept = true;

                                        KeyStroke?.Invoke(this, _replIndex);
                                    }
                                    else
                                    {
                                        _replaceText = false;
                                        Stopped?.Invoke(this, EventArgs.Empty);
                                        Debug("Stop replacing text : no more input data");
                                    }   
                                }
                                if (lettersTyped)
                                {
                                    // Cancel current keystroke
                                    Debug("Return (IntPtr)1");
                                    return (IntPtr)1;
                                }
                            }
                            break;
                    }
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        [Conditional("VERBOSE_DEBUG")]
        private void Debug(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
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

        #region Properties
        /// <summary>
        /// List the text to be typed for each function key (F1 => AutoTypedText[0] ...)
        /// </summary>
        public string[] AutoTypedText { get; private set; } = new string[12];

        /// <summary>
        /// Number of letters typed when striking one key
        /// </summary>
        public int NbOfLettersTyped
        {
            get
            {
                return _nbOfLettersTyped;
            }
            set
            {
                if (_nbOfLettersTyped != value) 
                {
                    _nbOfLettersTyped = Math.Min(Math.Max(1, value), 10);// When greater or equal than 11, does not cancel the key stroke
                    NbOfLettersTypedChanged?.Invoke(this, _nbOfLettersTyped);
                    Debug("Number of letters typed : " + _nbOfLettersTyped);
                }
            }
        }
        #endregion


    }
}
