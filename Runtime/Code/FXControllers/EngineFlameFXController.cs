using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class EngineFlameFXController : MonoBehaviour
    {
        public GameObject fx;
        public AnimationCurve intensity = new AnimationCurve();

        IForcePower m_force;
        Transform m_flameTransform;

        ParticleSystem m_particles;

        private void Awake()
        {
            enabled = false;
        }

        private void Start()
        {
            m_force = transform.root.GetComponentInChildren<IForcePower>();
            OnAssetInstantiated(fx);
        }

        private void OnAssetInstantiated(GameObject asset)
        {
            enabled = fx = asset;

            if (fx)
            {
                m_flameTransform = fx.transform;
                m_particles = fx.GetComponent<ParticleSystem>();
                m_particles.Play();
            }
        }

        private void LateUpdate()
        {
            float power = m_force.forcePower.z;

            SetAlpha(m_particles, this.intensity.Evaluate(power));
            SetScale(m_flameTransform, this.intensity.Evaluate(power));
        }

        private void SetAlpha(ParticleSystem particleSystem, float alpha)
        {
            ParticleSystem.MainModule main;
            ParticleSystem.MinMaxGradient minMax;

            main = particleSystem.main;
            minMax = main.startColor;

            Color minColor = minMax.colorMin;
            Color maxColor = minMax.colorMax;

            minColor.a = alpha;
            maxColor.a = alpha;

            minMax.colorMin = minColor;
            minMax.colorMax = maxColor;

            main.startColor = minMax;
        }

        private void SetScale(Transform transform, float scale)
        {
            Vector3 target = transform.localScale;
            target.z = scale;
            transform.localScale = target;
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