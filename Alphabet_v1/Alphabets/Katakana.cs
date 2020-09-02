using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Alphabet_v1.Alphabets
{
    class Katakana /*: BaseAlphabets*/
    {
        Font font;
        Rectangle rect;

        protected string[] sKatakana =
        {
            "a ", "i ", "u ", "e ", "o ",
            "ka ", "ki ", "ku ", "ke ", "ko ",
            "ga ", "gi ", "gu ", "ge ", "go ",
            "sa ", "shi ", "su ", "se ", "so ",
            "za ", "ji ", "zu ", "ze ", "zo ",
            "ta ", "chi ", "tsu ", "te ", "to ",
            "da ", "di ", "du ", "de ", "do ",
            "na ", "ni ", "nu ", "ne ", "no ",
            "ha ", "hi ", "fu ", "he ", "ho ",
            "ba ", "bi ", "bu ", "be ", "bo ",
            "pa ", "pi ", "pu ", "pe ", "po ",
            "ma ", "mi ", "mu ", "me ", "mo ",
            "ya ", "", "yu ", "", "yo ",
            "ra ", "ri ", "ru ", "re ", "ro ",
            "wa ", "", "", "", "wo ",
            "n ", "", "", "", ""
        };

       
        //public Katakana()
        //{
        //    Text = "Katakana Drag-n-Drop";
        //    font = new Font("Times New Roman", 12);
        //}

        //public override void Display(Graphics gr)
        //{
        //    gr.DrawString("Katakana Drag-n-Drop", font, Brushes.Red, 100, 100);
        //}

        //public override void Mouse_Down(MouseEventArgs e)
        //{
             
        //}

        //public override void Mouse_Move(MouseEventArgs e)
        //{
           
        //}

        //public override void Mouse_Up(MouseEventArgs e)
        //{

        //}
    }
}
