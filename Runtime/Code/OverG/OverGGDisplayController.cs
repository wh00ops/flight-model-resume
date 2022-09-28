using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverGGDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        IG m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.g;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<IG>();
        }
    }
}