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

        private void Timer_Tick(object sender, EventArgs e)
        {
            var today = DateTime.Now.Date;
            ResetTask(this.WorkOut, today);
            ResetTask(this.Study, today);

            var timeLeftInDay = this.GetTimeLeftInDay();

            this.SetTimeLeftInDay(timeLeftInDay);
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.SetTimeLeftInDay(this.GetTimeLeftInDay()); // Set time left in the day, needs to be updated to a countdown
        }
        #endregion EventHandlers

        #region Helper Functions

        private void ResetTask(Models.Task task, DateTime date)
        {
            if (!task.LastDayCompleted.Date.Equals(date) && !task.LastDayCompleted.Date.Equals(task.DefaultLastDayCompleted.Date)) // revert to ! (not)
            {
                var yesterday = DateTime.Now.AddDays(-1).Date;
                
                if (task.TaskComplete)
                {
                    Console.WriteLine("TaskComplete: {0}", task.TaskComplete);
                    Console.WriteLine("LastDayCompleted.Date: {0}", task.LastDayCompleted.Date);
                    Console.WriteLine("Yesterday: {0}", yesterday);
                    task.ResetTask();
                }
                else if (!task.TaskComplete && !task.LastDayCompleted.Date.Equals(yesterday)) // if taskComplete == false && LastDayCompleted != yesterday - second comparison was broken. shoudl work now b/c .date wasn't on yesterday
                {
                    // overnight test got in here. It should've only executed the if condition
                    Console.WriteLine("TaskComplete: {0}", task.TaskComplete);
                    Console.WriteLine("LastDayCompleted.Date: {0}", task.LastDayCompleted.Date);
                    Console.WriteLine("Yesterday: {0}", yesterday);
                    task.ResetConsecutiveDays();
                }
            }

        }

        #region Time Left in Day Functions
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