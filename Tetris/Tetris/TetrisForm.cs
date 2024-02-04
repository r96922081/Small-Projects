using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tetris
{
    enum Key { Left, Right, Down, Change, None };

    public partial class TetrisForm : Form
    {
        private PictureBox game_view = new PictureBox();
        TableLayoutPanel top_table_layout = new TableLayoutPanel();
        static int block_size = 20;
        const int width_count = 10;
        const int height_count = 20;
        int width = block_size * width_count;
        int height = block_size * height_count;
        int time_interval = 50;
        int elapsed_game_time = 0;
        int game_speed = 500;
        int elapsed_key_time = 0;
        int key_speed = 50;
        bool game_over = false;
        System.Timers.Timer timer;
        Key key = Key.None;
        Label level_title = new Label();
        Label level = new Label();
        Label score_title = new Label();
        Label score = new Label();
        Tetris tetris;
        System.Drawing.SolidBrush red_brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

        static System.Drawing.SolidBrush brush0 = new System.Drawing.SolidBrush(System.Drawing.Color.DarkSeaGreen);
        static System.Drawing.SolidBrush brush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Orange);
        static System.Drawing.SolidBrush brush2 = new System.Drawing.SolidBrush(System.Drawing.Color.Brown);
        static System.Drawing.SolidBrush brush3 = new System.Drawing.SolidBrush(System.Drawing.Color.DeepSkyBlue);
        static System.Drawing.SolidBrush brush4 = new System.Drawing.SolidBrush(System.Drawing.Color.Gold);
        static System.Drawing.SolidBrush brush5 = new System.Drawing.SolidBrush(System.Drawing.Color.Indigo);
        static System.Drawing.SolidBrush brush6 = new System.Drawing.SolidBrush(System.Drawing.Color.DarkSlateGray);

        SolidBrush[] brushes = new SolidBrush[] { brush0, brush1, brush2, brush3, brush4, brush5, brush6};

        public TetrisForm()
        {
            InitializeComponent();
            this.Load += TetrisForm_Load;
            this.KeyDown += TetrisForm_KeyDown;

            tetris = new Tetris(width_count, height_count, new DelegateFunction1(GameOverCallback));
#if DEBUG
            new Unittest().RunUt();
#endif
        }

        private void GameOverCallback()
        {
            game_over = true;
        }

        private void TetrisForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    key = Key.Left;
                    break;
                case Keys.Right:
                    key = Key.Right;
                    break;
                case Keys.Down:
                    key = Key.Down;
                    break;
                case Keys.Space:
                    key = Key.Change;
                    break;
            }
        }

        private void TetrisForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Width = width + 10 + 100;
            this.Height = height + 10 + 40;

            top_layout.Controls.Add(game_view);
            top_layout.BackColor = Color.FromArgb(0x66, 0x66, 0x66);
            this.Controls.Add(top_layout);

            Padding margin = game_view.Margin;
            margin.All = 5;
            game_view.Margin = margin;

            game_view.BackColor = Color.Black;
            game_view.Paint += new System.Windows.Forms.PaintEventHandler(this.game_view_Paint);
            game_view.Width = width;
            game_view.Height = height;
            game_view.MinimumSize = new Size(width, height);

            Font level_font = new Font("Times New Roman", 15, FontStyle.Bold);
            level_title.Text = "Level";
            level_title.Font = level_font;
            level_title.ForeColor = Color.DeepSkyBlue;
            top_right_lay_out.Controls.Add(level_title);

            level.Text = "1";
            level.Font = level_font;
            level.ForeColor = Color.DeepSkyBlue;
            top_right_lay_out.Controls.Add(level);

            Font score_font = new Font("Times New Roman", 15, FontStyle.Bold);
            score_title.Text = "Score";
            score_title.Font = score_font;
            score_title.ForeColor = Color.Gold;
            top_right_lay_out.Controls.Add(score_title, 0, 3);

            score.Text = "0";
            score.Font = score_font;
            score.ForeColor = Color.Gold;
            top_right_lay_out.Controls.Add(score, 0, 4);

            timer = new System.Timers.Timer(time_interval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

            tetris.Start();
        }

        private void RepaintView()
        {
            this.Refresh();
        }

        private void UpdateScore()
        {
            score.Text = tetris.GetClearLines().ToString();
        }

        private void HandleKeyEvent()
        {
            if (key == Key.Change)
            {
                tetris.ChangeShape();
            }
            else if(key == Key.Left)
            {
                tetris.MoveLeft();
            }
            else if(key == Key.Right)
            {
                tetris.MoveRight();
            }
            else if (key == Key.Down)
            {
                tetris.MoveDown();
            }

            key = Key.None;
        }

        private void UpdateView()
        {
            tetris.Step();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            elapsed_game_time += time_interval;
            elapsed_key_time += time_interval;

            if (elapsed_key_time >= key_speed)
            {
                elapsed_key_time = 0;
                HandleKeyEvent();
            }

            if (elapsed_game_time >= game_speed)
            {
                elapsed_game_time = 0;
                tetris.Step();
            }

            this.BeginInvoke(new DelegateFunction1(UpdateScore));
            this.BeginInvoke(new DelegateFunction1(RepaintView));

            if (game_over == true)
            {
                timer.Stop();
                return;
            }
        }

        private SolidBrush GetBrush(Block b)
        {
            return brushes[(int)b.type];
        }

        private void game_view_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Pen black_pen = new Pen(Brushes.Black);

            Graphics g = e.Graphics;

            if (tetris.moving_blocks != null)
            {
                foreach (Block b in tetris.moving_blocks)
                {
                    Rectangle r = new Rectangle();
                    r.X = b.x * block_size;
                    r.Y = b.y * block_size;
                    r.Width = block_size;
                    r.Height = block_size;

                    g.FillRectangle(GetBrush(b), r);
                    g.DrawRectangle(black_pen, r);
                }
            }

            foreach (Block b in tetris.fixed_blocks)
            {
                Rectangle r = new Rectangle();
                r.X = b.x * block_size;
                r.Y = b.y * block_size;
                r.Width = block_size;
                r.Height = block_size;

                g.FillRectangle(GetBrush(b), r);
                g.DrawRectangle(black_pen, r);
            }
            
            if (game_over)
            {
                Font game_over_font = new Font("Times New Roman", 30);
                var size = g.MeasureString("Game Over", game_over_font);

                int x = width / 2 - (int)size.Width / 2;
                int y = height / 2 - (int)size.Height / 2;

                g.DrawString("Game over", game_over_font, red_brush, x, y);
            }

        }
    }
}
