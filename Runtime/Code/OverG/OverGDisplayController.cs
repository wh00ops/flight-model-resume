using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverGDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        IOverG m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.overG;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<IOverG>();
        }
    }
}