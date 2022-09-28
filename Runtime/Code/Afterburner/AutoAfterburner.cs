using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class AutoAfterburner : AbstractReaction
    {
        public override bool ready { get => !inRange && inAlignment; }

        public bool inRange { get; private set; }
        public bool inAlignment { get; private set; }

        private void OnReset()
        {
            enabled = true;
            inAlignment = false;
            inRange = false;
        }

        private void OnAlignment(bool inAlignment)
        {
            this.inAlignment = inAlignment;
        }

        private void OnRange(bool inRange)
        {
            this.inRange = inRange;
        }

        protected override void OnReaction(bool reacted)
        {
            BroadcastMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, reacted, SendMessageOptions.DontRequireReceiver);
        }
    }
}