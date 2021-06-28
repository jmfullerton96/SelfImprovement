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

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                var sqlCmd = new SqlCommand(string.Format("Update Tasks SET TaskComplete = @TaskComplete WHERE Name = @Name"), connection);
                sqlCmd.Parameters.AddWithValue("@TaskComplete", this.TaskComplete);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);

                sqlCmd.ExecuteReader();
            }

            Console.WriteLine("It's a new day, resetting work out task!");
            MessageBox.Show("It's a new day, resetting work out task!");
        }

        public void ResetConsecutiveDays()
        {
            this.ConsecutiveDays = 0;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                var sqlCmd = new SqlCommand(string.Format("Update Tasks SET ConsecutiveDays = @ConsecutiveDays WHERE Name = @Name"), connection);
                sqlCmd.Parameters.AddWithValue("@ConsecutiveDays", this.ConsecutiveDays);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);

                sqlCmd.ExecuteReader();
            }

            Console.WriteLine("You missed {0} today.. resetting consecutive days back to 0.", this.Name);
        }

        private void IncrementConsecutiveDayTaskComplete()
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

            SetLabelText();
        }

        private bool TaskExists()
        {
            bool result = false;
            string nameRetrieved = string.Empty;

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();

                var sqlCmd = new SqlCommand(string.Format("SELECT TaskComplete, ConsecutiveDays FROM Tasks WHERE Name = @Name"), connection);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);

                var dataReader = sqlCmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        this.TaskComplete = dataReader.GetBoolean(0);
                        this.ConsecutiveDays = dataReader.GetInt32(1);
                    }

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

                var sqlCmd = new SqlCommand(string.Format("EXEC Insert_New_Task @Name, @TaskComplete, @TaskButton, @TaskLabel, @ConsecutiveDays"), connection);
                sqlCmd.Parameters.AddWithValue("@Name", this.Name);
                sqlCmd.Parameters.AddWithValue("@TaskComplete", this.TaskComplete);
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
