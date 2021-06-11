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
            this.WorkOut = new Models.Task("Work Out", this.WorkOutBtn, this.label4);
            this.Study = new Models.Task("Study", this.StudyBtn, this.label5);
        }

        #region EventHandlers
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Views.NewTaskForm tskForm = new Views.NewTaskForm();
            tskForm.Show();
        }

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
            var timeLeftInDay = this.GetTimeLeftInDay();

            if (timeLeftInDay.Equals("24:00"))
            {
                // Reset the completed tasks when time left in day is 24 hours, if both are flase don't bother checking TimeLeftInDay
                if (this.WorkOut.TaskComplete == true)
                {
                    this.WorkOut.ResetTask();
                }
                if (this.Study.TaskComplete == true)
                {
                    this.Study.ResetTask();
                }                
            }

            this.SetTimeLeftInDay(timeLeftInDay);
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.SetTimeLeftInDay(this.GetTimeLeftInDay()); // Set time left in the day, needs to be updated to a countdown
            this.WorkOut.TaskComplete = false;
            this.Study.TaskComplete = false;
        }
        #endregion EventHandlers

        #region Helper Functions
        public static void CreateNewTask(string task, object sender, EventArgs e)
        {
            // create new button
            Button newButton = new Button();
            newButton.BackColor = System.Drawing.Color.Red;
            newButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newButton.Location = new System.Drawing.Point(300, 275);
            newButton.Name = task + "Btn";
            newButton.Size = new System.Drawing.Size(209, 48);
            newButton.TabIndex = 4;
            newButton.TabStop = false;
            newButton.Text = task;
            newButton.UseVisualStyleBackColor = false;
            //newButton.Click += new System.EventHandler(this.StudyBtn_Click); // TODO - how do I dynamically create an event handler??
            Controls.Add(newButton);

            // create new label
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Location = new System.Drawing.Point(306, 345);
            newLabel.Name = task + "label";
            newLabel.Size = new System.Drawing.Size(145, 13);
            newLabel.TabIndex = 7;
            newLabel.Text = string.Format("Consecutive days {0}: 0", task);

            this.Controls.Add(newLabel);

            Models.Task newTask = new Models.Task(task, newButton, newLabel);
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