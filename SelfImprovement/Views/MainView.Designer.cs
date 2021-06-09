using System;
using System.Drawing;

namespace SelfImprovement
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.WorkOutBtn = new System.Windows.Forms.Button();
            this.StudyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WorkOutBtn
            // 
            this.WorkOutBtn.BackColor = System.Drawing.Color.Red;
            this.WorkOutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOutBtn.Location = new System.Drawing.Point(290, 164);
            this.WorkOutBtn.Name = "WorkOutBtn";
            this.WorkOutBtn.Size = new System.Drawing.Size(209, 49);
            this.WorkOutBtn.TabIndex = 4;
            this.WorkOutBtn.TabStop = false;
            this.WorkOutBtn.Text = "I worked out today!";
            this.WorkOutBtn.UseVisualStyleBackColor = false;
            this.WorkOutBtn.Click += new System.EventHandler(this.WorkOutBtn_Click);
            // 
            // StudyBtn
            // 
            this.StudyBtn.BackColor = System.Drawing.Color.Red;
            this.StudyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StudyBtn.Location = new System.Drawing.Point(290, 265);
            this.StudyBtn.Name = "StudyBtn";
            this.StudyBtn.Size = new System.Drawing.Size(209, 48);
            this.StudyBtn.TabIndex = 3;
            this.StudyBtn.TabStop = false;
            this.StudyBtn.Text = "I studied today!";
            this.StudyBtn.UseVisualStyleBackColor = false;
            this.StudyBtn.Click += new System.EventHandler(this.StudyBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(249, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select tasks below when completed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(286, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 24);
            this.label2.TabIndex = 1;
            this.WorkOutBtn.TabStop = false;
            this.label2.Text = "Time left in the day: foofoo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(155, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(509, 24);
            this.label3.TabIndex = 0;
            this.WorkOutBtn.TabStop = false;
            this.label3.Text = "\"We didn\'t come this far just to come this far.\" - The Situation";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // label4 - Consecutive days working out
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 5;
            this.WorkOutBtn.TabStop = false;
            this.label4.Text = "Consecutive days working out: 0";
            // 
            // label5 - Consecutive days studying
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(296, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 13);
            this.label5.TabIndex = 6;
            this.WorkOutBtn.TabStop = false;
            this.label5.Text = "Consecutive days studying: 0";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StudyBtn);
            this.Controls.Add(this.WorkOutBtn);
            this.Name = "MainView";
            this.Text = "Self Improvement";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button WorkOutBtn;
        private System.Windows.Forms.Button StudyBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

