using NLog;
using RurouniJones.DCS.Airfields;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Ciribob.DCS.SimpleRadio.Standalone.Client.Overlord.GameState
{
    class AirfieldUpdater
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private RurouniJones.CinC.ClientLib.Client _client;

        public string HostName { get; set; }
        public int Port { get; set; }

        public bool IsConnected
        {
            get
            {
                return (_client != null && _client.IsConnected);
            }
        }

        #region Singleton Code
        private static volatile AirfieldUpdater _instance;
        private static object _lock = new object();

        public static AirfieldUpdater Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new AirfieldUpdater();
                    }
                }

                return _instance;
            }
        }

        private AirfieldUpdater() {}
        #endregion

        public void UpdateAirfields(object sender, EventArgs e)
        {
            if (_client == null)
            {
                Logger.Debug($"Connecting to CinC");
                _client = new RurouniJones.CinC.ClientLib.Client(HostName, Port);
            }
            Logger.Debug($"Updating Airfields");
            foreach (RurouniJones.CinC.ClientLib.Models.Airfield cincAirfield in _client.GetAirfields())
            {
                try
                {
                    var airfield = Populator.Airfields.FirstOrDefault(x => x.Name == cincAirfield.Name);
                    if(airfield != null)
                    {
                        airfield.WindHeading = cincAirfield.WindHeading;
                        airfield.WindSpeed = cincAirfield.WindSpeed;
                        airfield.Coalition = cincAirfield.Coalition;
                        Logger.Debug($"Updated {airfield.Name}");
                    }
                } catch (Exception ex)
                {
                    Logger.Error(ex, "Could not update airfield attributes");
                }
            }
        }
    }
}
