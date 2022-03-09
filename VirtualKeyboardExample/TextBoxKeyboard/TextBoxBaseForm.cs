using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextBoxKeyboard
{
    public partial class TextBoxBaseForm : Form
    {
        const uint WS_EX_NOACTIVATE = 0x08000000;
        const uint WS_EX_TOPMOST = 0x00000008;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= (int)(WS_EX_NOACTIVATE | WS_EX_TOPMOST);
                return baseParams;
            }
        }

        public TextBoxBaseForm()
        {
            InitializeComponent();
        }
    }
}
