using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alphabet_v1.Alphabets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Alphabet_v1
{
  
    //struct s
    //{
    //    public Graphics e;
    //}

    public partial class Form1 :  Form

    {
        BaseAlphabets alphabet;   

        public Form1()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            Text = "Japanese trainer v0.1";
        }

       
        //public static object Clone(object obj)
        //{
        //    if (obj == null) { return null; }
        //    object result = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        BinaryFormatter bf = new BinaryFormatter();
        //        bf.Serialize(ms, obj);
        //        ms.Position = 0;
        //        result = bf.Deserialize(ms);
        //    }
        //    return result;
        //}


         
        static protected PaintEventArgs E;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {  
            E = new PaintEventArgs(this.CreateGraphics(), e.ClipRectangle);
          
            if (alphabet != null)
            {
                alphabet.Display(e.Graphics); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        public void hiraganaToolStripMenuItem_Click(object sender, EventArgs e)
        {
               alphabet = new Hiragana();
            //alphabet.Dispose();
            Invalidate(false);
        }

        protected void katakanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //alphabet = new Katakana();
            //alphabet.Dispose();
         
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (alphabet != null)
            {
                alphabet.Mouse_Down(e);
                Invalidate();
            }
                
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (alphabet != null)
            {
                alphabet.Mouse_Move(e);
                Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (alphabet != null)
            { 
                alphabet.Mouse_Up(e);
                Invalidate();
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            
           // alphabet.mResize();
            //Invalidate();
        }
    }
}
