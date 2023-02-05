using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.MVVM.Serializable
{
    internal class Pilot
    {
        public int primary_id { get; private set; }
        public int secondary_id { get; private set; }
        public string name { get; private set; }
        public string addr1 { get; private set; }
        public string addr2 { get; private set; }
        public string airplane { get; private set; }
        public bool missing_pilot_panel { get; private set; }
        public string comments { get; private set; }
        public bool active { get; private set; }
        public bool freestyle { get; private set; }
        public string[] classes { get; private set; }
        public bool spread_spectrum { get; private set; }
        public int frequency { get; private set; }

        public Pilot(int primary_id, int secondary_id, string name, string addr1, string addr2, string airplane, bool missing_pilot_panel, string comments, bool active, bool freestyle, string[] classes, bool spread_spectrum, int frequency)
        {
            this.primary_id = primary_id;
            this.secondary_id = secondary_id;
            this.name = name;
            this.addr1 = addr1;
            this.addr2 = addr2;
            this.airplane = airplane;
            this.missing_pilot_panel = missing_pilot_panel;
            this.comments = comments;
            this.active = active;
            this.freestyle = freestyle;
            this.classes = classes;
            this.spread_spectrum = spread_spectrum;
            this.frequency = frequency;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
