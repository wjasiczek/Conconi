using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Conconi
{
    public partial class Form1 : Form
    {
        private Prompt prompt;
        private PlotData data;       
        private Graphics graph;
        private Pen blackPen;
        private Pen bluePen;
        private Pen redPen;
        private SolidBrush pointBrush;
        private List<Label> labelList;
        private FormWindowState LastWindowState;
        private bool isDrawn;

        private List<string> listForTextLog;
        //private Timer myTimer;

        public Form1()
        {
            InitializeComponent();
            data = new PlotData();                                      
            blackPen = new Pen(Color.Black);
            bluePen = new Pen(Color.Blue, 2f);  
            redPen = new Pen(Color.Red, 2f);
            pointBrush = new SolidBrush(Color.Red);
            labelList = new List<Label>();
            LastWindowState = FormWindowState.Minimized;
            isDrawn = false;
            data.xValues = new List<int>(new int[] { 80, 76, 72, 68, 62, 55, 52, 49, 47, 45, 41 });
            data.yValues = new List<int>(new int[] { 108, 110, 116, 124, 133, 140, 149, 156, 160, 162, 163 });
            listForTextLog = new List<string>();
            graph = pictureBox.CreateGraphics();      
        }

        private void buttonAddData_Click(object sender, EventArgs e)
        {
            textBoxHeartbeat.Focus();
            if (textBoxHeartbeat.Text.Length != 0 || textBoxTime.Text.Length != 0)
            {
                int heartbeatResult;
                var heartbeatCheck = int.TryParse(textBoxHeartbeat.Text, out heartbeatResult);
                int timeResult;
                var timeCheck = int.TryParse(textBoxTime.Text, out timeResult);
                var minTime = data.GetMin(data.xValues);
                if (minTime <= timeResult)
                {
                    MessageBox.Show("Czas każdego kolejnego pomiaru musi być mniejszy od poprzedniego", "Błąd");
                }
                else
                {
                    if (!heartbeatCheck || !timeCheck)
                    {
                        MessageBox.Show("Podano złe dane", "Błąd");
                    }
                    else
                    {
                        data.xValues.Add(timeResult);
                        data.yValues.Add(heartbeatResult);
                        textBoxHeartbeat.Text = string.Empty;
                        textBoxTime.Text = string.Empty;
                        listForTextLog.Add("Tętno: " + heartbeatResult + " Czas: " + timeResult + "\r\n");
                        UpdateTextLog();
                        /*
                        myTimer = new Timer();
                        myTimer.Interval = 4000;
                        myTimer.Enabled = true;
                        myTimer.Tick += new System.EventHandler(OnTimerEvent);
                        myTimer.Start();
                        */
                    }
                }
            }
        }

        private void UpdateTextLog()
        {
            textBoxLog.Text = "";
            foreach (string line in listForTextLog)
            {
                textBoxLog.Text += line;
            }
        }

        /*
        private void OnTimerEvent(object sender, EventArgs e)
        {
            labelAdded.Text = string.Empty;
            myTimer.Stop();
        }
        */

        private double Scale(double minValue, double maxValue, double minRange, double maxRange, double value)
        {
            return minRange + ((value - minValue) * (maxRange - minRange) / (maxValue - minValue));
        }

        private double ComputeSecondDerivate(IInterpolation inter, double point)
        {
            var h = 0.001;
            return (inter.Interpolate((double)point + h) - 2 * inter.Interpolate((double)point) + inter.Interpolate((double)point - h)) 
                / (h * h);
        }

        private void CreateAxisLabel(double xPosition, double yPosition, double text)
        {
            Label label = new Label() { AutoSize = true, Location = new Point((int)xPosition, (int)yPosition), 
                Text = text.ToString("0.##") };
            this.Controls.Add(label);
            labelList.Add(label);
        }

        private void CreateResultLabel(string text)
        {
            Label label = new Label() { AutoSize = true, Text = text, BackColor = pictureBox.BackColor,
                                        Font = new Font(Font.FontFamily.Name, 10) };
            this.Controls.Add(label);
            label.Location = new Point(pictureBox.Width / 2 - label.Size.Width / 2 + 30, pictureBox.Location.Y);
            label.BringToFront();
            labelList.Add(label);
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            ClearForm(false);
            isDrawn = true;
            textBoxHeartbeat.Focus();
            if (data.xValues.Count < 3)
            {
                MessageBox.Show("Zbyt mało danych");
            }
            else
            {
                pictureBox.Invalidate();
            }
        }

        private double ShiftByThree(MathNet.Numerics.Interpolation.Algorithms.CubicSplineInterpolation inter, double point)
        {
            double originalValue = inter.Interpolate(point);
            double buffer = originalValue;
            while (originalValue - Scale(0, 110, 0, pictureBox.Height, 3.0) < buffer)
            {               
                point--;
                buffer = inter.Interpolate(point);
            }
            return point;
        }

        private void DrawResult(List<double> derivativePoints, MathNet.Numerics.Interpolation.Algorithms.CubicSplineInterpolation inter, 
            IInterpolation interForDerivative, double minSpeed, double maxSpeed, double minHeartrate, double maxHeartrate)
        {
            var checkValue = 0.0;
            double checkPoint = -50;
            foreach (double point in derivativePoints)
            {
                var interDerivativeResult = (int)interForDerivative.Interpolate(point);
                if (checkValue < interDerivativeResult)
                {
                    checkValue = interDerivativeResult;
                    checkPoint = point;
                }
            }
            checkPoint = ShiftByThree(inter, checkPoint);
            
            checkValue = (int)inter.Interpolate(checkPoint);

            graph.FillEllipse(pointBrush, (int)checkPoint - 5, pictureBox.Height - (int)checkValue - 5, 10, 10);
            graph.DrawLine(redPen, 0, pictureBox.Height - (int)checkValue, (int)checkPoint, pictureBox.Height - (int)checkValue);
            graph.DrawLine(redPen, (int)checkPoint, pictureBox.Height, (int)checkPoint, pictureBox.Height - (int)checkValue);
            CreateResultLabel("Tętno: " + Scale(0, pictureBox.Height, 90, 200, checkValue).ToString("0.##") +
                " [bps]\r\nTempo: " + Scale(0, pictureBox.Width, minSpeed, maxSpeed, checkPoint).ToString("0.##") + " [min/km]");

            graph.DrawLine(blackPen, new Point(1, pictureBox.Height - 1), new Point(pictureBox.Width, pictureBox.Height - 1));
            graph.DrawLine(blackPen, new Point(1, 0), new Point(1, pictureBox.Height - 1));
        }

        private List<double> ComputeSecondDerivativePoints(MathNet.Numerics.Interpolation.Algorithms.CubicSplineInterpolation inter,
             IInterpolation interForDerivative, double minSpeed, double maxSpeed)
        {         
            List<double> derivativePoints = new List<double>();
            var step = pictureBox.Width / Scale(minSpeed, maxSpeed, 0, pictureBox.Width, maxSpeed) -
                Scale(minSpeed, maxSpeed, 0, pictureBox.Width, minSpeed);

            for (int i = 0; i < pictureBox.Width; i++)
            {
                var interResult = (int)inter.Interpolate(i);
                if (interResult < pictureBox.Height && interResult > 0)
                {
                    graph.DrawRectangle(bluePen, i, pictureBox.Height - interResult, 1, 1);
                    var secondDerivative = ComputeSecondDerivate(interForDerivative, i);
                    if (secondDerivative > -0.001 && secondDerivative < 0.001)
                    {
                                               
                        derivativePoints.Add(i);
                    }
                }
            }

            return derivativePoints;
        }

        private void CreateYAxis()
        {
            for (var i = 0; i < 12; i++)
            {
                CreateAxisLabel(0, pictureBox.Location.Y + pictureBox.Height - 10 - ((double)pictureBox.Height / 11 * i),
                    90 + i * 10);
                if (i != 0)
                {
                    graph.DrawLine(blackPen, 0, (int)(pictureBox.Height - ((double)pictureBox.Height / 11 * i)), 5,
                        (int)(pictureBox.Height - ((double)pictureBox.Height / 11 * i)));
                    for (var j = 0; j < pictureBox.Width / 9; j++)
                    {
                        graph.DrawRectangle(blackPen, 10 * j, (int)(pictureBox.Height - ((double)pictureBox.Height / 11 * i)),
                            1, 1);
                    }
                }
            }
        }

        private void CreatePointsListAndXAxis(ref List<double> xList, ref List<double> yList, double minSpeed, double maxSpeed, 
            int minHeartrate, int maxHeartrate)
        {
            for (var i = 0; i < data.xValues.Count; i++)
            {
                var xAxis = Scale(minSpeed, maxSpeed, 0, pictureBox.Width, data.ComputeSpeed(data.xValues[i]));
                var yAxis = Scale(minHeartrate, maxHeartrate, 0, pictureBox.Height, data.yValues[i]);
                xList.Add(xAxis);
                yList.Add(yAxis);
                CreateXAxis(xAxis, i);
            }
        }

        private void CreateXAxis(double xAxis, int i)
        {

            CreateAxisLabel(xAxis + pictureBox.Location.X - 7, pictureBox.Location.Y + pictureBox.Height + 5,
                    data.ComputeSpeed(data.xValues[i]));
            if (i != 0)
            {
                graph.DrawLine(blackPen, new Point((int)xAxis, pictureBox.Height), new Point((int)xAxis, pictureBox.Height - 5));
                for (var j = 0; j < pictureBox.Height / 9; j++)
                {
                    graph.DrawRectangle(blackPen, (int)xAxis, pictureBox.Height - j * 10, 1, 1);
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            isDrawn = false;
            if (data != null)
            {
                data.xValues.Clear();
                data.yValues.Clear();
            }

            ClearForm(true);
        }

        private void ClearForm(bool clearLog)
        {
            if (graph != null)
            {
                graph = null;
            }

            if (clearLog && listForTextLog != null)
            {
                listForTextLog.Clear();
                textBoxLog.Text = "";
            }

            labelAdded.Text = string.Empty;
            labelX.Visible = false;
            labelY.Visible = false;
            foreach (Label label in labelList)
            {
                Controls.Remove(label);
            }
        }

        [DllImport("user32.dll")]
            private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save Conconi";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.Filter = "jpg (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp";
            saveFileDialog1.RestoreDirectory = true;

            Rectangle bounds;
            var foregroundWindowsHandle = GetForegroundWindow();
            var rect = new Rect();
            GetWindowRect(foregroundWindowsHandle, ref rect);
            bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

            Bitmap bitmap = new Bitmap(this.Size.Width, this.Size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, 
                    new Size(this.Size.Width, this.Size.Height));
            }
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(saveFileDialog1.FileName);
                bitmap.Dispose();
            }   
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;
                if (WindowState == FormWindowState.Maximized)
                {
                    pictureBox.Width = 1700;
                    pictureBox.Height = 850;
                    textBoxLog.Location = new Point(1742, textBoxLog.Location.Y);
                    textBoxLog.Height = 800;
                    labelX.Location = new Point(1742, 850 + pictureBox.Location.Y - 17);
                }
                if (WindowState == FormWindowState.Normal)
                {
                    pictureBox.Width = 817;
                    pictureBox.Height = 514;
                    textBoxLog.Location = new Point(861, 112);
                    textBoxLog.Height = 489;
                    labelX.Location = new Point(858, 609);
                }

                if (isDrawn)
                {
                    buttonDraw_Click(sender, e);
                }     
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBoxHeartbeat;
        }

        private void buttonDeleteData_Click(object sender, EventArgs e)
        {
            if (data.xValues.Count == 0)
            {
                MessageBox.Show("Jeszcze nie podano żadnych punktów", "Błąd");
                return;
            }
            prompt = new Prompt(data, ref listForTextLog);
            prompt.ShowDialog();
            UpdateTextLog();
        }

        public class Prompt
        {
            private Form prompt;
            private Label textLabelHeartrate;
            private TextBox textBoxHeartrate;
            private Label textLabelTime;
            private TextBox textBoxTime;
            private Button confirmation;
            private PlotData data;
            private List<string> listForTextLog;

            public Prompt(PlotData data, ref List<string> listForTextLog)
            {
                prompt = new Form() { Width = 400, Height = 130, Text = "Usuń dane" };
                textLabelHeartrate = new Label() { Left = 12, Top = 13, Text = "Tętno", Width = 45, Height = 17 };
                textBoxHeartrate = new TextBox() { Left = 15, Top = 33, Width = 100, Height = 22 };
                textLabelTime = new Label() { Left = 118, Top = 13, Text = "Czas w sekundach", Width = 125, Height = 17 };
                textBoxTime = new TextBox() { Left = 121, Top = 33, Width = 100, Height = 22 };
                confirmation = new Button() { Text = "Usuń", Left = 249, Top = 25, Width = 100, Height = 30 };
                prompt.AcceptButton = confirmation;
                this.data = data;
                this.listForTextLog = listForTextLog;
            }

            public void ShowDialog()
            {
                confirmation.Click += (sender, e) => 
                {
                    if (DeleteRecord(textBoxHeartrate.Text, textBoxTime.Text))
                    {
                        prompt.Close();                    
                    }
                    else
                    {
                        MessageBox.Show("Podano złe dane");
                    }
                };
                prompt.Load += new System.EventHandler(prompt_Load);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabelHeartrate);
                prompt.Controls.Add(textLabelTime);
                prompt.Controls.Add(textBoxHeartrate);
                prompt.Controls.Add(textBoxTime);
                prompt.ShowDialog();        
            }

            private bool DeleteRecord(string heartrate, string time)
            {
                bool found = false;
                double parsedHeartrate;
                var checkHeartrate = double.TryParse(heartrate, out parsedHeartrate);
                double parsedTime;
                var checkTime = double.TryParse(time, out parsedTime);

                if (checkHeartrate && checkTime)
                {
                    for (var i = 0; i < data.xValues.Count; i++)
                    {
                        if (data.xValues[i] == parsedTime && data.yValues[i] == parsedHeartrate)
                        {
                            data.xValues.RemoveAt(i);
                            data.yValues.RemoveAt(i);
                            listForTextLog.RemoveAt(i);
                            found = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Podano złe dane");
                }

                return found;
            }

            private void prompt_Load(object sender, EventArgs e)
            {
                prompt.ActiveControl = textBoxHeartrate;
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawn)
            {
                graph = e.Graphics;
                labelAdded.Text = string.Empty;
                labelY.Visible = true;
                labelX.Visible = true;
                List<double> xList = new List<double>();
                List<double> yList = new List<double>();
                var maxHeartrate = 200;
                var minHeartrate = 90;
                var maxTime = data.GetMax(data.xValues);
                var minTime = data.GetMin(data.xValues);
                var minSpeed = data.ComputeSpeed(maxTime);
                var maxSpeed = data.ComputeSpeed(minTime);

                CreatePointsListAndXAxis(ref xList, ref yList, minSpeed, maxSpeed, minHeartrate, maxHeartrate);
                CreateYAxis();

                var inter = new MathNet.Numerics.Interpolation.Algorithms.CubicSplineInterpolation();
                inter.Initialize(xList, yList);
                var interForDerivative = Interpolate.RationalWithoutPoles(xList, yList);

                List<double> derivativePoints = ComputeSecondDerivativePoints(inter, interForDerivative, minSpeed, maxSpeed);

                DrawResult(derivativePoints, inter, interForDerivative, minSpeed, maxSpeed, minHeartrate, maxHeartrate);
            }
        }
    }
}
