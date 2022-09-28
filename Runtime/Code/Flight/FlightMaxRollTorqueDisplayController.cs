using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class FlightMaxRollTorqueDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        Flight m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.maxTorque.z;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<Flight>();
        }
    }
}