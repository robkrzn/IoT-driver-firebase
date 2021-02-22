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
        public Form1()
        {
            InitializeComponent();
            this.config = new FirebaseConfig { AuthSecret = "gvsLlFof5OhtvW9SEFnpdLYzI6lVgJujVxD7HIHc", BasePath = "https://unikova-hra-default-rtdb.firebaseio.com/" };
            this.client = new FireSharp.FirebaseClient(config);
            if (this.client == null) {
                MessageBox.Show("Chyba pripojenia");
            }
            this.databazaSenzorov = nacitajDatabazu();

            for (int i = 1; i < databazaSenzorov.Length; i++) {
                deviceComboBox.Items.Add(databazaSenzorov[i].Id);
            }
            hryBox.Items.Add("Svetelna brana");
            hryBox.Items.Add("Morseovka");
            hryBox.Items.Add("LED Hra");
            hryBox.Items.Add("Miesaj farby");
            hryBox.Items.Add("Tlieskaj");
            hryBox.Items.Add("Dotyk");
        }
        private Senzor[] nacitajDatabazu() {
            FirebaseResponse response = client.Get("Zariadenie");
            Senzor[] todo = response.ResultAs<Senzor[]>();
            return todo;
        }
        private void startGameButton_Click(object sender, EventArgs e)
        {
            databazaSenzorov[deviceComboBox.SelectedIndex + 1].Start = "true";
            nastavovac(databazaSenzorov[deviceComboBox.SelectedIndex + 1]);
            ledLabel.ForeColor = Color.Green;
            //MessageBox.Show("Zariadenie zapnuté");
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            databazaSenzorov[deviceComboBox.SelectedIndex + 1].Start = "false";
            nastavovac(databazaSenzorov[deviceComboBox.SelectedIndex + 1]);
            ledLabel.ForeColor = Color.Red;
            //MessageBox.Show("Zariadenie zapnuté");
        }
        private void startAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < databazaSenzorov.Length; i++){
                databazaSenzorov[i].Start = "true";
                nastavovac(databazaSenzorov[i]);
            }
        }
        private void stopAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < databazaSenzorov.Length; i++)
            {
                databazaSenzorov[i].Start = "false";
                nastavovac(databazaSenzorov[i]);
            }
        }
        private void hryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            databazaSenzorov[deviceComboBox.SelectedIndex + 1].Volby = hryBox.SelectedItem.ToString();
            nastavovac(databazaSenzorov[deviceComboBox.SelectedIndex + 1]);
            //MessageBox.Show("Data odoslane");
        }
        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hryBox.SelectedIndex = hryBox.FindStringExact(databazaSenzorov[deviceComboBox.SelectedIndex + 1].Volby);
            if (databazaSenzorov[deviceComboBox.SelectedIndex + 1].Start == "true") ledLabel.ForeColor = Color.Green;
            else ledLabel.ForeColor = Color.Red;
            
        }
        Senzor nacitajZariadenie(string id) {
            FirebaseResponse nacitac = client.Get("Zariadenie/" + id);
            Senzor dev = nacitac.ResultAs<Senzor>();
            return dev;
        }
        private void nastavovac(Senzor dev) {
            var nastavovac = client.Set("Zariadenie/" + dev.Id, dev);
        }

        
    }
}
