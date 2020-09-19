﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciribob.DCS.SimpleRadio.Standalone.Common.Setting
{
    public enum ServerSettingsKeys
    {
        SERVER_PORT = 0,
        COALITION_AUDIO_SECURITY = 1,
        SPECTATORS_AUDIO_DISABLED = 2,
        CLIENT_EXPORT_ENABLED = 3,
        LOS_ENABLED = 4,
        DISTANCE_ENABLED = 5,
        IRL_RADIO_TX = 6,
        IRL_RADIO_RX_INTERFERENCE = 7,
        IRL_RADIO_STATIC = 8, // Not used
        RADIO_EXPANSION = 9,
        EXTERNAL_AWACS_MODE = 10,
        EXTERNAL_AWACS_MODE_BLUE_PASSWORD = 11,
        EXTERNAL_AWACS_MODE_RED_PASSWORD = 12,
        CLIENT_EXPORT_FILE_PATH = 13,
        CHECK_FOR_BETA_UPDATES = 14,
        ALLOW_RADIO_ENCRYPTION = 15,
        TEST_FREQUENCIES = 16,
        SHOW_TUNED_COUNT = 17,
        GLOBAL_LOBBY_FREQUENCIES = 18,
        SHOW_TRANSMITTER_NAME = 19,
        LOTATC_EXPORT_ENABLED = 20,
        LOTATC_EXPORT_PORT = 21,
        LOTATC_EXPORT_IP = 22,
        UPNP_ENABLED = 23
    }

    public class DefaultServerSettings
    {
        public static readonly Dictionary<string, string> Defaults = new Dictionary<string, string>()
        {
            { ServerSettingsKeys.CLIENT_EXPORT_ENABLED.ToString(), "false" },
            { ServerSettingsKeys.COALITION_AUDIO_SECURITY.ToString(), "false" },
            { ServerSettingsKeys.DISTANCE_ENABLED.ToString(), "false" },
            { ServerSettingsKeys.EXTERNAL_AWACS_MODE.ToString(), "true" },
            { ServerSettingsKeys.EXTERNAL_AWACS_MODE_BLUE_PASSWORD.ToString(), "" },
            { ServerSettingsKeys.EXTERNAL_AWACS_MODE_RED_PASSWORD.ToString(), "" },
            { ServerSettingsKeys.IRL_RADIO_RX_INTERFERENCE.ToString(), "false" },
            { ServerSettingsKeys.IRL_RADIO_STATIC.ToString(), "false" },
            { ServerSettingsKeys.IRL_RADIO_TX.ToString(), "false" },
            { ServerSettingsKeys.LOS_ENABLED.ToString(), "false" },
            { ServerSettingsKeys.RADIO_EXPANSION.ToString(), "false" },
            { ServerSettingsKeys.SERVER_PORT.ToString(), "5002" },
            { ServerSettingsKeys.SPECTATORS_AUDIO_DISABLED.ToString(), "false" },
            { ServerSettingsKeys.CLIENT_EXPORT_FILE_PATH.ToString(), "clients-list.json" },
            { ServerSettingsKeys.CHECK_FOR_BETA_UPDATES.ToString(), "false" },
            { ServerSettingsKeys.ALLOW_RADIO_ENCRYPTION.ToString(), "true" },
            { ServerSettingsKeys.TEST_FREQUENCIES.ToString(), "247.2,120.3" },
            { ServerSettingsKeys.SHOW_TUNED_COUNT.ToString(), "true" },
            { ServerSettingsKeys.GLOBAL_LOBBY_FREQUENCIES.ToString(), "248.22" },
            { ServerSettingsKeys.LOTATC_EXPORT_ENABLED.ToString(), "false" },
            { ServerSettingsKeys.LOTATC_EXPORT_PORT.ToString(), "10712" },
            { ServerSettingsKeys.LOTATC_EXPORT_IP.ToString(), "127.0.0.1" },
            { ServerSettingsKeys.UPNP_ENABLED.ToString(), "true" },
        };
    }
}
