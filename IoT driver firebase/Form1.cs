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
            deviceComboBox.Items.Add("1");
            deviceComboBox.Items.Add("2");
            deviceComboBox.Items.Add("3");
            hryBox.Items.Add("Svetelna brana");
            hryBox.Items.Add("Morseovka");
            hryBox.Items.Add("LED Hra");
            hryBox.Items.Add("Miesaj farby");
            hryBox.Items.Add("Tlieskaj");
            hryBox.Items.Add("Dotyk");
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            Zariadenie dev = new Zariadenie()
            {
                Zaradenie = deviceComboBox.SelectedItem.ToString(),
                Volby = hryBox.SelectedItem.ToString(),
                Start = "true"
            };
            var nastavovac = client.Set("Zariadenie/" + deviceComboBox.SelectedItem.ToString(),dev);
            ledLabel.ForeColor = Color.Green;
            //MessageBox.Show("Zariadenie zapnuté");
        }

        private void hryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Zariadenie dev = new Zariadenie()
            {
                Zaradenie = deviceComboBox.SelectedItem.ToString(),
                Volby = hryBox.SelectedItem.ToString(),
                Start = "false"
            };
            var nastavovac = client.Set("Zariadenie/" + deviceComboBox.SelectedItem.ToString(), dev);
            //MessageBox.Show("Data odoslane");
        }

        private void startAllButton_Click(object sender, EventArgs e)
        {
            var nacitac = client.Get("Zariadenie/" + "1");
            Zariadenie dev = nacitac.ResultAs<Zariadenie>();
            hryBox.SelectedIndex = hryBox.FindStringExact(dev.Volby);
        }

        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nacitac = client.Get("Zariadenie/" + deviceComboBox.SelectedItem.ToString());
            Zariadenie dev = nacitac.ResultAs<Zariadenie>();
            hryBox.SelectedIndex = hryBox.FindStringExact(dev.Volby);
            if (dev.Start == "true")ledLabel.ForeColor = Color.Green;
            else  ledLabel.ForeColor = Color.Red;
        }
    }
}
