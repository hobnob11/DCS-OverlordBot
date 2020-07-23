using Ciribob.DCS.SimpleRadio.Standalone.Client.UI;
using System.Collections.Generic;

namespace Ciribob.DCS.SimpleRadio.Standalone.Client.Preferences
{
    public interface IFavouriteServerStore
    {
        IEnumerable<ServerAddress> LoadFromStore();

        bool SaveToStore(IEnumerable<ServerAddress> addresses);
    }
}