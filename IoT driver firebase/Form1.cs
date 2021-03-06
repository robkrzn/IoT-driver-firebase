using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;


using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Globalization;

namespace IoT_driver_firebase
{
    public partial class Form1 : Form
    {
        private IFirebaseClient client;
        private IFirebaseConfig config;
        private FirebaseResponse response;
        private Senzor[] databazaSenzorov;
        private Dictionary<string, Senzor> zoznamSenzorov;
        private Dictionary<string, string> rebricekDic;
        private Stopwatch stopky;
        public Form1()
        {
            InitializeComponent();
            this.config = new FirebaseConfig { AuthSecret = "gvsLlFof5OhtvW9SEFnpdLYzI6lVgJujVxD7HIHc", BasePath = "https://unikova-hra-default-rtdb.firebaseio.com/" };
            this.client = new FireSharp.FirebaseClient(config);
            if (this.client == null) {
                MessageBox.Show("Chyba pripojenia");
            }
            this.stopky = new Stopwatch();
            obnovDatabazu();

            hryBox.Items.Add("Svetelna brana");
            hryBox.Items.Add("Morseovka");
            hryBox.Items.Add("LED Hra");
            hryBox.Items.Add("Miesaj farby");
            hryBox.Items.Add("Tlieskaj");
            hryBox.Items.Add("Dotyk");

            deviceComboBox.SelectedIndex = 0;
            
            nacitajRebricek();
        }
        private void nacitajRebricek() {
            rebricekDataGridView.Rows.Clear();
            this.response = client.Get("Rebricek");
            this.rebricekDic= response.ResultAs<Dictionary<string, string>>(); 
            for (int i = 0; i < this.rebricekDic.Count; i++) {
                rebricekDataGridView.Rows.Add(null, this.rebricekDic.ElementAt(i).Key, this.rebricekDic.ElementAt(i).Value);
            }
            this.rebricekDataGridView.Sort(casStlpec, ListSortDirection.Ascending);
            for (int i = 0; i < this.rebricekDataGridView.RowCount - 1; i++) {
                this.rebricekDataGridView.Rows[i].Cells[0].Value = i + 1;
                if (i == 0) this.rebricekDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                if (i == 1 || i == 2) this.rebricekDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            }

        }
        private void obnovDatabazu() {
            this.response = client.Get("Zariadenie");
            this.zoznamSenzorov = response.ResultAs<Dictionary<string, Senzor>>();
            databazaSenzorov = zoznamSenzorov.Values.ToArray();
            foreach (Senzor dev in databazaSenzorov) {
                if (dev.Volby > hryBox.Items.Count)dev.Volby = 0;
            }
            int index = deviceComboBox.SelectedIndex; 
            deviceComboBox.Items.Clear();
            for (int i = 0; i < databazaSenzorov.Length; i++)
            {
                deviceComboBox.Items.Add(databazaSenzorov[i].Id);
            }
            deviceComboBox.SelectedIndex = index;
        }
        private void startGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                databazaSenzorov[deviceComboBox.SelectedIndex].Start = true;
                nastavovac(databazaSenzorov[deviceComboBox.SelectedIndex]);
                ledOvalShape.BackColor = Color.Green;
            }
            catch {
                MessageBox.Show("Nevybral si žiadne zariadenie");
            }
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                databazaSenzorov[deviceComboBox.SelectedIndex].Start = false;
                nastavovac(databazaSenzorov[deviceComboBox.SelectedIndex]);
                ledOvalShape.BackColor = Color.Red;
            }
            catch {
                MessageBox.Show("Nevybral si žiadne zariadenie");
            }
        }
        private void startAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < databazaSenzorov.Length; i++){
                databazaSenzorov[i].Start = true;
                nastavovac(databazaSenzorov[i]);
            }
        }
        private void stopAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < databazaSenzorov.Length; i++)
            {
                databazaSenzorov[i].Start = false;
                nastavovac(databazaSenzorov[i]);
            }
        }
        private void hryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            databazaSenzorov[deviceComboBox.SelectedIndex].Volby = hryBox.SelectedIndex;
            nastavovac(databazaSenzorov[deviceComboBox.SelectedIndex]);
        }
        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hryBox.SelectedIndex = databazaSenzorov[deviceComboBox.SelectedIndex].Volby;
            if (databazaSenzorov[deviceComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;
            posledneCheckBox.Checked = databazaSenzorov[deviceComboBox.SelectedIndex].Posledne;
        }
        private void deviceComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            obnovDatabazu();
        }
        Senzor nacitajZariadenie(string id) {
            this.response = client.Get("Zariadenie/" + id);
            return this.response.ResultAs<Senzor>();
        }
        private void nastavovac(Senzor dev) {
            client.Set("Zariadenie/" + dev.Id, dev);
        }

        private void deviceDeleteButton_Click(object sender, EventArgs e)
        {
            if (deviceComboBox.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Naozaj cheš odstrániť zariadenie " + deviceComboBox.SelectedItem.ToString() + " ?", "Odstrániť zariadenie", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    client.Delete("Zariadenie/" + deviceComboBox.SelectedItem.ToString());
                    MessageBox.Show("Zariadenie bolo vymazane");
                }
                deviceComboBox.SelectedIndex = 0;
                obnovDatabazu();
            }
        }

        private void obnovButton_Click(object sender, EventArgs e)
        {
            obnovDatabazu();
            hryBox.SelectedIndex = databazaSenzorov[deviceComboBox.SelectedIndex].Volby;
            if (databazaSenzorov[deviceComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;
            posledneCheckBox.Checked = databazaSenzorov[deviceComboBox.SelectedIndex].Posledne;
        }

        private void stopkyStartButton_Click(object sender, EventArgs e)
        {
            if (stopkyStartButton.Text == "Start")
            {
                if (menoTextBox.Text != "")
                {
                    this.stopky.Start();
                    stopkyStartButton.Text = "Stop";
                }
                else MessageBox.Show("Zabudol si zadať meno!");
            }
            else {
                this.stopky.Stop();
                client.Set("Rebricek/" + menoTextBox.Text, this.stopky.Elapsed.ToString(@"hh\:mm\:ss"));
                MessageBox.Show(menoTextBox.Text + " mal/a čas " + this.stopky.Elapsed.ToString(@"hh\:mm\:ss"));
                nacitajRebricek();
                this.stopky.Reset();
                menoTextBox.Text = "";
                stopkyStartButton.Text = "Start";
            }
        }
        private void stopkyTimer_Tick(object sender, EventArgs e)
        {
            casLabel.Text = this.stopky.Elapsed.ToString(@"hh\:mm\:ss");
        }

        private void deleteCasbutton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Naozaj cheš odstrániť záznam " + this.rebricekDataGridView.SelectedRows[0].Cells[1].Value.ToString()+" - "+this.rebricekDataGridView.SelectedRows[0].Cells[2].Value.ToString() + " ?", "Odstrániť zariadenie", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                client.Delete("Rebricek/" + this.rebricekDataGridView.SelectedRows[0].Cells[1].Value.ToString());
                MessageBox.Show("Záznam bol odstránený");
                nacitajRebricek();
            }
        }

        private void posledneCheckBox_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (posledneCheckBox.Checked == true)
            {
                DialogResult dialogResult = MessageBox.Show("Prajete si zariadenie '"+ databazaSenzorov[deviceComboBox.SelectedIndex].Id  + "' nastaviť ako posledné? ", "Posledne", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < databazaSenzorov.Length; i++)
                    {
                        if (i != deviceComboBox.SelectedIndex)
                        {
                            databazaSenzorov[i].Posledne = false;
                            nastavovac(databazaSenzorov[i]);
                        }
                    }
                }
            }
            databazaSenzorov[deviceComboBox.SelectedIndex].Posledne = posledneCheckBox.Checked;
            nastavovac(databazaSenzorov[deviceComboBox.SelectedIndex]);
        }
    }
}
