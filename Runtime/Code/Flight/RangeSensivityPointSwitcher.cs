using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class RangeSensivityPointSwitcher : MonoBehaviour
    {   
        public float inRangeSensivityAngle;

        float m_defaultSensivity;
        ISensivityPoint m_sensivityPoint;

        private void Start()
        {
            m_sensivityPoint = GetComponentInParent<ISensivityPoint>();
            m_defaultSensivity = m_sensivityPoint.sensivityPoint;
        }

        private void OnReset()
        {
            m_sensivityPoint.sensivityPoint = m_defaultSensivity;
        }

        private void OnRange(bool inRange)
        {
            m_sensivityPoint.sensivityPoint = inRange ? inRangeSensivityAngle : m_defaultSensivity;
        }
    }
}