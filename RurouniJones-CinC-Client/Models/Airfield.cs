using Newtonsoft.Json;

namespace RurouniJones.CinC.ClientLib.Models
{
    public class Airfield
    {
        /// <summary>
        /// Name of the Airfield.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the Airfield as assigned by DCS in this mission.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Decimal latitude (e.g. 41.12324)
        /// </summary>
        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Decimal Longitude (e.g. 37.12324)
        /// </summary>
        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Altitude in Meters
        /// </summary>
        [JsonProperty(PropertyName = "altitude")]
        public double Altitude { get; set; }

        /// <summary>
        /// Direction the wind is COMING from
        /// </summary>
        [JsonProperty(PropertyName = "wind_heading")]
        public double WindHeading { get; set; }

        /// <summary>
        /// Speed of the wind in m/s
        /// </summary>
        [JsonProperty(PropertyName = "wind_speed")]
        public double WindSpeed { get; set; }

        /// <summary>
        /// Owning coalition ID
        /// </summary>
        [JsonProperty(PropertyName = "coalition")]
        public int Coalition { get; set; }

        /// <summary>
        /// Airfield Category
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public int Category { get; set; }
    }
}
