
namespace Lab1.Forms {
    partial class MainForm {
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.emailCheckBox = new System.Windows.Forms.CheckBox();
            this.lastNameCheckBox = new System.Windows.Forms.CheckBox();
            this.nameCheckBox = new System.Windows.Forms.CheckBox();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.emailCheckBox);
            this.groupBox.Controls.Add(this.lastNameCheckBox);
            this.groupBox.Controls.Add(this.nameCheckBox);
            this.groupBox.Controls.Add(this.emailTextBox);
            this.groupBox.Controls.Add(this.lastNameTextBox);
            this.groupBox.Controls.Add(this.nameTextBox);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(239, 116);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Information on <User>";
            // 
            // emailCheckBox
            // 
            this.emailCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.emailCheckBox.AutoSize = true;
            this.emailCheckBox.Location = new System.Drawing.Point(184, 80);
            this.emailCheckBox.Name = "emailCheckBox";
            this.emailCheckBox.Size = new System.Drawing.Size(47, 23);
            this.emailCheckBox.TabIndex = 8;
            this.emailCheckBox.Text = "<- Edit";
            this.emailCheckBox.UseVisualStyleBackColor = true;
            this.emailCheckBox.CheckedChanged += new System.EventHandler(this.emailCheckBox_CheckedChanged);
            // 
            // lastNameCheckBox
            // 
            this.lastNameCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.lastNameCheckBox.AutoSize = true;
            this.lastNameCheckBox.Location = new System.Drawing.Point(184, 51);
            this.lastNameCheckBox.Name = "lastNameCheckBox";
            this.lastNameCheckBox.Size = new System.Drawing.Size(47, 23);
            this.lastNameCheckBox.TabIndex = 7;
            this.lastNameCheckBox.Text = "<- Edit";
            this.lastNameCheckBox.UseVisualStyleBackColor = true;
            this.lastNameCheckBox.CheckedChanged += new System.EventHandler(this.lastNameCheckBox_CheckedChanged);
            // 
            // nameCheckBox
            // 
            this.nameCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.nameCheckBox.AutoSize = true;
            this.nameCheckBox.Location = new System.Drawing.Point(184, 22);
            this.nameCheckBox.Name = "nameCheckBox";
            this.nameCheckBox.Size = new System.Drawing.Size(47, 23);
            this.nameCheckBox.TabIndex = 6;
            this.nameCheckBox.Text = "<- Edit";
            this.nameCheckBox.UseVisualStyleBackColor = true;
            this.nameCheckBox.CheckedChanged += new System.EventHandler(this.nameCheckBox_CheckedChanged);
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(68, 80);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.ReadOnly = true;
            this.emailTextBox.Size = new System.Drawing.Size(100, 20);
            this.emailTextBox.TabIndex = 5;
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(68, 54);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.ReadOnly = true;
            this.lastNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.lastNameTextBox.TabIndex = 4;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(68, 25);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "E-mail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Last name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(32, 143);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 1;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(137, 143);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 189);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.groupBox);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "User Information";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.CheckBox nameCheckBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.CheckBox emailCheckBox;
        private System.Windows.Forms.CheckBox lastNameCheckBox;
    }
}