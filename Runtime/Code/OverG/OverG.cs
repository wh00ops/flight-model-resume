using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverG : MonoBehaviour, IThreshold, IStamina, IG, IOverG, IGlocValue, IGloc
    {
        public float threshold = 9f;
        public float stamina = 3f;
        public float g;
        public float overG;
        public float glocValue;
        public bool gloc;

        Transform m_transform;
        Rigidbody m_rigidbody;
        float m_overloadValue;

        float IThreshold.threshold { get => threshold; }
        float IStamina.stamina { get => stamina; }
        float IG.g { get => g; }
        float IOverG.overG { get => overG; }
        float IGlocValue.glocValue { get => glocValue; }
        bool IGloc.gloc { get => gloc; }

        private void Awake()
        {
            m_transform = transform;
        }

        private void Start()
        {
            m_rigidbody = GetComponentInParent<Rigidbody>();
        }

        private void OnReset()
        {
            g = 0f;
            m_overloadValue = 0f;
            gloc = false;
        }

        private void OnDamage(object[] damage)
        {
            float relative = stamina - m_overloadValue;
            if (relative > 0f)
                m_overloadValue += relative * ((float)(int)damage[3] * 0.1f);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            Vector3 localVelocity = m_transform.InverseTransformDirection(m_rigidbody.velocity);

            g = 1f - (Mathf.Abs(localVelocity.y) / Physics.gravity.y);

            overG = g - threshold;
            m_overloadValue += overG * deltaTime;
            m_overloadValue = Mathf.Clamp(m_overloadValue, 0f, float.MaxValue);

            glocValue = m_overloadValue / stamina;

            if (gloc)
            {
                if (glocValue <= 0f)
                {
                    gloc = false;
                    BroadcastMessage(FlightModelUtility.ON_GLOC_METHOD, gloc, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                if (glocValue >= 1f)
                {
                    gloc = true;
                    BroadcastMessage(FlightModelUtility.ON_GLOC_METHOD, gloc, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}