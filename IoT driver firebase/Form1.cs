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

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace IoT_driver_firebase
{
    public partial class Form1 : Form
    {
        private IFirebaseClient client;
        private IFirebaseConfig config;
        private Senzor[] databazaSenzorov;
        private Dictionary<string, Senzor> zoznamSenzorov;
        public Form1()
        {
            InitializeComponent();
            this.config = new FirebaseConfig { AuthSecret = "gvsLlFof5OhtvW9SEFnpdLYzI6lVgJujVxD7HIHc", BasePath = "https://unikova-hra-default-rtdb.firebaseio.com/" };
            this.client = new FireSharp.FirebaseClient(config);
            if (this.client == null) {
                MessageBox.Show("Chyba pripojenia");
            }
            obnovDatabazu();

            hryBox.Items.Add("Svetelna brana");
            hryBox.Items.Add("Morseovka");
            hryBox.Items.Add("LED Hra");
            hryBox.Items.Add("Miesaj farby");
            hryBox.Items.Add("Tlieskaj");
            hryBox.Items.Add("Dotyk");
        }
        private void obnovDatabazu() {
            zoznamSenzorov = nacitajDatabazu();
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
        private Dictionary<string,Senzor> nacitajDatabazu()
        {
            FirebaseResponse response = client.Get("Zariadenie");
            //Senzor[] todo = response.ResultAs<Senzor[]>();
            Dictionary<string, Senzor> todo = response.ResultAs<Dictionary<string, Senzor>>();
            return todo;
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
            //MessageBox.Show("Data odoslane");
        }
        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hryBox.SelectedIndex = databazaSenzorov[deviceComboBox.SelectedIndex].Volby;
            if (databazaSenzorov[deviceComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;

        }
        private void deviceComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            obnovDatabazu();
        }
        Senzor nacitajZariadenie(string id) {
            FirebaseResponse nacitac = client.Get("Zariadenie/" + id);
            Senzor dev = nacitac.ResultAs<Senzor>();
            return dev;
        }
        private void nastavovac(Senzor dev) {
            var nastavovac = client.Set("Zariadenie/" + dev.Id, dev);
        }

        private void deviceDeleteButton_Click(object sender, EventArgs e)
        {
            if (deviceComboBox.SelectedIndex != -1)
            {
                DialogResult dialogResult = MessageBox.Show("Naozaj cheš odstrániť zariadenie " + deviceComboBox.SelectedItem.ToString() + " ?", "Odstrániť zariadenie", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var vymazavac = client.Delete("Zariadenie/" + deviceComboBox.SelectedItem.ToString());
                    MessageBox.Show("Zariadenie bolo vymazane");
                }
            }
        }

        private void obnovButton_Click(object sender, EventArgs e)
        {
            //int index = deviceComboBox.SelectedIndex;
            obnovDatabazu();
            hryBox.SelectedIndex = databazaSenzorov[deviceComboBox.SelectedIndex].Volby;
            if (databazaSenzorov[deviceComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;
        }
    }
}
