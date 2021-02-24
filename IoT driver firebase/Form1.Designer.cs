
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
            this.deviceDeleteButton = new System.Windows.Forms.Button();
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.hryBox = new System.Windows.Forms.ComboBox();
            this.startAllButton = new System.Windows.Forms.Button();
            this.stopAllButton = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ledOvalShape = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.obnovButton = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.deviceDeleteButton);
            this.groupBox1.Controls.Add(this.deviceComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vyber zariadenie";
            // 
            // deviceDeleteButton
            // 
            this.deviceDeleteButton.Location = new System.Drawing.Point(282, 19);
            this.deviceDeleteButton.Name = "deviceDeleteButton";
            this.deviceDeleteButton.Size = new System.Drawing.Size(120, 23);
            this.deviceDeleteButton.TabIndex = 2;
            this.deviceDeleteButton.Text = "Vymazať zariadenie";
            this.deviceDeleteButton.UseVisualStyleBackColor = true;
            this.deviceDeleteButton.Click += new System.EventHandler(this.deviceDeleteButton_Click);
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Location = new System.Drawing.Point(6, 29);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(165, 21);
            this.deviceComboBox.TabIndex = 0;
            this.deviceComboBox.SelectedIndexChanged += new System.EventHandler(this.deviceComboBox_SelectedIndexChanged);
            this.deviceComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.deviceComboBox_MouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.stopButton);
            this.groupBox2.Controls.Add(startGameButton);
            this.groupBox2.Controls.Add(this.hryBox);
            this.groupBox2.Controls.Add(this.shapeContainer1);
            this.groupBox2.Location = new System.Drawing.Point(12, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nastavenie hry";
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
            this.startAllButton.Location = new System.Drawing.Point(12, 192);
            this.startAllButton.Name = "startAllButton";
            this.startAllButton.Size = new System.Drawing.Size(141, 23);
            this.startAllButton.TabIndex = 2;
            this.startAllButton.Text = "Zapnúť všetky";
            this.startAllButton.UseVisualStyleBackColor = true;
            this.startAllButton.Click += new System.EventHandler(this.startAllButton_Click);
            // 
            // stopAllButton
            // 
            this.stopAllButton.Location = new System.Drawing.Point(12, 221);
            this.stopAllButton.Name = "stopAllButton";
            this.stopAllButton.Size = new System.Drawing.Size(141, 23);
            this.stopAllButton.TabIndex = 3;
            this.stopAllButton.Text = "Vypnut všetky";
            this.stopAllButton.UseVisualStyleBackColor = true;
            this.stopAllButton.Click += new System.EventHandler(this.stopAllButton_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.ledOvalShape});
            this.shapeContainer1.Size = new System.Drawing.Size(402, 66);
            this.shapeContainer1.TabIndex = 4;
            this.shapeContainer1.TabStop = false;
            // 
            // ledOvalShape
            // 
            this.ledOvalShape.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ledOvalShape.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.ledOvalShape.FillGradientColor = System.Drawing.Color.Black;
            this.ledOvalShape.Location = new System.Drawing.Point(229, 12);
            this.ledOvalShape.Name = "ledOvalShape";
            this.ledOvalShape.SelectionColor = System.Drawing.SystemColors.ButtonShadow;
            this.ledOvalShape.Size = new System.Drawing.Size(30, 30);
            // 
            // obnovButton
            // 
            this.obnovButton.Location = new System.Drawing.Point(351, 192);
            this.obnovButton.Name = "obnovButton";
            this.obnovButton.Size = new System.Drawing.Size(69, 52);
            this.obnovButton.TabIndex = 4;
            this.obnovButton.Text = "Obnoviť databázu";
            this.obnovButton.UseVisualStyleBackColor = true;
            this.obnovButton.Click += new System.EventHandler(this.obnovButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 260);
            this.Controls.Add(this.obnovButton);
            this.Controls.Add(this.stopAllButton);
            this.Controls.Add(this.startAllButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "IoT Driver";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox deviceComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox hryBox;
        private System.Windows.Forms.Button startAllButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button stopAllButton;
        private System.Windows.Forms.Button deviceDeleteButton;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ledOvalShape;
        private System.Windows.Forms.Button obnovButton;
    }
}

