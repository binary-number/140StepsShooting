
using System;

namespace Shooting
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            timer1.Start();
            label1.Location = new System.Drawing.Point(label1.Location.X, (this.Size.Height / 2 - label1.Height / 2));
        }
        bool downkey = false;
        System.Windows.Forms.Keys Keys = System.Windows.Forms.Keys.None;
        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == (System.Windows.Forms.Keys.Up) || e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                Keys = e.KeyCode;
                downkey = true;
            }
            if (e.KeyCode == System.Windows.Forms.Keys.Space && time >= 20)
            {
                System.Windows.Forms.Label label = new System.Windows.Forms.Label() { Size = new System.Drawing.Size(10, 4), Text = " ", BackColor = System.Drawing.Color.GreenYellow, Location = new System.Drawing.Point(label1.Location.X + label1.Width / 2, label1.Location.Y + label1.Size.Height / 2 - 2) };
                this.Controls.Add(label);
                labels.Add(label);
                time = 0;
                Console.Beep(7000, 30);
            }
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
                System.Windows.Forms.Application.Exit();

        }
        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            downkey = false;
        }
        System.Collections.Generic.List<System.Windows.Forms.Label> labels = new System.Collections.Generic.List<System.Windows.Forms.Label>(), labels2 = new System.Collections.Generic.List<System.Windows.Forms.Label>();
        int time = 0, time2 = 0, count = 0, c2 = 50, level = 1;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            textBox1.Text = "レベル" + level;
            if (time2 > 52 - level)
            {
                System.Windows.Forms.Label label = new System.Windows.Forms.Label() { Size = new System.Drawing.Size(10, 4), Text = " ", BackColor = System.Drawing.Color.Red, Location = new System.Drawing.Point(label2.Location.X + label2.Size.Height / 2 - 2, label2.Location.Y + label2.Width / 2 - 4) };
                this.Controls.Add(label);
                labels2.Add(label);
                time2 = 0;
                Console.Beep(9000, 30);
            }
            time++;
            time2++;
            if (downkey)
            {
                if (Keys == System.Windows.Forms.Keys.Up && label1.Location.Y + 5 > 0)
                    label1.Location = new System.Drawing.Point(label1.Location.X, label1.Location.Y - 10);
                if (Keys == System.Windows.Forms.Keys.Down && label1.Location.Y + 5 <= Size.Height - label1.Height)
                    label1.Location = new System.Drawing.Point(label1.Location.X, label1.Location.Y + 10);

            }
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Location = new System.Drawing.Point(labels[i].Location.X + 20, labels[i].Location.Y);
                if (label2.Location.X < labels[i].Location.X)
                {
                    if (labels[i].Location.Y >= label2.Location.Y + 1 && labels[i].Location.Y <= (label2.Location.Y + label2.Height - labels[i].Height - 1))
                    {
                        Console.Beep(200, 100);
                        timer1.Stop();
                        if (level == 50)
                        {
                            System.Windows.Forms.MessageBox.Show("YouWIN\n貴方は勝ちました\nゲームを終了します");
                            System.Windows.Forms.Application.Exit();
                        }
                        if (System.Windows.Forms.MessageBox.Show("レベル" + level + "クリア\n次のレベルに進みますか?\n進むならYes,終了ならNoを押してください", "勝敗", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            level++;
                            ReStart();
                        }
                        else
                            System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        Controls.Remove(labels[i]);
                        labels.RemoveAt(i);
                    }
                }
            }
            for (int i = 0; i < labels2.Count; i++)
            {
                labels2[i].Location = new System.Drawing.Point(labels2[i].Location.X - 20, labels2[i].Location.Y);
                if (labels2[i].Location.X < 40)
                {
                    if (labels2[i].Location.Y >= label1.Location.Y + 5 && labels2[i].Location.Y <= (label1.Location.Y + label1.Height - labels2[i].Height - 5))
                    {
                        Console.Beep(100, 100);
                        timer1.Stop();
                        if (System.Windows.Forms.DialogResult.Yes == System.Windows.Forms.MessageBox.Show("GameOver\n" + level + "レベルで負けました\nリスタートしますか", "勝敗", System.Windows.Forms.MessageBoxButtons.YesNo))
                        {
                            ReStart();
                        }
                        else
                            System.Windows.Forms.Application.Exit();
                    }
                }
                if (labels2.Count > i && labels2[i].Location.X < 40)
                {
                    Controls.Remove(labels2[i]);
                    labels2.RemoveAt(i);
                    i--;
                }
            }
            System.Random rand = new System.Random(System.DateTime.Now.Second * System.DateTime.Now.Month);
            int c = rand.Next(-20, 20);
            if (label2.Location.Y + c <= Size.Height && label2.Location.Y + c > 0)
                label2.Location = new System.Drawing.Point(label2.Location.X, label2.Location.Y + c);
        }
        void ReStart()
        {
            for (int j = 0; j < labels.Count; j++)
                Controls.Remove(labels[j]);
            for (int j = 0; j < labels2.Count; j++)
                Controls.Remove(labels2[j]);
            labels.RemoveRange(0, labels.Count);
            labels2.RemoveRange(0, labels2.Count);
            timer1.Start();
        }
    }
}
