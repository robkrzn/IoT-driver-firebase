
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button startGameButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deviceDeleteButton = new System.Windows.Forms.Button();
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.hryBox = new System.Windows.Forms.ComboBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ledOvalShape = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.startAllButton = new System.Windows.Forms.Button();
            this.stopAllButton = new System.Windows.Forms.Button();
            this.obnovButton = new System.Windows.Forms.Button();
            this.rebricekDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menoTextBox = new System.Windows.Forms.TextBox();
            this.stopkyStartButton = new System.Windows.Forms.Button();
            this.stopkyStopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.casLabel = new System.Windows.Forms.Label();
            this.stopkyTimer = new System.Windows.Forms.Timer(this.components);
            this.poradieStlpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menoStlpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.casStlpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteCasbutton = new System.Windows.Forms.Button();
            startGameButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rebricekDataGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            // rebricekDataGridView
            // 
            this.rebricekDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rebricekDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.poradieStlpec,
            this.menoStlpec,
            this.casStlpec});
            this.rebricekDataGridView.Location = new System.Drawing.Point(18, 67);
            this.rebricekDataGridView.Name = "rebricekDataGridView";
            this.rebricekDataGridView.ReadOnly = true;
            this.rebricekDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rebricekDataGridView.Size = new System.Drawing.Size(276, 185);
            this.rebricekDataGridView.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.deleteCasbutton);
            this.groupBox3.Controls.Add(this.rebricekDataGridView);
            this.groupBox3.Controls.Add(this.casLabel);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.stopkyStopButton);
            this.groupBox3.Controls.Add(this.stopkyStartButton);
            this.groupBox3.Controls.Add(this.menoTextBox);
            this.groupBox3.Location = new System.Drawing.Point(15, 265);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 268);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rebríček";
            // 
            // menoTextBox
            // 
            this.menoTextBox.Location = new System.Drawing.Point(18, 44);
            this.menoTextBox.Name = "menoTextBox";
            this.menoTextBox.Size = new System.Drawing.Size(136, 20);
            this.menoTextBox.TabIndex = 6;
            // 
            // stopkyStartButton
            // 
            this.stopkyStartButton.Location = new System.Drawing.Point(300, 96);
            this.stopkyStartButton.Name = "stopkyStartButton";
            this.stopkyStartButton.Size = new System.Drawing.Size(75, 23);
            this.stopkyStartButton.TabIndex = 7;
            this.stopkyStartButton.Text = "Start";
            this.stopkyStartButton.UseVisualStyleBackColor = true;
            this.stopkyStartButton.Click += new System.EventHandler(this.stopkyStartButton_Click);
            // 
            // stopkyStopButton
            // 
            this.stopkyStopButton.Location = new System.Drawing.Point(300, 67);
            this.stopkyStopButton.Name = "stopkyStopButton";
            this.stopkyStopButton.Size = new System.Drawing.Size(75, 23);
            this.stopkyStopButton.TabIndex = 8;
            this.stopkyStopButton.Text = "Stop";
            this.stopkyStopButton.UseVisualStyleBackColor = true;
            this.stopkyStopButton.Click += new System.EventHandler(this.stopkyStopButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Meno ";
            // 
            // casLabel
            // 
            this.casLabel.AutoSize = true;
            this.casLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.casLabel.Location = new System.Drawing.Point(177, 39);
            this.casLabel.Name = "casLabel";
            this.casLabel.Size = new System.Drawing.Size(117, 25);
            this.casLabel.TabIndex = 10;
            this.casLabel.Text = "00:00:00.00";
            // 
            // stopkyTimer
            // 
            this.stopkyTimer.Enabled = true;
            this.stopkyTimer.Interval = 1000;
            this.stopkyTimer.Tick += new System.EventHandler(this.stopkyTimer_Tick);
            // 
            // poradieStlpec
            // 
            this.poradieStlpec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.poradieStlpec.HeaderText = "Poradie";
            this.poradieStlpec.Name = "poradieStlpec";
            this.poradieStlpec.ReadOnly = true;
            this.poradieStlpec.Width = 50;
            // 
            // menoStlpec
            // 
            this.menoStlpec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.menoStlpec.HeaderText = "Meno";
            this.menoStlpec.Name = "menoStlpec";
            this.menoStlpec.ReadOnly = true;
            // 
            // casStlpec
            // 
            this.casStlpec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.casStlpec.FillWeight = 65F;
            this.casStlpec.HeaderText = "Čas";
            this.casStlpec.Name = "casStlpec";
            this.casStlpec.ReadOnly = true;
            this.casStlpec.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.casStlpec.Width = 65;
            // 
            // deleteCasbutton
            // 
            this.deleteCasbutton.Cursor = System.Windows.Forms.Cursors.No;
            this.deleteCasbutton.Location = new System.Drawing.Point(300, 229);
            this.deleteCasbutton.Name = "deleteCasbutton";
            this.deleteCasbutton.Size = new System.Drawing.Size(75, 23);
            this.deleteCasbutton.TabIndex = 11;
            this.deleteCasbutton.Text = "Vymazať záznam";
            this.deleteCasbutton.UseVisualStyleBackColor = true;
            this.deleteCasbutton.Click += new System.EventHandler(this.deleteCasbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 549);
            this.Controls.Add(this.groupBox3);
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
            ((System.ComponentModel.ISupportInitialize)(this.rebricekDataGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.DataGridView rebricekDataGridView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label casLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button stopkyStopButton;
        private System.Windows.Forms.Button stopkyStartButton;
        private System.Windows.Forms.TextBox menoTextBox;
        private System.Windows.Forms.Timer stopkyTimer;
        private System.Windows.Forms.DataGridViewTextBoxColumn poradieStlpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn menoStlpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn casStlpec;
        private System.Windows.Forms.Button deleteCasbutton;
    }
}

