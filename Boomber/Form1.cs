using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boomber
{
    public partial class Form1 : Form
    {
        private int row = 50;
        private int colum = 50;

        private BombCell[,] GameField;


        Dictionary<int, Color> Collors = new Dictionary<int, Color>();

        private int buttonWidth;
        private int buttonHeight;




        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            buttonWidth = this.Width / row;
            buttonHeight = this.Height / colum;
            PushField();
            PushDictionary();
            GetCaunt();
            timer1.Start();
        }

        private void PushField()
        {
            Random random = new Random();
            GameField = new BombCell[row, colum];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colum; j++)
                {

                    Button butt = new Button();
                    butt.Size = new Size(buttonWidth, buttonHeight);
                    butt.Location = new Point(j * buttonWidth, i * buttonHeight);
                    butt.Click += new EventHandler(OnCleakButton);
                    GameField[i, j] = new BombCell(i, j,random.Next(1,8) == 3,butt);
                    this.Controls.Add(GameField[i, j].but);
                }
            }
        }
        private void PushDictionary()
        {
            Collors.Add(0, Color.DarkKhaki);
            Collors.Add(1, Color.Green);
            Collors.Add(2, Color.Yellow);
            Collors.Add(3, Color.Red);
            Collors.Add(4, Color.Coral);
            Collors.Add(5, Color.Gray);
            Collors.Add(6, Color.DarkCyan);
            Collors.Add(7, Color.Crimson);
            Collors.Add(8, Color.Aquamarine);
        }

        private int CountNeighbors(int x, int y)
        {
            int caunt = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i);
                    var rows = (y + j);
                    try
                    {
                        if (GameField[col, rows].IsBoomb())
                            caunt++;
                    }
                    catch 
                    {

                        continue;
                    }
                }
            }
            return caunt;
        }


        private void GetCaunt()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                    GameField[i, j].readCell = CountNeighbors(i, j);
                }

            }
        }

        private void NeighborsOpen(int x, int y)
        {
            if (GameField[x, y].readCell == 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        var col = (y + i);
                        var rows = (x + j);
                        try
                        {
                            GameField[rows, col].visibl = true;
                        }
                        catch 
                        {
                            continue;
                        }
                    }
                }
            }
            else return;
        }




        private void OnCleakButton(object sender,EventArgs e)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                    if(sender == GameField[i, j].but)
                    {
                        if(GameField[i, j].IsBoomb())
                        {
                            MessageBox.Show("Boom");
                            this.Close();
                        }
                        else
                        {
                            GameField[i, j].visibl = true;
                            NeighborsOpen(i, j);
                        }
                        
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                   if(GameField[i, j].visibl)
                   {
                        GameField[i, j].but.Text = GameField[i, j].readCell.ToString();

                        GameField[i, j].but.BackColor = Collors[GameField[i, j].readCell];
                   }
                }
            }
        }

        private void OpenZero()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                    
                        
                    GameField[i, j].visibl = true;
                    NeighborsOpen(i, j);
                    
                }
            }
        }
    }
}