using Newtonsoft.Json;
using System;

namespace Ciribob.DCS.SimpleRadio.Standalone.Common
{
    public class RadioReceivingState
    {
        [JsonIgnore]
        public long LastReceviedAt { get; set; }

        public bool IsSecondary { get; set; }
        public bool IsSimultaneous { get; set; }
        public int ReceivedOn { get; set; }

        public bool PlayedEndOfTransmission { get; set; }

        public string SentBy { get; set; }

        public bool IsReceiving
        {
            get
            {
                return (DateTime.Now.Ticks - LastReceviedAt) < 3500000;
            }
        }
    }
}