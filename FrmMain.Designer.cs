namespace Data_Server {
    partial class FrmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TextSystem = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TextPlayer = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSavePlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSerialize = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuDeserialize = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerClearText = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(520, 382);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.TextSystem);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(512, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // TextSystem
            // 
            this.TextSystem.BackColor = System.Drawing.SystemColors.Control;
            this.TextSystem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextSystem.DetectUrls = false;
            this.TextSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextSystem.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.TextSystem.Location = new System.Drawing.Point(3, 3);
            this.TextSystem.Name = "TextSystem";
            this.TextSystem.ReadOnly = true;
            this.TextSystem.Size = new System.Drawing.Size(506, 348);
            this.TextSystem.TabIndex = 0;
            this.TextSystem.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.TextPlayer);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(512, 354);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Player";
            // 
            // TextPlayer
            // 
            this.TextPlayer.BackColor = System.Drawing.SystemColors.Control;
            this.TextPlayer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextPlayer.DetectUrls = false;
            this.TextPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextPlayer.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.TextPlayer.Location = new System.Drawing.Point(3, 3);
            this.TextPlayer.Name = "TextPlayer";
            this.TextPlayer.ReadOnly = true;
            this.TextPlayer.ShortcutsEnabled = false;
            this.TextPlayer.Size = new System.Drawing.Size(506, 348);
            this.TextPlayer.TabIndex = 1;
            this.TextPlayer.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.playerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(544, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuExit});
            this.MenuFile.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(39, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuExit
            // 
            this.MenuExit.Name = "MenuExit";
            this.MenuExit.Size = new System.Drawing.Size(95, 22);
            this.MenuExit.Text = "Exit";
            this.MenuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // playerToolStripMenuItem
            // 
            this.playerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuClear,
            this.MenuSavePlayer,
            this.MenuSerialize,
            this.MenuDeserialize});
            this.playerToolStripMenuItem.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.playerToolStripMenuItem.Name = "playerToolStripMenuItem";
            this.playerToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.playerToolStripMenuItem.Text = "Player";
            // 
            // MenuClear
            // 
            this.MenuClear.Name = "MenuClear";
            this.MenuClear.Size = new System.Drawing.Size(137, 22);
            this.MenuClear.Text = "Clear";
            this.MenuClear.Click += new System.EventHandler(this.MenuClear_Click);
            // 
            // MenuSavePlayer
            // 
            this.MenuSavePlayer.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.MenuSavePlayer.Name = "MenuSavePlayer";
            this.MenuSavePlayer.Size = new System.Drawing.Size(137, 22);
            this.MenuSavePlayer.Text = "Save";
            this.MenuSavePlayer.Click += new System.EventHandler(this.MenuSavePlayer_Click);
            // 
            // MenuSerialize
            // 
            this.MenuSerialize.Font = new System.Drawing.Font("Lucida Sans", 9F);
            this.MenuSerialize.Name = "MenuSerialize";
            this.MenuSerialize.Size = new System.Drawing.Size(137, 22);
            this.MenuSerialize.Text = "Serialize";
            this.MenuSerialize.Click += new System.EventHandler(this.MenuSerialize_Click);
            // 
            // MenuDeserialize
            // 
            this.MenuDeserialize.Name = "MenuDeserialize";
            this.MenuDeserialize.Size = new System.Drawing.Size(137, 22);
            this.MenuDeserialize.Text = "Deserialize";
            this.MenuDeserialize.Click += new System.EventHandler(this.MenuDeserialize_Click);
            // 
            // TimerClearText
            // 
            this.TimerClearText.Enabled = true;
            this.TimerClearText.Interval = 25000;
            this.TimerClearText.Tick += new System.EventHandler(this.TimerClearText_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 421);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Relay ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.RichTextBox TextSystem;
        private System.Windows.Forms.ToolStripMenuItem MenuExit;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuSavePlayer;
        private System.Windows.Forms.ToolStripMenuItem MenuSerialize;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox TextPlayer;
        private System.Windows.Forms.Timer TimerClearText;
        private System.Windows.Forms.ToolStripMenuItem MenuClear;
        private System.Windows.Forms.ToolStripMenuItem MenuDeserialize;
    }
}

