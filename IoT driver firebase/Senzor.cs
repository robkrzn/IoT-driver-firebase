using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_driver_firebase
{
    class Senzor
    {
        public string Id { get; set; }
        
        public int Volby { get; set; }

        public bool Start { get; set; }

        public bool Hotovo { get; set; }

        public bool Posledne { get; set; }

        public bool Online { get; set; }

    }
}
