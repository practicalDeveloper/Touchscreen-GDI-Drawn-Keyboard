using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using Keyboard;

namespace Example
{

    public partial class CustomizeKeyboardForm : Form
    {

        VirtualKbButton button = new VirtualKbButton("", "");

        public CustomizeKeyboardForm()
        {
            InitializeComponent();

            panel_Begin.BackColor = virtualKeyboard.BeginGradientColor;
            panel_EndColor.BackColor = virtualKeyboard.EndGradientColor;
            panel_FontColor.BackColor = virtualKeyboard.FontColor;
            panel_OtionalFontColor.BackColor = virtualKeyboard.FontColorSpecialKey;
            panel_PrssedState.BackColor = virtualKeyboard.ColorPressedState;
            panel_BackgroundColor.BackColor = virtualKeyboard.BackgroundColor;

            ch_ShowBackground.Checked = virtualKeyboard.ShowBackground;

            SetLabelFont();
        }

        private void SetLabelFont()
        {
            lbl_Font.Text = virtualKeyboard.LabelFont.Name + " " +
                virtualKeyboard.LabelFont.Style  + " " + virtualKeyboard.LabelFont.Size;
            lbl_SpecialKeyFont.Text = virtualKeyboard.LabelFontSpecialKey.Name + " " +
                virtualKeyboard.LabelFontSpecialKey.Style + " " + virtualKeyboard.LabelFontSpecialKey.Size;
        }


        private void virtualKeyboard_KeyboardButtonPressed(string command, KeyboardButtonEventArgs e)
        {
            ch_Alt.Checked = virtualKeyboard.AltState;
            ch_CapsLock.Checked = virtualKeyboard.CapsLockState;
            ch_Ctrl.Checked = virtualKeyboard.CtrlState;
            ch_ShiftState.Checked = virtualKeyboard.ShiftState;
            ch_AltGr.Checked = virtualKeyboard.AltGrState;

        }

        private void ch_ShowNumeric_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowNumericButtons = ch_ShowNumeric.Checked;
        }

        private void ch_NumericKeyb_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.IsNumeric = ch_NumericKeyb.Checked;
        }

        private void ch_CapsLock_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.CapsLockState = ch_CapsLock.Checked;
        }

        private void ch_ShiftState_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShiftState = ch_ShiftState.Checked;
        }

        private void ch_Ctrl_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.CtrlState = ch_Ctrl.Checked;
        }

        private void ch_Alt_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.AltState = ch_Alt.Checked;
        }

        private void tr_Radius_Scroll(object sender, EventArgs e)
        {
            virtualKeyboard.ButtonRectRadius = tr_Radius.Value;
        }

        private void tr_SadowShift_Scroll(object sender, EventArgs e)
        {
            virtualKeyboard.ShadowShift = tr_SadowShift.Value;
        }

        private void btn_BeginColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                virtualKeyboard.BeginGradientColor = panel_Begin.BackColor = colorDialog.Color;
            }
        }

        private void btn_EndGradColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                virtualKeyboard.EndGradientColor = panel_EndColor.BackColor = colorDialog.Color;
            }
        }

        private void btn_Font_Click(object sender, EventArgs e)
        {
            fontDialog.Font = virtualKeyboard.LabelFont;
            DialogResult result = fontDialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                string str = fontDialog.Font.Name;

                virtualKeyboard.LabelFont = new Font(str, fontDialog.Font.Size, fontDialog.Font.Style);
                SetLabelFont();
            }
        }

        private void btn_OptFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = virtualKeyboard.LabelFontSpecialKey;
            DialogResult result = fontDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string str = fontDialog.Font.Name;

                virtualKeyboard.LabelFontSpecialKey = new Font(str, fontDialog.Font.Size, fontDialog.Font.Style);
                SetLabelFont();
            }
        }

        private void btn_PressedStateColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                virtualKeyboard.ColorPressedState = panel_PrssedState.BackColor = colorDialog.Color;
            }
        }

        private void ch_ShowBackground_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowBackground = ch_ShowBackground.Checked;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_FontColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                virtualKeyboard.FontColor = panel_FontColor.BackColor = colorDialog.Color;
            }
        }

        private void btn_SpecialKeyFont_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                virtualKeyboard.FontColorSpecialKey = panel_OtionalFontColor.BackColor = colorDialog.Color;
            }
        }

        private void ch_ShowFunctional_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowFunctionButtons = ch_ShowFunctional.Checked;
        }

        private void btn_BackgroundColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                virtualKeyboard.BackgroundColor = panel_BackgroundColor.BackColor = colorDialog.Color;
            }
        }

        private void CustomizeKeyboardForm_Load(object sender, EventArgs e)
        {

        }


        private void ch_AltGr_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.AltGrState = ch_AltGr.Checked;
        }

        private void ch_ShowTab_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowTab = ch_ShowTab.Checked;
        }

        private void ch_ShowDel_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowDeleteKey = ch_ShowDel.Checked;
        }

        private void ch_ShowCtrl_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowCtrl = ch_ShowCtrl.Checked;
        }

        private void ch_ShowAlt_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowAlt = ch_ShowAlt.Checked;
        }

        private void ch_ShowArrow_CheckedChanged(object sender, EventArgs e)
        {
            virtualKeyboard.ShowArrowKeys = ch_ShowArrow.Checked;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            virtualKeyboard.FirstRowCustomButtons.Add(button);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            virtualKeyboard.SecondRowCustomButtons.Add(button);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            virtualKeyboard.ThirdRowCustomButtons.Add(button);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            virtualKeyboard.FourthRowCustomButtons.Add(button);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            virtualKeyboard.FifthRowCustomButtons.Add(button);
        }



        /// <summary>
        /// Removes button from keyboard's button collection
        /// </summary>
        private void RemoveButton(ButtonsCollection buttons)
        {
            if (buttons.Count > 0)
            {
                var btn = buttons[buttons.Count - 1];
                buttons.Remove(btn);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RemoveButton(virtualKeyboard.FirstRowCustomButtons);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RemoveButton(virtualKeyboard.SecondRowCustomButtons);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RemoveButton(virtualKeyboard.ThirdRowCustomButtons);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RemoveButton(virtualKeyboard.FourthRowCustomButtons);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RemoveButton(virtualKeyboard.FifthRowCustomButtons);
        }




    }
}
