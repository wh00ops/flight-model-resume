using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverGThresholdDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        IThreshold m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.threshold;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<IThreshold>();
        }
    }
}