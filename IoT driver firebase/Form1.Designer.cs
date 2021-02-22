
namespace IoT_driver_firebase
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
            System.Windows.Forms.Button startGameButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Zariadenie = new System.Windows.Forms.Label();
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ledLabel = new System.Windows.Forms.Label();
            this.hryBox = new System.Windows.Forms.ComboBox();
            this.startAllButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.stopAllButton = new System.Windows.Forms.Button();
            startGameButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // startGameButton
            // 
            startGameButton.Location = new System.Drawing.Point(6, 46);
            startGameButton.Name = "startGameButton";
            startGameButton.Size = new System.Drawing.Size(126, 23);
            startGameButton.TabIndex = 1;
            startGameButton.Text = "Štart hry";
            startGameButton.UseVisualStyleBackColor = true;
            startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Zariadenie);
            this.groupBox1.Controls.Add(this.deviceComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vyber zariadenie";
            // 
            // Zariadenie
            // 
            this.Zariadenie.AutoSize = true;
            this.Zariadenie.Location = new System.Drawing.Point(180, 29);
            this.Zariadenie.Name = "Zariadenie";
            this.Zariadenie.Size = new System.Drawing.Size(57, 13);
            this.Zariadenie.TabIndex = 1;
            this.Zariadenie.Text = "Zariadenie";
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Location = new System.Drawing.Point(6, 29);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(165, 21);
            this.deviceComboBox.TabIndex = 0;
            this.deviceComboBox.SelectedIndexChanged += new System.EventHandler(this.deviceComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.stopButton);
            this.groupBox2.Controls.Add(this.ledLabel);
            this.groupBox2.Controls.Add(startGameButton);
            this.groupBox2.Controls.Add(this.hryBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nastavenie hry";
            // 
            // ledLabel
            // 
            this.ledLabel.AutoSize = true;
            this.ledLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(50)));
            this.ledLabel.Location = new System.Drawing.Point(222, 13);
            this.ledLabel.Name = "ledLabel";
            this.ledLabel.Size = new System.Drawing.Size(46, 63);
            this.ledLabel.TabIndex = 2;
            this.ledLabel.Text = "•";
            // 
            // hryBox
            // 
            this.hryBox.FormattingEnabled = true;
            this.hryBox.Location = new System.Drawing.Point(6, 19);
            this.hryBox.Name = "hryBox";
            this.hryBox.Size = new System.Drawing.Size(210, 21);
            this.hryBox.TabIndex = 0;
            this.hryBox.SelectedIndexChanged += new System.EventHandler(this.hryBox_SelectedIndexChanged);
            // 
            // startAllButton
            // 
            this.startAllButton.Location = new System.Drawing.Point(18, 210);
            this.startAllButton.Name = "startAllButton";
            this.startAllButton.Size = new System.Drawing.Size(141, 23);
            this.startAllButton.TabIndex = 2;
            this.startAllButton.Text = "Zapnúť všetky";
            this.startAllButton.UseVisualStyleBackColor = true;
            this.startAllButton.Click += new System.EventHandler(this.startAllButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(138, 46);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // stopAllButton
            // 
            this.stopAllButton.Location = new System.Drawing.Point(165, 210);
            this.stopAllButton.Name = "stopAllButton";
            this.stopAllButton.Size = new System.Drawing.Size(141, 23);
            this.stopAllButton.TabIndex = 3;
            this.stopAllButton.Text = "Vypnut všetky";
            this.stopAllButton.UseVisualStyleBackColor = true;
            this.stopAllButton.Click += new System.EventHandler(this.stopAllButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 260);
            this.Controls.Add(this.stopAllButton);
            this.Controls.Add(this.startAllButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "IoT Driver";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox deviceComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox hryBox;
        private System.Windows.Forms.Label Zariadenie;
        private System.Windows.Forms.Button startAllButton;
        private System.Windows.Forms.Label ledLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button stopAllButton;
    }
}

