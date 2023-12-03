using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Analouge
{
    public partial class Analogue : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public Analogue()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }
            base.WndProc(ref m);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void AppExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void FolderBrowserEvent_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Analogue Wants You To Select Your FortniteGame Path";
            commonOpenFileDialog.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult = commonOpenFileDialog.ShowDialog();


            bool flag = commonFileDialogResult == CommonFileDialogResult.Ok;
            if (flag)
            {
                if (File.Exists(System.IO.Path.Join(commonOpenFileDialog.FileName, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe")))
                {
                    this.PathBox.Text = commonOpenFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("Cant Find Your FortniteGame & Engine Path");

                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void Analogue_Load(object sender, EventArgs e)
        {
            analogueGamingMouse1.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            analogueGamingMouse1.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            analogueGamingMouse1.Hide();
        }
    }
}