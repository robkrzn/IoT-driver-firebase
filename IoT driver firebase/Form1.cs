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
        private FirebaseResponse opoved;
        private Senzor[] databazaSenzorov;
        private Senzor[] oldDatabazaSenzorov;
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
            hryBox.Items.Add("Vzdialenosť");

            zariadenieComboBox.SelectedIndex = 0;
            
            nacitajRebricek();
        }
        private void nacitajRebricek() {
            rebricekDataGridView.Rows.Clear();  //vymazanie aktualneho rebricka
            this.opoved = client.Get("Rebricek");   // ziskanie dat pod prvkom "Rebricek"
            this.rebricekDic= opoved.ResultAs<Dictionary<string, string>>(); //formatovanie JSON dat do slovnika
            for (int i = 0; i < this.rebricekDic.Count; i++) {  //naplnenie rebricka aktualnmi datami
                rebricekDataGridView.Rows.Add(null, this.rebricekDic.ElementAt(i).Key, this.rebricekDic.ElementAt(i).Value);
            }
            this.rebricekDataGridView.Sort(casStlpec, ListSortDirection.Ascending); //zoradenie rebricka podla casu
            for (int i = 0; i < this.rebricekDataGridView.RowCount - 1; i++) {  
                this.rebricekDataGridView.Rows[i].Cells[0].Value = i + 1;  //doplnenie poriadia k prvom v rebricku
                if (i == 0) this.rebricekDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;    //farebne oznacenie prvych 3 miest
                if (i == 1 || i == 2) this.rebricekDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;    
            }

        }
        private void obnovDatabazu() {
            this.oldDatabazaSenzorov = this.databazaSenzorov; //aktualna databaza sa ulozi do pomocnej premmennej
            this.opoved = client.Get("Zariadenie"); //ziskanie dat pod prvkom "Zariadenie"
            this.zoznamSenzorov = opoved.ResultAs<Dictionary<string, Senzor>>();  //formatovanie JSON dat do slovnika
            this.databazaSenzorov = zoznamSenzorov.Values.ToArray();    //prepis slovníka na pole
            int index = zariadenieComboBox.SelectedIndex;   //ulozenie aktualneho vybraneho zariadenia
            zariadenieComboBox.Items.Clear();   //vycistenie moznosti pre vyber zariadenia
            foreach (Senzor dev in this.databazaSenzorov) { //prejdenie pola s udajmi
                if (dev.Volby > hryBox.Items.Count)dev.Volby = 0;   //ak je nahodou niekde väcsi udaj nastav 0
                zariadenieComboBox.Items.Add(dev.Id);   //pridanie zariadeni do vyberu zariadenia
            }
            try
            {
                zariadenieComboBox.SelectedIndex = index;   //ak sa da nastav pôvodny index
            }
            catch { }
            postupVHreProgressBar.Maximum = databazaSenzorov.Length;    //podla poctu zariadeni nastav zobrazovac postupu
        }
        private void startGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                databazaSenzorov[zariadenieComboBox.SelectedIndex].Start = true;
                nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]);
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
                databazaSenzorov[zariadenieComboBox.SelectedIndex].Start = false;
                nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]);
                ledOvalShape.BackColor = Color.Red;
            }
            catch {
                MessageBox.Show("Nevybral si žiadne zariadenie");
            }
        }
        private void startAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < databazaSenzorov.Length; i++)
            {
                databazaSenzorov[i].Start = true;
                nastavovac(databazaSenzorov[i]);
            }
            ledOvalShape.BackColor = Color.Green;
        }
        private void stopAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < databazaSenzorov.Length; i++)
            {
                databazaSenzorov[i].Start = false;
                nastavovac(databazaSenzorov[i]);
            }
            ledOvalShape.BackColor = Color.Red;
        }
        private void hryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby = hryBox.SelectedIndex;
            nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]);
        }
        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hryBox.SelectedIndex = databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby;
            if (databazaSenzorov[zariadenieComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;
            posledneCheckBox.Checked = databazaSenzorov[zariadenieComboBox.SelectedIndex].Posledne;
        }
        private void deviceComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            obnovDatabazu();
        }
        Senzor nacitajZariadenie(string id) {
            this.opoved = client.Get("Zariadenie/" + id);
            return this.opoved.ResultAs<Senzor>();
        }
        private void nastavovac(Senzor dev) {
            client.Set("Zariadenie/" + dev.Id, dev);    //nastav k zariadeniu pod id jeho vsetky udaje
        }

        private void deviceDeleteButton_Click(object sender, EventArgs e)
        {
            if (zariadenieComboBox.SelectedIndex != -1) //ak je vybrane nejaké zariadenie
            {
                DialogResult dialogResult = MessageBox.Show("Naozaj cheš odstrániť zariadenie " + zariadenieComboBox.SelectedItem.ToString() + " ?", "Odstrániť zariadenie", MessageBoxButtons.YesNo); //dialogove okno s potvrdzujucou moznostou
                if (dialogResult == DialogResult.Yes) //ak je vybrane ano
                {
                    client.Delete("Zariadenie/" + zariadenieComboBox.SelectedItem.ToString()); //zariadenie pod konkretnym indexom je vymazane
                    MessageBox.Show("Zariadenie bolo vymazane"); //informativny zpis
                }
                zariadenieComboBox.SelectedIndex = 0; //vyberie sa prvy prvok vo vybere
                obnovDatabazu();    //obnovia sa udaje atabaze
            }
        }

        private void obnovButton_Click(object sender, EventArgs e)
        {
            obnovDatabazu();
            hryBox.SelectedIndex = databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby;
            if (databazaSenzorov[zariadenieComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;
            posledneCheckBox.Checked = databazaSenzorov[zariadenieComboBox.SelectedIndex].Posledne;
        }

        private void stopkyStartButton_Click(object sender, EventArgs e)
        {
            if (stopkyStartButton.Text == "Start")
            {
                bool posledneDev = false;
                foreach (Senzor dev in this.databazaSenzorov){
                    if (!dev.Posledne) posledneDev = !posledneDev;
                }
                if ((menoTextBox.Text != "") && !posledneDev)
                {
                    foreach (Senzor dev in this.databazaSenzorov) {
                        dev.Hotovo = false;
                        dev.Start = true;
                        nastavovac(dev);
                        //if (!dev.Start) vsetkyZap = true;
                    }
                    this.stopky.Start();
                    stopkyStartButton.Text = "Stop";
                }
                else if(posledneDev) MessageBox.Show("Nieje nastavené posledné zariadenie");
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
                postupVHreProgressBar.Value = 0;
                stopAllButton_Click(sender, e); //vypnutie vsetkych zariadeni a tak priprava na nove kolo
            }
        }
        private void stopkyTimer_Tick(object sender, EventArgs e)
        {
            casLabel.Text = this.stopky.Elapsed.ToString(@"hh\:mm\:ss");    //format casu do jLabelu
            if (this.stopky.IsRunning) {    //ak je casovac spusteny
                obnovDatabazu();    //každy impulz sa obnovy databaza
                for (int i = 0; i < this.databazaSenzorov.Length; i++) {    //po obnovení sa prejde zoznam dat
                    if(this.databazaSenzorov[i].Hotovo!=this.oldDatabazaSenzorov[i].Hotovo) postupVHreProgressBar.Increment(1); //ak niektore zaraidenie sa dokonci, zvacsi sa progress bar
                    if (this.databazaSenzorov[i].Posledne == true && this.databazaSenzorov[i].Hotovo == true)   // ak sa dokonci poslnedne zariadenie, zatavi sa casovac
                    {
                        postupVHreProgressBar.Value = postupVHreProgressBar.Maximum;    //nastavi sa progress bar na maximum
                        stopkyStartButton_Click(sender, e); //klikne sa na moznost stop
                    }
                }
            }
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
                DialogResult dialogResult = MessageBox.Show("Prajete si zariadenie '"+ databazaSenzorov[zariadenieComboBox.SelectedIndex].Id  + "' nastaviť ako posledné? ", "Posledne", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < databazaSenzorov.Length; i++)
                    {
                        if (i != zariadenieComboBox.SelectedIndex)
                        {
                            databazaSenzorov[i].Posledne = false;
                            nastavovac(databazaSenzorov[i]);
                        }
                    }
                }
            }
            databazaSenzorov[zariadenieComboBox.SelectedIndex].Posledne = posledneCheckBox.Checked;
            nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]);
        }

        private void rebricekDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            menoTextBox.Text = rebricekDataGridView.SelectedRows[0].Cells[1].Value.ToString();
        }
    }
}
