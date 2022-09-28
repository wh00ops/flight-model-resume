using System.Linq;
using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverGRingingAudioController : AbstractAudioController
    {
        public GameObject target;
        public float threshold = 0.9f;

        IGlocValue m_gloc;
        bool m_overg;

        protected override void Start()
        {
            base.Start();
            OnControlTargetSet(target);
        }

        private void OnEnable()
        {
            m_overg = false;
        }

        private void OnControlTargetBreak()
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

            if (m_overg)
            {
                if (value <= threshold)
                {
                    m_overg = false;
                    Play();
                }
            }
            else
            {
                if (value >= threshold)
                    m_overg = true;
            }
        }
    }
}