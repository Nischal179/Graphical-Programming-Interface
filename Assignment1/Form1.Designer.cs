namespace Assignment1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txt_Action_Cmd = new System.Windows.Forms.TextBox();
            this.txt_Error_Window = new System.Windows.Forms.TextBox();
            this.txt_Command_Window = new System.Windows.Forms.TextBox();
            this.pic_Output_Window = new System.Windows.Forms.PictureBox();
            this.btn_Run = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_syntax_check = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Output_Window)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Action_Cmd
            // 
            this.txt_Action_Cmd.Location = new System.Drawing.Point(103, 252);
            this.txt_Action_Cmd.Name = "txt_Action_Cmd";
            this.txt_Action_Cmd.Size = new System.Drawing.Size(197, 22);
            this.txt_Action_Cmd.TabIndex = 0;
            // 
            // txt_Error_Window
            // 
            this.txt_Error_Window.BackColor = System.Drawing.Color.Black;
            this.txt_Error_Window.ForeColor = System.Drawing.Color.White;
            this.txt_Error_Window.Location = new System.Drawing.Point(12, 323);
            this.txt_Error_Window.Multiline = true;
            this.txt_Error_Window.Name = "txt_Error_Window";
            this.txt_Error_Window.ReadOnly = true;
            this.txt_Error_Window.Size = new System.Drawing.Size(410, 123);
            this.txt_Error_Window.TabIndex = 1;
            // 
            // txt_Command_Window
            // 
            this.txt_Command_Window.BackColor = System.Drawing.Color.Black;
            this.txt_Command_Window.ForeColor = System.Drawing.Color.White;
            this.txt_Command_Window.Location = new System.Drawing.Point(12, 67);
            this.txt_Command_Window.Multiline = true;
            this.txt_Command_Window.Name = "txt_Command_Window";
            this.txt_Command_Window.Size = new System.Drawing.Size(410, 179);
            this.txt_Command_Window.TabIndex = 2;
            // 
            // pic_Output_Window
            // 
            this.pic_Output_Window.BackColor = System.Drawing.Color.LightGray;
            this.pic_Output_Window.Location = new System.Drawing.Point(446, 67);
            this.pic_Output_Window.Name = "pic_Output_Window";
            this.pic_Output_Window.Size = new System.Drawing.Size(342, 371);
            this.pic_Output_Window.TabIndex = 3;
            this.pic_Output_Window.TabStop = false;
            // 
            // btn_Run
            // 
            this.btn_Run.Image = ((System.Drawing.Image)(resources.GetObject("btn_Run.Image")));
            this.btn_Run.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Run.Location = new System.Drawing.Point(319, 252);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(103, 29);
            this.btn_Run.TabIndex = 5;
            this.btn_Run.Text = "   Run";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "       Command Window";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Error Window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Output Window";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(12, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "       Action";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.OwnerItem = this.fuleToolStripMenuItem;
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Text = "File";
            // 
            // fuleToolStripMenuItem
            // 
            this.fuleToolStripMenuItem.DropDown = this.contextMenuStrip1;
            this.fuleToolStripMenuItem.Name = "fuleToolStripMenuItem";
            this.fuleToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.fuleToolStripMenuItem.Text = "File";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fuleToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(102, 28);
            this.contextMenuStrip2.Text = "FIle";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSave,
            this.MenuItemLoad});
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MenuItemSave
            // 
            this.MenuItemSave.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemSave.Image")));
            this.MenuItemSave.Name = "MenuItemSave";
            this.MenuItemSave.Size = new System.Drawing.Size(224, 26);
            this.MenuItemSave.Text = "Save File";
            this.MenuItemSave.Click += new System.EventHandler(this.MenuItemSave_Click);
            // 
            // MenuItemLoad
            // 
            this.MenuItemLoad.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemLoad.Image")));
            this.MenuItemLoad.Name = "MenuItemLoad";
            this.MenuItemLoad.Size = new System.Drawing.Size(224, 26);
            this.MenuItemLoad.Text = "Load File";
            this.MenuItemLoad.Click += new System.EventHandler(this.MenuItemLoad_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem.Image")));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // btn_syntax_check
            // 
            this.btn_syntax_check.Image = ((System.Drawing.Image)(resources.GetObject("btn_syntax_check.Image")));
            this.btn_syntax_check.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_syntax_check.Location = new System.Drawing.Point(319, 287);
            this.btn_syntax_check.Name = "btn_syntax_check";
            this.btn_syntax_check.Size = new System.Drawing.Size(103, 30);
            this.btn_syntax_check.TabIndex = 14;
            this.btn_syntax_check.Text = "     Compile";
            this.btn_syntax_check.UseVisualStyleBackColor = true;
            this.btn_syntax_check.Click += new System.EventHandler(this.btn_syntax_check_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 458);
            this.Controls.Add(this.btn_syntax_check);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.pic_Output_Window);
            this.Controls.Add(this.txt_Command_Window);
            this.Controls.Add(this.txt_Error_Window);
            this.Controls.Add(this.txt_Action_Cmd);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "  Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Output_Window)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Action_Cmd;
        private System.Windows.Forms.TextBox txt_Error_Window;
        private System.Windows.Forms.TextBox txt_Command_Window;
        private System.Windows.Forms.PictureBox pic_Output_Window;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fuleToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLoad;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btn_syntax_check;
    }
}

