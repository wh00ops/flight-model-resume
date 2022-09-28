using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class GlocEventEmitter : AbstractEventEmitter<GlocEventCollector, bool>
    {
        protected override void HandleEvent(bool value)
        {
            BroadcastMessage(FlightModelUtility.ON_GLOC_METHOD, value, SendMessageOptions.DontRequireReceiver);
        }
    }
}