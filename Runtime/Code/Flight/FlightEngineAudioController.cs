using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class FlightEngineAudioController : AbstractAudioController
    {
        public GameObject target;

        public AnimationCurve volume = new AnimationCurve();
        public AnimationCurve pitch = new AnimationCurve();

        IForcePower m_flight;
        bool m_isInit;

        protected override void Start()
        {
            OnControlTargetSet(target);
            base.Start();

            m_isInit = true;
            OnEnable();
        }

        private void OnEnable()
        {
            if (m_isInit)
                Play(true);
        }

        private void Update()
        {
            float value = m_flight.forcePower.z;
            m_component.volume = volume.Evaluate(value);
            m_component.pitch = pitch.Evaluate(value);
        }

        private void OnControlTargetBreak()
        {
            OnControlTargetSet(null);
        }

        private void OnControlTargetSet(GameObject target)
        {
            enabled = this.target = target;
            if (this.target)
                m_flight = this.target.GetComponentInChildren<IForcePower>();
        }

        protected override void OnAssetLoaded(Object[] audioClips)
        {
            base.OnAssetLoaded(audioClips);
            if (enabled)
                Play(true);
        }
    }
}