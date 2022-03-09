using System;
using System.Drawing;
using System.Windows.Forms;
using Keyboard;

namespace TextBoxKeyboard
{
    public partial class OnScreenKeyboardForm : Form
    {
        const int WS_EX_NOACTIVATE = 0x08000000;
        const int WS_EX_TOPMOST = 0x00000008;
        const int WS_EX_WINDOWEDGE = 0x00000100;
        const int WS_CHILD = 0x40000000;
        private const int WS_EX_TOOLWINDOW = 0x00000080;


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams ret = base.CreateParams;
                ret.Style =
                   WS_CHILD;
                ret.ExStyle |= WS_EX_NOACTIVATE |
                    WS_EX_TOPMOST |
                    WS_EX_WINDOWEDGE |
                    WS_EX_TOOLWINDOW;
                ret.X = LocationPoint.X;
                ret.Y = LocationPoint.Y;
                return ret;
            }
        }


        public Point LocationPoint
        {
            get; set;
        }

        public OnScreenKeyboardForm()
        {
            InitializeComponent();
        }

        public OnScreenKeyboardForm(bool isNumeric, bool isCapsLock, bool isNumLock,
            bool isShift, bool isAlt, bool isCtrl)
        {
            InitializeComponent();

            virtualKeyboard.IsNumeric = isNumeric;
            virtualKeyboard.CapsLockState = isCapsLock;
            virtualKeyboard.NumLockState = isNumLock;
            virtualKeyboard.ShiftState = isShift;
            virtualKeyboard.AltState = isAlt;
            virtualKeyboard.CtrlState = isCtrl;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
            this.TransparencyKey = Color.Silver;
        }

        private void virtualKeyboard_ButtonClick(string command, Keyboard.KeyboardButtonEventArgs e)
        {
            KeyboardLayout layout = new KeyboardLayout();

            var secondRow = layout.SecondRowButtonsDefault();
            ApplyLowerUpperText(secondRow);
            virtualKeyboard.SecondRowCustomButtons = secondRow;

            var thirdRow = layout.ThirdRowButtonsDefault();
            ApplyLowerUpperText(thirdRow);
            virtualKeyboard.ThirdRowCustomButtons = thirdRow;

            var fourthRow = layout.FourthRowButtonsDefault();
            ApplyLowerUpperText(fourthRow);
            virtualKeyboard.FourthRowCustomButtons = fourthRow;
        }

        private void ApplyLowerUpperText(ButtonsCollection buttons)
        {
            if (virtualKeyboard.ShiftState && virtualKeyboard.CapsLockState)
            {
                foreach (var item in buttons)
                {
                    item.TopText = item.TopText.ToLower();
                }
            }
            else if (virtualKeyboard.ShiftState || virtualKeyboard.CapsLockState)
            {
                foreach (var item in buttons)
                {
                    item.TopText = item.TopText.ToUpper();
                }
            }
            else
            {
                foreach (var item in buttons)
                {
                    item.TopText = item.TopText.ToLower();
                }
            }
        }
    }
}
