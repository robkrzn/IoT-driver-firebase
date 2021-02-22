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
        private int pocetPrvkovNum=0;
        private Senzor[] databazaSenzorov;
        public Form1()
        {
            InitializeComponent();
            this.config = new FirebaseConfig { AuthSecret = "gvsLlFof5OhtvW9SEFnpdLYzI6lVgJujVxD7HIHc", BasePath = "https://unikova-hra-default-rtdb.firebaseio.com/" };
            this.client = new FireSharp.FirebaseClient(config);
            if (this.client==null) {
                MessageBox.Show("Chyba pripojenia");
            }
            for (int i = 0; i < this.pocetPrvkovNum; i++) {
                deviceComboBox.Items.Add(i);
            }
            this.databazaSenzorov = databaza();
            //pocetPrvkov();
            for (int i = 1; i < databazaSenzorov.Length; i++) {
                deviceComboBox.Items.Add(databazaSenzorov[i].Id);
            }
            //deviceComboBox.Items.Add("1");
            //deviceComboBox.Items.Add("2");
            deviceComboBox.Items.Add("4");
            hryBox.Items.Add("Svetelna brana");
            hryBox.Items.Add("Morseovka");
            hryBox.Items.Add("LED Hra");
            hryBox.Items.Add("Miesaj farby");
            hryBox.Items.Add("Tlieskaj");
            hryBox.Items.Add("Dotyk");
        }
        private async void pocetPrvkov() {
            FirebaseResponse response = await client.GetAsync("Zariadenie");
            Senzor[] todo = response.ResultAs<Senzor[]>();
            MessageBox.Show(todo[2].Volby);
        }
        private Senzor[] databaza() {
            FirebaseResponse response = client.Get("Zariadenie");
            Senzor[] todo = response.ResultAs<Senzor[]>();
            return todo;
        }
        private void startGameButton_Click(object sender, EventArgs e)
        {
            Senzor dev = new Senzor()
            {
                Id = deviceComboBox.SelectedItem.ToString(),
                Volby = hryBox.SelectedItem.ToString(),
                Start = "true"
            };
            var nastavovac = client.Set("Zariadenie/" + deviceComboBox.SelectedItem.ToString(),dev);
            ledLabel.ForeColor = Color.Green;
            //MessageBox.Show("Zariadenie zapnuté");
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            Senzor dev = new Senzor()
            {
                Id = deviceComboBox.SelectedItem.ToString(),
                Volby = hryBox.SelectedItem.ToString(),
                Start = "false"
            };
            var nastavovac = client.Set("Zariadenie/" + deviceComboBox.SelectedItem.ToString(), dev);
            ledLabel.ForeColor = Color.Red;
            //MessageBox.Show("Zariadenie zapnuté");
        }
        private void startAllButton_Click(object sender, EventArgs e)
        {
            foreach (var prvok in deviceComboBox.Items)
            {
                Senzor dev2 = nacitajZariadenie(prvok.ToString());
                Senzor dev = new Senzor()
                {
                    Id = dev2.Id,
                    Volby = dev2.Volby,
                    Start = "true"
                };
                var nastavovac = client.Set("Zariadenie/" + prvok.ToString(), dev);
            }
        }
        private void stopAllButton_Click(object sender, EventArgs e)
        {
            foreach (var prvok in deviceComboBox.Items)
            {
                Senzor dev2 = nacitajZariadenie(prvok.ToString());
                Senzor dev = new Senzor()
                {
                    Id = dev2.Id,
                    Volby = dev2.Volby,
                    Start = "false"
                };
                var nastavovac = client.Set("Zariadenie/" + prvok.ToString(), dev);
            }
        }
        private void hryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Senzor dev2 = nacitajZariadenie(deviceComboBox.SelectedItem.ToString());
            Senzor dev = new Senzor()
            {
                Id = deviceComboBox.SelectedItem.ToString(),
                Volby = hryBox.SelectedItem.ToString(),
                Start = dev2.Start
            };
            var nastavovac = client.Set("Zariadenie/" + deviceComboBox.SelectedItem.ToString(), dev);
            //MessageBox.Show("Data odoslane");
        }

        

        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Senzor dev = nacitajZariadenie(deviceComboBox.SelectedItem.ToString());
            hryBox.SelectedIndex = hryBox.FindStringExact(dev.Volby);
            if (dev.Start == "true") ledLabel.ForeColor = Color.Green;
            else ledLabel.ForeColor = Color.Red;
            
        }
        Senzor nacitajZariadenie(string id) {
            FirebaseResponse nacitac = client.Get("Zariadenie/" + id);
            Senzor dev = nacitac.ResultAs<Senzor>();
            return dev;
        }

        
    }
}
