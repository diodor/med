using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace EditEr
{
    public partial class Editor : Form
    {

        List<Shapes> Shapes = new List<Shapes>();

        Boolean flagStart = false; 
        Point LS; 
        Point Center; 

        Pen pMain = new Pen(Color.Black);
        Pen pTemp = new Pen(Color.Gray);
        Pen pSelection = new Pen(Color.Red);

        Shapes tempShape;


        public Editor()
        {
            InitializeComponent();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                tempShape = new Cross(e.X, e.Y);
                Refresh();
            }
            else
            {
                if (flagStart)
                {
                    if (radioButton2.Checked)
                    {
                        tempShape = new Line(LS, e.Location);
                        Refresh();
                    }
                    if (radioButton3.Checked)
                    {
                        tempShape = new Circle(Center, e.Location);
                        Refresh();
                    }
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                flagStart = false;
                addShape(tempShape);
                Refresh();
            }
            if (radioButton2.Checked)
            {
                if (!flagStart)
                {
                    LS = e.Location;
                    flagStart = true;
                }
                else
                {
                    addShape(tempShape);
                    flagStart = false;
                    Refresh();
                }
            }
            if (radioButton3.Checked)
            {
                if (!flagStart)
                {
                    Center = e.Location;
                    flagStart = true;
                }
                else
                {
                    addShape(tempShape);
                    flagStart = false;
                    Refresh();
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (tempShape != null) 
            {
                tempShape.DrawWith(e.Graphics, pTemp);
            }

            foreach (Shapes p in this.Shapes)
            { 
                p.DrawWith(e.Graphics, pMain);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curFile = "test.txt"; //Имя файла для записи
            StreamWriter sw = new StreamWriter(curFile);

            flagStart = false;
            foreach (Shapes p in this.Shapes)
            {
                p.SaveTo(sw);
            }
            sw.Close();
        }

        private void addShape(Shapes shape)
        {
            Shapes.Add(shape);
            shapesList.Items.Add(shape.DescriptionString);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curFile = "test.txt"; //Имя файла для записи
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //Выбрать файл вручную
            {
                curFile = openFileDialog1.FileName;
            }
            StreamReader sr = new StreamReader(curFile);

            while (!sr.EndOfStream)
            {
                string type = sr.ReadLine();
                flagStart = false;
                switch (type)
                {
                    case "Cross":
                        Shapes.Add(new Cross(sr));
                        break;
                    case "Line":
                        Shapes.Add(new Line(sr));
                        break;
                    case "Circle":
                        Shapes.Add(new Circle(sr));
                        break;
                }
            }
            sr.Close();
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (shapesList.SelectedIndices.Count > 0)
            {
                Shapes.RemoveAt(shapesList.SelectedIndices[0]);
                shapesList.Items.RemoveAt(shapesList.SelectedIndices[0]);
            }
        }
    }

}
