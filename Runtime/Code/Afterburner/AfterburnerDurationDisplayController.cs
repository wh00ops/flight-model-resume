using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class AfterburnerDurationDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        Afterburner m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.duration;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<Afterburner>();
        }
    }
}