using UnityEngine;
using UnityEngine.Audio;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverGMixerFilterController : MonoBehaviour
    {
        public GameObject target;
        public AudioMixer mixer;
        public string filterLabel;
        public AnimationCurve filter = new AnimationCurve();

        IGlocValue m_gloc;

        private void Start()
        {
            OnControlTargetSet(target);
        }

        private void OnControlTargetBreak()
        {
            OnControlTargetSet(null);
        }

        private void OnControlTargetSet(GameObject target)
        {
            enabled = this.target = target;
            m_gloc = target ? target.GetComponentInChildren<IGlocValue>() : null;
            mixer.SetFloat(filterLabel, 1f);
        }

        private void LateUpdate()
        {
            mixer.SetFloat(filterLabel, filter.Evaluate(m_gloc.glocValue));
        }

        private void OnRuined()
        {
            enabled = false;
        }

        private void OnDisable()
        {
            mixer.SetFloat(filterLabel, filter.Evaluate(0f));
        }
    }
}
