using System;

namespace Keyboard
{
    /// <summary>
    /// The layout functions for keybaord buttons
    /// </summary>
    public class KeyboardLayout
    {
        private VirtualKeyboard _keyboard;


        /// <summary>
        /// Buttons layout for specified keyboard
        /// </summary>
        public KeyboardLayout()
        {

        }

        /// <summary>
        /// Buttons layout for specified keyboard
        /// </summary>
        /// <param name="keyboard"></param>
        internal KeyboardLayout(VirtualKeyboard keyboard)
        {
            if (keyboard == null)
            {
                throw new ArgumentNullException("keyboard");
            }

            _keyboard = keyboard;
        }

        #region Regular area

        /// <summary>
        /// The entire function buttons row
        /// </summary>
        internal ButtonsCollection FunctionRowButtons;

        /// <summary>
        /// The entire first buttons row
        /// </summary>
        internal ButtonsCollection FirstRowButtons;

        /// <summary>
        /// The entire second buttons row
        /// </summary>
        internal ButtonsCollection SecondRowButtons;

        /// <summary>
        /// The entire third buttons row
        /// </summary>
        internal ButtonsCollection ThirdRowButtons;

        /// <summary>
        /// The entire fourth buttons row
        /// </summary>
        internal ButtonsCollection FourthRowButtons;

        /// <summary>
        /// The entire fifth buttons row
        /// </summary>
        internal ButtonsCollection FifthRowButtons;


        /// <summary>
        /// The first buttons row only with custom buttons
        /// </summary>
        internal ButtonsCollection FirstRowButtonsCustom;

        /// <summary>
        /// The second buttons row only with custom buttons
        /// </summary>
        internal ButtonsCollection SecondRowButtonsCustom;

        /// <summary>
        /// The third buttons row only with custom buttons
        /// </summary>
        internal ButtonsCollection ThirdRowButtonsCustom;

        /// <summary>
        /// The fourth buttons row only with custom buttons
        /// </summary>
        internal ButtonsCollection FourthRowButtonsCustom;

        /// <summary>
        /// The fifth buttons row only with custom buttons
        /// </summary>
        internal ButtonsCollection FifthRowButtonsCustom;


        /// <summary>
        /// The function buttons row with deafult buttons
        /// </summary>
        public ButtonsCollection FunctionButtonsDefault = new ButtonsCollection()
        {
            new VirtualKbButton(KeyboardKeyConstants.EscText, String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.NoneString ,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F1,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F2,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F3,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F4,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F5,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F6,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F7,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F8,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F9,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F10,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F11,String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.F12,String.Empty)
        };



        /// <summary>
        /// The first buttons row only with deafult buttons
        /// </summary>
        public ButtonsCollection FirstRowButtonsDefault()
        {
            return new ButtonsCollection
            {
                    new VirtualKbButton("~", "`"),
                    new VirtualKbButton("!", "1"),
                    new VirtualKbButton("@", "2"),
                    new VirtualKbButton("#", "3"),
                    new VirtualKbButton("$", "4"),
                    new VirtualKbButton("%", "5"),
                    new VirtualKbButton("^", "6"),
                    new VirtualKbButton("&", "7"),
                    new VirtualKbButton("*", "8"),
                    new VirtualKbButton("(", "9"),
                    new VirtualKbButton(")", "0"),
                    new VirtualKbButton("_", "-"),
                    new VirtualKbButton("=", "+"),
            };
        }

        /// <summary>
        /// The second buttons row only with deafult buttons
        /// </summary>
        public ButtonsCollection SecondRowButtonsDefault()
        {
            return new ButtonsCollection
            {
                new VirtualKbButton(Helper.UpperLowerString("q", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("w", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("e", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("r", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("t", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("y", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("u", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("i", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("o", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("p", _keyboard), String.Empty),
                new VirtualKbButton("{", "["),
                new VirtualKbButton("}", "]"),
                new VirtualKbButton("|" , "\\"),

            };
        }


        /// <summary>
        /// The third buttons row only with deafult buttons
        /// </summary>
        public ButtonsCollection ThirdRowButtonsDefault()
        {
            return new ButtonsCollection
            {
                new VirtualKbButton(Helper.UpperLowerString("a", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("s", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("d", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("f", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("g", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("h", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("j", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("k", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("l", _keyboard), String.Empty),
                new VirtualKbButton(":", ";"),
                new VirtualKbButton("\"" , "'"),
            };
        }


        /// <summary>
        /// The fourth buttons row only with deafult buttons
        /// </summary>
        public ButtonsCollection FourthRowButtonsDefault()
        {
            return new ButtonsCollection
            {
                new VirtualKbButton(Helper.UpperLowerString("z", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("x", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("c", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("v", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("b", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("n", _keyboard), String.Empty),
                new VirtualKbButton(Helper.UpperLowerString("m", _keyboard), String.Empty),
                new VirtualKbButton("<",","),
                new VirtualKbButton(">", "."),
                new VirtualKbButton("?", "/"),
            };
        }


        /// <summary>
        /// The fifth buttons row only with deafult buttons
        /// </summary>
        public ButtonsCollection FifthRowButtonsDefaultShort()
        {
            return new ButtonsCollection
            {

            };
        }

        #endregion

        #region Advanced area


        internal ButtonsCollection FunctionButtonsRowNum = new ButtonsCollection
        {
            new VirtualKbButton(KeyboardKeyConstants.PrtScrText, String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.InsertText, String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.ScrollLockText, String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.WinText, String.Empty)
        };


        internal ButtonsCollection FirstRowButtonsNumAdv = new ButtonsCollection
        {
            new VirtualKbButton(KeyboardKeyConstants.NumLockText, String.Empty),
            new VirtualKbButton("/", String.Empty),
            new VirtualKbButton("*", String.Empty),
            new VirtualKbButton("-", String.Empty)
        };

        internal ButtonsCollection SecondRowButtonsNumAdv = new ButtonsCollection
        {
            new VirtualKbButton("7", KeyboardKeyConstants.HomeText),
            new VirtualKbButton("8", String.Empty),
            new VirtualKbButton("9", KeyboardKeyConstants.PageUpText),
            new VirtualKbButton("+", String.Empty)
        };

        internal ButtonsCollection ThirdRowButtonsNumAdv = new ButtonsCollection
        {
            new VirtualKbButton("4", String.Empty),
            new VirtualKbButton("5", String.Empty),
            new VirtualKbButton("6", String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.NoneString, String.Empty)
        };


        internal ButtonsCollection FourthRowButtonsNumAdv = new ButtonsCollection
        {
            new VirtualKbButton("1", KeyboardKeyConstants.EndText),
            new VirtualKbButton("2", String.Empty),
            new VirtualKbButton("3", KeyboardKeyConstants.PageDownText),
            new VirtualKbButton(KeyboardKeyConstants.EnterText, String.Empty)
        };



        internal ButtonsCollection FifthRowButtonsNumAdv = new ButtonsCollection
        {
            new VirtualKbButton("0", String.Empty),
            new VirtualKbButton(".", KeyboardKeyConstants.DelText),
            new VirtualKbButton(KeyboardKeyConstants.NoneString, String.Empty)
        };


        #endregion

        #region Numeric keyboard

        internal ButtonsCollection FirstRowNum = new ButtonsCollection
        {
            new VirtualKbButton("7", String.Empty),
            new VirtualKbButton("8", String.Empty),
            new VirtualKbButton("9", String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.BackSpaceText, String.Empty)
        };

        internal ButtonsCollection SecondRowNum = new ButtonsCollection
        {
            new VirtualKbButton("4", String.Empty),
            new VirtualKbButton("5", String.Empty),
            new VirtualKbButton("6", String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.DelText, String.Empty)
        };

        internal ButtonsCollection ThirdRowNum = new ButtonsCollection
        {
            new VirtualKbButton("1", String.Empty),
            new VirtualKbButton("2", String.Empty),
            new VirtualKbButton("3", String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.EnterText, String.Empty)
        };

        internal ButtonsCollection FourthRowNum = new ButtonsCollection
        {
            new VirtualKbButton("0", String.Empty),
            new VirtualKbButton(" ", "."),
            new VirtualKbButton(KeyboardKeyConstants.LeftText, String.Empty),
            new VirtualKbButton(KeyboardKeyConstants.RightText, String.Empty)
        };

        #endregion

        #region Layout functions


        /// <summary>
        /// Populates lists fof the buttons
        /// </summary>
        internal void ConcatLists()
        {
            PopulateLists();



            if (_keyboard.ShowNumericButtons)
            {
                if (_keyboard.ShowFunctionButtons)
                {
                    FunctionRowButtons.AddRange(FunctionButtonsDefault);
                    FunctionRowButtons.AddRange(FunctionButtonsRowNum);
                }

                FirstRowButtons.AddRange(FirstRowButtonsNumAdv);
                SecondRowButtons.AddRange(SecondRowButtonsNumAdv);
                ThirdRowButtons.AddRange(ThirdRowButtonsNumAdv);
                FourthRowButtons.AddRange(FourthRowButtonsNumAdv);
                FifthRowButtons.AddRange(FifthRowButtonsNumAdv);
            }
            else
            {
                if (_keyboard.ShowFunctionButtons)
                {
                    FunctionRowButtons = FunctionButtonsDefault;
                }
            }

        }


        /// <summary>
        /// Populates lists from custom settings
        /// </summary>
        private void PopulateLists()
        {
            FunctionRowButtons = new ButtonsCollection();
            FirstRowButtons = new ButtonsCollection();
            FirstRowButtons.AddRange(_keyboard.FirstRowCustomButtons);
            FirstRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.BackSpaceText, String.Empty));


            SecondRowButtons = new ButtonsCollection();
            if (_keyboard.ShowTab)
            {
                SecondRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.TabText, String.Empty));
            }
            SecondRowButtons.AddRange(_keyboard.SecondRowCustomButtons);
            if (_keyboard.ShowDeleteKey)
            {
                SecondRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.DelText, String.Empty));
            }


            ThirdRowButtons = new ButtonsCollection();
            ThirdRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.CapsText, String.Empty));
            ThirdRowButtons.AddRange(_keyboard.ThirdRowCustomButtons);
            ThirdRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.EnterText, String.Empty));
            ThirdRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.NoneString, String.Empty));


            FourthRowButtons = new ButtonsCollection();
            FourthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.ShiftText, String.Empty));
            FourthRowButtons.AddRange(_keyboard.FourthRowCustomButtons);


            if (_keyboard.ShowArrowKeys)
            {
                FourthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.UpText, String.Empty));
            }

            FourthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.ShiftText, String.Empty));
            PopulateFifthRow();

            // Sets decimal separator 
            FourthRowNum.RemoveAt(1);
            FourthRowNum.Insert(1, new VirtualKbButton(" ", _keyboard.DecimalSeparator.ToString()));
            FifthRowButtonsNumAdv.RemoveAt(1);
            FifthRowButtonsNumAdv.Insert(1, new VirtualKbButton(_keyboard.DecimalSeparator.ToString(), KeyboardKeyConstants.DelText));
        }

        /// <summary>
        /// Initial lists filling with default buttons
        /// </summary>
        internal void InitilizeLists()
        {
            FirstRowButtonsCustom = new ButtonsCollection(_keyboard);
            FirstRowButtonsCustom.AddRange(FirstRowButtonsDefault());

            SecondRowButtonsCustom = new ButtonsCollection(_keyboard);
            SecondRowButtonsCustom.AddRange(SecondRowButtonsDefault());

            ThirdRowButtonsCustom = new ButtonsCollection(_keyboard);
            ThirdRowButtonsCustom.AddRange(ThirdRowButtonsDefault());

            FourthRowButtonsCustom = new ButtonsCollection(_keyboard);
            FourthRowButtonsCustom.AddRange(FourthRowButtonsDefault());

            FifthRowButtonsCustom = new ButtonsCollection(_keyboard);
            FifthRowButtonsCustom.AddRange(FifthRowButtonsDefaultShort());
        }


        private void PopulateFifthRow()
        {

            Action addCtrl = () => { FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.CtrlText, String.Empty)); };
            Action addAlt = () => { FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.AltText, String.Empty)); };
            Action addAltGr = () => { FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.AltGrText, String.Empty)); };
            Action addSpace = () => { FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.SpaceText, String.Empty)); };
            Action addArrows = () =>
            {
                FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.LeftText, String.Empty));
                FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.DownText, String.Empty));
                FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.RightText, String.Empty));
            };
            Action addLanguage = () =>
            {
                if (_keyboard.ShowLanguageButton)
                {
                    FifthRowButtons.Add(new VirtualKbButton(_keyboard.LanguageButtonTopText,
                        _keyboard.LanguageButtonBottomText));
                }
            };

            Action addSettings = () => { FifthRowButtons.AddRange(_keyboard.FifthRowCustomButtons); };
            Action addNone = () => { FifthRowButtons.Add(new VirtualKbButton(KeyboardKeyConstants.NoneString, String.Empty)); };

            FifthRowButtons = new ButtonsCollection();

            if (_keyboard.ShowCtrl && _keyboard.ShowAlt && _keyboard.ShowArrowKeys)
            {

                addLanguage();
                addCtrl();
                addAlt();
                addSpace();
                addSettings();
                addAltGr();
                addCtrl();
                addArrows();
            }

            if (!_keyboard.ShowArrowKeys)
            {
                if (_keyboard.ShowCtrl && !_keyboard.ShowAlt)
                {
                    addLanguage();
                    addCtrl();
                    addSpace();
                    addSettings();
                    addCtrl();
                }

                if (!_keyboard.ShowCtrl && _keyboard.ShowAlt)
                {
                    addLanguage();
                    addAlt();
                    addSpace();
                    addSettings();
                    addAltGr();
                }


                if (_keyboard.ShowCtrl && _keyboard.ShowAlt)
                {
                    addLanguage();
                    addCtrl();
                    addAlt();
                    addSpace();
                    addSettings();
                    addAltGr();
                    addCtrl();
                }

                if (!_keyboard.ShowCtrl && !_keyboard.ShowAlt)
                {
                    addNone();
                    addNone();

                    addLanguage();
                    addSpace();
                    addSettings();

                    addNone();
                    addNone();
                }
            }

            if (_keyboard.ShowArrowKeys)
            {
                if (!_keyboard.ShowCtrl && !_keyboard.ShowAlt)
                {
                    addLanguage();
                    addSpace();
                    addSettings();
                    addArrows();
                }

                if (_keyboard.ShowCtrl && !_keyboard.ShowAlt)
                {
                    addLanguage();
                    addCtrl();
                    addSpace();
                    addSettings();
                    addCtrl();
                    addArrows();
                }

                if (!_keyboard.ShowCtrl && _keyboard.ShowAlt)
                {
                    addLanguage();
                    addAlt();
                    addSpace();
                    addSettings();
                    addAltGr();
                    addArrows();
                }
            }
        }

        #endregion
    }

}
