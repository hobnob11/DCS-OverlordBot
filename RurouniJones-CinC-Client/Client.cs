
using Newtonsoft.Json;
using RurouniJones.CinC.ClientLib.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace RurouniJones.CinC.ClientLib
{
    public class Client
    {
        private readonly string _hostname = "localhost";
        private readonly int _port = 9001;
        private readonly TcpClient _tcpClient = new TcpClient();

        public bool IsConnected
        {
            get
            {
                return (_tcpClient != null && _tcpClient.Connected);
            }
        }

        public Client() { }

        public Client(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;
        }

        public IEnumerable<Airfield> GetAirfields()
        {
            if (!_tcpClient.Connected && !_tcpClient.ConnectAsync(_hostname, _port).Wait(10000))
            {
                return new List<Airfield>();
            }

            StreamWriter streamWriter = new StreamWriter(_tcpClient.GetStream(), Encoding.ASCII);
            StreamReader streamReader = new StreamReader(_tcpClient.GetStream(), Encoding.ASCII);

            Command command = new Command()
            {
                Name = "get_airbases"
            };

            string jsonCommand = JsonConvert.SerializeObject(command);
            string response;
            try
            {
                streamWriter.WriteLine(jsonCommand);
                streamWriter.Flush();

                response = streamReader.ReadLine();
            } catch (SocketException)
            {
                _tcpClient.Close();
                return new List<Airfield>();
            }

            return JsonConvert.DeserializeObject<List<Airfield>>(response);
        }

        private class Command
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "arguments")]
            public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();
        }
    }
}
