namespace Ciribob.DCS.SimpleRadio.Standalone.Client.UI.ClientWindow
{
    public class AudioDeviceListItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}