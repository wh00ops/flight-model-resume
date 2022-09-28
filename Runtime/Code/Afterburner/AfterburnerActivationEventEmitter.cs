using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class AfterburnerActivationEventEmitter : AbstractEventEmitter<AfterburnerActivationEventCollector, bool>
    {
        protected override void HandleEvent(bool value)
        {
            BroadcastMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, value, SendMessageOptions.DontRequireReceiver);
        }
    }
}