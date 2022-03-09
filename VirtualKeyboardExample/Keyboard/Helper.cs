using System;
using System.Text.RegularExpressions;

namespace Keyboard
{
    internal class Helper
    {
        public const string AdvBtn = "btn_Adv_";
        private static readonly string advBtnNaming = AdvBtn + "{0}";
        private static readonly string symbBtnNaming = "btn_Symb_{0}";
        public const string LangBtnName = "btn_LangButton";


        /// <summary>
        /// Function to get Upper/Lower string depending on a Caps/Shift state
        /// </summary>
        /// <returns>The Upper/Lower string.</returns>
        public static string UpperLowerString(string s, VirtualKeyboard kb = null)
        {

            if (kb == null)
            {
                return s;
            }
            if (kb.ShiftState && kb.CapsLockState)
            {
                return s.ToLower();
            }
            if (kb.ShiftState || kb.CapsLockState)
            {
                return s.ToUpper();
            }

            return s;
        }



        public static bool IsSpecialKeyText(string text)
        {
            var isOptionalButton = text == KeyboardKeyConstants.NumLockText || text == KeyboardKeyConstants.BackSpaceText ||
                text == KeyboardKeyConstants.CapsText || text == KeyboardKeyConstants.EnterText ||
                text == KeyboardKeyConstants.ShiftText ||
                text == KeyboardKeyConstants.DelText || text == KeyboardKeyConstants.UpText ||
                text == KeyboardKeyConstants.LeftText ||
                text == KeyboardKeyConstants.RightText || text == KeyboardKeyConstants.DownText ||
                text == KeyboardKeyConstants.CtrlText ||
                text == KeyboardKeyConstants.AltText || text == KeyboardKeyConstants.AltGrText ||
                text == KeyboardKeyConstants.DelText ||
                text == KeyboardKeyConstants.SpaceText || text == KeyboardKeyConstants.PrtScrText ||
                text == KeyboardKeyConstants.InsertText ||
                text == KeyboardKeyConstants.HomeText || text == KeyboardKeyConstants.EndText ||
                text == KeyboardKeyConstants.PageDownText ||
                text == KeyboardKeyConstants.PageUpText || text == KeyboardKeyConstants.ScrollLockText ||
                text == KeyboardKeyConstants.EscText ||
                text == KeyboardKeyConstants.TabText || text == KeyboardKeyConstants.WinText ||
                Regex.Match(text, KeyboardKeyConstants.FuncButtonsReg).Success;

            return isOptionalButton;
        }


        /// <summary>
        /// Applies corresponding name for a button
        /// </summary>
        /// <returns>Button name</returns>
        public static string GiveButtonName(
                            VirtualKeyboard kb,
                            int rowNumber,
                            int columnNumber,
                            VirtualKbButton item,
                            bool isLangButton)
        {
            int beginAdvArea = 0;

            if (!kb.ShowFunctionButtons)
            {
                rowNumber = rowNumber + 1;
            }

            if (rowNumber == 1)
            {
                beginAdvArea = kb.BeginFirsthRowAdvArea();
            }

            if (rowNumber == 2)
            {
                beginAdvArea = kb.BeginSecondRowAdvArea();
            }

            if (rowNumber == 3)
            {
                beginAdvArea = kb.BeginThirdRowAdvArea();
            }

            if (rowNumber == 4)
            {
                beginAdvArea = kb.BeginFourthRowAdvArea();
            }


            if (rowNumber == 5)
            {
                beginAdvArea = kb.BeginFifthRowAdvArea();
            }

            var name = (columnNumber < beginAdvArea ||
                                item.TopText == KeyboardKeyConstants.PrtScrText
                           || item.TopText == KeyboardKeyConstants.InsertText ||
                           item.TopText == KeyboardKeyConstants.ScrollLockText ||
                           item.TopText == KeyboardKeyConstants.WinText)
                ? String.Format(symbBtnNaming, columnNumber.ToString() + rowNumber.ToString())
                : String.Format(advBtnNaming, columnNumber.ToString() + rowNumber.ToString());

            if (isLangButton)
            {
                name = LangBtnName;
            }
            return name;
        }

        public static bool HoldPressedState(string text, VirtualKeyboard kb)
        {
            if ((kb.ShiftState && text == KeyboardKeyConstants.ShiftText) ||
             (kb.CapsLockState && text == KeyboardKeyConstants.CapsText) ||
             (kb.NumLockState && text == KeyboardKeyConstants.NumLockText) ||
             (kb.CtrlState && text == KeyboardKeyConstants.CtrlText) ||
             (kb.AltState && text == KeyboardKeyConstants.AltText) ||
             (kb.AltGrState && text == KeyboardKeyConstants.AltGrText) ||
             (kb.ScrollLockState && text == KeyboardKeyConstants.ScrollLockText))
            {
                return true;
            }

            return false;
        }
    }
}
