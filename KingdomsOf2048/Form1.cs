using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KingdomsOf2048
{
    public partial class Form1 : Form
    {
        KingdomsOf2048 kingdoms = new KingdomsOf2048();
        int TileSize = 50;
        int moves = 0;

        public Form1()
        {
            InitializeComponent();
            kingdoms.SpawnRandom();
            Render();
        }

        public void Render()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            Point offset = new Point(pictureBox1.Width / 2 - TileSize * KingdomsOf2048.SIZE / 2, pictureBox1.Height / 2 - TileSize * KingdomsOf2048.SIZE / 2);
            g.DrawRectangle(Pens.Green, new Rectangle(offset, new Size(TileSize * KingdomsOf2048.SIZE, TileSize * KingdomsOf2048.SIZE)));
            for(int y = 0; y < KingdomsOf2048.SIZE; y++)
            {
                for(int x = 0; x < KingdomsOf2048.SIZE; x++)
                {
                    int val = kingdoms.GameArea[y * KingdomsOf2048.SIZE + x];
                    if (val != 0)
                    {
                        g.FillRectangle(Brushes.Red, offset.X + TileSize * x, offset.Y + TileSize * y, TileSize, TileSize);
                        g.DrawString("" + val, SystemFonts.CaptionFont, Brushes.Blue, offset.X + TileSize * x, offset.Y + TileSize * y);
                    }
                }
            }
            g.Flush();
            g.Dispose();
            pictureBox1.Image = bmp;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int[] i = Clone(kingdoms.GameArea);
            if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                kingdoms.MoveUp();
            if(e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                kingdoms.MoveDown();
            if(e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                kingdoms.MoveLeft();
            if(e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                kingdoms.MoveRight();
            bool change = false;
            for(int k = 0; k < i.Length; k++)
                if (i[k] != kingdoms.GameArea[k])
                    change = true;
            if (change)
            {
                moves++;
                try
                {
                    kingdoms.SpawnRandom();
                }
                catch { }
            }
            Render();
            Application.DoEvents();
            if (!kingdoms.MovePossible())
            {
                MessageBox.Show("You've mastered " + moves + " moves!");
                kingdoms = new KingdomsOf2048();
                kingdoms.SpawnRandom();
                Render();
            }
        }

        private int[] Clone(int[] arr)
        {
            int[] i = new int[arr.Length];
            for(int k = 0; k < arr.Length; k++)
                i[k] = arr[k];
            return i;
        }
    }
}
