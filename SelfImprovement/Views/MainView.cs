using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace SelfImprovement
{
    public partial class MainView : Form
    {
        #region Properties
        Models.Task WorkOut;

        Models.Task Study;
        #endregion Properties

        public MainView()
        {
            InitializeComponent();

            string connetionString = ConfigurationManager.ConnectionStrings["SelfImprovement.Properties.Settings.SelfImprovementConnectionString"].ConnectionString;

            this.WorkOut = new Models.Task("Work Out", this.WorkOutBtn, this.label4, connetionString);
            this.Study = new Models.Task("Study", this.StudyBtn, this.label5, connetionString);

            this.backgroundWorker1.RunWorkerAsync(1000);
        }

        #region EventHandlers
        private void WorkOutBtn_Click(object sender, EventArgs e)
        {
            this.WorkOut.CompleteTask();
        }

        private void StudyBtn_Click(object sender, EventArgs e)
        {
            this.Study.CompleteTask();
        }

        private void backgroundWorker1_Timer_Tick(object sender, EventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            this.TimerTick(bw);
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.SetTimeLeftInDay(this.GetTimeLeftInDay()); // Set time left in the day, needs to be updated to a countdown
        }
        #endregion EventHandlers

        #region Helper Functions
        #region Time Left in Day Functions
        private void TimerTick(BackgroundWorker bw)
        {
            while(!bw.CancellationPending) {
                this.Invoke(new MethodInvoker(delegate()
                {
                    Console.WriteLine("Timer ticking...");

                    //Thread.Sleep(1000);

                    var timeLeftInDay = this.GetTimeLeftInDay();

                    if (timeLeftInDay.Equals("24:00"))
                    {
                        Console.WriteLine("24 hours left in the day.. It's a new day!");

                        // Reset the completed tasks when time left in day is 24 hours, if false reset consecutive days to 0
                        if (!this.WorkOut.TaskComplete)
                        {
                            this.WorkOut.ResetConsecutiveDays();
                        }
                        else if (this.WorkOut.TaskComplete)
                        {
                            this.WorkOut.ResetTask();
                        }

                        if (!this.Study.TaskComplete)
                        {
                            this.Study.ResetConsecutiveDays();
                        }
                        else if (this.Study.TaskComplete)
                        {
                            this.Study.ResetTask();
                        }
                    }
                
                    this.SetTimeLeftInDay(timeLeftInDay);
                }));
            }
        }

        private string GetTimeLeftInDay()
        {
            var timeLeft = string.Empty;
            var hoursLeft = (24 - Int32.Parse(DateTime.Now.ToString("HH")));
            var minutesLeft = (60 - Int32.Parse(DateTime.Now.ToString("mm")));
            if (minutesLeft == 60)
            {
                hoursLeft++;
                timeLeft = string.Format("{0}:00", hoursLeft);
            }
            else if (minutesLeft < 10)
            {
                timeLeft = string.Format("{0}:0{1}", hoursLeft, minutesLeft);
            }
            else
            {
                timeLeft = string.Format("{0}:{1}", hoursLeft, minutesLeft);
            }

            return timeLeft;
        }

        private void SetTimeLeftInDay(string timeLeft)
        {
            this.label2.Text = string.Format("Time left in the day: {0}", timeLeft);
        }
        #endregion Time Left in Day Functions
        #endregion Helper Functions
    }
}