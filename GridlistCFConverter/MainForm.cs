using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NcInterop;
using NcInterop.Base;

namespace GridlistCFConverter
{
    public partial class MainForm : Form
    {
        private NcReader reader;
        private ResolutionInterop finder;
        private Color tumBlue = Color.FromArgb(0, 101, 189);

        private int w_sel;
        private int h_sel;

        private DirectBitmap internalDirectBitmap;

        private byte[,] coords;

        public MainForm()
        {
            InitializeComponent();

            this.buttonClose.FlatAppearance.BorderColor = Color.White;
            this.buttonClose.FlatAppearance.BorderSize = 2;

            this.buttonConvert.BackColor = tumBlue;
            this.checkBoxReduced.ForeColor = tumBlue;
            this.label7.ForeColor = tumBlue;

            this.AutoScaleMode = AutoScaleMode.None;

            w_sel = 1;
            h_sel = 1;


            //var imageSize = pictureBoxDisplay.BackgroundImage.Size;
            //var fitSize = pictureBoxDisplay.ClientSize;
            //pictureBoxDisplay.SizeMode = imageSize.Width > fitSize.Width || imageSize.Height > fitSize.Height ?
            //    PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
         
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

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

        private bool mouseDown;
        private Point lastLocation;


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            this.buttonClose.FlatAppearance.BorderColor = Color.Red;
            this.buttonClose.FlatAppearance.BorderSize = 2;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            this.buttonClose.FlatAppearance.BorderColor = Color.White;
            this.buttonClose.FlatAppearance.BorderSize = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogDriver.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialogDriver.FileName;
                try
                {
                    textBox1.Text = file;
                    ProcessInputFile(file);

                }
                catch (IOException)
                {

                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));

                string fileName = filePaths[0];

                textBox1.Text = fileName;

                ProcessInputFile(fileName);

            }
        }




        private void ProcessInputFile(string filename)
        {
            try
            {
                reader = new NcReader(filename);
                finder = new ResolutionInterop(reader);

                
                textBoxLon.Text = String.Format("({0},  {1})", Convert.ToString(finder.MinLon, CultureInfo.InvariantCulture), Convert.ToString(finder.MaxLon, CultureInfo.InvariantCulture));
                textBoxLat.Text = String.Format("({0},  {1})", Convert.ToString(finder.MinLat, CultureInfo.InvariantCulture), Convert.ToString(finder.MaxLat, CultureInfo.InvariantCulture));

                textBoxResolution.Text = Convert.ToString(finder.Resolution, CultureInfo.InvariantCulture);
                checkBoxReduced.Checked = finder.IsOrthogonal;


                this.pictureBoxDisplay.Image = new Bitmap(720, 360);

                this.coords = finder.FindCoords();


                internalDirectBitmap = new DirectBitmap(720, 360);

                for (int w = 0; w < 720; w++)
                {
                    for (int h = 0; h < 360; h++)
                    {

                        if (coords[w, h] == 1)
                        {

                            int bitI = 4 * w + 4 * 720 * (360 - h);
                            internalDirectBitmap.Bits[bitI] = 189;
                            internalDirectBitmap.Bits[bitI + 1] = 101;
                            internalDirectBitmap.Bits[bitI + 2] = 0;
                            internalDirectBitmap.Bits[bitI + 3] = 255;
                            //((Bitmap) pictureBoxDisplay.Image).SetPixel(w, h, Color.Red);
                        }


                    }
                }
                this.pictureBoxDisplay.Image = internalDirectBitmap.Bitmap;
                this.pictureBoxDisplay.Refresh();
            }

            catch (NetCDFException exception)
            {
                MessageBox.Show(exception.Message);
                textBox1.Text = String.Empty;

            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
            {
                String[] strGetFormats = e.Data.GetFormats();
                e.Effect = DragDropEffects.None;
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                double lon = Convert.ToDouble(textBoxConLon.Text, CultureInfo.InvariantCulture);
                double lat = Convert.ToDouble(textBoxConLat.Text, CultureInfo.InvariantCulture);

                int[] index = finder.FindIndex(lon, lat);
                
                if (index[0] == -1)
                {
                    textBoxResult.Text = "Not found!";
                }

                else if(index.Length == 1)
                {

                    textBoxResult.Text = index[0].ToString();
                }
                
                else
                {
                    string s = index[0] + "  " + index[1];
                    textBoxResult.Text = s;
                }

                DrawPoint(finder.FindSelectedLonIndex(lon), 360 - finder.FindSelectedLatIndex(lat));
            }
            catch (Exception exception)
            {
                this.textBoxResult.Text = exception.Message;
            }
        }

        private void checkBoxReduced_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxResult_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxResult.SelectAll();
        }

        private void pictureBoxDisplay_MouseUp(object sender, MouseEventArgs e)
        {


        }

        private void pictureBoxDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (this.coords != null)
            {

                DrawPoint(e.X, e.Y);

                try
                {
                    this.textBoxConLon.Text = finder.FindSelectedLon(w_sel).ToString(CultureInfo.InvariantCulture);
                    this.textBoxConLat.Text = finder.FindSelectedLat(h_sel).ToString(CultureInfo.InvariantCulture);


                    double lon = Convert.ToDouble(textBoxConLon.Text, CultureInfo.InvariantCulture);
                    double lat = Convert.ToDouble(textBoxConLat.Text, CultureInfo.InvariantCulture);

                    int[] index = finder.FindIndex(lon, lat);

                    if (index[0] == -1)
                    {
                        textBoxResult.Text = "Not found!";
                    }

                    else if (index.Length == 1)
                    {

                        textBoxResult.Text = index[0].ToString();
                    }

                    else
                    {
                        string s = index[0] + "  " + index[1];
                        textBoxResult.Text = s;
                    }
                }
                catch (Exception exception)
                {
                    this.textBoxResult.Text = exception.Message;
                }





            }
        }


        private void DrawPoint(int x, int y)
        {
            //Redraw prev selected coordiate first
            int zoom = 3;
            int[] ws = new int[zoom];
            int[] hs = new int[zoom];
            for (int i = 0; i < ws.Length; i++)
            {
                ws[i] = w_sel - zoom / 2 + i;
                hs[i] = h_sel - zoom / 2 + i;
            }
            foreach (int wEx in ws)
            {
                foreach (int hEx in hs)
                {

                    int bitI = 4 * wEx + 4 * 720 * (360 - hEx - 1);
                    if (coords[wEx, hEx] == 1)
                    {
                        internalDirectBitmap.Bits[bitI] = 189;
                        internalDirectBitmap.Bits[bitI + 1] = 101;
                        internalDirectBitmap.Bits[bitI + 2] = 0;
                        internalDirectBitmap.Bits[bitI + 3] = 255;
                    }
                    else
                    {
                        Bitmap btm = (Bitmap)(pictureBoxDisplay.BackgroundImage);
                        Color col = btm.GetPixel(wEx, 360 - hEx -1);

                        internalDirectBitmap.Bits[bitI] = col.B;
                        internalDirectBitmap.Bits[bitI + 1] = col.G;
                        internalDirectBitmap.Bits[bitI + 2] = col.R;
                        internalDirectBitmap.Bits[bitI + 3] = col.A;
                    }
                }
            }

            //Get new selected coordiantes
            w_sel = x;
            h_sel = 360 - y;

            for (int i = 0; i < ws.Length; i++)
            {
                ws[i] = w_sel - zoom / 2 + i;
                hs[i] = h_sel - zoom / 2 + i;
            }
            foreach (int wEx in ws)
            {
                foreach (int hEx in hs)
                {
                    //if (coords[wEx, hEx] == 1)
                    //{}

                    int bitI = 4 * wEx + 4 * 720 * (360 - hEx -1);
                    internalDirectBitmap.Bits[bitI] = 0;
                    internalDirectBitmap.Bits[bitI + 1] = 0;
                    internalDirectBitmap.Bits[bitI + 2] = 255;
                    internalDirectBitmap.Bits[bitI + 3] = 255;

                }
            }


            pictureBoxDisplay.Refresh();
        }
    }
}
