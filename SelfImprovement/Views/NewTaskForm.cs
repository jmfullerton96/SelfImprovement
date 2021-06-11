using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfImprovement.Views
{
    public partial class NewTaskForm : Form
    {
        public NewTaskForm()
        {
            InitializeComponent();
        }

        private void AddTaskBtn_Click(object sender, EventArgs e)
        {
            // close the current dialog

            // Create new task object
            MainView.CreateNewTask(this.TaskName.Text);
            //Models.Task newTask = new Models.Task(this.TaskName); // Don't think this should be called here
        }
    }
}
