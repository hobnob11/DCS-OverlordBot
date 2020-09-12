﻿using Ciribob.DCS.SimpleRadio.Standalone.Common.Helpers;
using System.Collections.Concurrent;

namespace Ciribob.DCS.SimpleRadio.Standalone.Common
{
    public class RadioInformation
    {
        public enum EncryptionMode
        {
            NO_ENCRYPTION = 0,
            ENCRYPTION_JUST_OVERLAY = 1,
            ENCRYPTION_FULL = 2,
            ENCRYPTION_COCKPIT_TOGGLE_OVERLAY_CODE = 3

            // 0  is no controls
            // 1 is FC3 Gui Toggle + Gui Enc key setting
            // 2 is InCockpit toggle + Incockpit Enc setting
            // 3 is Incockpit toggle + Gui Enc Key setting
        }

        public enum VolumeMode
        {
            COCKPIT = 0,
            OVERLAY = 1,
        }

        public enum FreqMode
        {
            COCKPIT = 0,
            OVERLAY = 1,
        }

        public enum Modulation
        {
            AM = 0,
            FM = 1,
            INTERCOM = 2,
            DISABLED = 3,
            HAVEQUICK = 4, //unsupported currently
            SATCOM = 5, //unsupported currently
            MIDS = 6
        }

        public bool enc = false; // encrytion enabled
        public byte encKey = 0;

        [JsonNetworkIgnoreSerialization]
        public EncryptionMode encMode = EncryptionMode.NO_ENCRYPTION;

        [JsonDCSIgnoreSerialization]
        [JsonNetworkIgnoreSerialization]
        public double freqMax = 1;

        [JsonDCSIgnoreSerialization]
        [JsonNetworkIgnoreSerialization]
        public double freqMin = 1;

        public double freq = 1;
        
        public Modulation modulation = Modulation.DISABLED;

        [JsonNetworkIgnoreSerialization]
        public string name = "";
        
        public double secFreq = 1;

        [JsonNetworkIgnoreSerialization]
        public float volume = 1.0f;

        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public FreqMode freqMode = FreqMode.COCKPIT;
        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public FreqMode guardFreqMode = FreqMode.COCKPIT;
        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public VolumeMode volMode = VolumeMode.COCKPIT;
        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public bool expansion = false;

        [JsonNetworkIgnoreSerialization]
        public int channel = -1;

        [JsonNetworkIgnoreSerialization]
        public bool simul = false;

        [JsonNetworkIgnoreSerialization]
        public string voice = null;

        [JsonNetworkIgnoreSerialization]
        public string botType = null;

        [JsonNetworkIgnoreSerialization]
        public string coalitionPassword = null;

        [JsonNetworkIgnoreSerialization]
        public string callsign = null;

        [JsonNetworkIgnoreSerialization]
        public ConcurrentQueue<byte[]> TransmissionQueue { get; set; }

        [JsonNetworkIgnoreSerialization]
        public ulong discordTransmissionLogChannelId = 0;

        /**
         * Used to determine if we should send an update to the server or not
         * We only need to do that if something that would stop us Receiving happens which
         * is frequencies and modulation
         */

        public override bool Equals(object obj)
        {
            if ((obj == null) || (GetType() != obj.GetType()))
                return false;

            var compare = (RadioInformation) obj;

            if (!name.Equals(compare.name))
            {
                return false;
            }
            if (!DCSPlayerRadioInfo.FreqCloseEnough(freq , compare.freq))
            {
                return false;
            }
            if (modulation != compare.modulation)
            {
                return false;
            }
            if (enc != compare.enc)
            {
                return false;
            }
            if (encKey != compare.encKey)
            {
                return false;
            }
            if (!DCSPlayerRadioInfo.FreqCloseEnough(secFreq, compare.secFreq))
            {
                return false;
            }
            //if (volume != compare.volume)
            //{
            //    return false;
            //}
            //if (freqMin != compare.freqMin)
            //{
            //    return false;
            //}
            //if (freqMax != compare.freqMax)
            //{
            //    return false;
            //}


            return true;
        }
    }
}