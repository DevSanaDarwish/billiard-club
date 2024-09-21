using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace Billiard_Club
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const short PRICE_PER_ONE_HOUR = 500;

        float _totalPrice = 0;

        TimeSpan zero = TimeSpan.Zero;
        DateTime timeNow = DateTime.Now;

        StringBuilder formattedTimeNow = new StringBuilder();
        StringBuilder formattedTimerElapsed1 = new StringBuilder();
        StringBuilder formattedTimerElapsed2 = new StringBuilder();
        StringBuilder formattedTimerElapsed3 = new StringBuilder();
        StringBuilder formattedTimerElapsed4 = new StringBuilder();
        StringBuilder formattedTimerElapsed5 = new StringBuilder();
        StringBuilder formattedTimerElapsed6 = new StringBuilder();
        StringBuilder formattedTimerElapsed7 = new StringBuilder();
        StringBuilder formattedTimerElapsed8 = new StringBuilder();

        TimeSpan timerElapsed1 = new TimeSpan();
        TimeSpan timerElapsed2 = new TimeSpan();
        TimeSpan timerElapsed3 = new TimeSpan();
        TimeSpan timerElapsed4 = new TimeSpan();
        TimeSpan timerElapsed5 = new TimeSpan();
        TimeSpan timerElapsed6 = new TimeSpan();
        TimeSpan timerElapsed7 = new TimeSpan();
        TimeSpan timerElapsed8 = new TimeSpan();

        List<Label> _lstNames;

        enum enPauseResume { Pause, Resume};



        private void FillListWithlblNames()
        {
            _lstNames = new List<Label>
            { 
               lblName1, lblName2, lblName3, lblName4,
               lblName5, lblName6, lblName7, lblName8
            };
        }

        private bool IslblNameNull(Label name)
        {
            return (string.IsNullOrEmpty(name.Text));
        }

        private void AddName()
        {
            foreach (Label name in _lstNames)
            {
                if (IslblNameNull(name))
                {
                    name.Text = txtName.Text;
                    return;
                }
            }

            MessageBox.Show("Sorry, All the tables are reserved.");           
        }

        private void EnabledButton(Guna2Button button)
        {
            button.Enabled = true;
        }

        private void DisabledButton(Guna2Button button)
        {
            button.Enabled = false;
        }

        private bool EnabledbtnStart(Label name, Guna2Button button)
        {
            if (!string.IsNullOrEmpty(name.Text))
            {
                EnabledButton(button);
                return true;
            }

            return false;
        }
        
        private void SplitTimeToHoursAndMinutes(Label time, ref float numberOfHours, ref float minutes, ref float seconds)
        {
            string[] parts = time.Text.Split(':');

            numberOfHours = float.Parse(parts[0]);

            minutes = float.Parse(parts[1]);

            seconds = float.Parse(parts[2]);

        }
        private void CalculateTotalPrice(Label time)
        {
            _totalPrice = 0;

            float numberOfHours = 0, minutes = 0, seconds = 0;

            SplitTimeToHoursAndMinutes(time, ref numberOfHours, ref minutes, ref seconds);
            
            float pricePerValueOfHours = (float)nudPrice.Value * PRICE_PER_ONE_HOUR;

            float timeTaken = (float)(numberOfHours + (minutes / 60.0f) + (seconds / 3600.0f));

           _totalPrice = timeTaken * pricePerValueOfHours;
        }

        private void SetTimeNow()
        {
            timeNow = timeNow.AddSeconds(1);

            formattedTimeNow.Clear();

            formattedTimeNow.AppendFormat("{0:dd/MM/yyyy HH:mm}", timeNow);

            mtbTimeNow.Text = formattedTimeNow.ToString();
        }
        
        private void AddNameAndEnabledbtnStart()
        {
           AddName();

            if (EnabledbtnStart(lblName8, btnStart8))
                return;

            else if (EnabledbtnStart(lblName7, btnStart7))
                return;

            else if (EnabledbtnStart(lblName6, btnStart6))
                return;

            else if (EnabledbtnStart(lblName5, btnStart5))
                return;

            else if (EnabledbtnStart(lblName4, btnStart4))
                return;

            else if (EnabledbtnStart(lblName3, btnStart3))
                return;

            else if (EnabledbtnStart(lblName2, btnStart2))
                return;

            else if (EnabledbtnStart(lblName1, btnStart1))
                return;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            AddNameAndEnabledbtnStart();

            txtName.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timerNow.Start();

            FillListWithlblNames();
        }

        private void CommonEventsOnClickbtnStart(Guna2Button btnStart, Guna2Button btnPause, Guna2Button btnEnd, PictureBox image, Timer timer)
        {
            EnabledButton(btnPause);

            EnabledButton(btnEnd);

            DisabledButton(btnStart);

            image.BackColor = Color.Green;

            timer.Start();
        }

        private void CommonEventsOnClickbtnPause(Guna2Button btnPause, Timer timer,PictureBox image)
        {
            if(btnPause.Text == enPauseResume.Pause.ToString())
            {
                btnPause.Text = enPauseResume.Resume.ToString();

                timer.Stop();

                image.BackColor = Color.Yellow;
            }

            else
            {
                btnPause.Text = enPauseResume.Pause.ToString();

                timer.Start();

                image.BackColor = Color.Green;
            }    
        }

        private void Reset(Guna2Button btnStart, Guna2Button btnEnd,Timer timer,
            Label time, Label Name, StringBuilder formattedTimer, Guna2Button btnPause, 
            PictureBox image, TimeSpan timerElapsed)
        {
            //Image
            image.BackColor = Color.Transparent;

            //Buttons
            DisabledButton(btnStart);
            DisabledButton(btnEnd);
            DisabledButton(btnPause);

            //Timer
            timer.Stop();
            
            //Name
            Name.Text = "";
        }

        private void CommonEventsOnClickbtnEnd(Guna2Button btnEnd, Label time, Guna2Button btnStart,Timer timer, Label Name,
            StringBuilder formattedTimer, Guna2Button btnPause, PictureBox image, TimeSpan timerElapsed)
        {
            CalculateTotalPrice(time);

            Reset(btnStart, btnEnd, timer, time, Name, formattedTimer, btnPause, image, timerElapsed);

            MessageBox.Show("Game Over!\n\nThe Price Is: " + _totalPrice + "$", "The Bill",
                MessageBoxButtons.OK, MessageBoxIcon.Information);       
        }

        private void btnStart1_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart1, btnPause1, btnEnd1, pbTabel1, timer1);
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart2, btnPause2, btnEnd2, pbTabel2, timer2);
        }

        private void btnStart3_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart3, btnPause3, btnEnd3, pbTabel3, timer3);
        }

        private void btnStart4_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart4, btnPause4, btnEnd4, pbTabel4, timer4);
        }

        private void btnStart5_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart5, btnPause5, btnEnd5, pbTabel5, timer5);
        }

        private void btnStart6_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart6, btnPause6, btnEnd6, pbTabel6, timer6);
        }

        private void btnStart7_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart7, btnPause7, btnEnd7, pbTabel7, timer7);
        }

        private void btnStart8_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnStart(btnStart8, btnPause8, btnEnd8, pbTabel8, timer8);
        }

        private void btnPause1_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause1, timer1, pbTabel1);
        }

        private void btnPause2_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause2, timer2, pbTabel2);
        }

        private void btnPause3_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause3, timer3, pbTabel3);
        }

        private void btnPause4_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause4, timer4, pbTabel4);
        }

        private void btnPause5_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause5, timer5, pbTabel5);
        }

        private void btnPause6_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause6, timer6, pbTabel6);
        }

        private void btnPause7_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause7, timer7, pbTabel7);
        }

        private void btnPause8_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnPause(btnPause8, timer8, pbTabel8);
        }

        private void btnEnd1_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd1, lblTime1, btnStart1, timer1, lblName1, formattedTimerElapsed1, btnPause1, pbTabel1, timerElapsed1);

            timerElapsed1 = zero;

            formattedTimerElapsed1.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed1.Hours, timerElapsed1.Minutes, timerElapsed1.Seconds);

            lblTime1.Text = formattedTimerElapsed1.ToString();

            formattedTimerElapsed1.Clear();
        }

        private void btnEnd2_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd2, lblTime2, btnStart2, timer2, lblName2, formattedTimerElapsed2, btnPause2, pbTabel2, timerElapsed2);

            timerElapsed2 = zero;

            formattedTimerElapsed2.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed2.Hours, timerElapsed2.Minutes, timerElapsed2.Seconds);

            lblTime2.Text = formattedTimerElapsed2.ToString();

            formattedTimerElapsed2.Clear();
        }

        private void btnEnd3_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd3, lblTime3, btnStart3, timer3, lblName3, formattedTimerElapsed3, btnPause3, pbTabel3, timerElapsed3);

            timerElapsed3 = zero;

            formattedTimerElapsed3.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed3.Hours, timerElapsed3.Minutes, timerElapsed3.Seconds);

            lblTime3.Text = formattedTimerElapsed3.ToString();

            formattedTimerElapsed3.Clear();
        }

        private void btnEnd4_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd4, lblTime4, btnStart4, timer4, lblName4, formattedTimerElapsed4, btnPause4, pbTabel4, timerElapsed4);

            timerElapsed4 = zero;

            formattedTimerElapsed4.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed4.Hours, timerElapsed4.Minutes, timerElapsed4.Seconds);

            lblTime4.Text = formattedTimerElapsed4.ToString();

            formattedTimerElapsed4.Clear();
        }

        private void btnEnd5_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd5, lblTime5, btnStart5, timer5, lblName5, formattedTimerElapsed5, btnPause5, pbTabel5, timerElapsed5);

            timerElapsed5 = zero;

            formattedTimerElapsed5.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed5.Hours, timerElapsed5.Minutes, timerElapsed5.Seconds);

            lblTime5.Text = formattedTimerElapsed5.ToString();

            formattedTimerElapsed5.Clear();
        }

        private void btnEnd6_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd6, lblTime6, btnStart6, timer6, lblName6, formattedTimerElapsed6, btnPause6, pbTabel6, timerElapsed6);

            timerElapsed6 = zero;

            formattedTimerElapsed6.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed6.Hours, timerElapsed6.Minutes, timerElapsed6.Seconds);

            lblTime6.Text = formattedTimerElapsed6.ToString();

            formattedTimerElapsed6.Clear();
        }

        private void btnEnd7_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd7, lblTime7, btnStart7, timer7, lblName7, formattedTimerElapsed7, btnPause7, pbTabel7, timerElapsed7);

            timerElapsed7 = zero;

            formattedTimerElapsed7.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed7.Hours, timerElapsed7.Minutes, timerElapsed7.Seconds);

            lblTime7.Text = formattedTimerElapsed7.ToString();

            formattedTimerElapsed7.Clear();
        }

        private void btnEnd8_Click(object sender, EventArgs e)
        {
            CommonEventsOnClickbtnEnd(btnEnd8, lblTime8, btnStart8, timer8, lblName8, formattedTimerElapsed8, btnPause8, pbTabel8, timerElapsed8);

            timerElapsed8 = zero;

            formattedTimerElapsed8.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed8.Hours, timerElapsed8.Minutes, timerElapsed8.Seconds);

            lblTime8.Text = formattedTimerElapsed8.ToString();

            formattedTimerElapsed8.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed1 += oneSecond;

            formattedTimerElapsed1.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed1.Hours, timerElapsed1.Minutes, timerElapsed1.Seconds);


            lblTime1.Text = formattedTimerElapsed1.ToString();

            formattedTimerElapsed1.Clear();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed2 += oneSecond;

            formattedTimerElapsed2.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed2.Hours, timerElapsed2.Minutes, timerElapsed2.Seconds);

            lblTime2.Text = formattedTimerElapsed2.ToString();

            formattedTimerElapsed2.Clear();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed3 += oneSecond;

            formattedTimerElapsed3.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed3.Hours, timerElapsed3.Minutes, timerElapsed3.Seconds);

            lblTime3.Text = formattedTimerElapsed3.ToString();

            formattedTimerElapsed3.Clear();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed4 += oneSecond;

            formattedTimerElapsed4.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed4.Hours, timerElapsed4.Minutes, timerElapsed4.Seconds);

            lblTime4.Text = formattedTimerElapsed4.ToString();

            formattedTimerElapsed4.Clear();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed5 += oneSecond;

            formattedTimerElapsed5.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed5.Hours, timerElapsed5.Minutes, timerElapsed5.Seconds);

            lblTime5.Text = formattedTimerElapsed5.ToString();

            formattedTimerElapsed5.Clear();
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed6 += oneSecond;

            formattedTimerElapsed6.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed6.Hours, timerElapsed6.Minutes, timerElapsed6.Seconds);

            lblTime6.Text = formattedTimerElapsed6.ToString();

            formattedTimerElapsed6.Clear();
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed7 += oneSecond;

            formattedTimerElapsed7.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed7.Hours, timerElapsed7.Minutes, timerElapsed7.Seconds);

            lblTime7.Text = formattedTimerElapsed7.ToString();

            formattedTimerElapsed7.Clear();
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            timerElapsed8 += oneSecond;

            formattedTimerElapsed8.AppendFormat("{0:00}:{1:00}:{2:00}", timerElapsed8.Hours, timerElapsed8.Minutes, timerElapsed8.Seconds);

            lblTime8.Text = formattedTimerElapsed8.ToString();

            formattedTimerElapsed8.Clear();
        }

        private void BackColorWithDarkSlateBlue(Label lbl)
        {
            lbl.BackColor = Color.DarkSlateBlue;
        }

        private void ForeColorWithWhite(Label lbl)
        {
            lbl.ForeColor = Color.White;
        }

        private void ForeColorWithBlack(Label lbl)
        {
            lbl.ForeColor = Color.Black;
        }

        private void BackColorWithLightGray(Label lbl)
        {
            lbl.BackColor = Color.LightGray;
        }

        private void LightMode()
        {
                this.BackColor = Color.FromArgb(192, 192, 255);

                label1.ForeColor = Color.FromArgb(128, 128, 255);

                mtbTimeNow.ForeColor = Color.Black;
                mtbTimeNow.BackColor = Color.FromArgb(192, 192, 255);

                BackColorWithDarkSlateBlue(lbl1);
                BackColorWithDarkSlateBlue(lbl2);
                BackColorWithDarkSlateBlue(lbl3);
                BackColorWithDarkSlateBlue(lbl4);
                BackColorWithDarkSlateBlue(lbl5);
                BackColorWithDarkSlateBlue(lbl6);
                BackColorWithDarkSlateBlue(lbl7);
                BackColorWithDarkSlateBlue(lbl8);

                ForeColorWithWhite(lbl1);
                ForeColorWithWhite(lbl2);
                ForeColorWithWhite(lbl3);
                ForeColorWithWhite(lbl4);
                ForeColorWithWhite(lbl5);
                ForeColorWithWhite(lbl6);
                ForeColorWithWhite(lbl7);
                ForeColorWithWhite(lbl8);
        }

        private void DarkMode()
        {
                this.BackColor = Color.Black;

                label1.ForeColor = Color.LawnGreen;

                mtbTimeNow.ForeColor = Color.White;
                mtbTimeNow.BackColor = Color.Black;

                BackColorWithLightGray(lbl1);
                BackColorWithLightGray(lbl2);
                BackColorWithLightGray(lbl3);
                BackColorWithLightGray(lbl4);
                BackColorWithLightGray(lbl5);
                BackColorWithLightGray(lbl6);
                BackColorWithLightGray(lbl7);
                BackColorWithLightGray(lbl8);

                ForeColorWithBlack(lbl1);
                ForeColorWithBlack(lbl2);
                ForeColorWithBlack(lbl3);
                ForeColorWithBlack(lbl4);
                ForeColorWithBlack(lbl5);
                ForeColorWithBlack(lbl6);
                ForeColorWithBlack(lbl7);
                ForeColorWithBlack(lbl8);
        }

        private void tsMood_CheckedChanged(object sender, EventArgs e)
        {
            if (tglMode.Checked)
                LightMode();

            else
                DarkMode();                  
        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            SetTimeNow();
        }
    }
}
