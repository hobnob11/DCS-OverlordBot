using RurouniJones.CinC.ClientLib;
using RurouniJones.CinC.ClientLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace RurouniJones.CinC.Console
{
    class Program
    {
        static void Main(string[] _)
        {
            var client = new Client();
            List<Airfield> airfields = client.GetAirfields().ToList();

            foreach (Airfield airfield in airfields)
            {
                string airfieldOutput = $"{airfield.Id} : {airfield.Name}\n" +
                    $"\tPosition: {airfield.Latitude} / {airfield.Longitude}, {airfield.Altitude}m\n" +
                    $"\tWind: {airfield.WindHeading} at {airfield.WindSpeed} km/h\n" +
                    $"\tCoalition: {airfield.Coalition}\n" +
                    $"\tCategory: {airfield.Category}\n";

                System.Console.WriteLine(airfieldOutput);
            }
            System.Console.ReadLine();
        }
    }
}
