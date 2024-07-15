namespace PracticeP1
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Task2Button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DeleteXMLButton = new System.Windows.Forms.Button();
            this.LoadXMLButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.Task2Button);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 555);
            this.panel1.TabIndex = 0;
            // 
            // Task2Button
            // 
            this.Task2Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(148)))), ((int)(((byte)(94)))));
            this.Task2Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Task2Button.FlatAppearance.BorderSize = 0;
            this.Task2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Task2Button.Font = new System.Drawing.Font("Helvetica", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Task2Button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Task2Button.Location = new System.Drawing.Point(299, 402);
            this.Task2Button.Name = "Task2Button";
            this.Task2Button.Size = new System.Drawing.Size(183, 82);
            this.Task2Button.TabIndex = 2;
            this.Task2Button.Text = "Сформировать файл";
            this.Task2Button.UseVisualStyleBackColor = false;
            this.Task2Button.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(148)))), ((int)(((byte)(94)))));
            this.panel2.Controls.Add(this.DeleteXMLButton);
            this.panel2.Controls.Add(this.LoadXMLButton);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(79, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(641, 284);
            this.panel2.TabIndex = 0;
            // 
            // DeleteXMLButton
            // 
            this.DeleteXMLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(148)))), ((int)(((byte)(94)))));
            this.DeleteXMLButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteXMLButton.FlatAppearance.BorderSize = 0;
            this.DeleteXMLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteXMLButton.Font = new System.Drawing.Font("Helvetica", 8.765218F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteXMLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.DeleteXMLButton.Location = new System.Drawing.Point(242, 184);
            this.DeleteXMLButton.Name = "DeleteXMLButton";
            this.DeleteXMLButton.Size = new System.Drawing.Size(145, 27);
            this.DeleteXMLButton.TabIndex = 3;
            this.DeleteXMLButton.Text = "Удалить файл";
            this.DeleteXMLButton.UseVisualStyleBackColor = false;
            this.DeleteXMLButton.Click += new System.EventHandler(this.DeleteXMLButton_Click);
            // 
            // LoadXMLButton
            // 
            this.LoadXMLButton.AutoSize = true;
            this.LoadXMLButton.BackColor = System.Drawing.Color.White;
            this.LoadXMLButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadXMLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadXMLButton.Font = new System.Drawing.Font("Helvetica", 13.77391F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadXMLButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(148)))), ((int)(((byte)(94)))));
            this.LoadXMLButton.Location = new System.Drawing.Point(220, 133);
            this.LoadXMLButton.Name = "LoadXMLButton";
            this.LoadXMLButton.Size = new System.Drawing.Size(183, 45);
            this.LoadXMLButton.TabIndex = 2;
            this.LoadXMLButton.Text = "Загрузить";
            this.LoadXMLButton.UseVisualStyleBackColor = false;
            this.LoadXMLButton.Click += new System.EventHandler(this.LoadXMLButton_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Helvetica", 16.27826F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(82, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Загрузите сюда XML файл";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(782, 555);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoadXMLButton;
        private System.Windows.Forms.Button DeleteXMLButton;
        private System.Windows.Forms.Button Task2Button;
    }
}