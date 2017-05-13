using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTroubleshooter
{
    class ResponseStage
    {
        public string DisplayText { get; private set; }
        public int MinDelay { get; private set; }
        public int MaxDelay { get; private set; }

        public ResponseStage(string text, int min, int max)
        {
            DisplayText = text;
            MinDelay = min;
            MaxDelay = max;
        }
    }
}
