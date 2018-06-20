using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEICHOU_WPA_8._1
{
    public class sender
    {
        public string Id { get; set; }
        public string userName { get; set; }

        public bool isLoaded { get; set; }
        public string particle { get; set; }

        public List<String> sentencesRESULTS = new List<String>();
        public List<String> answersRESULTS = new List<String>();

        public List<String> tempS = new List<String>();
        public List<String> tempA = new List<String>();

        public int resetgame { get; set; }
        public int selectedlevel { get; set; }
    }
}
