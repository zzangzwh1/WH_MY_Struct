using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;

namespace ICA08_MyStruct
{
    public partial class Form1 : Form
    {
        private enum eState { State_Idle, State_Armed };
        static private Random _rnd = new Random();
        eState type;
        Point start;
        Queue<SLine> q = new Queue<SLine>();
        struct SLine
        {
            public Point p;
            public Color c;
            public byte thikness;
            
            public SLine(Point p, Color c, byte thikness)
            {
               this.p = p;
               this.c = c;
               this.thikness = thikness;
            }

        }
      
             CDrawer _drawer = new CDrawer(800,800);
             List<SLine> _lines = new List<SLine>();
        public Form1()
        {
            InitializeComponent();
       
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
      
            switch (type) 
            {
                case eState.State_Idle:
                    UI_lbl_Result.Text = eState.State_Idle.ToString();
                        if(_drawer.GetLastMouseLeftClick(out start))
                        {
                        type = eState.State_Armed;
                        }
                    break;
                case eState.State_Armed:
                   SLine sl = new SLine();
                    UI_lbl_Result.Text = eState.State_Armed.ToString();
                    if (_drawer.GetLastMouseLeftClick(out sl.p))
                    {
                        sl.c = Color.Red;
                        sl.thikness = 5;

                        Render(sl);
                        type = eState.State_Idle;
                        _lines.Add(sl);
                    }
                
                
                        break;

            }
            Point temp;
            if (_drawer.GetLastMouseRightClick(out temp))
            {

                for (int i = 1; i < _lines.Count; i++)
                {
                    SLine slNew = new SLine();
                    slNew = _lines[i];
                    slNew.c = RandColor.GetColor();
                    slNew.thikness = (byte)_rnd.Next(1, 16);
                    q.Enqueue(slNew);


                }
                Console.WriteLine(q.Count);
                RenderRight();


            }







        }
        private void Render(SLine li)
        {
            _drawer.AddLine(start.X,start.Y, li.p.X, li.p.Y,li.c, li.thikness);
        }
       private void RenderRight()
        {
            _drawer.Clear();
            for(int i =0; i <_lines.Count; i++)
            {
              SLine slNew  = q.Dequeue();
                _drawer.AddLine(start.X,start.Y,slNew.p.X,slNew.p.Y,slNew.c,slNew.thikness);
            }
            

        }

    }
}
