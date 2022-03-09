using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Keyboard
{
    /// <summary>
    /// List of buttons to add to the keyboard
    /// </summary>
    public class ButtonsCollection : List<VirtualKbButton>
    {
        public VirtualKeyboard Keyboard
        {
            get { return _keyboard; }
        }

        private readonly VirtualKeyboard _keyboard;

        public ButtonsCollection()
        {

        }

        public ButtonsCollection(VirtualKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public new void Add(VirtualKbButton button)
        {
            button.Keyboard = _keyboard;
            base.Add(button);
            if (Keyboard != null)
            {
                _keyboard.Invalidate();
            }
        }

        public new void Remove(VirtualKbButton button)
        {
            button.Keyboard = _keyboard;
            base.Remove(button);
            if (Keyboard != null)
            {
                _keyboard.Invalidate();
            }
        }

    }



    /// <summary>
    /// Button for virtual keyboard
    /// </summary>
    public class VirtualKbButton
    {
        private string topText = string.Empty;
        private string bottomText = string.Empty;
        private bool canSendCommand = true;

        internal VirtualKeyboard Keyboard { set; get; }
        internal Rectangle Rectangle { get; set; }
        internal bool IsSpecialKey { get; set; }


        public string ButtonName
        {
            get; internal set;
        }


        /// <summary>
        /// Top text of button
        /// </summary>
        public string TopText { 
            get { return topText; } 
            set { topText = value; } 
        }


        /// <summary>
        /// Bottom text of button
        /// </summary>
        public string BottomText
        {
            get { return bottomText; }
            set { bottomText = value; }
        }

        public string Tag { get; set; }

        /// <summary>
        /// Picture to draw in the center of button
        /// </summary>
        public Image Picture { get; set; }

        /// <summary>
        /// Font for top text of button
        /// </summary>
        public Font TopFont { get; set; }

        /// <summary>
        /// Font for bottom text of button
        /// </summary>
        public Font BottomFont { get; set; }
        
        /// <summary>
        /// Whether Button can send text command
        /// </summary>
        public bool CanSendCommand
        {
            get { return canSendCommand; }
            set { canSendCommand = value; }
        }

        

        public VirtualKbButton()
        {

        }

        public VirtualKbButton(VirtualKeyboard k)
        {
            Keyboard = k;
        }

        /// <summary>
        /// Button for virtual keyboard
        /// </summary>
        /// <param name="topText">Top Text for button</param>
        /// <param name="bottomtext">Bottom Text for button</param>
        /// <param name="picture">Image for button</param>
        /// <param name="buttonTopFont">Top Text font</param>
        /// <param name="bottomnBottomFont">Bottom Text for button</param>
        public VirtualKbButton(string topText, string bottomtext,
            Image picture = null, Font buttonTopFont = null, Font bottomnBottomFont = null)
        {
            TopText = topText;
            BottomText = bottomtext;
            Picture = picture;
            TopFont = buttonTopFont;
            BottomFont = bottomnBottomFont;
        }
    }



}
