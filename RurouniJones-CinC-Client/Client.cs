
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
        private readonly TcpClient _tcpClient;

        public Client()
        {
            _tcpClient = new TcpClient(_hostname, _port)
            {
                NoDelay = true
            };
        }

        public Client(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;

            _tcpClient = new TcpClient(_hostname, _port)
            {
                NoDelay = true
            };
        }

        public IEnumerable<Airfield> GetAirfields()
        {
            if(_tcpClient.Connected == false)
            {
                _tcpClient.Connect(_hostname, _port);
            }

            StreamWriter streamWriter = new StreamWriter(_tcpClient.GetStream(), Encoding.ASCII);
            StreamReader streamReader = new StreamReader(_tcpClient.GetStream(), Encoding.ASCII);

            Command command = new Command()
            {
                Name = "get_airbases"
            };

            string jsonCommand = JsonConvert.SerializeObject(command);

            streamWriter.WriteLine(jsonCommand);
            streamWriter.Flush();

            string response = streamReader.ReadLine();

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
