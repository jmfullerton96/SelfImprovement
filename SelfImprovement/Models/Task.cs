using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        public int ConsecutiveDays;

        public DateTime LastDayCompleted;

        public readonly DateTime DefaultLastDayCompleted = new DateTime(1996, 11, 18);

        private readonly string Name;

        private readonly Button TaskButton;

        private readonly Label TaskLabel;

        private readonly string ConnectionString;

        public Task(string name, Button taskButton, Label taskLabel, string connectionString)
        {
            this.Name = name;
            this.TaskButton = taskButton;
            this.TaskLabel = taskLabel;
            this.ConnectionString = connectionString;

            if (!this.TaskExists())
            {
                this.TaskComplete = false;
                this.ConsecutiveDays = 0;
                this.LastDayCompleted = this.DefaultLastDayCompleted;
                this.CreateTask();
            }
            else
            {
                if (this.TaskComplete)
                {
                    this.TaskButton.BackColor = Color.Green;
                }

                SetLabelText();
            }
        }

        public void CompleteTask()
        {
            if (!this.TaskComplete)
            {
                this.TaskComplete = true;
                this.TaskButton.BackColor = Color.Green;
                this.LastDayCompleted = DateTime.Now.Date;
                this.CompleteTaskDB();
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
        }

        public void ResetConsecutiveDays()
        {
            // General reset then take care of the ConsecutiveDays
            this.ResetTask();

            this.ConsecutiveDays = 0;
            this.LastDayCompleted = this.DefaultLastDayCompleted;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                var sqlCmd = new SqlCommand(string.Format("Update Tasks SET ConsecutiveDays = @ConsecutiveDays, LastDayCompleted = @LastDayCompleted WHERE Name = @Name"), connection);
                sqlCmd.Parameters.AddWithValue("@ConsecutiveDays", this.ConsecutiveDays);
                sqlCmd.Parameters.AddWithValue("@LastDayCompleted", this.LastDayCompleted);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);

                sqlCmd.ExecuteReader();
            }

            this.SetLabelText();

            Console.WriteLine("You missed {0} today.. resetting consecutive days back to 0.", this.Name);
        }

        private void SetTaskComplete()
        {
            if (this.LastDayCompleted.Date.Equals(DateTime.Now.Date))
            {
                this.TaskComplete = true;
            }
            else
            {
                this.TaskComplete = false;
            }
        }

        private void CompleteTaskDB()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                var sqlCmd = new SqlCommand(string.Format("EXEC Complete_Task @Name"), connection);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);

                var dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    this.ConsecutiveDays = dataReader.GetInt32(0);
                }
            }

            this.SetLabelText();
        }

        private bool TaskExists()
        {
            bool result = false;
            string nameRetrieved = string.Empty;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                var sqlCmd = new SqlCommand(string.Format("SELECT LastDayCompleted, ConsecutiveDays FROM Tasks WHERE Name = @Name"), connection);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);

                var dataReader = sqlCmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        this.LastDayCompleted = dataReader.GetDateTime(0);
                        this.ConsecutiveDays = dataReader.GetInt32(1);
                    }

                    this.SetTaskComplete();

                    result = true;
                }
            }

            return result;
        }

        private void CreateTask()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                var sqlCmd = new SqlCommand(string.Format("EXEC Insert_New_Task @Name, @LastDayCompleted, @TaskButton, @TaskLabel, @ConsecutiveDays"), connection);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);
                sqlCmd.Parameters.AddWithValue("@LastDayCompleted", this.LastDayCompleted.Date);
                sqlCmd.Parameters.AddWithValue("@TaskButton", this.TaskButton.Name);
                sqlCmd.Parameters.AddWithValue("@TaskLabel", this.TaskLabel.Name);
                sqlCmd.Parameters.AddWithValue("@ConsecutiveDays", this.ConsecutiveDays);
                
                sqlCmd.ExecuteReader();
            }
        }

        private void SetLabelText()
        {
            this.TaskLabel.Text = string.Format("Consecutive days {0}: {1}", this.Name, this.ConsecutiveDays);
        }
    }
}
