namespace WindowsFormsApp1
{
    partial class Add_Task
    {
        private System.ComponentModel.IContainer components = null;

        // Control declarations
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Button button2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtChat = new System.Windows.Forms.TextBox();
            this.Input = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(12, 12);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(776, 300);
            this.txtChat.TabIndex = 0;
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(12, 330);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(668, 20);
            this.Input.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(700, 328);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Add_Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.button2);
            this.Name = "Add_Task";
            this.Text = "Add Tasks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}