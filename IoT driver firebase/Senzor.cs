using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_driver_firebase
{
    class Senzor
    {
        public string Id { get; set; }      //udaj o ID zariadenia - jeho IP adresa
        
        public int Volby { get; set; }      //udaj o nastavenej hre

        public bool Start { get; set; }     //udaj o spusteni hry

        public bool Hotovo { get; set; }    //udaj o dokonceni hry

        public bool Posledne { get; set; }  //udaj o tom ci je zariadenie posledne

        public bool Online { get; set; }    //udaj o tom ci je zariadenie online

        public int LED { get; set; }        //udaj o nastavenej identifikacnej led diode

    }
}
