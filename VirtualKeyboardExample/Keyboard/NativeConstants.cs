using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Keyboard
{
    public static class NativeConstants
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetPointerInfo(int pointerID, ref POINTER_INFO pointerInfo);

        public const int WM_POINTERDOWN = 0x0246;
        public const int WM_POINTERUP = 0x247;
        public const int WM_GESTURENOTIFY = 0x011A;
        public const int WM_GESTURE = 0x0119;

        internal enum POINTER_INPUT_TYPE
        {
            POINTER = 0x00000001,
            TOUCH = 0x00000002,
            PEN = 0x00000003,
            MOUSE = 0x00000004
        }

        [Flags]
        internal enum POINTER_FLAGS
        {
            NONE = 0x00000000,
            NEW = 0x00000001,
            INRANGE = 0x00000002,
            INCONTACT = 0x00000004,
            FIRSTBUTTON = 0x00000010,
            SECONDBUTTON = 0x00000020,
            THIRDBUTTON = 0x00000040,
            FOURTHBUTTON = 0x00000080,
            FIFTHBUTTON = 0x00000100,
            PRIMARY = 0x00002000,
            CONFIDENCE = 0x00004000,
            CANCELED = 0x00008000,
            DOWN = 0x00010000,
            UPDATE = 0x00020000,
            UP = 0x00040000,
            WHEEL = 0x00080000,
            HWHEEL = 0x00100000,
            CAPTURECHANGED = 0x00200000,
        }

        [Flags]
        internal enum VIRTUAL_KEY_STATES
        {
            NONE = 0x0000,
            LBUTTON = 0x0001,
            RBUTTON = 0x0002,
            SHIFT = 0x0004,
            CTRL = 0x0008,
            MBUTTON = 0x0010,
            XBUTTON1 = 0x0020,
            XBUTTON2 = 0x0040
        }

        #region POINT

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }
            public POINT(Point pt)
            {
                X = pt.X;
                Y = pt.Y;
            }
            public Point ToPoint()
            {
                return new Point(X, Y);
            }
            public void AssignTo(ref Point destination)
            {
                destination.X = X;
                destination.Y = Y;
            }
            public void CopyFrom(Point source)
            {
                X = source.X;
                Y = source.Y;
            }
            public void CopyFrom(POINT source)
            {
                X = source.X;
                Y = source.Y;
            }
        }

        #endregion

        #region POINTER_INFO

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINTER_INFO
        {
            public POINTER_INPUT_TYPE pointerType;
            public int PointerID;
            public int FrameID;
            public POINTER_FLAGS PointerFlags;
            public IntPtr SourceDevice;
            public IntPtr WindowTarget;
            [MarshalAs(UnmanagedType.Struct)]
            public POINT PtPixelLocation;
            [MarshalAs(UnmanagedType.Struct)]
            public POINT PtPixelLocationRaw;
            [MarshalAs(UnmanagedType.Struct)]
            public POINT PtHimetricLocation;
            [MarshalAs(UnmanagedType.Struct)]
            public POINT PtHimetricLocationRaw;
            public uint Time;
            public uint HistoryCount;
            public uint InputData;
            public VIRTUAL_KEY_STATES KeyStates;
            public long PerformanceCount;
            public int ButtonChangeType;
        }

        internal static int GET_POINTER_ID(IntPtr wParam)
        {
            return LOWORD(wParam);
        }

        internal static int LOWORD(IntPtr wParam)
        {
            return (int)(wParam.ToInt64() & 0xffff);
        }

        internal static void CheckLastError()
        {
            int errCode = Marshal.GetLastWin32Error();
            if (errCode != 0)
            {
                throw new Win32Exception(errCode);
            }
        }

        #endregion

    }
}
