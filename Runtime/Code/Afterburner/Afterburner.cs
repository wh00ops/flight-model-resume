using System.Collections;
using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class Afterburner : MonoBehaviour
    {
        public float throttle = 1.2f;
        public float duration = 1f;
        public float cooldown = 1f;

        public bool activation { get; set; }
        public bool restoring { get; set; }

        IThrottle m_throttle;

        private void Start()
        {
            m_throttle = GetComponent<IThrottle>();
            OnReset();
        }

        private void OnReset()
        {
            activation = false;
            restoring = false;
            enabled = false;
            StopAllCoroutines();
        }

        private void OnAfterburnerActivation(bool activation)
        {
            enabled = activation;
        }

        IEnumerator ActivationProcess()
        {
            float endTime = Time.time + duration;

            Activation();

            while (enabled && m_throttle.throttle == throttle && endTime > Time.time)
                yield return null;

            Deactivation();

            restoring = true;
            yield return new WaitForSeconds(cooldown);
            restoring = false;
        }

        private void Activation()
        {
            m_throttle.throttle = throttle;
            activation = true;
            BroadcastMessage(FlightModelUtility.ON_AFTERBURNER_METHOD, activation, SendMessageOptions.DontRequireReceiver);
        }

        private void Deactivation()
        {
            if (m_throttle.throttle == throttle)
                m_throttle.throttle = 1f;
            activation = false;
            BroadcastMessage(FlightModelUtility.ON_AFTERBURNER_METHOD, activation, SendMessageOptions.DontRequireReceiver);
        }

        private void Update()
        {
            if (!activation && !restoring)
                StartCoroutine(ActivationProcess());
        }
    }
}