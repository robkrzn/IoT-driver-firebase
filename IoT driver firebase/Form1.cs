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
        //pomocne premenne
        private IFirebaseClient client;
        private IFirebaseConfig config;
        private FirebaseResponse opoved;
        private Senzor[] databazaSenzorov;
        private Senzor[] oldDatabazaSenzorov;
        private Dictionary<string, Senzor> zoznamSenzorov;
        private Dictionary<string, string> rebricekDic;
        private Stopwatch stopky;
        private int onlinePocet;
        public Form1()
        {
            InitializeComponent();  //inicializacia komponentov
            this.config = new FirebaseConfig { AuthSecret = "gvsLlFof5OhtvW9SEFnpdLYzI6lVgJujVxD7HIHc", BasePath = "https://unikova-hra-default-rtdb.firebaseio.com/" };    //nastavenie udajov od databazy
            this.client = new FireSharp.FirebaseClient(config); //konfiguracia pripojenia databazy
            if (this.client == null) {  //overenie pripojenia
                MessageBox.Show("Chyba pripojenia");
            }
            this.stopky = new Stopwatch();  //vytvorenie stopiek
            
            //naplnenie comboboxu pre vyber hier
            hryBox.Items.Add("Svetelna brana");
            hryBox.Items.Add("Morseovka");
            hryBox.Items.Add("LED Hra");
            hryBox.Items.Add("Miesaj farby");
            hryBox.Items.Add("Tlieskaj");
            hryBox.Items.Add("Dotyk");
            hryBox.Items.Add("Vzdialenosť");
            hryBox.Items.Add("Voda");
            
            obnovDatabazu();
            obnovPocetOnline(true);

            zariadenieComboBox.SelectedIndex = 0;   //nastavenie vyberu po spusteni
            hryBox.SelectedIndex = databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby;

            nacitajRebricek();  
        }
        //metoda pre nacitanie rebricka
        private void nacitajRebricek() {
            rebricekDataGridView.Rows.Clear();  //vymazanie aktualneho rebricka
            this.opoved = client.Get("Rebricek");   // ziskanie dat pod prvkom "Rebricek"
            this.rebricekDic= opoved.ResultAs<Dictionary<string, string>>(); //formatovanie JSON dat do slovnika
            if (rebricekDic != null)
            {
                for (int i = 0; i < this.rebricekDic.Count; i++)
                {  //naplnenie rebricka aktualnmi datami
                    rebricekDataGridView.Rows.Add(null, this.rebricekDic.ElementAt(i).Key, this.rebricekDic.ElementAt(i).Value);
                }
            }
            this.rebricekDataGridView.Sort(casStlpec, ListSortDirection.Ascending); //zoradenie rebricka podla casu
            for (int i = 0; i < this.rebricekDataGridView.RowCount - 1; i++) {  
                this.rebricekDataGridView.Rows[i].Cells[0].Value = i + 1;  //doplnenie stplca k prvom v rebricku
                if (i == 0) this.rebricekDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;    //farebne oznacenie prvych 3 miest
                if (i == 1 || i == 2) this.rebricekDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;    
            }

        }
        //metoda pre nacitanie databazy so senzormi 
        private void obnovDatabazu() {
            this.onlinePocet = 0;
            this.oldDatabazaSenzorov = this.databazaSenzorov; //aktualna databaza sa ulozi do pomocnej premmennej
            this.opoved = client.Get("Zariadenie"); //ziskanie dat pod prvkom "Zariadenie"
            this.zoznamSenzorov = opoved.ResultAs<Dictionary<string, Senzor>>();  //formatovanie JSON dat do slovnika
            this.databazaSenzorov = zoznamSenzorov.Values.ToArray();    //prepis slovníka na pole
            int index = zariadenieComboBox.SelectedIndex;   //ulozenie aktualneho vybraneho zariadenia
            zariadenieComboBox.Items.Clear();   //vycistenie moznosti pre vyber zariadenia
            foreach (Senzor dev in this.databazaSenzorov) { //prejdenie pola s udajmi
                if (dev.Volby > hryBox.Items.Count)dev.Volby = 0;   //ak je nahodou niekde väcsi udaj nastav 0
                zariadenieComboBox.Items.Add(dev.Id);   //pridanie zariadeni do vyberu zariadenia
                if (dev.Online) this.onlinePocet++;
            }
            onlineLabel.Text = onlinePocet + " je online";
            try
            {
                zariadenieComboBox.SelectedIndex = index;   //ak sa da nastav pôvodny index
            }
            catch { }
            postupVHreProgressBar.Maximum = databazaSenzorov.Length;    //podla poctu zariadeni nastav zobrazovac postupu
            //hryBox.SelectedIndex = databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby;
        }
        //metoda pre obsluhu tlacidla pre start hry
        private void startGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                databazaSenzorov[zariadenieComboBox.SelectedIndex].Start = true;//ulozenie startu hry k prislusnemu zariadeniu
                nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]); //odosle sa spustenie do databazy
                ledOvalShape.BackColor = Color.Green;   //nastavenie zelenej farby na diodu
            }
            catch {
                MessageBox.Show("Nevybral si žiadne zariadenie");   //info vypis ak nieje vybrane zariadenie
            }
        }
        //metoda pre obsluhu tlacidla pre zastavenie hry
        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                databazaSenzorov[zariadenieComboBox.SelectedIndex].Start = false;   //ulozenie startu hry k prislusnemu zariadeniu
                nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]); //odosle sa spustenie do databazy
                ledOvalShape.BackColor = Color.Red; //nastavenie cervenej farby na diodu
            }
            catch {
                MessageBox.Show("Nevybral si žiadne zariadenie");   //info vypis ak nieje vybrane zariadenie
            }
        }
        //metoda pre osluhu tlacidla pre spustenie vsetkych zariadeni
        private void startAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < databazaSenzorov.Length; i++)   //cyklus prejde vsetky zariadenia
            {
                databazaSenzorov[i].Start = true;   //na vsetkych zariadeniach sa nastavi start
                nastavovac(databazaSenzorov[i]);    //prislusne nastavenie sa odosle do databaze
            }
            ledOvalShape.BackColor = Color.Green;   //nastavi sa farba diody
        }
        //metoda pre osluhu tlacidla pre zastavenie vsetkych zariadeni
        private void stopAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < databazaSenzorov.Length; i++)//cyklus prejde vsetky zariadenia
            {
                databazaSenzorov[i].Start = false;  //na vsetkych zariadeniach sa nastavi stop
                nastavovac(databazaSenzorov[i]);     //prislusne nastavenie sa odosle do databaze
            }
            ledOvalShape.BackColor = Color.Red; //nastavi sa farba diody
        }
        //metoda ktora sa vykona pri zmene vyberu v ponuke s hrami
        private void hryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby = hryBox.SelectedIndex;    //k vybranemu zariadeniu sa nastavi vybrana hra
            nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]); //nastavenie sa odosle do databazy
        }
        //metoda ktora sa vykona pri mene vo vybere aktualneho zariadenia 
        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)    
        {
            hryBox.SelectedIndex = databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby;    //podla nacitanych dat sa nastavia volby a farba diody
            LEDcomboBox.SelectedIndex = databazaSenzorov[zariadenieComboBox.SelectedIndex].LED;
            if (databazaSenzorov[zariadenieComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;
            posledneCheckBox.Checked = databazaSenzorov[zariadenieComboBox.SelectedIndex].Posledne;
            if (databazaSenzorov[zariadenieComboBox.SelectedIndex].Online == true) onlineOvalShape.BackColor = Color.Green;
            else onlineOvalShape.BackColor = Color.Red;
        }
        //metoda ktora sa vykona po kliknuti na combobox so zariadeniami
        private void deviceComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            obnovDatabazu();
        }
        //metoda ktora sa vykona pri zmene vyberu farby led diody
        private void LEDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            databazaSenzorov[zariadenieComboBox.SelectedIndex].LED = LEDcomboBox.SelectedIndex; //vybrana farba sa ulozi
            nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]); //zmena sa ulozi do databazy 
        }
        //metoda pre nacitanie jedneho konretneho zariadenia
        Senzor nacitajZariadenie(string id) {
            this.opoved = client.Get("Zariadenie/" + id);
            return this.opoved.ResultAs<Senzor>();
        }
        //metoda pre nastavenie dat konkretneho zariadenia
        private void nastavovac(Senzor dev) {
            client.Set("Zariadenie/" + dev.Id, dev);    //nastav k zariadeniu pod id jeho vsetky udaje
        }
        //metoda pre obsluhu tlacidla na vymazanie zariadenia zo zoznamu
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
        //metoda pre obnovenie poctu zariadeni ktore su online, parameter full sluzi aj pre nasledne obnovenie databazy
        void obnovPocetOnline(bool full) {
            foreach (Senzor dev in this.databazaSenzorov)   //cyklus prejde vsetky zariadenia
            {
                dev.Online = false; //na vsetkych zariadeniach sa nastavi ze su offline
                nastavovac(dev);    // zmena sa zapise do databazy
            }
            if (full)obnovDatabazu();   //po obnoveni databazy sa zisti ktore zariadenia zmenili stav offline na online 
        }
        //metoda pre obsuhu tlacidla pre obnovenie udajov v programe
        private void obnovButton_Click(object sender, EventArgs e)
        {
            obnovPocetOnline(false);    //obnovenie poctu zariadeni online bez onovenia databazy
            obnovDatabazu();    //samostatne obnovenie databazy
            hryBox.SelectedIndex = databazaSenzorov[zariadenieComboBox.SelectedIndex].Volby;    //nastavenie prvkov podla nacitanych dat z databazy
            if (databazaSenzorov[zariadenieComboBox.SelectedIndex].Start == true) ledOvalShape.BackColor = Color.Green;
            else ledOvalShape.BackColor = Color.Red;
            posledneCheckBox.Checked = databazaSenzorov[zariadenieComboBox.SelectedIndex].Posledne;
            if (databazaSenzorov[zariadenieComboBox.SelectedIndex].Online == true) onlineOvalShape.BackColor = Color.Green;
            else onlineOvalShape.BackColor = Color.Red;
        }
        //metoda pre obsluhu tlacidla pre spustenia a zastevenia casomiery
        private void stopkyStartButton_Click(object sender, EventArgs e)
        {
            if (stopkyStartButton.Text == "Start")  //ak je tlacidlo momentalne ako start
            {
                bool posledneDev = true;   //zistenie ci je nastavene posledne zariadenie pomocou cyklu
                foreach (Senzor dev in this.databazaSenzorov){
                    if (!dev.Posledne) posledneDev = !posledneDev;
                }
                if ((menoTextBox.Text != "") && !posledneDev)   //zistenie ci je vyplnene meno hraca
                {
                    foreach (Senzor dev in this.databazaSenzorov) { //cyklus prejde vsetky zariadenia
                        dev.Hotovo = false; //na vsetkych zariadeniach sa nastavy pociatocny stav
                        dev.Start = true;
                        nastavovac(dev);    //zmeny sa zapisu do databazy
                    }
                    this.stopky.Start();    //spustia sa meranie casu
                    stopkyStartButton.Text = "Stop";    //tlacidlo sa zmeni na stop
                }
                else if(posledneDev) MessageBox.Show("Nieje nastavené posledné zariadenie");    //pri chybe infovypis
                else MessageBox.Show("Zabudol si zadať meno!");
            }
            else {  //ak je tlacidlo nastavene ako stop
                this.stopky.Stop(); //zastavi sa casovac 
                bool najdeny = false;   //hladanie ci hrac dosiahol lepsi cas mal ulozeny v databaze
                for (int i = 0; i < rebricekDataGridView.Rows.Count-1; i++) {
                    if (rebricekDataGridView.Rows[i].Cells[1].Value.ToString() == menoTextBox.Text) {   //ak je dane meno uz v rebricku zapisane
                        DateTime stary = DateTime.ParseExact(rebricekDataGridView.Rows[i].Cells[2].Value.ToString(),"hh:mm:ss",CultureInfo.InvariantCulture);   //naformatuje sa stary cas
                        DateTime aktualy = DateTime.ParseExact(this.stopky.Elapsed.ToString(@"hh\:mm\:ss"), "hh:mm:ss", CultureInfo.InvariantCulture);  //naformatuje sa novy cas
                        najdeny = true; 
                        if (TimeSpan.Compare(stary.TimeOfDay, aktualy.TimeOfDay)== 1) najdeny = false;  //ak sa zisti ze predchadazajuci cas bol lepsi neprepise sa
                    }
                }
                if (!najdeny) //ak hrac nemal udaj v rebricku
                    client.Set("Rebricek/" + menoTextBox.Text, this.stopky.Elapsed.ToString(@"hh\:mm\:ss"));    //zapise sa udaj do databazy
                MessageBox.Show(menoTextBox.Text + " mal/a čas " + this.stopky.Elapsed.ToString(@"hh\:mm\:ss"));    //vypise sa info okno s prislusnym casom
                nacitajRebricek();  //obnovi sa nacitany rebricek
                this.stopky.Reset();    //stopky sa vynuluju
                menoTextBox.Text = "";  //vynuluje sa nastavene meno
                stopkyStartButton.Text = "Start";   //tlacidlo sa zmeni na start
                postupVHreProgressBar.Value = 0;    //progress bar sa vynuluje
                stopAllButton_Click(sender, e); //vypnutie vsetkych zariadeni a tak priprava na nove kolo
            }
        }
        //metoda pre obsluhu vlakna Timer
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
        //metoda pre obsluhu tlacidla pre vymazanie udaju o case z rebricka
        private void deleteCasbutton_Click(object sender, EventArgs e)
        {
            //dialogove okno ktore sa opyta ci uvivatel svoju volbu mysli vazne
            DialogResult dialogResult = MessageBox.Show("Naozaj cheš odstrániť záznam " + this.rebricekDataGridView.SelectedRows[0].Cells[1].Value.ToString()+" - "+this.rebricekDataGridView.SelectedRows[0].Cells[2].Value.ToString() + " ?", "Odstrániť zariadenie", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)   //ak ano
            {
                client.Delete("Rebricek/" + this.rebricekDataGridView.SelectedRows[0].Cells[1].Value.ToString());   //vymaze sa zaznam z rebricka
                MessageBox.Show("Záznam bol odstránený");   //info vypis
                nacitajRebricek();  //onovenie rebricka
            }
        }
        //metoda pre zakliknutie moznosti posledneho zariadenia
        private void posledneCheckBox_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (posledneCheckBox.Checked == true) //ak je zakliknute posledne zariadenie
            {
                //zobrazi sa okno ci uzivatel svoje nastavenie mysli vazne
                DialogResult dialogResult = MessageBox.Show("Prajete si zariadenie '"+ databazaSenzorov[zariadenieComboBox.SelectedIndex].Id  + "' nastaviť ako posledné? ", "Posledne", MessageBoxButtons.YesNo);
                //ak ano tak sa na vstekch ostatnych zariadeniach nastavi ze niesu posledne 
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
            databazaSenzorov[zariadenieComboBox.SelectedIndex].Posledne = posledneCheckBox.Checked; //nastavenie sa ulozi do databaze
            nastavovac(databazaSenzorov[zariadenieComboBox.SelectedIndex]);
        }
        //metoda ktora po dvojkliku na riadok z rebricka nastavi meno z riadku na meno hraca
        private void rebricekDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            menoTextBox.Text = rebricekDataGridView.SelectedRows[0].Cells[1].Value.ToString();
        }
        //metoda pre obsluhu zakliknutia na indikator o tom ci je zariadenie online, pre zistenie aktualneho stavu
        private void onlineOvalShape_Click(object sender, EventArgs e)
        {
            onlineOvalShape.BackColor = Color.Blue; //nastavi sa modra farba identifikatora
            Senzor dev = databazaSenzorov[zariadenieComboBox.SelectedIndex]; //vytvori sa pomocny udaj
            dev.Online = false; //zariadenie sa nastavi ako offline
            nastavovac(dev);
            System.Threading.Thread.Sleep(500); //delay
            obnovDatabazu();    // onovenia databazy
            foreach (Senzor zar in this.databazaSenzorov)   //cyklus na prejdenie vsetkych nacitanych zariadeni
            {
                if (zar.Id == dev.Id)   //najde sa vybrane zariadenie
                {
                    if (databazaSenzorov[zariadenieComboBox.SelectedIndex].Online == true) onlineOvalShape.BackColor = Color.Green; //ak je online nastavi sa zeleny identifikator
                    else onlineOvalShape.BackColor = Color.Red; //ak je zariadenie offline nastavi sa cerveny identifikator 
                }
            }
        }

        
    }
}
