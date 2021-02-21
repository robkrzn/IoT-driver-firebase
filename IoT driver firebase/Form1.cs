using System;
using System.Collections.Generic;
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
        public Form1()
        {
            InitializeComponent();
            this.config = new FirebaseConfig { AuthSecret = "gvsLlFof5OhtvW9SEFnpdLYzI6lVgJujVxD7HIHc", BasePath = "https://unikova-hra-default-rtdb.firebaseio.com/" };
            this.client = new FireSharp.FirebaseClient(config);
            if (this.client==null) {
                MessageBox.Show("Chyba servera");
            }
            for (int i = 0; i < pocetPrvkov(); i++) {
                deviceComboBox.Items.Add(i);
            }
            
            //deviceComboBox.Items.Add("2");
            //deviceComboBox.Items.Add("3");
            hryBox.Items.Add("Svetelna brana");
            hryBox.Items.Add("Morseovka");
            hryBox.Items.Add("LED Hra");
            hryBox.Items.Add("Miesaj farby");
            hryBox.Items.Add("Tlieskaj");
            hryBox.Items.Add("Dotyk");
        }
        private int pocetPrvkov() {
            int pocet=0;
            try
            {
                var nacitac = client.Get(@"Zariadenie");
                Dictionary<string, Zariadenie> nacitaneZariadenia = nacitac.ResultAs<Dictionary<string, Zariadenie>>();
                pocet = nacitaneZariadenia.Count;
            }
            catch {
                MessageBox.Show("smola");
                deviceComboBox.Items.Add("1");
                deviceComboBox.Items.Add("2");
                deviceComboBox.Items.Add("3");
            }

            //FirebaseResponse response = await client.GetAsync("Zariadenie");
            //Dictionary<string, Zariadenie> data = response.ResultAs<Dictionary<string, Zariadenie>>();
            //Dictionary<string, Zariadenie> nacitaneZariadenia = 
            return pocet;
        }
        private void startGameButton_Click(object sender, EventArgs e)
        {
            Zariadenie dev = new Zariadenie()
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
            Zariadenie dev = new Zariadenie()
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
                Zariadenie dev2 = nacitajZariadenie(prvok.ToString());
                Zariadenie dev = new Zariadenie()
                {
                    Id = dev2.Id,
                    Volby = dev2.Volby,
                    Start = "true"
                };
                var nastavovac = client.Set("Zariadenie/" + prvok.ToString(), dev);
            }
        }
        private void hryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Zariadenie dev2 = nacitajZariadenie(deviceComboBox.SelectedItem.ToString());
            Zariadenie dev = new Zariadenie()
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
            Zariadenie dev = nacitajZariadenie(deviceComboBox.SelectedItem.ToString());
            hryBox.SelectedIndex = hryBox.FindStringExact(dev.Volby);
            if (dev.Start == "true")ledLabel.ForeColor = Color.Green;
            else  ledLabel.ForeColor = Color.Red;
        }
        Zariadenie nacitajZariadenie(string id) {
            FirebaseResponse nacitac = client.Get("Zariadenie/" + id);
            Zariadenie dev = nacitac.ResultAs<Zariadenie>();
            return dev;
        }

        
    }
}
