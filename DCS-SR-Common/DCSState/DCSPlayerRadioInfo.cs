﻿using System;
using System.Collections.Generic;
using Ciribob.DCS.SimpleRadio.Standalone.Common.DCSState;
using Ciribob.DCS.SimpleRadio.Standalone.Common.Helpers;
using Newtonsoft.Json;

namespace Ciribob.DCS.SimpleRadio.Standalone.Common
{
    public class DCSPlayerRadioInfo
    {
        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public string name = "";

        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public DCSLatLngPosition latLng = new DCSLatLngPosition();

        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public bool inAircraft = false;

        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public volatile bool ptt = false;

        public List<RadioInformation> radios  = new List<RadioInformation>();
        
        [JsonNetworkIgnoreSerialization]
        public short selected = 0;

        public string unit = "";
        
        public uint unitId;

        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public bool intercomHotMic = false; //if true switch to intercom and transmit

        public Transponder iff = new Transponder();

        [JsonIgnore]
        public readonly static uint UnitIdOffset = 100000001
            ; // this is where non aircraft "Unit" Ids start from for satcom intercom

        [JsonNetworkIgnoreSerialization]
        [JsonDCSIgnoreSerialization]
        public bool simultaneousTransmission = false; // Global toggle enabling simultaneous transmission on multiple radios, activated via the AWACS panel

        [JsonNetworkIgnoreSerialization]
        public SimultaneousTransmissionControl simultaneousTransmissionControl =
            SimultaneousTransmissionControl.EXTERNAL_DCS_CONTROL;

        public enum SimultaneousTransmissionControl
        {
            ENABLED_INTERNAL_SRS_CONTROLS = 1,
            EXTERNAL_DCS_CONTROL = 0,
        }

        [JsonIgnore]
        public long LastUpdate { get; set; }

        public void Reset()
        {
            name = "";
            latLng = new DCSLatLngPosition();
            ptt = false;
            selected = 0;
            unit = "";
            simultaneousTransmission = false;
            simultaneousTransmissionControl = SimultaneousTransmissionControl.EXTERNAL_DCS_CONTROL;
            LastUpdate = 0;
        }

        // override object.Equals
        public override bool Equals(object compare)
        {
            try
            {
                if ((compare == null) || (GetType() != compare.GetType()))
                {
                    return false;
                }

                var compareRadio = compare as DCSPlayerRadioInfo;

                if (!name.Equals(compareRadio.name))
                {
                    return false;
                }
                if (!unit.Equals(compareRadio.unit))
                {
                    return false;
                }

                if (unitId != compareRadio.unitId)
                {
                    return false;
                }

                if (((iff == null) || (compareRadio.iff == null)))
                {
                    return false;
                }
                else
                {
                    //check iff
                    if (!iff.Equals(compareRadio.iff))
                    {
                        return false;
                    }
                }

                for (var i = 0; i < radios.Count; i++)
                {
                    var radio1 = radios[i];
                    var radio2 = compareRadio.radios[i];

                    if (radio1 == null || radio2 == null) continue;
                    if (!radio1.Equals(radio2))
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
          

            return true;
        }
        
        //comparing doubles is risky - check that we're close enough to hear (within 100hz)
        public static bool FreqCloseEnough(double freq1, double freq2)
        {
            var diff = Math.Abs(freq1 - freq2);

            return diff < 500;
        }

        public RadioInformation CanHearTransmission(double frequency,
            RadioInformation.Modulation modulation,
            byte encryptionKey,
            uint sendingUnitId,
            List<int> blockedRadios,
            out RadioReceivingState receivingState,
            out bool decryptable)
        {
            RadioInformation bestMatchingRadio = null;
            RadioReceivingState bestMatchingRadioState = null;
            bool bestMatchingDecryptable = false;

            for (var i = 0; i < radios.Count; i++)
            {
                var receivingRadio = radios[i];

                if (receivingRadio != null)
                {
                    //handle INTERCOM Modulation is 2
                    if ((receivingRadio.modulation == RadioInformation.Modulation.INTERCOM) &&
                        (modulation == RadioInformation.Modulation.INTERCOM))
                    {
                        if ((unitId > 0) && (sendingUnitId > 0)
                            && (unitId == sendingUnitId) )
                        {
                            receivingState = new RadioReceivingState
                            {
                                IsSecondary = false,
                                LastReceviedAt = DateTime.Now.Ticks,
                                ReceivedOn = i
                            };
                            decryptable = true;
                            return receivingRadio;
                        }
                        decryptable = false;
                        receivingState = null;
                        return null;
                    }

                    if (modulation == RadioInformation.Modulation.DISABLED
                        || receivingRadio.modulation == RadioInformation.Modulation.DISABLED)
                    {
                        continue;
                    }

                    //within 1khz
                    if ((FreqCloseEnough(receivingRadio.freq,frequency))
                        && (receivingRadio.modulation == modulation)
                        && (receivingRadio.freq > 10000))
                    {
                        bool isDecryptable = (encryptionKey == 0 || (receivingRadio.enc ? receivingRadio.encKey : (byte)0) == encryptionKey);

                        if (isDecryptable && !blockedRadios.Contains(i))
                        {
                            receivingState = new RadioReceivingState
                            {
                                IsSecondary = false,
                                LastReceviedAt = DateTime.Now.Ticks,
                                ReceivedOn = i
                            };
                            decryptable = true;
                            return receivingRadio;
                        }

                        bestMatchingRadio = receivingRadio;
                        bestMatchingRadioState = new RadioReceivingState
                        {
                            IsSecondary = false,
                            LastReceviedAt = DateTime.Now.Ticks,
                            ReceivedOn = i
                        };
                        bestMatchingDecryptable = isDecryptable;
                    }
                    if ((receivingRadio.secFreq == frequency)
                        && (receivingRadio.secFreq > 10000))
                    {
                        if (encryptionKey == 0 || (receivingRadio.enc ? receivingRadio.encKey : (byte)0) == encryptionKey)
                        {
                            receivingState = new RadioReceivingState
                            {
                                IsSecondary = true,
                                LastReceviedAt = DateTime.Now.Ticks,
                                ReceivedOn = i
                            };
                            decryptable = true;
                            return receivingRadio;
                        }

                        bestMatchingRadio = receivingRadio;
                        bestMatchingRadioState = new RadioReceivingState
                        {
                            IsSecondary = true,
                            LastReceviedAt = DateTime.Now.Ticks,
                            ReceivedOn = i
                        };
                    }
                }
            }

            decryptable = bestMatchingDecryptable;
            receivingState = bestMatchingRadioState;
            return bestMatchingRadio;
        }

        public bool IsValid()
        {
            foreach (var radio in radios)
            {
                if (radio?.modulation != RadioInformation.Modulation.DISABLED)
                {
                    return true;
                }
            }
            return false;
        }
    }
}