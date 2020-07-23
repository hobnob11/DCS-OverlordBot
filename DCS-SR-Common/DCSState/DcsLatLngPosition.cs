namespace Ciribob.DCS.SimpleRadio.Standalone.Common.DCSState
{
    public class DCSLatLngPosition
    {
        public double lat;
        public double lng;
        public double alt;

        public bool isValid()
        {
            return lat != 0 && lng != 0;
        }

        public override string ToString()
        {
            return $"Pos:[{lat},{lng},{alt}]";
        }
    }
}
