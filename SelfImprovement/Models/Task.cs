using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Mvc;
using NHibernate.Linq;
using NHibernate;

namespace SelfImprovement.Models
{
    class Task
    {
        public bool TaskComplete;

        public int ConsecutiveDays;

        private readonly string Name;

        private readonly Button TaskButton;

        private readonly Label TaskLabel;

        public Task(string name, Button taskButton, Label taskLabel)
        {
            this.Name = name;
            this.TaskButton = taskButton;
            this.TaskLabel = taskLabel;
            this.TaskComplete = false;
            this.ConsecutiveDays = 0;

            using (ISession session = NHibernateSession.OpenSession())
            {
                var existingTask = session.Query<Task>(this.Name);

                if (existingTask != null)
                {
                    // create new task entry
                    this.Create();
                }
            }

            this.GetConsecutiveDaysTaskCompleteBaseline();
        }

        [HttpPost]
        private void Create()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(this);
                    transaction.Commit();
                }
            }
        }

        [HttpPost]
        private void Edit()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var taskUpdateObj = session.Get<Task>(this.Name);
                    taskUpdateObj.TaskComplete = this.TaskComplete;
                    taskUpdateObj.ConsecutiveDays = this.ConsecutiveDays;

                    session.Save(this);
                    transaction.Commit();
                }
            }
        }

        [HttpPost]
        private void Delete()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(this);
                    transaction.Commit();
                }
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

            Console.WriteLine("It's a new day, resetting work out task!");
            MessageBox.Show("It's a new day, resetting work out task!");
        }

        public void GetConsecutiveDaysTaskCompleteBaseline()
        {
            this.TaskLabel.Text = string.Format("Consecutive days {0}: {1}", this.Name, this.GetConsecutiveTaskComplete());
        }

        public int GetConsecutiveTaskComplete()
        {
            //var consTaskComplete = ConfigurationManager.AppSettings.Get(this.AppConfigValue);
            //return Int32.Parse(consTaskComplete);
            return 0;
        }

        public void IncrementConsecutiveDayTaskComplete()
        {
            var consTaskComplete = GetConsecutiveTaskComplete();
            consTaskComplete++;

            //this.UpdateAppConfigEntry(consTaskComplete);

            this.TaskLabel.Text = string.Format("Consecutive days {0}: {1}", this.Name, consTaskComplete);
        }
    }
}
