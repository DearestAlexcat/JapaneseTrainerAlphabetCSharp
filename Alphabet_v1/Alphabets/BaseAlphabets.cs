using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Alphabet_v1.Alphabets
{
 
    public abstract class BaseAlphabets : Form1
    {
        public abstract void Display(Graphics gr);
        public abstract void Mouse_Down(System.Windows.Forms.MouseEventArgs e);
        public abstract void Mouse_Move(System.Windows.Forms.MouseEventArgs e);
        public abstract void Mouse_Up(System.Windows.Forms.MouseEventArgs e);
        public abstract void mResize();   
         
    }
}
