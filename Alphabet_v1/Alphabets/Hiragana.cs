using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;

namespace Alphabet_v1.Alphabets
{
    class Hiragana : BaseAlphabets
    {

        //используется для сохранения индекса выбранного символа
        int index;
        //Разница между курсором и верхней левой границы прямоугольника     
        int deltaX, deltaY;     
        //ширина рамки в которой размещаются столбцы
        int cx_ramka = 0;
        //Информирует о перетаскивании элемента
        bool Drag = false;
        //количество столбцов
        byte kol_st = 15;
        /*отступ между столбцами внутри рамуки
          1 отступ = 1 доп. пустой ячейке*/
        byte oztup_st = 3;
        //количество ячеек в одном столбце по вертикали  
        byte kol_cell = 5;
        /*отступ между ячейками внутри столбца
          1 отступ = 1 доп. пустой ячейке*/
        byte oztup_cell = 1;
        //
        byte col = 0;

        

        Font fontSymbol;
        Font fontZag;
        SizeF sizeSymbol;
        SizeF sizeZag;
        StringFormat strfm;

        Rectangle rect_cells;
        Rectangle rect_column;

        Rectangle obl;
        Rectangle Allobl;

         
        //для возвращения символа в первоначальные координаты в случае неудачного перетаскивания
        Rectangle StartCoordinatesSymbol;

        List<Rectangle> lSymbols = new List<Rectangle>();
        List<Rectangle> lCoordinatesCell = new List<Rectangle>();

        #region Symbols
        //Хирагана ромадзи 
        protected string[] sHRomandzi =  
       {
            "a", "i", "u", "e", "o",
            "ka", "ki", "ku", "ke", "ko",
            "ga", "gi", "gu", "ge", "go",
            "sa", "shi", "su", "se", "so",
            "za", "ji", "zu", "ze", "zo",
            "ta", "chi", "tsu", "te", "to",
            "da", "di", "du", "de", "do",
            "na", "ni", "nu", "ne", "no",
            "ha", "hi", "fu", "he", "ho",
            "ba", "bi", "bu", "be", "bo",
            "pa", "pi", "pu", "pe", "po",
            "ma", "mi", "mu", "me", "mo",
            "ya", "", "yu", "", "yo",
            "ra", "ri", "ru", "re", "ro",
            "wa", "", "wo", "", "n"
        };

        protected string[] sHSymbols =
       {
            "あ", "い", "う", "え", "お",
            "か","き", "く", "け", "こ",
            "が","ぎ", "ぐ", "げ", "ご",
            "さ", "し", "す", "せ", "そ",
            "ざ", "じ", "ず", "ぜ", "ぞ",
            "た", "ち", "つ", "て", "と",
            "だ", "ぢ", "づ", "で", "ど",
            "な","に", "ぬ",  "ね", "の",
            "は", "ひ", "ふ", "へ", "ほ",
            "ば", "び", "ぶ", "べ", "ぼ",
            "ぱ", "ぴ", "ぷ", "ぺ", "ぽ",
            "ま", "み", "む", "め", "も",
            "や", "ゆ", "よ",
            "ら", "り", "る", "れ", "ろ",
            "わ", "を", "ん"
        };
        #endregion
        
        public Hiragana()
        {
            Text = "Hiragana Drag-n-Drop";
            fontSymbol = new Font("Time New Roman", 16);
            fontZag = new Font("Time New Roman", 15, FontStyle.Underline);
            strfm = new StringFormat();
            Init();
            RandomCoordSymbol();        
        }

        //Метод рассчитывает и возвращает ширину или высоту колонки
        int GetWHColumn(int W0H1)
        {
            //ширина и высота колонки
            int WidthColumn, HeightColumn;
            switch (W0H1)
            {
                case 0:
                    {
                        WidthColumn = (int)sizeSymbol.Width * 2;
                        return WidthColumn;
                    }
                case 1:
                    {
                        HeightColumn = (int)sizeSymbol.Height * (kol_cell + oztup_cell); // kol_cell ячеек + oztup_st для отступов
                        return HeightColumn;
                    }
                default: return 0;
            }
        }

    

        private void Init()
        {
            Graphics gr = CreateGraphics();
            sizeSymbol = gr.MeasureString(sHSymbols[0], fontSymbol);
            sizeZag = gr.MeasureString("H", fontSymbol);

            int w = GetWHColumn(0);
            int h = GetWHColumn(1);

            cx_ramka = w * (kol_st + oztup_st);           //kol_st столбцов + oztup_st для отступов

            Allobl = new Rectangle
            (
                (this.ClientSize.Width - cx_ramka) / 2, 
                (this.ClientSize.Height - h * 2) / 2, 
                cx_ramka, 
                h * 2 + (int)sizeZag.Height
            );
            obl = new Rectangle(Allobl.X, Allobl.Y + (int)sizeZag.Height, cx_ramka, h);            
            gr.Dispose();
        }

        

        private void RandomCoordSymbol()
        {
            Random rand = new Random();
            int x, y, w, h;

            w = GetWHColumn(0);
            h = GetWHColumn(1);

            Rectangle rectTemp;
            obl.Offset(0, h);
            for (int n = 0; (lSymbols.Count < sHSymbols.Length) && (n < 1000); n++)
            {
                x = obl.X + rand.Next(obl.Width - (int)sizeSymbol.Width);
                y = obl.Y + 4 + rand.Next(obl.Height - (int)sizeSymbol.Height - 4); //+4 и -4 чтобы верхняя или нижняя граница символа не совпадала с границей рамки 
                rectTemp = new Rectangle(x, y, (int)sizeSymbol.Width, (int)sizeSymbol.Height);
                if (!IsIntersect(rectTemp))
                {
                    lSymbols.Add(rectTemp);
                }
            }
            obl.Offset(0, -h);
        }

        private bool IsIntersect(Rectangle rect)
        {
            foreach (var ob in lSymbols)
                if (rect.IntersectsWith(ob))
                    return true;
            return false;
        }
        
        public override void mResize()
        {
            int w = GetWHColumn(0);
            int h = GetWHColumn(1);
            Allobl = new Rectangle((this.ClientSize.Width - cx_ramka) / 2, (this.ClientSize.Height - h * 2) / 2,
                                                     cx_ramka, h * 2 + (int)sizeZag.Height);      
            obl = new Rectangle(Allobl.X, Allobl.Y + (int)sizeZag.Height, cx_ramka, h);
        }

        public override void Display(Graphics gr)
        {
            byte i, j, item = 0;          
            int w = GetWHColumn(0);
            int h = GetWHColumn(1);
            float otstupX, otstupY, otstupX_Column, otstupY_Cell;

            otstupY_Cell =  1.0f * sizeSymbol.Height * oztup_cell / (kol_cell + 1); //отступ по y. для ячеек
            otstupX_Column = 1.0f * w * oztup_st / (kol_st - 1); //отступ по y. для ячеек
       
            gr.TextRenderingHint = TextRenderingHint.AntiAlias;
            strfm.Alignment = StringAlignment.Center;
            strfm.LineAlignment = StringAlignment.Center;

            //gr.DrawRectangle(Pens.Green, obl.X, obl.Y, obl.Width, obl.Height+4);
            //gr.DrawRectangle(Pens.Red, Allobl.X, Allobl.Y, Allobl.Width, Allobl.Height);

            gr.DrawString("Hiragana Drag-n-Drop",fontZag, Brushes.Red, Allobl.X, Allobl.Y);
             
            rect_column = new Rectangle(obl.X, obl.Y, w, h);
            rect_cells = new Rectangle(rect_column.X, rect_column.Y, (int)sizeSymbol.Width, (int)sizeSymbol.Height);
 
            for (i = 0; i < kol_st; i++)
            {
                otstupX = otstupX_Column * i + w * i;                
                gr.FillRectangle(Brushes.DarkBlue,
                    rect_column.X + otstupX, rect_column.Y,
                    rect_column.Width, rect_column.Height
                );

                for (j = 1; j <= 5; j++)
                {
                    otstupY = otstupY_Cell * j + (j - 1) * (int)sizeSymbol.Height;
                    Rectangle cell = new Rectangle
                    (
                        rect_column.X + (int)(otstupX + 0.5f), 
                        rect_cells.Y + (int)(otstupY + 0.5f), 
                        rect_cells.Width, 
                        rect_cells.Height
                    );   
                                
                    //Зарисовываем пустые ячейки
                    if(sHRomandzi[item] == "")
                        gr.FillRectangle(Brushes.DarkBlue, cell);
                    else
                    {
                        gr.FillRectangle(Brushes.Yellow, cell.X - (int)sizeSymbol.Width * 0.25f, cell.Y, cell.Width, cell.Height);
                        lCoordinatesCell.Add(new Rectangle((int)(cell.X - sizeSymbol.Width * 0.25f), cell.Y, cell.Width, cell.Height));
                    }
                    gr.DrawString 
                    (
                        sHRomandzi[item++],
                        new Font("Time New Roman", 10, FontStyle.Bold),
                        Brushes.White,
                        rect_column.X + otstupX + (int)sizeSymbol.Width,
                        rect_cells.Y + otstupY
                    );             
                }
            }       
            for(i = 0; i < lSymbols.Count; i++)
                gr.DrawString(sHSymbols[i], fontSymbol, Brushes.Blue, lSymbols[i], strfm);
        }

        public override void Mouse_Down(System.Windows.Forms.MouseEventArgs e)
        {

            
            for (int i = 0; i < lSymbols.Count; i++)
            {
                if (lSymbols[i].Contains(new Point(e.X, e.Y)))
                {
                    index = i; 
                    Drag = true;
                    deltaX = e.X - lSymbols[i].X;
                    deltaY = e.Y - lSymbols[i].Y;
                    StartCoordinatesSymbol = new Rectangle(lSymbols[i].Location, lSymbols[i].Size);
                    E.Graphics.DrawRectangle(Pens.Red, lSymbols[i]);   
                    return;
                }
            }
        } 
              
        public override void Mouse_Move(System.Windows.Forms.MouseEventArgs e)
        {

            Rectangle rectTemp;
            if (Drag)
            {
                rectTemp = lSymbols[index];
                rectTemp.X = e.X - deltaX;
                rectTemp.Y = e.Y - deltaY;
                lSymbols[index] = rectTemp;        
            }
            
        }
        public override void Mouse_Up(System.Windows.Forms.MouseEventArgs e)
        {
            if (!Drag)
                return;
            else
                Drag = false;

            if (lSymbols[index].IntersectsWith(lCoordinatesCell[index]) && lCoordinatesCell[index].Contains(new Point(e.X, e.Y)))
            { 
                lSymbols[index] = lCoordinatesCell[index];
            }
            else
                lSymbols[index] = StartCoordinatesSymbol;            
        }

    }

}