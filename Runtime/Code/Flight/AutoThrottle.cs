using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class AutoThrottle : AbstractReaction
    {
        public float lowThrottle = 0.5f;
        public float highThrottle = 1f;

        Transform m_transform;

        bool m_inAlignment;

        public override bool ready { get => m_inAlignment; }

        private void Awake()
        {
            m_transform = transform;
        }

        private void OnReset()
        {
            enabled = true;
            m_inAlignment = false;
        }

        private void OnAlignment(bool inAlignment)
        {
            m_inAlignment = inAlignment;
        }

        protected override void OnReaction(bool reacted)
        {
            BroadcastMessage(FlightModelUtility.ON_THROTTLE_SET_METHOD, reacted ? highThrottle : lowThrottle, SendMessageOptions.DontRequireReceiver);
        }
    }
}