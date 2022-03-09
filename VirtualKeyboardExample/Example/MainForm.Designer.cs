using TextBoxKeyboard;

namespace Example
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_NumericKeyboard = new System.Windows.Forms.Button();
            this.lbl_ExampleText = new System.Windows.Forms.Label();
            this.btn_CustomizeKeyboard = new System.Windows.Forms.Button();
            this.btn_Keyboard = new System.Windows.Forms.Button();
            this.txt_ExampleText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxWithKeyboard3 = new TextBoxKeyboard.TextBoxWithKeyboard();
            this.textBoxWithKeyboard1 = new TextBoxKeyboard.TextBoxWithKeyboard();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_NumericKeyboard);
            this.groupBox1.Controls.Add(this.lbl_ExampleText);
            this.groupBox1.Controls.Add(this.btn_CustomizeKeyboard);
            this.groupBox1.Controls.Add(this.btn_Keyboard);
            this.groupBox1.Controls.Add(this.txt_ExampleText);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(28, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(808, 239);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyboard examples:";
            // 
            // btn_NumericKeyboard
            // 
            this.btn_NumericKeyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_NumericKeyboard.Location = new System.Drawing.Point(682, 74);
            this.btn_NumericKeyboard.Name = "btn_NumericKeyboard";
            this.btn_NumericKeyboard.Size = new System.Drawing.Size(120, 53);
            this.btn_NumericKeyboard.TabIndex = 6;
            this.btn_NumericKeyboard.Text = "Keyboard for .NET application";
            this.btn_NumericKeyboard.UseVisualStyleBackColor = true;
            this.btn_NumericKeyboard.Click += new System.EventHandler(this.btn_NumericKeyboard_Click);
            // 
            // lbl_ExampleText
            // 
            this.lbl_ExampleText.AutoSize = true;
            this.lbl_ExampleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_ExampleText.Location = new System.Drawing.Point(6, 26);
            this.lbl_ExampleText.Name = "lbl_ExampleText";
            this.lbl_ExampleText.Size = new System.Drawing.Size(80, 15);
            this.lbl_ExampleText.TabIndex = 4;
            this.lbl_ExampleText.Text = "Text for input:";
            // 
            // btn_CustomizeKeyboard
            // 
            this.btn_CustomizeKeyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_CustomizeKeyboard.Location = new System.Drawing.Point(682, 134);
            this.btn_CustomizeKeyboard.Name = "btn_CustomizeKeyboard";
            this.btn_CustomizeKeyboard.Size = new System.Drawing.Size(120, 46);
            this.btn_CustomizeKeyboard.TabIndex = 3;
            this.btn_CustomizeKeyboard.Text = "Customize Keyboard";
            this.btn_CustomizeKeyboard.UseVisualStyleBackColor = true;
            this.btn_CustomizeKeyboard.Click += new System.EventHandler(this.btn_CustomizeKeyboard_Click);
            // 
            // btn_Keyboard
            // 
            this.btn_Keyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Keyboard.Location = new System.Drawing.Point(682, 20);
            this.btn_Keyboard.Name = "btn_Keyboard";
            this.btn_Keyboard.Size = new System.Drawing.Size(120, 50);
            this.btn_Keyboard.TabIndex = 2;
            this.btn_Keyboard.Text = "Keyboard Emulator";
            this.btn_Keyboard.UseVisualStyleBackColor = true;
            this.btn_Keyboard.Click += new System.EventHandler(this.btn_Keyboard_Click);
            // 
            // txt_ExampleText
            // 
            this.txt_ExampleText.Location = new System.Drawing.Point(92, 20);
            this.txt_ExampleText.Multiline = true;
            this.txt_ExampleText.Name = "txt_ExampleText";
            this.txt_ExampleText.Size = new System.Drawing.Size(572, 206);
            this.txt_ExampleText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "TextBox with keyboard:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxWithKeyboard3);
            this.groupBox2.Controls.Add(this.textBoxWithKeyboard1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(28, 269);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(808, 109);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TextBox with keyboard examples:";
            // 
            // textBoxWithKeyboard3
            // 
            this.textBoxWithKeyboard3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWithKeyboard3.IsNumeric = true;
            this.textBoxWithKeyboard3.Location = new System.Drawing.Point(245, 71);
            this.textBoxWithKeyboard3.Name = "textBoxWithKeyboard3";
            this.textBoxWithKeyboard3.Size = new System.Drawing.Size(539, 22);
            this.textBoxWithKeyboard3.TabIndex = 9;
            // 
            // textBoxWithKeyboard1
            // 
            this.textBoxWithKeyboard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWithKeyboard1.IsNumeric = false;
            this.textBoxWithKeyboard1.Location = new System.Drawing.Point(245, 38);
            this.textBoxWithKeyboard1.Name = "textBoxWithKeyboard1";
            this.textBoxWithKeyboard1.Size = new System.Drawing.Size(539, 22);
            this.textBoxWithKeyboard1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Numeric TextBox with keyboard:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 390);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ExampleText;
        private System.Windows.Forms.Button btn_Keyboard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_ExampleText;
        private System.Windows.Forms.Button btn_CustomizeKeyboard;
        private System.Windows.Forms.Button btn_NumericKeyboard;
        private System.Windows.Forms.Label label1;
        private TextBoxWithKeyboard textBoxWithKeyboard1;
        private System.Windows.Forms.GroupBox groupBox2;
        private TextBoxWithKeyboard textBoxWithKeyboard3;
        private System.Windows.Forms.Label label2;




    }
}

