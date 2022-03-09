using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Keyboard;

namespace TextBoxKeyboard
{
    public class TextBoxWithKeyboard : TextBox
    {
        private const int numFormWidth = 200; // width of the form with Numeric keyboard
        private const int numFormHeight = 180;// height of the form with Numeric keyboard

        private const string frmKbName = "OnScreenKeyboardForm";
        private const string kbControlName = "virtualKeyboard";

        private static KeyboardFilter kf = null;
        private bool isShown = false;
        private bool isCapsLock = false;
        private bool isNumLock = false;
        private bool isAlt = false;
        private bool isShift = false;
        private bool isCtrl = false;

        Point locationPoint;

        /// <summary>
        /// Checks If the keyboard Is Numeric
        /// </summary>
        [Browsable(true), Category("TextBox with Virtual Keyboard"), Description("Checks If the keyboard Is Numeric")]
        public bool IsNumeric
        {
            get;
            set;
        }


        public TextBoxWithKeyboard()
        {
            if (kf == null)
            {
                kf = new KeyboardFilter();
                Application.AddMessageFilter(kf);
            }
            kf.MouseDown += kf_MouseDown;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            ToggleForm();

        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                this.Focus();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {

        }



        private bool FormVisible
        {
            get
            {
                var f = FindKeyboardForm();
                if (f != null)
                {
                    return true;
                }

                return false;
            }
        }

        private Form FindKeyboardForm()
        {
            var formName = frmKbName;

            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == formName)
                {
                    return frm;
                    break;
                }
            }

            return null;
        }




        private void kf_MouseDown()
        {
            if (FormVisible)
            {
                var f = FindKeyboardForm() as OnScreenKeyboardForm;

                VirtualKeyboard kb = f.Controls.Find(kbControlName, true).FirstOrDefault() as VirtualKeyboard;

                if (!this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position) &&
                !kb.RectangleToScreen(kb.ClientRectangle).Contains(Cursor.Position))
                {
                    isShown = false;

                }
                else
                {
                    isShown = true;
                }


                ToggleForm();
            }
        }

        private void ToggleForm()
        {
            if (FormVisible)
            {
                if (!isShown)
                {
                    CloseForm();
                }
            }
            else
            {
                ShowKeyboard();
            }
        }

        private void ShowKeyboard()
        {

                var popup = new OnScreenKeyboardForm(IsNumeric, isCapsLock, isNumLock, isShift, isAlt, isCtrl);
                int posX;

                if (IsNumeric)
                {
                    posX = Left;
                    locationPoint = GetPosition(posX, popup);
                    popup.Width = numFormWidth;
                    popup.Height = numFormHeight;
                }
                else
                {
                    posX = (Left + Width/2) - popup.Width/2;
                    locationPoint = GetPosition(posX, popup);
                }

                popup.LocationPoint = locationPoint;
                popup.Show();

        }

        private Point GetPosition(int posX, Form popup)
        {
            Screen Srn = Screen.PrimaryScreen;
            const int startPanelHeight = 45;
            Point location;
            location = (this.Parent.PointToScreen(new Point(posX, Bottom)));

            int x = 0;
            int y = 0;

            if (location.X + popup.Width > Srn.Bounds.Width)
            {
                x = popup.Width - (Srn.Bounds.Width - location.X);
            }
            else if (location.X < 0)
            {
                x = location.X;
            }
            else
            {
                x = 0;
            }

            if (location.Y + popup.Height > (Srn.Bounds.Height - startPanelHeight))
            {
                y = popup.Height + this.Height + 5;
            }

            location = new Point((location.X - x), location.Y - y);

            return location;
        }

        private void CloseForm()
        {
            var form = FindKeyboardForm() as OnScreenKeyboardForm;

            if (form != null)
            {
                VirtualKeyboard kb = form.Controls.Find(kbControlName, true).FirstOrDefault() as VirtualKeyboard;
                isCapsLock = kb.CapsLockState;
                isNumLock = kb.NumLockState;
                isShift = kb.ShiftState;
                isAlt = kb.AltState;
                isCtrl = kb.CtrlState;

                form.Close();
                form.Dispose();
            }

        }

        protected override void Dispose(bool disposing)
        {
            
        }



        private class KeyboardFilter : IMessageFilter
        {

            public delegate void LeftButtonDown();
            public event LeftButtonDown MouseDown;

            public delegate void KeyPressUp(IntPtr target);
            public event KeyPressUp KeyUp;

            private const int WM_KEYUP = 0x101;
            private const int WM_LBUTTONDOWN = 0x201;

            bool IMessageFilter.PreFilterMessage(ref Message m)
            {
                switch (m.Msg)
                {
                    // raises our KeyUp() event whenever a key is released 
                    case WM_KEYUP:
                        if (KeyUp != null)
                        {
                            KeyUp(m.HWnd);
                        }
                        break;

                    // raises our MouseDown() event whenever the mouse is left clicked 
                    case WM_LBUTTONDOWN:
                        if (MouseDown != null)
                        {
                            MouseDown();
                        }
                        break;
                }
                return false;
            }
        }
    }
}
