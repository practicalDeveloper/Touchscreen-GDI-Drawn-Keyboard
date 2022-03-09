using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Keyboard
{
    internal class KeyboardColorTable
    {
        public static readonly Color StartGradientColor = Color.White;
        public static readonly Color EndGradientColor = Color.FromArgb(224, 224, 224);
        public static readonly Color ColorPressedState = Color.FromArgb(254, 145, 78);
        
        public static readonly Color ButtonBorderColor = Color.DarkGray;
        public static readonly Color FontColor = Color.Black;
        public static readonly Color FontColorShiftDisabled = Color.DimGray;
        public static readonly Color BackgroundColor = Color.FromArgb(164, 209, 255);
        public static readonly Color ShadowColorControl = Color.LightGray;
        public static readonly Color BorderColor = Color.Black;
        public static readonly Color FontColorSpecialkey = Color.DimGray;
        public static readonly Font LabelFont = new Font("Tahoma", 11, FontStyle.Bold,
                                GraphicsUnit.Point, 204);
        public static readonly Font LabelFontSpecialKey = new Font("Tahoma", 8, FontStyle.Bold,
                        GraphicsUnit.Point, 204);
        public static readonly Font LabelFontShiftDisabled= new Font("Tahoma", 9, FontStyle.Bold,
                        GraphicsUnit.Point, 204);


        public static LinearGradientBrush ItemGradientBackBrush(Rectangle bounds, Color startBackColor, Color endBackColor)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, startBackColor, endBackColor, LinearGradientMode.Vertical);
            return linearGradientBrush;
        }


    }
}
