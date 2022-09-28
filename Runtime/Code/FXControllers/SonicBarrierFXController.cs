using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class SonicBarrierFXController : MonoBehaviour
    {
        public GameObject fx;
        public AnimationCurve intensity = new AnimationCurve();
        public AnimationCurve shift = new AnimationCurve();

        Rigidbody m_rigidbody;
        ParticleSystem m_particles;
        float m_initLifetime;
        bool m_isPlaying;

        Transform m_barrierTranform;

        private void Start()
        {
            m_rigidbody = transform.GetComponentInParent<Rigidbody>();
            OnAssetInstantiated(fx);
        }

        private void OnReset()
        {
            m_isPlaying = false;
        }

        private void OnAssetInstantiated(GameObject asset)
        {
            enabled = m_barrierTranform = (fx = asset).transform;

            if (fx)
            {
                m_particles = fx.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule main = m_particles.main;
                main.stopAction = ParticleSystemStopAction.None;
                m_initLifetime = main.startLifetime.constant;
                m_particles.Stop();
            }
        }

        private void LateUpdate()
        {
            float speed = m_rigidbody.velocity.magnitude;

            float intensity = this.intensity.Evaluate(speed);
            float shift = this.shift.Evaluate(speed);

            bool play = intensity > 0 && !m_isPlaying;
            bool stop = intensity == 0 && m_isPlaying;

            if (play)
                Play();
            else if (stop)
                Stop();

            SetLifetime(m_particles, m_initLifetime * intensity);
            SetAlpha(m_particles, intensity);
            SetShift(m_barrierTranform, shift);
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

        private void SetAlpha(ParticleSystem barrier, float alpha)
        {
            ParticleSystem.MainModule main;
            ParticleSystem.MinMaxGradient minMax;

            main = barrier.main;
            minMax = main.startColor;

            Color minColor = minMax.colorMin;
            Color maxColor = minMax.colorMax;

            minColor.a = alpha;
            maxColor.a = alpha;

            minMax.colorMin = minColor;
            minMax.colorMax = maxColor;

            main.startColor = minMax;
        }

        private void SetLifetime(ParticleSystem barrier, float lifeTime)
        {
            ParticleSystem.MainModule main = barrier.main;
            ParticleSystem.MinMaxCurve minMax;

            main = barrier.main;
            minMax = main.startLifetime;

            minMax.constant = lifeTime;

            main.startLifetime = minMax;
        }

        private void SetShift(Transform barrier, float shift)
        {
            barrier.localPosition = Vector3.forward * shift;
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