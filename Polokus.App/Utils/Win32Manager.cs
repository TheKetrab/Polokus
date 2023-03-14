using Polokus.App.Forms;

namespace Polokus.App.Utils
{
    public static class Win32Manager
    {
        public const int WM_NCCALCSIZE = 0x0083;   //Standar Title Bar - Snap Window
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MINIMIZE = 0xF020;     //Minimize form (Before)
        public const int SC_RESTORE = 0xF120;      //Restore form (Before)
        public const int WM_NCHITTEST = 0x0084;    //Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
        public const int resizeAreaSize = 10;

        // Resize/WM_NCHITTEST values
        public const int HTCLIENT = 1;             //Represents the client area of the window
        public const int HTLEFT = 10;              //Left border of a window, allows resize horizontally to the left
        public const int HTRIGHT = 11;             //Right border of a window, allows resize horizontally to the right
        public const int HTTOP = 12;               //Upper-horizontal border of a window, allows resize vertically up
        public const int HTTOPLEFT = 13;           //Upper-left corner of a window border, allows resize diagonally to the left
        public const int HTTOPRIGHT = 14;          //Upper-right corner of a window border, allows resize diagonally to the right
        public const int HTBOTTOM = 15;            //Lower-horizontal border of a window, allows resize vertically down
        public const int HTBOTTOMLEFT = 16;        //Lower-left corner of a window border, allows resize diagonally to the left
        public const int HTBOTTOMRIGHT = 17;       //Lower-right corner of a window border, allows resize diagonally to the right

        // More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest



        public static void HandleMsgResize(MainWindow window, ref Message m)
        {
            if (window.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
            {
                if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                {
                    Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                    Point clientPoint = window.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                    if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                    {
                        if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                            m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                        else if (clientPoint.X < (window.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                            m.Result = (IntPtr)HTTOP; //Resize vertically up
                        else //Resize diagonally to the right
                            m.Result = (IntPtr)HTTOPRIGHT;
                    }
                    else if (clientPoint.Y <= (window.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                    {
                        if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                            m.Result = (IntPtr)HTLEFT;
                        else if (clientPoint.X > (window.Width - resizeAreaSize))//Resize horizontally to the right
                            m.Result = (IntPtr)HTRIGHT;
                    }
                    else
                    {
                        if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else if (clientPoint.X < (window.Size.Width - resizeAreaSize)) //Resize vertically down
                            m.Result = (IntPtr)HTBOTTOM;
                        else //Resize diagonally to the right
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                    }
                }
            }

        }
        public static void HandleRestoringSize(MainWindow window, ref Message m)
        {
            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
            /// Quote:
            /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
            /// are used internally by the system.To obtain the correct result when testing 
            /// the value of wParam, an application must combine the value 0xFFF0 with the 
            /// wParam value by using the bitwise AND operator.
            int wParam = (m.WParam.ToInt32() & 0xFFF0);
            if (wParam == SC_MINIMIZE)  //Before
                window.FormSize = window.ClientSize;
            if (wParam == SC_RESTORE)// Restored form(Before)
                window.Size = window.FormSize;
        }
    }
}
