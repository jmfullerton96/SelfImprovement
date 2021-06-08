using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfImprovement.Models
{
    class Task
    {
        public bool TaskComplete;

        public string Name;

        public Button TaskButton;

        public Label TaskLabel;

        public void CompleteTask()
        {
            if (!this.TaskComplete)
            {
                this.TaskComplete = true;
                this.TaskButton.BackColor = Color.Green;
                this.IncrementConsecutiveDayTaskComplete();
                Console.WriteLine("Congratulations, you bettered yourself with time spent on {0} today!", this.Name);
            }
            else
            {
                Console.WriteLine("A {0} was already completed today, but an extra one doesn't hurt!", this.Name);
            }
        }

        public void ResetTask()
        {
            this.TaskComplete = false;
            this.TaskButton.BackColor = Color.Red;

            Console.WriteLine("It's a new day, resetting work out task!");
            MessageBox.Show("It's a new day, resetting work out task!");
        }

        public void GetConsecutiveDaysTaskCompleteBaseline()
        {
            this.TaskLabel.Text = string.Format("Consecutive days working out: {0}", this.GetConsecutiveTaskComplete());
        }

        public int GetConsecutiveTaskComplete()
        {
            var consWorkOuts = ConfigurationManager.AppSettings.Get("ConsecutiveDaysWorkingOut");
            return Int32.Parse(consWorkOuts);
        }

        public void IncrementConsecutiveDayTaskComplete()
        {
            var consWorkOuts = GetConsecutiveTaskComplete();
            consWorkOuts++;

            // Update the value in the config.. not a great place/way to store but works for now
            Console.WriteLine("Incremented ConsecutiveDaysWorkingOut to {0}. Updating app.config value.", consWorkOuts);
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ConsecutiveDaysWorkingOut"].Value = consWorkOuts.ToString(); // TODO - Should be setting ConsecutiveDays[Name]Complete
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            this.TaskLabel.Text = string.Format("Consecutive days working out: {0}", consWorkOuts);
        }
    }
}
