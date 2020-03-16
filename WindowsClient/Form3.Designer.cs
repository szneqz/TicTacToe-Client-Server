namespace WindowsClient
{
    partial class Form3
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
            this.serverNameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AcceptAddButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverNameBox
            // 
            this.serverNameBox.Location = new System.Drawing.Point(71, 29);
            this.serverNameBox.Name = "serverNameBox";
            this.serverNameBox.Size = new System.Drawing.Size(265, 22);
            this.serverNameBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Wprowadź nazwę serwera";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // AcceptAddButton
            // 
            this.AcceptAddButton.Location = new System.Drawing.Point(125, 58);
            this.AcceptAddButton.Name = "AcceptAddButton";
            this.AcceptAddButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptAddButton.TabIndex = 2;
            this.AcceptAddButton.Text = "Załóż";
            this.AcceptAddButton.UseVisualStyleBackColor = true;
            this.AcceptAddButton.Click += new System.EventHandler(this.AcceptAddButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(218, 57);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 3;
            this.BackButton.Text = "Wstecz";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 97);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.AcceptAddButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverNameBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form3";
            this.Text = "Zakładanie serwera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverNameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AcceptAddButton;
        private System.Windows.Forms.Button BackButton;
    }
}