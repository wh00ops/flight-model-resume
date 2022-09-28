using System.Linq;
using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverGNoiseAudioController : AbstractAudioController
    {
        public GameObject target;
        public AnimationCurve volume = new AnimationCurve();

        IGlocValue m_gloc;

        protected override void Start()
        {
            base.Start();
            OnControlTargetSet(target);
        }

        private void OnControlTargetNull()
        {
            OnControlTargetSet(null);
        }

        private void OnControlTargetSet(GameObject target)
        {
            enabled = this.target = target;
            m_gloc = target ? target.GetComponentInChildren<IGlocValue>() : null;
        }

        private void LateUpdate()
        {
            float value = m_gloc.glocValue;

            if (value > 0f)
                if (m_component.isPlaying)
                    m_component.volume = volume.Evaluate(value);
                else if (clips != null)
                    Play();
                else if (value <= 0f && m_component.isPlaying)
                    m_component.Stop();
        }
    }
}