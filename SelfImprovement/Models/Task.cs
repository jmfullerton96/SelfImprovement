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

        private readonly string Name;

        private readonly Button TaskButton;

        private readonly Label TaskLabel;

        private readonly string AppConfigValue;

        public Task(string name, Button taskButton, Label taskLabel)
        {
            this.Name = name;
            this.TaskButton = taskButton;
            this.TaskLabel = taskLabel;
            this.TaskComplete = false;

            if (this.Name.Contains(" "))
            {
                this.AppConfigValue = "ConsecutiveDays" + this.Name.Replace(" ", "");
            }
            else
            {
                this.AppConfigValue = "ConsecutiveDays" + this.Name;
            }

            CreateAppConfigEntry();

            this.GetConsecutiveDaysTaskCompleteBaseline();
        }

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
            this.TaskLabel.Text = string.Format("Consecutive days {0}: {1}", this.Name, this.GetConsecutiveTaskComplete());
        }

        public int GetConsecutiveTaskComplete()
        {
            var consTaskComplete = ConfigurationManager.AppSettings.Get(this.AppConfigValue);
            return Int32.Parse(consTaskComplete);
        }

        public void IncrementConsecutiveDayTaskComplete()
        {
            var consTaskComplete = GetConsecutiveTaskComplete();
            consTaskComplete++;

            this.UpdateAppConfigEntry(consTaskComplete);

            this.TaskLabel.Text = string.Format("Consecutive days {0}: {1}", this.Name, consTaskComplete);
        }

        private void UpdateAppConfigEntry(int consecutiveDays)
        {
            // Update the value in the config.. not a great place/way to store but works for now
            Console.WriteLine("Incremented {0} to {1}. Updating app.config value.", this.Name, consecutiveDays);
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[this.AppConfigValue].Value = consecutiveDays.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void CreateAppConfigEntry()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            try
            {
                var val = config.AppSettings.Settings[this.AppConfigValue].Value;
                Console.WriteLine("{0} app.config entry already exists.", this.AppConfigValue);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                Console.WriteLine("Creating {0} app.config entry.", this.AppConfigValue);
                config.AppSettings.Settings.Add(this.AppConfigValue, "0");
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}
