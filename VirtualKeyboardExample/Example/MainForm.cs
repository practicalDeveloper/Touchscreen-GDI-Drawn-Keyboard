using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Example
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Keyboard_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(KeyboardLoop);
            txt_ExampleText.Focus();
        }

        private void KeyboardLoop(object state)
        {
            if (!(IsFoundForm("KeyboradExampleForm")))
            {
                Application.Run(new KeyboradExampleForm() { });
            }
        }

        private bool IsFoundForm(string formName)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == formName)
                {
                    return true;
                }
            }

            return false;
        }



        private void btn_CustomizeKeyboard_Click(object sender, EventArgs e)
        {
            var frm = new CustomizeKeyboardForm();
            frm.Show();
        }

        private void btn_NumericKeyboard_Click(object sender, EventArgs e)
        {
            var frm = new KeyboardNetApplicationForm();
            frm.Show();
            txt_ExampleText.Focus();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


    }
}
