namespace WindowsClient
{
    partial class Form2
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
            this.serverList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addGameButton = new System.Windows.Forms.Button();
            this.joinGameButton = new System.Windows.Forms.Button();
            this.exitServerButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverList
            // 
            this.serverList.FormattingEnabled = true;
            this.serverList.Items.AddRange(new object[] {
            "Heheheh",
            "dwa",
            "3 trzy"});
            this.serverList.Location = new System.Drawing.Point(12, 36);
            this.serverList.Margin = new System.Windows.Forms.Padding(2);
            this.serverList.Name = "serverList";
            this.serverList.Size = new System.Drawing.Size(155, 303);
            this.serverList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lista stołów";
            // 
            // addGameButton
            // 
            this.addGameButton.Location = new System.Drawing.Point(178, 36);
            this.addGameButton.Margin = new System.Windows.Forms.Padding(2);
            this.addGameButton.Name = "addGameButton";
            this.addGameButton.Size = new System.Drawing.Size(94, 19);
            this.addGameButton.TabIndex = 4;
            this.addGameButton.Text = "Załóż grę";
            this.addGameButton.UseVisualStyleBackColor = true;
            this.addGameButton.Click += new System.EventHandler(this.AddGameButton_Click);
            // 
            // joinGameButton
            // 
            this.joinGameButton.Location = new System.Drawing.Point(178, 59);
            this.joinGameButton.Margin = new System.Windows.Forms.Padding(2);
            this.joinGameButton.Name = "joinGameButton";
            this.joinGameButton.Size = new System.Drawing.Size(94, 19);
            this.joinGameButton.TabIndex = 5;
            this.joinGameButton.Text = "Dołącz do gry";
            this.joinGameButton.UseVisualStyleBackColor = true;
            this.joinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
            // 
            // exitServerButton
            // 
            this.exitServerButton.Location = new System.Drawing.Point(178, 318);
            this.exitServerButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitServerButton.Name = "exitServerButton";
            this.exitServerButton.Size = new System.Drawing.Size(94, 19);
            this.exitServerButton.TabIndex = 6;
            this.exitServerButton.Text = "Wyjdź z serwera";
            this.exitServerButton.UseVisualStyleBackColor = true;
            this.exitServerButton.Click += new System.EventHandler(this.ExitServerButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(178, 82);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(2);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(94, 19);
            this.refreshButton.TabIndex = 7;
            this.refreshButton.Text = "Odśwież";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 366);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.exitServerButton);
            this.Controls.Add(this.joinGameButton);
            this.Controls.Add(this.addGameButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Lista serwerów";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox serverList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addGameButton;
        private System.Windows.Forms.Button joinGameButton;
        private System.Windows.Forms.Button exitServerButton;
        private System.Windows.Forms.Button refreshButton;
    }
}