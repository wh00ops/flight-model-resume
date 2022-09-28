using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class CondensationTrailFXController : MonoBehaviour
    {
        public GameObject fx;
        public float altitudeThreshold = 8000;

        Transform m_transform;
        ParticleSystem m_particles;
        bool m_isPlaying;

        private void Awake()
        {
            m_transform = transform;
        }
        
        private void Start()
        {
            OnAssetInstantiated(fx);
        }
        
        private void OnReset()
        {
            m_isPlaying = false;
        }

        private void OnAssetInstantiated(GameObject asset)
        {
            fx = asset;

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
            if (!fx)
                return;

            bool play = m_transform.position.y >= altitudeThreshold && !m_isPlaying;
            bool stop = m_transform.position.y < altitudeThreshold && m_isPlaying;

            if (play)
                Play();
            else if (stop)
                Stop();
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
    }
}