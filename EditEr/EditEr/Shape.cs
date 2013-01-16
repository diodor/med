using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace EditEr
{
    public abstract class Shapes
    {
        public abstract string DescriptionString { get; }
        public abstract void DrawWith(Graphics g, Pen p);
        public abstract void SaveTo(StreamWriter sw);
    }

    public class Cross : Shapes
    {
        private int X, Y;

        public override string DescriptionString
        {
            get
            {
                return "Cross {X=" + X + ",Y=" + Y + "}";
            }
        }

        public Cross(int _X, int _Y)
        {
            this.X = _X;
            this.Y = _Y;
        }

        public Cross(StreamReader sr) // Загрузка
        {
            String line = sr.ReadLine();
            string[] foo = line.Split(' ');
            X = Convert.ToInt32(foo[0]);
            Y = Convert.ToInt32(foo[1]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            p = new Pen(Color.Red, 1);
            g.DrawLine(p, X - 3, Y - 3, X + 3, Y + 3);
            g.DrawLine(p, X + 3, Y - 3, X - 3, Y + 3);
        }

        public override void SaveTo(StreamWriter sw) //Сохранение
        {
            sw.WriteLine("Cross");
            sw.WriteLine(Convert.ToString(X) + " " + Convert.ToString(Y));
        }
    }

    class Line : Shapes
    {
        private Point S, F;

        public override string DescriptionString
        {
            get
            {
                return "Line " + S + "; " + F;
            }
        }

        public Line(Point _S, Point _F)
        {
            this.S = _S;
            this.F = _F;
        }

        public Line(StreamReader sr) // Загрузка
        {
            string line = sr.ReadLine();
            string[] foo = line.Split(' ');
            S.X = Convert.ToInt32(foo[0]);
            S.Y = Convert.ToInt32(foo[1]);

            line = sr.ReadLine();
            foo = line.Split(' ');
            F.X = Convert.ToInt32(foo[0]);
            F.Y = Convert.ToInt32(foo[1]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            p = new Pen(Color.Red, 1);
            g.DrawLine(p, S.X, S.Y, F.X, F.Y);
        }

        public override void SaveTo(StreamWriter sw) //Сохранение
        {
            sw.WriteLine("Line");
            sw.WriteLine(Convert.ToString(S.X) + " " + Convert.ToString(S.Y));
            sw.WriteLine(Convert.ToString(F.X) + " " + Convert.ToString(F.Y));
        }
    }
    class Circle : Shapes
    {
        private Point C, onR;
        Pen p = new Pen(Color.Black);

        public override string DescriptionString
        {
            get
            {
                return "Circle {C=" + C + ",onR=" + onR + "}";
            }
        }

        public float Radius
        {
            get { return (float)Math.Sqrt(Math.Pow(onR.X - C.X, 2) + Math.Pow(onR.Y - C.Y, 2)); }
        }

        public Circle(StreamReader sr)
        {
            String circle = sr.ReadLine();
            string[] foo = circle.Split(' ');
            C.X = Convert.ToInt32(foo[0]);
            C.Y = Convert.ToInt32(foo[1]);

            circle = sr.ReadLine();
            foo = circle.Split(' ');
            onR.X = Convert.ToInt32(foo[0]);
            onR.Y = Convert.ToInt32(foo[1]);
        }

        public Circle(Point _C, Point _point_onR)
        {
            this.C = _C;
            this.onR = _point_onR;
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            p = new Pen(Color.Red, 1);
            g.DrawEllipse(p, C.X - this.Radius, C.Y - this.Radius, Radius * 2, Radius * 2);

        }

        public override void SaveTo(StreamWriter sw) //Сохранение
        {
            sw.WriteLine("Circle");
            sw.WriteLine(Convert.ToString(C.X) + " " + Convert.ToString(C.Y));
            sw.WriteLine(Convert.ToString(onR.X) + " " + Convert.ToString(onR.Y));
        }

    }
}

