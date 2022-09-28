using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class AfterburnerEventEmitter : AbstractEventEmitter<AfterburnerEventCollector, bool>
    {
        protected override void HandleEvent(bool value)
        {
            BroadcastMessage(FlightModelUtility.ON_AFTERBURNER_METHOD, value, SendMessageOptions.DontRequireReceiver);
        }
    }
}