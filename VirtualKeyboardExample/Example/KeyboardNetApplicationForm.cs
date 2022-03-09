using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Example.Properties;
using Keyboard;

namespace Example
{
    public partial class KeyboardNetApplicationForm : Form
    {
        const int WS_EX_NOACTIVATE = 0x08000000;
        const int WS_EX_TOPMOST = 0x00000008;
        const int WS_CHILD = 0x40000000;
        const int WS_BORDER = 0x00800000;
        const int WS_DLGFRAME = 0x00400000;
        const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;

        const int WS_SYSMENU = 0x00080000;
        const int WS_MAXIMIZEBOX = 0x00010000;
        const int WS_MINIMIZEBOX = 0x00020000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_SIZEBOX = WS_THICKFRAME;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams ret = base.CreateParams;
                ret.Style = WS_CAPTION |
                    WS_SIZEBOX | WS_SYSMENU |
                    WS_MINIMIZEBOX |
                    WS_MAXIMIZEBOX | WS_CHILD;

                ret.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOPMOST);
                // ret.X = this.Location.X;
                // ret.Y = this.Location.Y;
                StartPosition = FormStartPosition.CenterScreen;
                return ret;
            }
        }

        public KeyboardNetApplicationForm()
        {
            InitializeComponent();
        }

        private void KeyboardNetApplicationForm_Load(object sender, EventArgs e)
        {
            Font buttonFont = new Font("Tahoma", 8, FontStyle.Bold, GraphicsUnit.Point);
            VirtualKbButton btn = new VirtualKbButton();
            btn.TopFont = buttonFont;
            btn.Tag = "btn_Internet";
            btn.TopText = "www";
            btn.CanSendCommand = false;

            virtualKeyboard1.FifthRowCustomButtons.Add(btn);
        }

        private void virtualKeyboard1_ButtonClick(string command, Keyboard.KeyboardButtonEventArgs e)
        {
            StateHelpers.ApplyState(virtualKeyboard1);
            if (e.Button.Tag == "btn_Internet")
            {
                System.Diagnostics.Process.Start("iexplore");
            }
        }
    }
}
