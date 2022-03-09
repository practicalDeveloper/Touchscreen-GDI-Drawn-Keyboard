using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Keyboard
{
  public abstract class RoundedRectangle
  {

    public static GraphicsPath DrawRoundedRectangle(Rectangle rect, 
                                      int radius)
    {
      
      int x = rect.X;
      int y = rect.Y;
      int width = rect.Width;
      int height = rect.Height;

      int xw = x + width;
      int yh = y + height;
      int xwr = xw - radius;
      int yhr = yh - radius;
      int xr = x + radius;
      int yr = y + radius;
      int r2 = radius * 2;
      int xwr2 = xw - r2;
      int yhr2 = yh - r2;

      GraphicsPath p = new GraphicsPath();
      p.StartFigure();

      //Top Left Corner
      if (r2 >0)
      {
          p.AddArc(x, y, r2, r2, 180, 90);
      }
      else
      {
          p.AddLine(x, yr, x, y);
          p.AddLine(x, y, xr, y);
      }

      //Top Edge
      p.AddLine(xr, y, xwr, y);

      //Top Right Corner
      if (r2 > 0)
      {
        p.AddArc(xwr2, y, r2, r2, 270, 90);
      }
      else
      {
          p.AddLine(xwr, y, xw, y);
          p.AddLine(xw, y, xw, yr);
      }

      //Right Edge
      p.AddLine(xw, yr, xw, yhr);

      //Bottom Right Corner
      if (r2 > 0)
     {
        p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
     }
      else
      {
          p.AddLine(xw, yhr, xw, yh);
          p.AddLine(xw, yh, xwr, yh);
      }

      //Bottom Edge
      p.AddLine(xwr, yh, xr, yh);

      //Bottom Left Corner
      if (r2 > 0)
      {
          p.AddArc(x, yhr2, r2, r2, 90, 90);
      }
      else
      {
          p.AddLine(xr, yh, x, yh);
          p.AddLine(x, yh, x, yhr);
      }

      //Left Edge
      p.AddLine(x, yhr, x, yr);

      p.CloseFigure();
      return p;
    }

    public static GraphicsPath DrawFilledRoundedRectangle(Graphics graphics, Brush rectBrush, Rectangle rect,
                        int radius)
    {
        GraphicsPath path = DrawRoundedRectangle(rect, radius);
        SmoothingMode mode = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.FillPath(rectBrush, path);
        graphics.SmoothingMode = mode;
        return path;
    }

    public static GraphicsPath DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle rect,
                 int radius)
    {
        GraphicsPath path = DrawRoundedRectangle(rect, radius);
        SmoothingMode mode = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.DrawPath(pen, path);
        graphics.SmoothingMode = mode;
        return path;
    }


  }
}

