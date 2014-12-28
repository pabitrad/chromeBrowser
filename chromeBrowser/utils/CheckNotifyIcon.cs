using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace chromeBrowser
{
    class CheckNotifyIcon
    {
        
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;

                public override string ToString()
                {
                    return "(" + left + ", " + top + ") --> (" + right + ", " + bottom + ")";
                }
            }

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr FindWindow(string strClassName, string strWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);


            public static IntPtr GetTrayHandle()
            {
                IntPtr taskBarHandle = CheckNotifyIcon.FindWindow("Shell_TrayWnd", null);
                if (!taskBarHandle.Equals(IntPtr.Zero))
                {
                    return CheckNotifyIcon.FindWindowEx(taskBarHandle, IntPtr.Zero, "TrayNotifyWnd", IntPtr.Zero);
                }
                return IntPtr.Zero;
            }

            public static Rectangle GetTrayRectangle()
            {
                CheckNotifyIcon.RECT rect;
                CheckNotifyIcon.GetWindowRect(CheckNotifyIcon.GetTrayHandle(), out rect);
                return new Rectangle(new Point(rect.left, rect.top), new Size((rect.right - rect.left) + 1, (rect.bottom - rect.top) + 1));
            }
        }
    
}
