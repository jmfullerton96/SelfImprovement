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
        public bool WorkOutComplete;

        public bool StudyComplete;
        #endregion Properties

        public MainView()
        {
            InitializeComponent();
        }

        #region EventHandlers
        private void WorkOutBtn_Click(object sender, EventArgs e)
        {
            if (!this.WorkOutComplete)
            {
                this.WorkOutComplete = true;
                this.WorkOutBtn.BackColor = Color.Green;
                this.IncrementConsecutiveDaysWorkingOut();
                Console.WriteLine("Congratulations, you bettered yourself with time spent working out today!");
            }
            else
            {
                Console.WriteLine("A work out was already completed today, but an extra one doesn't hurt!");
            }
        }

        private void StudyBtn_Click(object sender, EventArgs e)
        {
            if (!this.StudyComplete)
            {
                this.StudyComplete = true;
                this.StudyBtn.BackColor = Color.Green;
                this.IncrementConsecutiveDaysStudying();
                Console.WriteLine("Congratulations, you bettered yourself with time spend studying your craft today!");
            }
            else
            {
                Console.WriteLine("A study sesh was already completed today, but an extra one doesn't hurt!");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var timeLeftInDay = this.GetTimeLeftInDay();

            if (timeLeftInDay.Equals("24:00"))
            {
                // Reset the completed tasks when time left in day is 24 hours, if both are flase don't bother checking TimeLeftInDay
                if (this.WorkOutComplete == true)
                {
                    this.ResetWorkOut();
                }
                if (this.StudyComplete == true)
                {
                    this.ResetStudying();
                }                
            }

            this.SetTimeLeftInDay(timeLeftInDay);
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            this.SetTimeLeftInDay(this.GetTimeLeftInDay()); // Set time left in the day, needs to be updated to a countdown
            this.WorkOutComplete = false;
            this.StudyComplete = false;
            this.GetConsecutiveDaysWorkingOutBaseline();
            this.GetConsecutiveDaysStudyingBaseline();
        }
        #endregion EventHandlers

        #region Helper Functions
        #region Consecutive Workout Functions
        private void ResetWorkOut()
        {
            this.WorkOutComplete = false;
            this.WorkOutBtn.BackColor = Color.Red;
            
            Console.WriteLine("It's a new day, resetting work out task!");
            MessageBox.Show("It's a new day, resetting work out task!");
        }

        private void GetConsecutiveDaysWorkingOutBaseline()
        {
            this.label4.Text = string.Format("Consecutive days working out: {0}", this.GetConsecutiveDaysWorkingOut());
        }

        private int GetConsecutiveDaysWorkingOut()
        {
            var consWorkOuts = ConfigurationManager.AppSettings.Get("ConsecutiveDaysWorkingOut");
            return Int32.Parse(consWorkOuts);
        }

        private void IncrementConsecutiveDaysWorkingOut()
        {
            var consWorkOuts = GetConsecutiveDaysWorkingOut();
            consWorkOuts++;

            // Update the value in the config.. not a great place/way to store but works for now
            Console.WriteLine("Incremented ConsecutiveDaysWorkingOut to {0}. Updating app.config value.", consWorkOuts);
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ConsecutiveDaysWorkingOut"].Value = consWorkOuts.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            this.label4.Text = string.Format("Consecutive days working out: {0}", consWorkOuts);
        }
        #endregion Consecutive Workout Functions

        #region Consecutive Studying Functions
        private void ResetStudying()
        {
            this.StudyComplete = false;
            this.StudyBtn.BackColor = Color.Red;

            Console.WriteLine("It's a new day, resetting study task!");
            MessageBox.Show("It's a new day, resetting study task!");
        }

        private void GetConsecutiveDaysStudyingBaseline()
        {
            this.label5.Text = string.Format("Consecutive days studying: {0}", this.GetConsecutiveDaysStudying());
        }

        private int GetConsecutiveDaysStudying()
        {
            var consStudy = ConfigurationManager.AppSettings.Get("ConsecutiveDaysStudying");
            return Int32.Parse(consStudy);
        }

        private void IncrementConsecutiveDaysStudying()
        {
            var consStudy = GetConsecutiveDaysStudying();
            consStudy++;

            // Update the value in the config.. not a great place/way to store but works for now
            Console.WriteLine("Incremented ConsecutiveDaysStudying to {0}. Updating app.config value.", consStudy);
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ConsecutiveDaysStudying"].Value = consStudy.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            this.label5.Text = string.Format("Consecutive days studying: {0}", consStudy);
        }
        #endregion Consecutive Studying Functions

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