using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;

namespace Keyboard
{
    [Docking(DockingBehavior.Ask)]
    [Designer(typeof(VirtualKeyboardDesigner))]
    public class VirtualKeyboard : Control
    {
        #region Events

        /// <summary>
        /// Delegate that supports item-related events
        /// </summary>
        /// <param name="command"></param>
        /// <param name="e"></param>
        public delegate void KeyboardButtonEventHandler(string command, KeyboardButtonEventArgs e);


        /// <summary>
        /// Occurs when an keyboard button is clicked
        /// </summary>
        [Description("Occurs when an keyboard button is clicked")]
        public event KeyboardButtonEventHandler ButtonClick;

        /// <summary>
        /// Occurs when the keyboard area is clicked
        /// </summary>
        [Description("Occurs when the keyboard area is clicked")]
        public event EventHandler<EventArgs> KeyboardClick;

        #endregion

        #region Private fields

        #region Const

        private const string categoryGroup = "Virtual Keyboard";
        private const string showButtonsCategoryGroup = "Virtual Keyboard Buttons Appearance";
        private const string statesCategoryGroup = "Virtual Keyboard States";

        #endregion

        #region Variables

        private Color _startGradientColor;
        private Color _endGradientColor;
        private Color _colorPressedState;
        private Color _fontColorShiftDisabled;
        private Color _fontColor;
        private Color _fontColorSpecialKey;
        private Font _labelFont;
        private Font _labelFontShiftDisabled;
        private Font _labelFontSpecialKey;
        private Color _buttonBorderColor;
        private int _shadowShift = 0;
        private int _rectRadius = 6;
        private Color _backgroundColor;
        private Color _shadowColorControl;
        private Color _borderColor;
        private bool _isNumeric = false;
        private bool _showBackground = true;
        private bool _showLangButton = false;
        private bool _showTabButton = true;
        private bool _showCtrlButton = true;
        private bool _showAltButton = true;
        private bool _showArrowKeys = true;
        private bool _showDelKey = true;
        private bool _showNumericButtons = true;
        private bool _showFunctionButtons = false;
        private string _languageButtonTopText = "Eng";
        private string _languageButtonBottomtext = String.Empty;
        private Image _languageButtonImage;
        private XDocument _layoutSettings;

        private ButtonsCollection _firstRowCustomButtons;

        private Point mouseClickPosition; // coord. during mouse down event

        private VirtualKbButton buttonOnState;

        private CommandInstruction _cmdInstructions;
        private CommandCombination sentCommand;
        private KeyboardLayout layout;

        private List<VirtualKbButton> buttonsList = new List<VirtualKbButton>();

        private bool _capsLockState = false;
        private bool _numLockState = false;
        private bool _shiftState = false;
        private bool _ctrlState = false;
        private bool _altState = false;
        private bool _altGrState = false;
        private bool _scrollLockState = false;

        private char _decimalSeparator = '.';


        #endregion

        #region Fields

        /// <summary>
        /// The gap between buttons
        /// </summary>
        private int ButtonGap { get { return 4; } }

        /// <summary>
        /// The gap between numeric are and letter buttons area
        /// </summary>
        private int NumKbGap { get { return 10; } }

        /// <summary>
        /// The keyboard padding
        /// </summary>
        private int KeyboardPadding { get { return 10; } }

        /// <summary>
        /// The rows count
        /// </summary>
        private int RowsCount { get { return ShowFunctionButtons ? 6 : 5; } }

        /// <summary>
        /// The default buttons count width of the Space button
        /// </summary>
        private int DefaultSpaceBtnCount { get { return 7; } }

        #endregion

        #region Methods


        /// <summary>
        /// The start index of the numeric area
        /// </summary>
        /// <param name="itemsCount">total buttons count on row</param>
        internal int BeginAdvAreaIndex(int itemsCount, int advNumButtons = 0)
        {
            return
                ShowNumericButtons
                ? itemsCount - advNumButtons
                : itemsCount;
        }

        /// <summary>
        /// The start index of the numeric area for the fifth row
        /// </summary>
        internal int BeginFirsthRowAdvArea()
        {
            return BeginAdvAreaIndex(layout.FirstRowButtons.Count, layout.FirstRowButtonsNumAdv.Count);
        }


        /// <summary>
        /// The start index of the numeric area for the second row
        /// </summary>
        internal int BeginSecondRowAdvArea()
        {
            return BeginAdvAreaIndex(layout.SecondRowButtons.Count, layout.SecondRowButtonsNumAdv.Count);
        }

        /// <summary>
        /// The start index of the numeric area for the third row
        /// </summary>
        internal int BeginThirdRowAdvArea()
        {
            return BeginAdvAreaIndex(layout.ThirdRowButtons.Count, layout.ThirdRowButtonsNumAdv.Count);
        }

        /// <summary>
        /// The start index of the numeric area for the fourth row
        /// </summary>
        internal int BeginFourthRowAdvArea()
        {
            return BeginAdvAreaIndex(layout.FourthRowButtons.Count, layout.FourthRowButtonsNumAdv.Count);
        }

        /// <summary>
        /// The start index of the numeric area for the fifth row
        /// </summary>
        internal int BeginFifthRowAdvArea()
        {
            return
                ShowNumericButtons
                ? (layout.FifthRowButtons.Count + this.SpaceButtonsCountWidth) - layout.FifthRowButtonsNumAdv.Count - 1
                : (layout.FifthRowButtons.Count + this.SpaceButtonsCountWidth) - 1;
        }


        /// <summary>
        ///  Buttons count width for the "Space" button
        /// </summary>
        private int SpaceButtonsCountWidth
        {
            get { return ShowLanguageButton ? 6 - layout.FifthRowButtonsCustom.Count : 7 - layout.FifthRowButtonsCustom.Count; }
        }

        /// <summary>
        /// The width of the "Tab" button
        /// </summary>
        private int TabWidth(int buttonWidth)
        {
            return buttonWidth / 3 + buttonWidth + ButtonGap;
        }

        /// <summary>
        /// The width of the "Backspace" button
        /// </summary>
        private int BackspaceWidth(int buttonWidth)
        {
            return DoubleButtonWidth(buttonWidth) + ButtonGap;
        }


        /// <summary>
        /// The width of the ". Del" from numeric area
        /// </summary>
        private int NumDelWidth(int buttonWidth)
        {
            return DoubleButtonWidth(buttonWidth) + ButtonGap;
        }

        /// <summary>
        /// The width of the "Language" button
        /// </summary>
        private int LangWidth(int buttonWidth)
        {
            return buttonWidth / 3 + buttonWidth + ButtonGap;
        }

        private int SpaceButtonsCount
        {
            get
            {
                return
                  ShowLanguageButton ?
                  (DefaultSpaceBtnCount - 1) - layout.FifthRowButtonsCustom.Count :
                  DefaultSpaceBtnCount - layout.FifthRowButtonsCustom.Count;
            }
        }


        /// <summary>
        /// The width of the "Space" button
        /// </summary>
        private int SpaceWidth(int buttonWidth, int shiftingWidth = 0)
        {
            return buttonWidth * SpaceButtonsCount + ButtonGap + shiftingWidth;
        }

        /// <summary>
        /// The width of the "CapsLock" button
        /// </summary>
        private int CapsWidth(int buttonWidth)
        {
            return buttonWidth / 2 + buttonWidth + ButtonGap;
        }

        /// <summary>
        /// The width of the left "Shift" button
        /// </summary>
        private int LShiftWidth(int buttonWidth, int shiftingWidth = 0)
        {
            int w = DoubleButtonWidth(buttonWidth) + ButtonGap + shiftingWidth;
            return w;
        }


        /// <summary>
        /// The width of the right "Shift" button
        /// </summary>
        private int RShiftWidth(int buttonWidth)
        {
            if (!ShowArrowKeys)
            {
                return LShiftWidth(buttonWidth);
            }
            return buttonWidth;
        }


        /// <summary>
        /// The double width of button
        /// </summary>
        private int DoubleButtonWidth(int buttonWidth)
        {
            return buttonWidth * 2;
        }

        /// <summary>
        /// The width of the "Enter" button
        /// </summary>
        private int EnterWidth(int buttonWidth, int shiftingWidth = 0)
        {
            int w1 = CapsWidth(buttonWidth);
            int w2 = DoubleButtonWidth(buttonWidth) + ButtonGap;

            return DoubleButtonWidth(buttonWidth) + (w2 - w1) + ButtonGap + shiftingWidth;
        }

        /// <summary>
        /// The width of the last button on the second row
        /// </summary>
        private int SecondRowLastBtnWidth(int buttonWidth, int shiftingWidth = 0)
        {
            int w1 = TabWidth(buttonWidth);
            int w2 = DoubleButtonWidth(buttonWidth) + ButtonGap;
            int w3 = w2 - w1;
            int totalW = ShowTab ? buttonWidth + w3 + +shiftingWidth : buttonWidth + shiftingWidth;
            return totalW;
        }



        private int FuncGap(int buttonWidth)
        {
            return buttonWidth / 2;
        }

        /// <summary>
        /// Font for top text of button
        /// </summary>
        private Font ButtonTopFont(VirtualKbButton btn)
        {
            Font topFont = LabelFont;

            // Bottom Text is empty
            if (btn.BottomText == String.Empty)
            {
                topFont = !Helper.IsSpecialKeyText(btn.TopText) ? LabelFont : LabelFontSpecialKey;

                if (btn.TopFont != null)
                {
                    topFont = btn.TopFont;
                }
            }

            // Bottom Text is not empty
            else
            {

                //sets font according to shift state
                if (!Helper.IsSpecialKeyText(btn.BottomText))
                {
                    if (btn.ButtonName != Helper.LangBtnName)
                    {
                        if (ShiftState)
                        {
                            topFont = btn.TopFont == null ? LabelFont : btn.TopFont;
                        }
                        else
                        {
                            topFont = btn.TopFont == null ? LabelFontShiftDisabled : btn.TopFont;
                        }
                    }
                }
            }

            return topFont;
        }

        /// <summary>
        /// Font for bottom text of button
        /// </summary>
        private Tuple<Font, Color, Color> ButtonBottomFont(VirtualKbButton btn)
        {
            Color fontColorBottom = FontColorShiftDisabled;
            Color topFontColor = FontColor;
            Font bottomFont = btn.BottomFont == null ? LabelFontShiftDisabled : btn.BottomFont;

            if (Helper.IsSpecialKeyText(btn.BottomText))
            {
                fontColorBottom = FontColorSpecialKey;
                bottomFont = btn.BottomFont == null ? LabelFontSpecialKey : btn.BottomFont;
            }
            else
            {
                if (btn.ButtonName != Helper.LangBtnName)
                {
                    if (ShiftState)
                    {
                        fontColorBottom = FontColorShiftDisabled;
                        topFontColor = FontColor;
                        bottomFont = btn.BottomFont == null ? LabelFontShiftDisabled : btn.BottomFont;
                    }
                    else
                    {
                        bottomFont = btn.BottomFont == null ? LabelFont : btn.BottomFont;
                        fontColorBottom = FontColor;
                        topFontColor = FontColorShiftDisabled;
                    }
                }
            }

            return Tuple.Create(bottomFont, fontColorBottom, topFontColor);
        }

        #endregion

        #endregion

        #region VirtualKeyboard members


        /// <summary>
        /// The first custom row buttons
        /// </summary>
        [Browsable(false)]
        public ButtonsCollection FirstRowCustomButtons
        {
            get
            {
                return layout.FirstRowButtonsCustom;
            }

            set
            {
                layout.FirstRowButtonsCustom = value;
            }
        }


        /// <summary>
        /// The second custom row buttons
        /// </summary>
        [Browsable(false)]
        public ButtonsCollection SecondRowCustomButtons
        {
            get
            {
                return layout.SecondRowButtonsCustom;
            }
            set
            {
                layout.SecondRowButtonsCustom = value;
            }
        }


        /// <summary>
        /// The third custom row buttons
        /// </summary>
        [Browsable(false)]
        public ButtonsCollection ThirdRowCustomButtons
        {
            get
            {
                return layout.ThirdRowButtonsCustom;
            }
            set
            {
                layout.ThirdRowButtonsCustom = value;
            }
        }

        /// <summary>
        /// The fourth custom row buttons
        /// </summary>
        [Browsable(false)]
        public ButtonsCollection FourthRowCustomButtons
        {
            get
            {
                return layout.FourthRowButtonsCustom;
            }
            set
            {
                layout.FourthRowButtonsCustom = value;
            }
        }


        /// <summary>
        /// The fifth custom row buttons
        /// </summary>
        [Browsable(false)]
        public ButtonsCollection FifthRowCustomButtons
        {
            get
            {
                return layout.FifthRowButtonsCustom;
            }
            set
            {
                layout.FifthRowButtonsCustom = value;
            }
        }

        /// <summary>
        /// The Caps Lock State of the control
        /// </summary>
        [Browsable(true), Category(statesCategoryGroup), Description("Whether the Caps Lock key is pressed.")]
        public bool CapsLockState
        {
            get
            {
                return _capsLockState;
            }
            set
            {
                _capsLockState = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The Num Lock State of the control
        /// </summary>
        [Browsable(true), Category(statesCategoryGroup), Description("Whether the Num Lock key is pressed.")]
        public bool NumLockState
        {
            get
            {
                return _numLockState;
            }
            set
            {
                _numLockState = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The Scroll Lock State of the control
        /// </summary>
        [Browsable(true), Category(statesCategoryGroup), Description("Whether the Scroll Lock key is pressed.")]
        public bool ScrollLockState
        {
            get
            {
                return _scrollLockState;
            }
        }

        /// <summary>
        /// Shows/Hides numeric area of the control
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides numeric area of the control.")]
        public bool ShowNumericButtons
        {
            get
            {
                return _showNumericButtons;
            }
            set
            {
                _showNumericButtons = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Shows/Hides function buttons area of the control
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides function buttons area of the control.")]
        public bool ShowFunctionButtons
        {
            get
            {
                return _showFunctionButtons;
            }
            set
            {
                _showFunctionButtons = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Shows/Hides background of the control
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Shows/Hides background of the control.")]
        public bool ShowBackground
        {
            get
            {
                return _showBackground;
            }
            set
            {
                _showBackground = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Shows/Hides language button
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides language button.")]
        public bool ShowLanguageButton
        {
            get
            {
                return _showLangButton;
            }
            set
            {
                _showLangButton = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Shows/Hides the Arrow buttons
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides the Arrow buttons.")]
        public bool ShowArrowKeys
        {
            get
            {
                return _showArrowKeys;
            }
            set
            {
                _showArrowKeys = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Shows/Hides the Delete button
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides the Delete button.")]
        public bool ShowDeleteKey
        {
            get
            {
                return _showDelKey;
            }
            set
            {
                _showDelKey = value;
                Invalidate();
            }
        }



        /// <summary>
        /// Shows/Hides the Tab button
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides the Tab button.")]
        public bool ShowTab
        {
            get
            {
                return _showTabButton;
            }
            set
            {
                _showTabButton = value;
                Invalidate();
            }
        }



        /// <summary>
        /// Shows/Hides the Ctrl buttons
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides the Ctrl buttons.")]
        public bool ShowCtrl
        {
            get
            {
                return _showCtrlButton;
            }
            set
            {
                _showCtrlButton = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Shows/Hides the Alt buttons
        /// </summary>
        [Browsable(true), Category(showButtonsCategoryGroup), Description("Shows/Hides the Ctrl buttons.")]
        public bool ShowAlt
        {
            get
            {
                return _showAltButton;
            }
            set
            {
                _showAltButton = value;
                Invalidate();
            }
        }



        /// <summary>
        /// Sets language button top text
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Sets language button top text.")]
        public string LanguageButtonTopText
        {
            get
            {
                return _languageButtonTopText;
            }
            set
            {
                _languageButtonTopText = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Sets language button bottom text
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Sets language button bottom text.")]
        public string LanguageButtonBottomText
        {
            get
            {
                return _languageButtonBottomtext;
            }
            set
            {
                _languageButtonBottomtext = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Sets language button picture
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Sets language button picture.")]
        public Image LanguageButtonImage
        {
            get
            {
                return _languageButtonImage;
            }
            set
            {
                _languageButtonImage = value;
                Invalidate();
            }
        }


        /// <summary>
        /// The shift State of the control
        /// </summary>
        [Browsable(true), Category(statesCategoryGroup), Description("Whether Shift key is pressed.")]
        public bool ShiftState
        {
            get
            {
                return _shiftState;
            }
            set
            {
                _shiftState = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The Ctrl State of the control
        /// </summary>
        [Browsable(true), Category(statesCategoryGroup), Description("Whether the Ctrl Lock key is pressed.")]
        public bool CtrlState
        {
            get
            {
                return _ctrlState;
            }
            set
            {
                _ctrlState = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The Alt State of the control
        /// </summary>
        [Browsable(true), Category(statesCategoryGroup), Description("Whether the Alt Lock key is pressed.")]
        public bool AltState
        {
            get
            {
                return _altState;
            }
            set
            {
                _altState = value;
                Invalidate();
            }
        }


        /// <summary>
        /// The AltGr State of the control
        /// </summary>
        [Browsable(true), Category(statesCategoryGroup), Description("Whether the AltGr Lock key is pressed.")]
        public bool AltGrState
        {
            get
            {
                return _altGrState;
            }
            set
            {
                _altGrState = value;
                Invalidate();
            }
        }


        /// <summary>
        /// The begin grad. color for the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The begin grad. color for the buttons.")]
        public Color BeginGradientColor
        {
            get { return _startGradientColor; }
            set
            {
                _startGradientColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The end grad. color of the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The end grad. color of the buttons.")]
        public Color EndGradientColor
        {
            get { return _endGradientColor; }
            set
            {
                _endGradientColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The end grad. color of the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The end grad. color of the buttons.")]
        public Color ColorPressedState
        {
            get { return _colorPressedState; }
            set
            {
                _colorPressedState = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The font color for the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The font color for the buttons.")]
        public Color FontColor
        {
            get { return _fontColor; }
            set
            {
                _fontColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The font color for the advanced buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The font color for the modifier buttons.")]
        public Color FontColorSpecialKey
        {
            get { return _fontColorSpecialKey; }
            set
            {
                _fontColorSpecialKey = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Shift state disabled font color for the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Shift state disabled font color for the buttons.")]
        public Color FontColorShiftDisabled
        {
            get { return _fontColorShiftDisabled; }
            set
            {
                _fontColorShiftDisabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The font for the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The font for the buttons.")]
        public Font LabelFont
        {
            get { return _labelFont; }
            set
            {
                _labelFont = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The font for the advanced buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The font for the modifier buttons.")]
        public Font LabelFontSpecialKey
        {
            get { return _labelFontSpecialKey; }
            set
            {
                _labelFontSpecialKey = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Shift state disabled font for the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Shift state disabled font for the buttons.")]
        public Font LabelFontShiftDisabled
        {
            get { return _labelFontShiftDisabled; }
            set
            {
                _labelFontShiftDisabled = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The border color for the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The border color for the buttons.")]
        public Color ButtonBorderColor
        {
            get { return _buttonBorderColor; }
            set
            {
                _buttonBorderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The shift of the shadow
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The shift of the shadow.")]
        public int ShadowShift
        {
            get { return _shadowShift; }
            set
            {
                if (value > 15 || value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "/* Out of range value*/");
                }

                _shadowShift = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The background color of the control
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The background color of the control.")]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The color of the shadow
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The color of the shadow.")]
        public Color ShadowColor
        {
            get { return _shadowColorControl; }
            set
            {
                _shadowColorControl = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The radius of corners for the buttons
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The radius of corners for the buttons.")]
        public int ButtonRectRadius
        {
            get { return _rectRadius; }
            set
            {
                if (value > 15 || value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "/* Out of range value*/");
                }
                _rectRadius = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The border color of the control
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("The border color of the control.")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Is it numeric keyboard
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Is it numeric keyboard")]
        public bool IsNumeric
        {
            get { return _isNumeric; }
            set
            {
                _isNumeric = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Decimal separator for the numeric keyboard
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description("Decimal separator forthe  numeric keyboard.")]
        public char DecimalSeparator
        {
            get { return _decimalSeparator; }
            set
            {
                _decimalSeparator = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Buttons layout for the keyboard
        /// </summary>
        [Browsable(true), Category(categoryGroup), Description(" Buttons layout for the keyboard.")]
        public KeyboardLayout ButtonsLayout
        {
            get { return layout; }
        }

        #endregion

        public VirtualKeyboard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);


            _startGradientColor = KeyboardColorTable.StartGradientColor;
            _endGradientColor = KeyboardColorTable.EndGradientColor;
            _colorPressedState = KeyboardColorTable.ColorPressedState;
            _fontColor = KeyboardColorTable.FontColor;
            _fontColorShiftDisabled = KeyboardColorTable.FontColorShiftDisabled;
            _fontColorSpecialKey = KeyboardColorTable.FontColorSpecialkey;
            _shadowColorControl = KeyboardColorTable.ShadowColorControl;
            _backgroundColor = KeyboardColorTable.BackgroundColor;
            _borderColor = KeyboardColorTable.BorderColor;
            _buttonBorderColor = KeyboardColorTable.ButtonBorderColor;

            _labelFont = KeyboardColorTable.LabelFont;
            _labelFontSpecialKey = KeyboardColorTable.LabelFontSpecialKey;
            _labelFontShiftDisabled = KeyboardColorTable.LabelFontShiftDisabled;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.Size = new Size(750, 300);
            _cmdInstructions = new CommandInstruction(this);
            layout = new KeyboardLayout(this);

            layout.InitilizeLists();

        }

        #region Apply layout Methods

        /// <summary>
        /// The function to apply deafault buttons layouts
        /// </summary>
        public void ResetLayout()
        {
            layout.InitilizeLists();
        }


        #endregion

        #region Paint Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            layout.ConcatLists();

            DrawKeyboard(e.Graphics);
            base.OnPaint(e);
        }


        #region Draw keyboard area

        /// <summary>
        /// Draws whole keyboard
        /// </summary>
        private void DrawKeyboard(Graphics graphics)
        {
            if (ShowBackground)
            {
                DrawBackground(graphics);
            }

            if (!IsNumeric)
            {
                DrawKeyboardItems(graphics);
            }
            else
            {
                DrawKeyboardItemsNumeric(graphics);
            }
        }

        /// <summary>
        /// Draws all rows for numeric keyboard
        /// </summary>
        private void DrawKeyboardItemsNumeric(Graphics graphics)
        {
            int btnColumnsCountNum = layout.FirstRowNum.Count;
            int btnRowsCountNum = 4;
            buttonsList = new List<VirtualKbButton>();

            int totalWidth = Width - ((btnColumnsCountNum - 2) * ButtonGap) -
                              ShadowShift - (KeyboardPadding * 2);

            int btnWidth = totalWidth / btnColumnsCountNum;

            int btnHeight = (Height - ((btnRowsCountNum - 2) * ButtonGap)
                - (KeyboardPadding * 2) - ShadowShift) / btnRowsCountNum;

            int calcWidth = btnColumnsCountNum * btnWidth + (KeyboardPadding * 2)
                            + ShadowShift + ((btnColumnsCountNum - 1) * ButtonGap);


            int calcHeight = btnRowsCountNum * btnHeight + (KeyboardPadding * 2)
                            + ShadowShift + ((btnRowsCountNum - 1) * ButtonGap);

            int xCoord = ((Width - calcWidth) / 2) + KeyboardPadding;
            int yCoord = ((Height - calcHeight) / 2) + KeyboardPadding;
            int rowNumber = 0;
            DrawButtonsRow(graphics, layout.FirstRowNum, xCoord, yCoord, btnWidth, btnHeight, rowNumber,
                BeginFirsthRowAdvArea());
            yCoord += btnHeight + ButtonGap;
            rowNumber++;

            DrawButtonsRow(graphics, layout.SecondRowNum, xCoord, yCoord, btnWidth, btnHeight, rowNumber,
                BeginFirsthRowAdvArea());
            yCoord += btnHeight + ButtonGap;
            rowNumber++;

            DrawButtonsRow(graphics, layout.ThirdRowNum, xCoord, yCoord, btnWidth, btnHeight, rowNumber,
                BeginFirsthRowAdvArea());
            yCoord += btnHeight + ButtonGap;
            rowNumber++;

            DrawButtonsRow(graphics, layout.FourthRowNum, xCoord, yCoord, btnWidth, btnHeight, rowNumber,
                BeginFirsthRowAdvArea());
        }






        /// <summary>
        /// The function to calcualte width of buttons and end X coordinate before the numeric area
        /// </summary>
        /// <param name="buttonsCount">Total buttons count on row</param>
        /// <param name="buttonsCountCustom">Count only of custom buttons</param>
        /// <param name="buttonsCountDefault">Count only of default buttons</param>
        /// <returns>Tuple of width of buttons and end X coordinate before the numeric area</returns>
        private Tuple<int, int> ApplyWidth(
            int buttonsCount,
            int buttonsCountCustom,
            int buttonsCountDefault)
        {
            int btnNumColumnsCount = layout.FirstRowButtonsNumAdv.Count;
            int w = FirstRowButtonWidth();// button width


            if (buttonsCount > layout.FirstRowButtons.Count)
            {
                w = ButtonsWidthLong(buttonsCountCustom, buttonsCountDefault);
            }

            if (buttonsCount < layout.FirstRowButtons.Count)
            {
                if (layout.FirstRowButtonsDefault().Count < layout.FirstRowButtonsCustom.Count &&
                    buttonsCountDefault < buttonsCountCustom)
                {
                    w = ButtonsWidthLong(buttonsCountCustom, buttonsCountDefault);
                }
                else if ((buttonsCountCustom - buttonsCountDefault) < 0)
                {
                    w = ButtonsWidthLong(buttonsCountCustom, buttonsCountDefault);
                }
                else
                {
                    w = ButtonsWidthShort();
                }

            }

            int endX = w
                * ((buttonsCount + 1) - btnNumColumnsCount)
                + ((buttonsCount - btnNumColumnsCount)) * ButtonGap; // end X coord of area without numeric buttons

            if (!ShowNumericButtons)
            {
                endX = w * (buttonsCount + 1) + buttonsCount * ButtonGap;
            }


            return new Tuple<int, int>(w, endX);
        }


        /// <summary>
        /// The function to calcualte width of buttons and end X coordinate before the numeric area
        /// for the fifth row
        /// </summary>
        /// <returns>Tuple of width of buttons and end X coordinate before the numeric area</returns>
        private Tuple<int, int> ApplyFifthWidth()
        {
            // applying width of buttons for the fifth row
            var fifthWidth = FirstRowButtonWidth(); //width of buttons for the fifth row
            var addedCount = layout.FirstRowButtonsCustom.Count - layout.FirstRowButtonsDefault().Count;

            if (layout.FifthRowButtonsCustom.Count > 0)
            {
                fifthWidth = ButtonsWidthShort();
            }

            if (layout.FifthRowButtonsCustom.Count == 0 && addedCount > 0)
            {
                fifthWidth = ButtonsWidthShort();
            }


            int spaceButtonsCount = SpaceButtonsCount;
            var fifthButtonsCount = (layout.FifthRowButtons.Count - 1) + spaceButtonsCount;

            int fifthEndX = fifthWidth * fifthButtonsCount +
                (layout.FifthRowButtons.Count) * ButtonGap; // end X coord of area without numeric buttons

            if (ShowNumericButtons)
            {
                int reduceAdvCount = layout.FifthRowButtonsNumAdv.Count;
                fifthButtonsCount = ((layout.FifthRowButtons.Count - reduceAdvCount) - 1) +
                    spaceButtonsCount;

                fifthEndX = fifthWidth * (fifthButtonsCount) +
                                (layout.FifthRowButtons.Count - reduceAdvCount) * ButtonGap; // end X coord of area with numeric buttons

            }


            return new Tuple<int, int>(fifthWidth, fifthEndX);
        }



        /// <summary>
        /// Calculates width of area with buttons
        /// </summary>
        /// <param name="columnsCount">buttons count per row</param>
        private int CalculateButtonsArea(int columnsCount)
        {
            return Width - (columnsCount * ButtonGap) -
              ShadowShift - (KeyboardPadding * 2);
        }



        /// <summary>
        ///  Width of buttons on the first row
        /// </summary>
        /// <returns></returns>
        private int FirstRowButtonWidth()
        {
            int btnWidth = 0;
            int totalWidth = 0; // total width of buttons area
            int firstRowColCount = layout.FirstRowButtons.Count;
            totalWidth = CalculateButtonsArea(FirstRowColumnsCount - 2);

            if (ShowNumericButtons)
            {
                totalWidth = totalWidth - NumKbGap;
            }

            btnWidth = totalWidth / (firstRowColCount + 1);
            return btnWidth;
        }


        private int FirstRowColumnsCount
        {
            get { return layout.FirstRowButtons.Count + 1; }
        }



        //qwerty
        /// <summary>
        /// The count of additionally added buttons on the first row
        /// </summary>
        private int FirstRowCustomButtonsCount
        {
            get { return layout.FirstRowButtonsCustom.Count - layout.FirstRowButtonsDefault().Count; }
        }





        /// <summary>
        ///  Width of buttons for row with fewer count of buttons
        /// </summary>
        /// <returns></returns>
        private int ButtonsWidthShort()
        {
            int btnWidthShort = 0;

            int btnColumnsCountShort = FirstRowColumnsCount - FirstRowCustomButtonsCount;
            var totalWidthShort = CalculateButtonsArea(btnColumnsCountShort - 2);
            if (ShowNumericButtons)
            {
                totalWidthShort = totalWidthShort - NumKbGap;
            }

            btnWidthShort = totalWidthShort / btnColumnsCountShort;
            return btnWidthShort;
        }




        /// <summary>
        /// Width of buttons for row with more count of buttons
        /// </summary>
        /// <param name="buttonsCountCustom">Count only of custom buttons</param>
        /// <param name="buttonsCountDefault">Count only of default buttons</param>
        private int ButtonsWidthLong(int buttonsCountCustom, int buttonsCountDefault)
        {
            int btnWidthLong = 0; // width of buttons for row with more count of buttons

            var columnLong = buttonsCountCustom - buttonsCountDefault - FirstRowCustomButtonsCount;
            int btnColumnCountLong = FirstRowColumnsCount + columnLong;

            var totalWLong = CalculateButtonsArea(btnColumnCountLong + (columnLong * 2));

            btnWidthLong = totalWLong / btnColumnCountLong;

            return btnWidthLong;
        }


        private void DrawKeyboardItems(Graphics graphics)
        {
            buttonsList = new List<VirtualKbButton>();
            int calcWidth = 0; // total width of the drawn area for the first row

            int funcButtonsHeight = 0;
            int totalHeight = 0; // total height of the drawn area
            int btnHeight = 0;
            int calcHeight = 0;// total height of the applied area of buttons
            int rowNumber = 0; // current row number

            int xCoord = 0; // start X coordinate of buttons
            int yCoord = 0; // start Y coordinate of buttons


            calcWidth = FirstRowColumnsCount * FirstRowButtonWidth() + (KeyboardPadding * 2)
                + ShadowShift + ((FirstRowColumnsCount - 1) * ButtonGap);

            if (ShowNumericButtons)
            {
                calcWidth = calcWidth + NumKbGap;
            }

            totalHeight = (Height - ((RowsCount - 2) * ButtonGap)
                - (KeyboardPadding * 2) - ShadowShift);

            if (ShowFunctionButtons)
            {
                totalHeight = totalHeight - NumKbGap;
            }


            btnHeight = totalHeight / RowsCount;
            calcHeight = RowsCount * btnHeight + (KeyboardPadding * 2)
                            + ShadowShift + ((RowsCount - 1) * ButtonGap);

            if (ShowFunctionButtons)
            {
                calcHeight = calcHeight + NumKbGap;
                funcButtonsHeight = btnHeight - (btnHeight / 3);
            }

            xCoord = ((Width - calcWidth) / 2) + KeyboardPadding;
            yCoord = ((Height - calcHeight) / 2) + KeyboardPadding;

            // applying width of buttons for the first row row
            var applyButtonWidth =
                ApplyWidth(layout.FirstRowButtons.Count, layout.FirstRowButtonsCustom.Count,
                layout.FirstRowButtonsDefault().Count);


            int endCoordX = xCoord + applyButtonWidth.Item2; // end X coordinate of the first row
            int startNumX = xCoord + applyButtonWidth.Item2 + NumKbGap + ButtonGap; // start X coordinate of the numeric area


            // the row withFunction buttons
            if (ShowFunctionButtons)
            {
                int firstCount = 0;
                int firstDefaultCount = 0;

                if (!ShowNumericButtons)
                {
                    firstCount = firstDefaultCount = layout.FirstRowButtonsDefault().Count;
                }
                else
                {
                    firstCount = firstDefaultCount = layout.FirstRowButtonsDefault().Count;
                    if (layout.FirstRowButtonsDefault().Count > layout.FirstRowButtonsCustom.Count)
                    {
                        int added = layout.FirstRowButtonsDefault().Count - layout.FirstRowButtonsCustom.Count + 1;
                        firstCount = layout.FirstRowButtonsDefault().Count +
                             layout.FirstRowButtonsNumAdv.Count + added;
                        firstDefaultCount = layout.FirstRowButtonsCustom.Count + added;
                    }
                }

                DrawButtonsRow(graphics, layout.FunctionRowButtons, xCoord,
                    yCoord, ApplyWidth(
                    firstCount + 1,
                    firstDefaultCount, 
                    layout.FirstRowButtonsDefault().Count).Item1, 
                    funcButtonsHeight, rowNumber,
                    BeginAdvAreaIndex(layout.FirstRowButtons.Count - FirstRowCustomButtonsCount,
                    layout.FirstRowButtonsNumAdv.Count),
                    FirstRowButtonWidth(), 0, startNumX);
                yCoord += btnHeight + ButtonGap + NumKbGap;
                rowNumber++;
            }

            // the first row
            DrawButtonsRow(graphics, layout.FirstRowButtons, xCoord, yCoord, FirstRowButtonWidth(),
                btnHeight, rowNumber, BeginFirsthRowAdvArea(),
                FirstRowButtonWidth(),
                0,
                startNumX);
            yCoord += btnHeight + ButtonGap;
            rowNumber++;



            int btnCustomCount = 0;
            // applying width of buttons for the second row
            if (!ShowTab)
            {
                btnCustomCount = ShowDeleteKey ? -1 : -2;
                applyButtonWidth =
                    ApplyWidth(layout.SecondRowButtons.Count - 1, layout.SecondRowButtonsCustom.Count + btnCustomCount,
                    layout.SecondRowButtonsDefault().Count);
            }
            else
            {
                btnCustomCount = ShowDeleteKey ? 1 : 0;
                applyButtonWidth =
                    ApplyWidth(layout.SecondRowButtons.Count, layout.SecondRowButtonsCustom.Count + btnCustomCount,
                    layout.SecondRowButtonsDefault().Count);
            }


            // the second row
            DrawButtonsRow(graphics, layout.SecondRowButtons, xCoord, yCoord,
               applyButtonWidth.Item1,
                btnHeight, rowNumber, BeginSecondRowAdvArea(), FirstRowButtonWidth(),
                endCoordX - (xCoord + applyButtonWidth.Item2),
                startNumX);

            yCoord += btnHeight + ButtonGap;
            rowNumber++;


            // applying width of buttons for the third row
            applyButtonWidth =
                ApplyWidth(layout.ThirdRowButtons.Count, layout.ThirdRowButtonsCustom.Count,
                layout.ThirdRowButtonsDefault().Count);

            // the third row
            DrawButtonsRow(graphics, layout.ThirdRowButtons, xCoord, yCoord,
                applyButtonWidth.Item1,
                btnHeight, rowNumber, BeginThirdRowAdvArea(), FirstRowButtonWidth(),
                endCoordX - (xCoord + applyButtonWidth.Item2),
                startNumX);
            yCoord += btnHeight + ButtonGap;
            rowNumber++;

            // applying width of buttons for the fourth row
            btnCustomCount = 1;
            if (!ShowArrowKeys)
            {
                int btnAddCount = 1;

                applyButtonWidth =
                    ApplyWidth(layout.FourthRowButtons.Count + btnAddCount,
                    layout.FourthRowButtonsCustom.Count - btnCustomCount,
                        layout.FourthRowButtonsDefault().Count);
            }
            else if (ShowArrowKeys)
            {
                btnCustomCount = 1;
                applyButtonWidth =
                ApplyWidth(layout.FourthRowButtons.Count, layout.FourthRowButtonsCustom.Count - btnCustomCount,
                layout.FourthRowButtonsDefault().Count);
            }
            else
            {
                applyButtonWidth =
                    ApplyWidth(layout.FourthRowButtons.Count, layout.FourthRowButtonsCustom.Count,
                    layout.FourthRowButtonsDefault().Count);
            }

            // the fourth row
            DrawButtonsRow(graphics, layout.FourthRowButtons, xCoord, yCoord,
                applyButtonWidth.Item1,
                btnHeight, rowNumber, BeginFourthRowAdvArea(), FirstRowButtonWidth(),
                endCoordX - (xCoord + applyButtonWidth.Item2),
                startNumX);
            yCoord += btnHeight + ButtonGap;
            rowNumber++;

            // applying width of buttons for the fifth row row
            var applyFifthButtonsWidth = ApplyFifthWidth();

            // the fifth row
            DrawButtonsRow(graphics, layout.FifthRowButtons, xCoord, yCoord,
                applyFifthButtonsWidth.Item1,
                btnHeight, rowNumber,

                BeginFifthRowAdvArea(), FirstRowButtonWidth(),
                endCoordX - (xCoord + applyFifthButtonsWidth.Item2), startNumX);
        }



        /// <summary>
        /// Draws keyboard single row of buttons
        /// </summary>
        private void DrawButtonsRow(Graphics graphics,
            ButtonsCollection buttons,
            int xCoord,
            int yCoord,
            int btnWidth,
            int btnHeight,
            int rowNumber,
            int beginAdvArea,
            int defaultButtonWidth = 0,
            int shiftingWidth = 0,
            int startNumX = 0)
        {
            Rectangle rect = new Rectangle();
            var btn = new VirtualKbButton();

            // Draws buttons row
            int columnNumber = 0;
            foreach (var item in buttons)
            {

                var isLangButton = ShowLanguageButton && (item.TopText == LanguageButtonTopText);

                btn = new VirtualKbButton(this);
                btn.TopText = item.TopText;
                btn.BottomText = item.BottomText;
                btn.Picture = item.Picture;
                btn.TopFont = item.TopFont;
                btn.BottomFont = item.BottomFont;
                btn.Tag = item.Tag;
                btn.CanSendCommand = item.CanSendCommand;

                //gives names for buttons
                btn.ButtonName = Helper.GiveButtonName(this, rowNumber, columnNumber, item, isLangButton);

                //double height for "+" and "Enter" buttons from numeric area
                var isDoubleHeight =
                    (item.TopText == "+" && columnNumber >= beginAdvArea) ||
                    ((item.TopText == KeyboardKeyConstants.EnterText && columnNumber >= beginAdvArea));

                var isSpecialKeyButton = Helper.IsSpecialKeyText(item.TopText) || Helper.IsSpecialKeyText(item.TopText);

                if (columnNumber >= beginAdvArea)
                {
                    btnWidth = defaultButtonWidth;
                }



                if (item.TopText != KeyboardKeyConstants.NoneString)
                {
                    var w = btnWidth;

                    btnWidth = SetButtonWidth(btnWidth, rowNumber, item,
                         isLangButton, columnNumber, shiftingWidth);

                    rect = new Rectangle(xCoord, yCoord, btnWidth,
                        !isDoubleHeight ? btnHeight : (2 * btnHeight) + ButtonGap);

                    btn.Rectangle = rect;

                    DrawButton(graphics, btn);

                    btn.IsSpecialKey = isSpecialKeyButton;
                    buttonsList.Add(btn);
                    btnWidth = w;
                }


                if (columnNumber == (beginAdvArea - 1))
                {
                    xCoord = startNumX;
                }

                else
                {
                    xCoord = SetButtonXCoord(buttons, xCoord, btnWidth, rowNumber,
                        item, isLangButton, columnNumber, beginAdvArea, shiftingWidth);
                }

                columnNumber++;

                if (item.TopText == KeyboardKeyConstants.SpaceText)
                {
                    columnNumber += (this.SpaceButtonsCountWidth - 1);
                }
            }
        }




        private int SetButtonXCoord(ButtonsCollection buttons, int startX, int btnWidth,
            int rowNumber, VirtualKbButton item,
            bool isLangButton, int columnNumber,
            int beginAdvArea,
            int shiftingWidth = 0)
        {

            // the function to calcualte X coordinate according to applied width
            Func<int, int> applyX = width => width - btnWidth;

            //reached "Space" button
            if (item.TopText == KeyboardKeyConstants.SpaceText)
            {
                // startX += (btnWidth * this.SpaceButtonsCountWidth) + (ButtonGap * this.SpaceButtonsCountWidth);
                startX += applyX(SpaceWidth(btnWidth, shiftingWidth));
            }
            if (item.TopText == KeyboardKeyConstants.TabText)
            {
                startX += applyX(TabWidth(btnWidth));

            }

            //reached "Language" button from numeric area
            if (isLangButton)
            {
                startX += applyX(LangWidth(btnWidth));

            }

            if (item.TopText == KeyboardKeyConstants.CapsText)
            {
                startX += applyX(CapsWidth(btnWidth));

            }

            //reached  "Left Shift" button
            if (item.TopText == KeyboardKeyConstants.ShiftText &&
                buttonsList.Count(b => b.TopText == KeyboardKeyConstants.ShiftText) == 1)
            {
                startX += applyX(LShiftWidth(btnWidth, shiftingWidth));

            }


            if (item.TopText == KeyboardKeyConstants.SpaceText)
            {
                if (ShowLanguageButton)
                {
                    startX = startX - applyX(LangWidth(btnWidth));
                }
            }


            startX += (btnWidth + ButtonGap);

            //if reached the area of advanced buttons
            if ((columnNumber == (beginAdvArea - 1) && !buttons.Any(b => b.TopText == KeyboardKeyConstants.EscText)))
            {
                startX += NumKbGap;
            }

            //if reached the area of advanced buttons for function buttons row
            if ((columnNumber == (beginAdvArea - 1) && buttons.Any(b => b.TopText == KeyboardKeyConstants.EscText)))
            {
                startX += NumKbGap;
            }


            if (item.TopText == KeyboardKeyConstants.F4 || item.TopText == KeyboardKeyConstants.F8)
            {
                startX += FuncGap(btnWidth);
            }
            return startX;
        }


        private int SetButtonWidth(int btnWidth, int rowNumber, VirtualKbButton item,
                bool isLangButton, int columnNumber, int shiftingWidth = 0)
        {
            // with for "Space" button
            if (item.TopText == KeyboardKeyConstants.SpaceText)
            {
                var oldW = btnWidth;
                btnWidth = SpaceWidth(btnWidth, shiftingWidth);
                if (ShowLanguageButton)
                {
                    btnWidth = btnWidth - LangWidth(oldW) + oldW;
                }
            }

            // with for ". Del" button from numeric area
            if (item.TopText == DecimalSeparator.ToString() && item.BottomText == KeyboardKeyConstants.DelText)
            {
                btnWidth = NumDelWidth(btnWidth);
            }

            // with for "Backspace" button
            if (item.TopText == KeyboardKeyConstants.BackSpaceText && !IsNumeric)
            {
                btnWidth = BackspaceWidth(btnWidth);
            }

            // with for "Tab" button
            if (item.TopText == KeyboardKeyConstants.TabText)
            {
                btnWidth = TabWidth(btnWidth);
            }

            // width of language button
            if (isLangButton)
            {
                btnWidth = LangWidth(btnWidth);
            }


            // width for the "Left Shift" button
            if (item.TopText == KeyboardKeyConstants.ShiftText &&
                !buttonsList.Any(b => b.TopText == KeyboardKeyConstants.ShiftText))
            {
                btnWidth = LShiftWidth(btnWidth, shiftingWidth);
            }

            // width for the "Right Shift" button
            if (item.TopText == KeyboardKeyConstants.ShiftText &&
                buttonsList.Any(b => b.TopText == KeyboardKeyConstants.ShiftText))
            {
                btnWidth = RShiftWidth(btnWidth);
            }

            // width for "Caps" button
            if (item.TopText == KeyboardKeyConstants.CapsText)
            {
                btnWidth = CapsWidth(btnWidth);
            }

            // width for the last button on the second row
            if ((ShowFunctionButtons ? rowNumber == 2 : rowNumber == 1) && (columnNumber ==
                BeginSecondRowAdvArea() - 1))
            {
                btnWidth = SecondRowLastBtnWidth(btnWidth, shiftingWidth);
            }

            // width for "Enter" button
            if (item.TopText == KeyboardKeyConstants.EnterText && !IsNumeric &&
                !buttonsList.Any(b => b.TopText == KeyboardKeyConstants.EnterText))
            {

                btnWidth = EnterWidth(btnWidth, shiftingWidth);
            }
            return btnWidth;
        }

        #endregion

        #region Draw Button

        /// <summary>
        /// Draws keyboard button single button with top and bottom text
        /// </summary>
        private void DrawButton(Graphics graphics, VirtualKbButton btn)
        {
            int xTextOffset = 2; // left text offset
            int yTextTopOffset = 2; // top text offset
            int yTextBottomOffset = 4; // bottom text offset

            int offsetBorder = 1; // border of button offset
            int offsetBorderShadow = 2; // border of buttons's shadow offset

            if (btn.Rectangle.Width > 0 && btn.Rectangle.Width > 0)
            {
                DrawButtonBackground(graphics, btn, offsetBorder, offsetBorderShadow);

                if (string.IsNullOrEmpty(btn.BottomText))
                {
                    DrawTopText(graphics, btn, xTextOffset, yTextTopOffset);
                }
                else
                {
                    DrawTopBottomText(graphics, btn,
                        xTextOffset, yTextTopOffset,
                        yTextBottomOffset);
                }

                DrawButtonImage(graphics, btn, xTextOffset,
                    yTextTopOffset, yTextBottomOffset, offsetBorder, offsetBorder);


                //draws image for lang. button
                if (btn.ButtonName == Helper.LangBtnName)
                {
                    DrawLangImage(graphics, btn,
                        xTextOffset, yTextTopOffset, yTextBottomOffset);
                }
            }

        }

        private void DrawButtonImage(Graphics graphics, VirtualKbButton btn,
            int xTextOffset, int yTextTopOffset, int yTextBottomOffset,
            int offsetBorder,
            int offsetBorderShadow)
        {

            int x = btn.Rectangle.X;
            int y = btn.Rectangle.Y;

            int btnHeight = btn.Rectangle.Height;
            int btnWidth = btn.Rectangle.Width;

            if (btn.Picture != null)
            {

                var sizeBottom = graphics.MeasureString(btn.BottomText, ButtonBottomFont(btn).Item1);
                var sizeTop = graphics.MeasureString(btn.TopText, ButtonTopFont(btn));

                int xBottomText = x + xTextOffset;
                int imageX = 0;
                int imageY = 0;

                int imageWidth = 0;
                int imageHeight = 0;

                if (string.IsNullOrEmpty(btn.BottomText) && string.IsNullOrEmpty(btn.TopText))
                {
                    imageWidth = btnWidth / 2;
                    imageHeight = btnHeight / 2;

                    imageX = x + offsetBorder + btnWidth / 2 - (imageWidth / 2);
                    imageY = (y + offsetBorderShadow + btnHeight / 2) - (imageHeight / 2); ;
                }
                else if (string.IsNullOrEmpty(btn.BottomText))
                {
                    imageX = x + 5 * xTextOffset + offsetBorder;
                    imageY = (int)(y + yTextTopOffset + sizeTop.Height);
                    imageWidth = btnWidth - 2 * (imageX - x);
                    imageHeight = (int)(btnHeight - sizeTop.Height - 2 * yTextBottomOffset);
                }
                else
                {
                    imageX = x + 5 * xTextOffset + offsetBorder;
                    imageY = (int)(y + yTextTopOffset + sizeTop.Height);
                    imageWidth = btnWidth - 2 * (imageX - x);
                    imageHeight = (int)(btnHeight - sizeBottom.Height - sizeTop.Height - yTextBottomOffset);

                }

                graphics.DrawImage(btn.Picture,
                    new Rectangle(imageX,
                        imageY,
                        imageWidth,
                        imageHeight));
            }


        }

        private void DrawLangImage(Graphics graphics, VirtualKbButton btn,
            int xTextOffset, int yTextTopOffset, int yTextBottomOffset)
        {
            int x = btn.Rectangle.X;
            int y = btn.Rectangle.Y;

            int btnHeight = btn.Rectangle.Height;
            int btnWidth = btn.Rectangle.Width;

            if (LanguageButtonImage != null)
            {
                var sizeBottom = graphics.MeasureString(btn.BottomText, LabelFontShiftDisabled);
                var sizeTop = graphics.MeasureString(btn.TopText, LabelFont);

                int xBottomText = x + xTextOffset;
                int imageX = 0;
                int imageY = 0;

                int imageWidth = 0;
                int imageHeight = 0;

                if (string.IsNullOrEmpty(btn.BottomText) && string.IsNullOrEmpty(btn.TopText))
                {
                    imageX = x + 5 * xTextOffset;
                    imageY = y + 5 * xTextOffset;
                    imageWidth = btnWidth - (imageX - x) * 2;
                    imageHeight = btnHeight - (imageY - y) * 2;
                }
                else if (string.IsNullOrEmpty(btn.BottomText))
                {
                    imageX = x + 5 * xTextOffset;
                    imageY = (int)(y + yTextTopOffset + sizeTop.Height);
                    imageWidth = btnWidth - 2 * (imageX - x);
                    imageHeight = (int)(btnHeight - sizeTop.Height - 2 * yTextBottomOffset);
                }
                else
                {
                    imageX = (int)(xBottomText + sizeBottom.Width) + 2 * xTextOffset;
                    imageY = (int)(y + yTextTopOffset + sizeTop.Height);
                    imageWidth = (x + btnWidth) - (int)(xBottomText + sizeBottom.Width) - 5 * xTextOffset;
                    imageHeight =
                        ((int)((y + btnHeight - sizeBottom.Height - yTextBottomOffset) +
                                sizeBottom.Height) -
                         (int)(y + yTextTopOffset + sizeTop.Height)) - yTextBottomOffset;

                }

                graphics.DrawImage(LanguageButtonImage,
                    new Rectangle(imageX,
                        imageY,
                        imageWidth,
                        imageHeight));
            }
        }

        /// <summary>
        /// The function to return font and color for text according to Shift state
        /// </summary>
        private void DrawTopBottomText(Graphics graphics, VirtualKbButton btn,
            int xTextOffset, int yTextTopOffset, int yTextBottomOffset)
        {


            var topText = btn.TopText;
            var bottomText = btn.BottomText;
            int xBottomText;

            var bottomFont = ButtonBottomFont(btn);
            Font topFont = ButtonTopFont(btn);

            int x = btn.Rectangle.X;
            int y = btn.Rectangle.Y;

            int height = btn.Rectangle.Height;
            int width = btn.Rectangle.Width;


            var sizeBottom = graphics.MeasureString(bottomText, bottomFont.Item1);

            if (Helper.IsSpecialKeyText(bottomText))
            {
                xBottomText = x + xTextOffset;
            }
            else
            {
                if (btn.ButtonName != Helper.LangBtnName)
                {
                    xBottomText = (int)((x + width) - 3 * xTextOffset - sizeBottom.Width);
                }
                else
                {
                    xBottomText = x + xTextOffset;
                }
            }

            graphics.TextRenderingHint =
                TextRenderingHint.ClearTypeGridFit;

            using (Brush brush = new SolidBrush(bottomFont.Item3))
            {
                graphics.DrawString(topText, topFont, brush,
                    new Point(x + xTextOffset, y + yTextTopOffset));
            }

            using (Brush brush = new SolidBrush(bottomFont.Item2))
            {
                graphics.DrawString(bottomText, bottomFont.Item1, brush,
                    new Point(xBottomText,
                        (int)(y + height - sizeBottom.Height - yTextBottomOffset)));
            }
        }

        private void DrawTopText(Graphics graphics,
            VirtualKbButton btn,
            int xTextOffset, int yTextTopOffset)
        {
            var topText = btn.TopText;
            var fontTop = ButtonTopFont(btn);

            var fontColorTop = !Helper.IsSpecialKeyText(topText) ? FontColor : FontColorSpecialKey;
            int x = btn.Rectangle.X;
            int y = btn.Rectangle.Y;

            if (topText == KeyboardKeyConstants.SpaceText)
            {
                topText = String.Empty;
            }

            if (topText == KeyboardKeyConstants.TabText || topText == KeyboardKeyConstants.ShiftText)
            {
                fontTop = new Font(fontTop.FontFamily, fontTop.Size * (float)2, fontTop.Style);
            }

            var size = graphics.MeasureString(topText, fontTop);

            //splits button text by space (i.e. Caps Lock, Num Lock...)
            var buttonText = topText.Split(' ');

            using (Brush brush = new SolidBrush(fontColorTop))
            {
                graphics.TextRenderingHint =
                    TextRenderingHint.ClearTypeGridFit;

                graphics.DrawString(buttonText[0], fontTop, brush,
                    new Point(x + xTextOffset, y + yTextTopOffset));

                if (buttonText.Count() > 1)
                {
                    graphics.TextRenderingHint =
                        TextRenderingHint.ClearTypeGridFit;

                    yTextTopOffset -= 3;
                    graphics.DrawString(buttonText[1], fontTop, brush,
                        new Point(x + xTextOffset, y + yTextTopOffset + (int)size.Height));
                }
            }
        }

        private void DrawButtonBackground(
            Graphics graphics,
            VirtualKbButton btn,
            int offsetBorder,
            int offsetBorderShadow)
        {
            var topText = btn.TopText;

            var gradientColor = EndGradientColor;
            var rect = btn.Rectangle;

            if ((buttonOnState != null && buttonOnState.ButtonName == btn.ButtonName) ||
                Helper.HoldPressedState(topText, this))
            {
                gradientColor = ColorPressedState;
            }

            //Draws button border
            using (SolidBrush solidBrush = new SolidBrush(ControlPaint.Dark(ButtonBorderColor, 0.2f)))
            {
                RoundedRectangle.DrawFilledRoundedRectangle(graphics, solidBrush, rect, _rectRadius);
            }

            //Draws button background
            using (LinearGradientBrush gradientBrush = KeyboardColorTable.ItemGradientBackBrush(rect,
                BeginGradientColor, gradientColor))
            {
                rect = new Rectangle(rect.X + offsetBorder, rect.Y + offsetBorder,
                    rect.Width - offsetBorderShadow, rect.Height - offsetBorderShadow);
                RoundedRectangle.DrawFilledRoundedRectangle(graphics, gradientBrush, rect, _rectRadius);
            }


            //Draws button border
            using (Pen pen = new Pen(ControlPaint.Light(SystemColors.InactiveBorder, 0.00f)))
            {
                rect = new Rectangle(rect.X + offsetBorder, rect.Y + offsetBorder,
                    rect.Width - offsetBorderShadow, rect.Height - offsetBorderShadow);
                RoundedRectangle.DrawRoundedRectangle(graphics, pen, rect, _rectRadius);
            }

        }

        #endregion

        #region Draw keyboard background

        /// <summary>
        /// Draws background with shadow
        /// </summary>
        private void DrawBackground(Graphics graphics)
        {
            //filled shadow rect
            Rectangle Rect = new Rectangle(ShadowShift, ShadowShift, Width - ShadowShift, Height - ShadowShift);
            using (Brush brush = new SolidBrush(ShadowColor))
            {
                RoundedRectangle.DrawFilledRoundedRectangle(graphics, brush, Rect, _rectRadius);
            }


            //filled background rect
            Rect = new Rectangle(0, 0, Width - ShadowShift, Height - ShadowShift);
            using (Brush BackgroundGradientBrush = new SolidBrush(BackgroundColor))
            {
                RoundedRectangle.DrawFilledRoundedRectangle(graphics, BackgroundGradientBrush, Rect, _rectRadius);
            }

            //background border
            using (Pen pen = new Pen(this._borderColor))
            {
                var w = Width - ShadowShift;
                var h = Height - ShadowShift;
                Rect = new Rectangle(0, 0, ShadowShift == 0 ? w - 1 : w
                    , ShadowShift == 0 ? h - 1 : h);
                graphics.DrawRectangle(pen, Rect);
            }

        #endregion

        }
        #endregion

        #region Mouse Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseClickPosition = new Point(e.X, e.Y);

            var btn = buttonsList.Where(d =>
                           d.Rectangle.Contains(mouseClickPosition)).SingleOrDefault();
            sentCommand = new CommandCombination();

            if (btn != null)
            {
                buttonOnState = btn;
                if (btn.ButtonName != Helper.LangBtnName)
                {
                    if (btn.CanSendCommand)
                    {
                        //if not special key and not numeric button pressed
                        if (!btn.IsSpecialKey && !btn.ButtonName.Contains(Helper.AdvBtn))
                        {
                            sentCommand = _cmdInstructions.RegularCommand(btn);
                        }
                        else
                        {
                            // if function button pressed
                            if (Regex.Match(btn.TopText, KeyboardKeyConstants.FuncButtonsReg).Success)
                            {
                                sentCommand = _cmdInstructions.FuncKeyCombinationCommand(btn);
                            }
                            // if button in the numeric area  pressed
                            else if (btn.ButtonName.Contains(Helper.AdvBtn))
                            {
                                sentCommand = _cmdInstructions.NumericAreaCommand(btn);
                            }

                            else
                            {
                                // if special key(i.e. Enter, Shift, Tab...) pressed
                                sentCommand = _cmdInstructions.SpecialKeyCommand(btn);
                            }

                        }
                    }
                }
                else
                {
                    sentCommand.CommandText = KeyboardKeyConstants.LanguageSelection;
                }

                Invalidate();
            }

            if (!string.IsNullOrEmpty(sentCommand.Command))
            {
                SendKeys.Send(sentCommand.Command);
            }
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (buttonOnState != null)
            {
                OnButtonClick(sentCommand.CommandText, new KeyboardButtonEventArgs(buttonOnState));
                buttonOnState = null;
                sentCommand = new CommandCombination();
            }

            if (KeyboardClick != null)
            {
                KeyboardClick(this, EventArgs.Empty);
            }

            Invalidate();
        }



        protected override void OnClick(EventArgs e)
        {

        }


        protected virtual void OnButtonClick(string command, KeyboardButtonEventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(command, e);
            }
        }


        #region Touch device Methods

        /// <summary>
        /// To recognize MouseDown, MouseUp events on a touch device
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeConstants.WM_POINTERDOWN:
                    break;
                case NativeConstants.WM_POINTERUP:
                    break;
                default:
                    base.WndProc(ref m);
                    return;
            }


            int pointerID = NativeConstants.GET_POINTER_ID(m.WParam);
            NativeConstants.POINTER_INFO pi = new NativeConstants.POINTER_INFO();
            if (!NativeConstants.GetPointerInfo(pointerID, ref pi))
            {
                NativeConstants.CheckLastError();
            }
            Point pt = PointToClient(pi.PtPixelLocation.ToPoint());
            MouseEventArgs me = new MouseEventArgs(MouseButtons.Left, 0, pt.X, pt.Y, 0);
            switch (m.Msg)
            {
                case NativeConstants.WM_POINTERDOWN:
                    OnMouseDown(me);
                    break;
                case NativeConstants.WM_POINTERUP:
                    OnClick(me);
                    OnMouseUp(me);
                    break;

            }
        }
        #endregion

        #endregion
    }

    #region Smart Tag Methods


    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class VirtualKeyboardDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new VirtualKeyboardActionList(this.Component));
                }
                return actionLists;
            }
        }
    }



    public class VirtualKeyboardActionList : DesignerActionList
    {
        private VirtualKeyboard keybControl;

        //The constructor associates the control with the smart tag list.
        public VirtualKeyboardActionList(IComponent component)
            : base(component)
        {
            this.keybControl = component as VirtualKeyboard;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(keybControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching VirtualKeyboard property not found!", propName);
            else
                return prop;
        }

        private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        // Properties that are targets of DesignerActionPropertyItem entries.
        public bool ShowNumericButtons
        {
            get
            {
                return keybControl.ShowNumericButtons;
            }
            set
            {
                GetPropertyByName(GetPropertyName(() => keybControl.ShowNumericButtons)).SetValue(keybControl, value);
            }
        }

        public bool ShowFunctionButtons
        {
            get
            {
                return keybControl.ShowFunctionButtons;
            }
            set
            {
                GetPropertyByName(GetPropertyName(() => keybControl.ShowFunctionButtons)).SetValue(keybControl, value);
            }
        }

        public bool IsNumeric
        {
            get
            {
                return keybControl.IsNumeric;
            }
            set
            {
                GetPropertyByName(GetPropertyName(() => keybControl.IsNumeric)).
                    SetValue(keybControl, value);
            }
        }



        // Implementation of this abstract method creates smart tag  items, associates their targets, and collects into list.
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            var appearanceArea = "Appearance";
            items.Add(new DesignerActionHeaderItem(appearanceArea));

            items.Add(new DesignerActionPropertyItem(GetPropertyName(() => keybControl.ShowNumericButtons),
                                 "Show Numeric Buttons", appearanceArea,
                                 "Shows/Hides numeric area."));
            items.Add(new DesignerActionPropertyItem(GetPropertyName(() => keybControl.ShowFunctionButtons),
                                 "Show Function Buttons", appearanceArea,
                                 "Shows/Hides function buttons area."));
            items.Add(new DesignerActionPropertyItem(GetPropertyName(() => keybControl.IsNumeric),
                     "Numeric keyboard", appearanceArea,
                     "Is it numeric keyboard"));



            return items;
        }

    }

    #endregion


}