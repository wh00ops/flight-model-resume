using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class FlightMaxYawTorqueDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        Flight m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.maxTorque.y;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<Flight>();
        }
    }
}