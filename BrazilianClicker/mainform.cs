﻿
// code by https://github.com/b1scoito
// his channel https://www.youtube.com/channel/UC-fFWfKQi5EHYTDFx3R9Ing
// thx for letting me skidd <3
// keeploving#2448

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Rainbow;
using System.Windows.Forms;

namespace b1ac
{
    public partial class metodorainbow : Form
    {
        #region importsvars
        Random rnd = new Random();
        bool estado = false;
        [DllImport("user32", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0016;
        public static bool ApplicationIsActivated()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            bool result;
            if (foregroundWindow == IntPtr.Zero)
            {
                result = false;
            }
            else
            {
                int id = Process.GetCurrentProcess().Id;
                int num;
                GetWindowThreadProcessId(foregroundWindow, out num);
                result = (num == id);
            }
            return result;
        }
        private string GetCaptionOfActiveWindow()
        {
            var strTitle = string.Empty;
            var handle = GetForegroundWindow();
            var intLength = GetWindowTextLength(handle) + 1;
            var stringBuilder = new StringBuilder(intLength);
            if (GetWindowText(handle, stringBuilder, intLength) > 0)
            {
                strTitle = stringBuilder.ToString();
            }
            return strTitle;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        public metodorainbow()
        {
            PictureBox pb1 = new PictureBox();
            pb1.ImageLocation = "BrazilianClicker/img.png";
            pb1.SizeMode = PictureBoxSizeMode.AutoSize;
            InitializeComponent();
            int FirstHotkeyId = 1;
            int FirstHotKeyKey = (int)Keys.F8;
            bool F8Registered = RegisterHotKey(Handle, FirstHotkeyId, 0x0000, FirstHotKeyKey);
            int SecondHotkeyId = 2;
            int SecondHotKeyKey = (int)Keys.F4;
            bool F4Registered = RegisterHotKey(Handle, SecondHotkeyId, 0x0000, SecondHotKeyKey);
            int ThirdHotkeyId = 3;
            int ThirdHotkeyKey = (int)Keys.F10;
            bool F10Registered = RegisterHotKey(Handle, ThirdHotkeyId, 0x0000, ThirdHotkeyKey);
            if (!F8Registered)
            {
                Console.WriteLine("Global Hotkey F8 couldn't be registered !");
            }

            if (!F4Registered)
            {
                Console.WriteLine("Global Hotkey F4 couldn't be registered !");
            }
            if (!F10Registered)
            {
                Console.WriteLine("Global Hotkey F4 couldn't be registered !");
            }
        }

        private static readonly Random _rand = new Random();

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                switch (id)
                {
                    case 1:
                        if (estado == true)
                        {
                            estado = false;
                            Show();
                        }
                        else
                        {
                            estado = true;
                            Hide();
                        }
                        break;
                    case 2:
                        btnAC_Click(this, new EventArgs());
                        break;
                    case 3:
                        bunifuFlatButton1_Click(this, new EventArgs());
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            if (label1.Text == "OFF")
            {
                bunifuCheckbox5.Checked = true;
                label1.Text = "ON";
                rodarAC.Start();
            }
            else if (label1.Text == "ON")
            {
                bunifuCheckbox5.Checked = false;
                label1.Text = "OFF";
                rodarAC.Stop();
            }
        }
        private void rangelol_RangeChanged(object sender, EventArgs e)
        {
            lblMin.Text = rangelol.RangeMin.ToString();
            lblMax.Text = rangelol.RangeMax.ToString();
        }
        private void mainform_Load(object sender, EventArgs e)
        {
            lblMin.Text = rangelol.RangeMin.ToString();
            lblMax.Text = rangelol.RangeMax.ToString();
        }
        private void rodar_Tick(object sender, EventArgs e)
        {
            if (rangelol.RangeMin == 0)
            {
                rangelol.RangeMin = 1;
            }
            int minval;
            int maxval;
            int cpsmed;
            if (bunifuCheckbox2.Checked == true)
            {
                cpsmed = rnd.Next(rangelol.RangeMin, rangelol.RangeMax);
                if (rangelol.RangeMin > 0)
                {
                    minval = 1000 / rangelol.RangeMin + rangelol.RangeMax * (int)0.2;
                    maxval = 1000 / rangelol.RangeMin + rangelol.RangeMax * (int)0.48;
                    rodarAC.Interval = rnd.Next(minval, maxval);
                }
                if (bunifuCheckbox1.Checked == true)
                {
                    if (GetCaptionOfActiveWindow().Contains("Minecraft") || GetCaptionOfActiveWindow().Contains("Badlion") || GetCaptionOfActiveWindow().Contains("Labymod") || GetCaptionOfActiveWindow().Contains("OCMC") || GetCaptionOfActiveWindow().Contains("Cheatbreaker") || GetCaptionOfActiveWindow().Contains("J3Ultimate") || GetCaptionOfActiveWindow().Contains("Lunar"))
                    {
                        if (!ApplicationIsActivated())
                        {
                            if (MouseButtons == MouseButtons.Left)
                            {
                                label5.Text = "Media CPS: " + cpsmed;
                                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                if (trackjitter.Value == 0)
                                {
                                    trackjitter.Value = 1;
                                }
                                int randx = rnd.Next(1, trackjitter.Value);
                                int randy = rnd.Next(1, trackjitter.Value);
                                int mX = MousePosition.X;
                                int mY = MousePosition.Y;
                                Cursor.Position = new Point(mX - randx, mY - randy);
                            }
                        }
                    }
                }
                else
                {
                    if (!ApplicationIsActivated())
                    {
                        if (MouseButtons == MouseButtons.Left)
                        {
                            label5.Text = "Media CPS: " + cpsmed;
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            if (trackjitter.Value == 0)
                            {
                                trackjitter.Value = 1;
                            }
                            if (bunifuCheckbox4.Checked)
                            {
                                int randx1 = (rnd.Next(-(trackjitter.Value), (trackjitter.Value)));
                                int randy1 = (rnd.Next(-(trackjitter.Value), (trackjitter.Value)));
                                int mX1 = MousePosition.X;
                                int mY1 = MousePosition.Y;
                                Cursor.Position = new Point(mX1 - randx1, mY1 + randy1);
                            }
                            else
                            {
                                int randx = rnd.Next(1, trackjitter.Value);
                                int randy = rnd.Next(1, trackjitter.Value);
                                int mX = MousePosition.X;
                                int mY = MousePosition.Y;
                                Cursor.Position = new Point(mX - randx, mY + randy);
                            }
                        }
                    }
                }
            }
            else
            {
                cpsmed = rnd.Next(rangelol.RangeMin, rangelol.RangeMax);
                if (rangelol.RangeMin > 0)
                {
                    minval = 1000 / rangelol.RangeMin + rangelol.RangeMax * (int)0.2;
                    maxval = 1000 / rangelol.RangeMin + rangelol.RangeMax * (int)0.48;
                    rodarAC.Interval = rnd.Next(minval, maxval);
                }
                if (bunifuCheckbox1.Checked == true)
                {
                    if (GetCaptionOfActiveWindow().Contains("Minecraft") || GetCaptionOfActiveWindow().Contains("Badlion") || GetCaptionOfActiveWindow().Contains("Labymod") || GetCaptionOfActiveWindow().Contains("OCMC") || GetCaptionOfActiveWindow().Contains("Cheatbreaker") || GetCaptionOfActiveWindow().Contains("J3Ultimate") || GetCaptionOfActiveWindow().Contains("Lunar"))
                    {
                        if (!ApplicationIsActivated())
                        {
                            if (MouseButtons == MouseButtons.Left)
                            {
                                label5.Text = "Media CPS: " + cpsmed;
                                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                            }
                        }
                    }
                }
                else
                {
                    if (!ApplicationIsActivated())
                    {
                        if (MouseButtons == MouseButtons.Left)
                        {
                            label5.Text = "Media CPS: " + cpsmed;
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        }
                    }
                }
            }
        }
        private void delall()
        {
            var version = Environment.OSVersion.Version;
            // Null
            btnAC.Text = null;
            btnMain.Text = null;
            btnSelf.Text = null;
            label1.Text = null;
            label2.Text = null;
            label3.Text = null;
            label4.Text = null;
            lblMax.Text = null;
            lblMin.Text = null;
            label5.Text = null;
            label5.Text = null;
            label6.Text = null;
            label7.Text = null;
            label8.Text = null;
            // Dispose
            btnAC.Dispose();
            btnMain.Dispose();
            btnSelf.Dispose();
            label1.Dispose();
            label2.Dispose();
            label3.Dispose();
            label4.Dispose();
            lblMax.Dispose();
            lblMin.Dispose();
            label5.Dispose();
            label5.Dispose();
            label6.Dispose();
            label7.Dispose();
            label8.Dispose();
            // Explorer
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (version < new Version(6, 2))
                    {
                        if (process.ProcessName == "explorer")
                        {
                            process.Kill();
                            string explorer = string.Format("{0}\\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe");
                            Process processa = new Process();
                            processa.StartInfo.FileName = explorer;
                            processa.StartInfo.UseShellExecute = true;
                            processa.Start();
                            Environment.Exit(0);
                            break;
                        }
                    }
                    else
                    {
                        if (process.ProcessName == "explorer")
                        {
                            process.Kill();
                            Environment.Exit(0);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex, RandomString(5), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
        private void deletarlol()
        {
            string location = Path.Combine(Directory.GetCurrentDirectory() + @"\" + AppDomain.CurrentDomain.FriendlyName);
            string dll = Path.Combine(Directory.GetCurrentDirectory() + @"\" + "Bunifu_UI_v1.5.4.dll");
            if (File.Exists(dll))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/C ping 1.1.1.1 -n 1 & Del " + location + " & Del " + dll,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
            }
            else
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/C ping 1.1.1.1 -n 1 & Del " + location,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
            }
            delall();
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            const string message = "Voçê tem certeza que quer ativar o SelfDestruct?";
            const string caption = "LunarClient.xyz";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.Yes)
            {
                if (chkDeletar.Checked == true)
                {
                    deletarlol();
                }
                else
                {
                    delall();
                }
            }

        }

        private void trackjitter_ValueChanged(object sender, EventArgs e)
        {
            if (trackjitter.Value > 0)
            {
                label8.Text = trackjitter.Value.ToString();
            }
        }

        private void AC_Click(object sender, EventArgs e)
        {

        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void BunifuCheckbox2_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox2.Checked)
            {
                trackjitter.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                bunifuCheckbox4.Visible = true;
                label10.Visible = true;
            }
            else if (bunifuCheckbox2.Checked == false)
            {
                trackjitter.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                bunifuCheckbox4.Visible = false;
                label10.Visible = false;
            }

        }


        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void BtnMain_Click(object sender, EventArgs e)
        {
            tablessControl1.Visible = true;
            tablessControl2.Visible = false;
            tablessControl3.Visible = false;
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        string janela = "Minecraft";
        private void BunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked)
            {
                textBox1.Visible = true;
                label15.Visible = true;

            }
            else if (bunifuCheckbox1.Checked == false)
            {
                textBox1.Visible = false;
                label15.Visible = false;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            // QUALQ DIA EU ARRUMO SA PORRA 
            janela = textBox1.Text;

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Pb1_Click(object sender, EventArgs e)
        {
            const string message1 = "Skidded by BrazilianCheater\n" +
            "code by b1scoito";
            const string caption1 = "LunarClient.xyz";
            var result1 = MessageBox.Show(message1, caption1,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
        }

        private void BunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            const string message1 = "O AutoClicker da bypass em:\n" +
                "Forge\n" +
                "Badlion\n" +
                "OCMC\n" +
                "Lunar (Não Recomendo)\n" +
                "Labymod\n";
            const string caption1 = "LunarClient.xyz";
            var result1 = MessageBox.Show(message1, caption1,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Question);
            if (result1 == DialogResult.OK)
            {
                const string message2 = "As keybinds por padrão são:\n" +
                    "F4 - AutoClicker\n" +
                    "F8 - Esconder\n" +
                    "F10 - SelfDestruct\n" +
                    "Sistema de keybinds em breve\n";
                const string caption2 = "LunarClient.xyz";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Question);
            }
        }

        private void BunifuCheckbox3_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox3.Checked)
            {
                label9.Visible = true;
                bunifuTrackbar1.Visible = true;
            }
            else
            {
                label9.Visible = false;
                bunifuTrackbar1.Visible = false;
            }
        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    
        private void BunifuTrackbar1_ValueChanged(object sender, EventArgs e)
        {
            // funfa mais ta bugado
            //int ndesentadaqsuamaeda = 50;
            //ndesentadaqsuamaeda = bunifuTrackbar1.Value;
            //bunifuTrackbar1.Value = rodarAC.Interval;
        }

        private void BunifuFlatButton2_Click(object sender, EventArgs e)
        {
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            Class1.RainbowEffect();
        }

        private void BunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
        }

        private void BunifuFlatButton2_Click_2(object sender, EventArgs e)
        {
            tablessControl1.Visible = false;
            tablessControl2.Visible = false;
            tablessControl3.Visible = true;
        }

        private void BunifuCheckbox4_OnChange(object sender, EventArgs e)
        {

        }

        private void BtnMain_Click_1(object sender, EventArgs e)
        {

        }

        private void BunifuFlatButton3_Click(object sender, EventArgs e)
        {
            tablessControl1.Visible = false;
            tablessControl2.Visible = true;
            tablessControl3.Visible = false;
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BunifuTrackbar2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
        Class1.RainbowEffect();
            btnAC.BackColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            btnMain.BackColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            btnSelf.BackColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label1.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label2.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label3.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label4.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            lblMax.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            lblMin.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label5.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label5.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label6.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label7.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label8.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label9.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label10.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label11.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label12.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label14.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label15.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            label17.ForeColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            bunifuFlatButton1.BackColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            bunifuFlatButton2.BackColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            bunifuFlatButton3.BackColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            bunifuFlatButton4.BackColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            rangelol.IndicatorColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            trackjitter.IndicatorColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
            bunifuTrackbar1.IndicatorColor = Color.FromArgb(Class1.A, Class1.R, Class1.G);
        }

        private void BunifuFlatButton5_Click(object sender, EventArgs e)
        {
        }
        private void BunifuFlatButton4_Click(object sender, EventArgs e)
        {
            timer2.Start();

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
