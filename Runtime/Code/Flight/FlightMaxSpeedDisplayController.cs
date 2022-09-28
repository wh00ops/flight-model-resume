using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class FlightMaxSpeedDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        Flight m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.maxSpeed;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<Flight>();
        }
    }
}