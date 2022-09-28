using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class OverGStaminaDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        IStamina m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.stamina;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<IStamina>();
        }
    }
}