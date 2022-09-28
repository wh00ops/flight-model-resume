using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class FlowBreakFXController : MonoBehaviour
    {
        public GameObject fx;
        public AnimationCurve intensity = new AnimationCurve();

        Transform m_transform;
        Rigidbody m_rigidbody;
        ParticleSystem m_particles;

        bool m_isPlaying;

        private void Awake()
        {
            enabled = false;
            m_transform = transform;
        }

        private void Start()
        {
            m_rigidbody = GetComponentInParent<Rigidbody>();
            OnAssetInstantiated(fx);
        }

        private void OnReset()
        {
            m_isPlaying = false;
        }

        private void OnAssetInstantiated(GameObject asset)
        {
            enabled = fx = asset;

            if (fx)
            {
                m_particles = fx.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule main = m_particles.main;
                main.stopAction = ParticleSystemStopAction.None;
                m_particles.Stop();
            }
        }

        private void LateUpdate()
        {
            Vector3 localVelocity = m_transform.InverseTransformDirection(m_rigidbody.velocity);
            float intensity = this.intensity.Evaluate(Mathf.Abs(localVelocity.y));

            bool play = intensity > 0 && !m_isPlaying;
            bool stop = intensity == 0 && m_isPlaying;

            if (play)
                Play();
            else if (stop)
                Stop();

            SetAlpha(m_particles, intensity);
        }

        private void Play()
        {
            m_isPlaying = true;
            m_particles.Play();
        }

        private void Stop()
        {
            m_isPlaying = false;
            m_particles.Stop();
        }

        private void SetAlpha(ParticleSystem particles, float alpha)
        {
            ParticleSystem.MainModule main;
            ParticleSystem.MinMaxGradient minMax;

            main = particles.main;
            minMax = main.startColor;

            Color minColor = minMax.colorMin;
            Color maxColor = minMax.colorMax;

            minColor.a = alpha;
            maxColor.a = alpha;

            minMax.colorMin = minColor;
            minMax.colorMax = maxColor;

            main.startColor = minMax;
        }

        private void OnBecameVisible()
        {
            enabled = fx;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }
    }
}