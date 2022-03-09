using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Keyboard
{
    /// <summary>
    /// The class with functions to send commands from buttons
    /// </summary>
    internal class CommandInstruction
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;
        const int SCROLLLOCK = 0x91;
        const int WIN = 0x5B;

        private VirtualKeyboard _keyboard;

         /// <summary>
        /// Commands instructions for the specified keyboard
        /// </summary>
        /// <param name="keyboard"></param>
        public CommandInstruction(VirtualKeyboard keyboard)
        {
            if (keyboard == null)
            {
                throw new ArgumentNullException("keyboard");
            }

            _keyboard = keyboard;
        }

        /// <summary>
        /// Send numeric area command 
        /// </summary>
        public CommandCombination NumericAreaCommand(VirtualKbButton btn)
        {
            var cmd = new CommandCombination();

            if (_keyboard.NumLockState)
            {
                if (!string.IsNullOrEmpty(btn.BottomText))
                {
                    cmd = CommandCombination.GetCommandByText(btn.BottomText);
                }
            }
            else
            {
                if (!btn.TopText.Contains(KeyboardKeyConstants.NumLockText)
                    && !btn.TopText.Contains(KeyboardKeyConstants.EnterText))
                {
                    cmd.Command = Regex.Replace(btn.TopText, KeyboardKeyConstants.PatternReg,
                        KeyboardKeyConstants.ReplacementReg);
                    cmd.CommandText = btn.TopText;
                }
            }


            if (btn.TopText.Contains(KeyboardKeyConstants.NumLockText)
                || btn.TopText.Contains(KeyboardKeyConstants.EnterText))
            {
                cmd = CommandCombination.GetCommandByText(btn.TopText);
            }


            if (btn.TopText == KeyboardKeyConstants.NumLockText)
            {
                _keyboard.NumLockState = !_keyboard.NumLockState;
            }



            return cmd;
        }

        /// <summary>
        /// Send keyboard regular area command 
        /// </summary>
        public CommandCombination RegularCommand(VirtualKbButton btn)
        {
            var cmd = new CommandCombination();
            string txtTop = Regex.Replace(btn.TopText, KeyboardKeyConstants.PatternReg,
                KeyboardKeyConstants.ReplacementReg);
            string txtBottom = Regex.Replace(btn.BottomText, KeyboardKeyConstants.PatternReg,
                KeyboardKeyConstants.ReplacementReg);
            var txt = !String.IsNullOrEmpty(txtTop.ToLower()) ? txtTop.ToLower() : txtBottom.ToLower();

            if (_keyboard.CtrlState && _keyboard.AltState && _keyboard.ShiftState)
            {
                _keyboard.AltState = _keyboard.CtrlState = _keyboard.ShiftState = false;
                cmd.Command = KeyboardKeyConstants.CtrlCommmand +
                    KeyboardKeyConstants.AltCommmand + KeyboardKeyConstants.ShiftCommmand + txtTop.ToLower();
                cmd.CommandText = txtTop.ToLower();
            }
            else if (_keyboard.CtrlState && _keyboard.AltState)
            {
                _keyboard.AltState = _keyboard.CtrlState = false;
                cmd.Command = KeyboardKeyConstants.CtrlCommmand + KeyboardKeyConstants.AltCommmand + txtTop.ToLower();
                cmd.CommandText = txtTop.ToLower();
            }
            else if (_keyboard.CtrlState)
            {
                if (_keyboard.ShiftState)
                {
                    _keyboard.ShiftState = !_keyboard.ShiftState;
                }
                else if (_keyboard.CapsLockState)
                {
                    _keyboard.CapsLockState = !_keyboard.CapsLockState;
                }
                else if (_keyboard.CapsLockState && _keyboard.ShiftState)
                {
                    _keyboard.CapsLockState = !_keyboard.CapsLockState;
                    _keyboard.ShiftState = !_keyboard.ShiftState;
                }
                if (!_keyboard.ShiftState || !_keyboard.CapsLockState)
                {
                    _keyboard.CtrlState = false;
                    cmd.Command = KeyboardKeyConstants.CtrlCommmand + txt;
                    cmd.CommandText = txtTop.ToLower();
                }
            }
            else if (_keyboard.AltState)
            {
                if (_keyboard.ShiftState)
                {
                    _keyboard.ShiftState = !_keyboard.ShiftState;
                }
                else if (_keyboard.CapsLockState)
                {
                    _keyboard.CapsLockState = !_keyboard.CapsLockState;
                }
                else if (_keyboard.CapsLockState && _keyboard.ShiftState)
                {
                    _keyboard.CapsLockState = !_keyboard.CapsLockState;
                    _keyboard.ShiftState = !_keyboard.ShiftState;
                }
                if (!_keyboard.ShiftState || !_keyboard.CapsLockState)
                {
                    _keyboard.AltState = false;
                    cmd.Command = KeyboardKeyConstants.AltCommmand + txt;
                    cmd.CommandText = txtTop.ToLower();
                }
            }

            else
            {
                if (_keyboard.ShiftState)
                {
                    cmd.Command = txtTop;
                    cmd.CommandText = cmd.Command;
                    _keyboard.ShiftState = false;
                }
                else
                {
                    cmd.Command = string.IsNullOrEmpty(txtBottom) ? txtTop : txtBottom;
                    cmd.CommandText = cmd.Command;
                }
            }
            return cmd;
        }

        /// <summary>
        /// Send Func. keys combinations command 
        /// </summary>
        public CommandCombination FuncKeyCombinationCommand(VirtualKbButton btn)
        {
            var cmd = new CommandCombination();
            var funcCommand = "{" + btn.TopText + "}";
            cmd.CommandText = btn.TopText;

            if (_keyboard.CtrlState && _keyboard.AltState && _keyboard.ShiftState)
            {
                _keyboard.AltState = _keyboard.CtrlState = _keyboard.ShiftState = false;
                cmd.Command = KeyboardKeyConstants.CtrlCommmand +
                    KeyboardKeyConstants.AltCommmand +
                    KeyboardKeyConstants.ShiftCommmand + funcCommand;
            }
            else if (_keyboard.CtrlState && _keyboard.AltState)
            {
                _keyboard.AltState = _keyboard.CtrlState = false;
                cmd.Command = KeyboardKeyConstants.CtrlCommmand +
                    KeyboardKeyConstants.AltCommmand + funcCommand;
            }
            else if (_keyboard.CtrlState && _keyboard.ShiftState)
            {
                _keyboard.AltState = _keyboard.ShiftState = false;
                cmd.Command = KeyboardKeyConstants.CtrlCommmand +
                    KeyboardKeyConstants.ShiftCommmand + funcCommand;
            }
            else if (_keyboard.CtrlState)
            {
                _keyboard.CtrlState = false;
                cmd.Command = KeyboardKeyConstants.CtrlCommmand + funcCommand;
            }
            else if (_keyboard.AltState)
            {
                _keyboard.AltState = false;
                cmd.Command  = KeyboardKeyConstants.AltCommmand + funcCommand;
            }
            else
            {
                cmd.Command = funcCommand;
            }
            return cmd;
        }

        /// <summary>
        /// Send Special key command (i.e. Enter, Shift, Tab...)
        /// </summary>
        public CommandCombination SpecialKeyCommand(VirtualKbButton btn)
        {

            var cmd = CommandCombination.GetCommandByText(btn.TopText);

            if (btn.TopText == KeyboardKeyConstants.ShiftText)
            {
                _keyboard.ShiftState = !_keyboard.ShiftState;
            }

            if (btn.TopText == KeyboardKeyConstants.CapsText)
            {
                _keyboard.CapsLockState = !_keyboard.CapsLockState;
            }

            if (btn.TopText == KeyboardKeyConstants.CtrlText)
            {
                _keyboard.CtrlState = !_keyboard.CtrlState;
            }

            if (btn.TopText == KeyboardKeyConstants.AltText)
            {
                _keyboard.AltState = !_keyboard.AltState;
            }

            if (btn.TopText == KeyboardKeyConstants.AltGrText)
            {
                _keyboard.AltGrState = !_keyboard.AltGrState;
            }


            if (btn.TopText == KeyboardKeyConstants.NumLockText)
            {
                _keyboard.NumLockState = !_keyboard.NumLockState;
            }

            if (btn.TopText == KeyboardKeyConstants.WinText)
            {
                keybd_event(WIN, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(WIN, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }

            if (btn.TopText == KeyboardKeyConstants.ScrollLockText)
            {
                keybd_event(SCROLLLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(SCROLLLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }

            return cmd;
        }
    }
}
