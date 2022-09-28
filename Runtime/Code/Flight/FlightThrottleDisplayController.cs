using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class FlightThrottleDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        Flight m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.throttle;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<Flight>();
        }
    }
}