using System;
using System.Collections.Generic;
using System.Linq;

namespace Keyboard
{
    public class KeyboardKeyConstants
    {
        #region Command text

        public static readonly string NumLockText = "Num Lock";
        public static readonly string CapsText = "Caps Lock";
        public static readonly string EnterText = "Enter ";
        public static readonly string ShiftText = char.ConvertFromUtf32(8679);
        public static readonly string UpText = char.ConvertFromUtf32(8593);
        public static readonly string LeftText = char.ConvertFromUtf32(8592);
        public static readonly string DownText = char.ConvertFromUtf32(8595);
        public static readonly string RightText = char.ConvertFromUtf32(8594);
        public static readonly string DelText = "Del";
        public static readonly string CtrlText = "Ctrl";
        public static readonly string AltText = "Alt";
        public static readonly string AltGrText = "AltGr";
        public static readonly string SpaceText = "Space";
        public static readonly string PrtScrText = "Prt Scr";
        public static readonly string BackSpaceText = "Back space";
        public static readonly string TabText = char.ConvertFromUtf32(8646);
        public static readonly string EscText = "Esc";
        public static readonly string InsertText = "Insert";
        public static readonly string ScrollLockText = "Scroll Lock";
        public static readonly string WinText = "Win";
        public static readonly string HomeText = "Home";
        public static readonly string EndText = "End";
        public static readonly string PageUpText = "Pg Up";
        public static readonly string PageDownText = "Pg Dn";
        public static readonly string F1 = "F1";
        public static readonly string F2 = "F2";
        public static readonly string F3 = "F3";
        public static readonly string F4 = "F4";
        public static readonly string F5 = "F5";
        public static readonly string F6 = "F6";
        public static readonly string F7 = "F7";
        public static readonly string F8 = "F8";
        public static readonly string F9 = "F9";
        public static readonly string F10 = "F10";
        public static readonly string F11 = "F11";
        public static readonly string F12 = "F12";
        public static readonly string LanguageSelection = "Language Selection";

        #endregion

        #region Commands

        internal const string UpCommand = "{UP}";
        internal const string DownCommand = "{DOWN}";
        internal const string LeftCommmand = "{LEFT}";
        internal const string RightCommmand = "{RIGHT}";
        internal const string BackSpaceCommmand = "{BACKSPACE}";
        internal const string EnterCommmand = "{ENTER}";

        internal const string CtrlCommmand = "^";
        internal const string AltCommmand = "%";
        internal const string ShiftCommmand = "+";

        internal const string DelCommmand = "{DEL}";
        internal const string NumLockCommmand = "{NUMLOCK}";
        internal const string CapsLockCommmand = "{CAPSLOCK}";
        internal const string HomeCommmand = "{HOME}";
        internal const string EndCommmand = "{END}";
        internal const string PageUpCommmand = "{PGUP}";
        internal const string PageDownCommmand = "{PGDN}";
        internal const string ScrolLockCommmand = "{SCROLLLOCK}";
        internal const string EscCommmand = "{ESC}";
        internal const string PrtScrCommand = "{PRTSC}";
        internal const string InsertCommand = "{INSERT}";
        internal const string TabCommand = "{TAB}";

        #endregion

        internal const string NoneString = "None"; // button with this text will not be inserted
        internal const string PatternReg = "[{}+^%~()]";
        internal const string ReplacementReg = "{$0}";
        internal const string FuncButtonsReg = @"F+[0-9]";// to detect Function button
    }
    
    #region Command combinations

    internal struct CommandCombination
    {

        public string CommandText { get; set; }
        public string Command { get; set; }

        /// <summary>
        /// Get combinations of command and command text
        /// </summary>
        public static List<CommandCombination> GetAllCommands()
        {
            List<CommandCombination> commands = new List<CommandCombination>
                {
                    new CommandCombination {CommandText = KeyboardKeyConstants.HomeText, Command = KeyboardKeyConstants.HomeCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.EndText, Command = KeyboardKeyConstants.EndCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.PageDownText, Command = KeyboardKeyConstants.PageDownCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.PageUpText, Command = KeyboardKeyConstants.PageUpCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.DelText, Command = KeyboardKeyConstants.DelCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.NumLockText, Command = KeyboardKeyConstants.NumLockCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.CapsText, Command = KeyboardKeyConstants.CapsLockCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.CtrlText, Command = String.Empty},
                    new CommandCombination {CommandText = KeyboardKeyConstants.AltText, Command = String.Empty},
                    new CommandCombination {CommandText = KeyboardKeyConstants.AltGrText, Command = String.Empty},
                    new CommandCombination {CommandText = KeyboardKeyConstants.EscText, Command = KeyboardKeyConstants.EscCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.EnterText, Command = KeyboardKeyConstants.EnterCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.TabText, Command = KeyboardKeyConstants.TabCommand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.PrtScrText, Command = KeyboardKeyConstants.PrtScrCommand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.SpaceText, Command = " "},
                    new CommandCombination {CommandText = KeyboardKeyConstants.BackSpaceText, Command = KeyboardKeyConstants.BackSpaceCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.UpText, Command = KeyboardKeyConstants.UpCommand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.DownText, Command = KeyboardKeyConstants.DownCommand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.LeftText, Command = KeyboardKeyConstants.LeftCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.RightText, Command = KeyboardKeyConstants.RightCommmand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.InsertText, Command = KeyboardKeyConstants.InsertCommand},
                    new CommandCombination {CommandText = KeyboardKeyConstants.ShiftText, Command = String.Empty},
                    new CommandCombination {CommandText = KeyboardKeyConstants.ScrollLockText, Command = String.Empty},
                    new CommandCombination {CommandText = KeyboardKeyConstants.WinText, Command = String.Empty}
                };

            return commands;
        }

        /// <summary>
        /// Get command by command text
        /// </summary>
        public static CommandCombination GetCommandByText(string commandText)
        {
            List<CommandCombination> commands = GetAllCommands();

            var comm = from c in commands
                       where c.CommandText == commandText
                       select c;

            return comm.Single();
        }
    }

    #endregion
}
