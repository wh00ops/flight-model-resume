using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class AfterburnerCooldownDisplayController : AbstractTextMeshProNumericalDisplayController
    {
        Afterburner m_valueProvider;

        protected override float GetNumericalDisplayValue()
        {
            return m_valueProvider.cooldown;
        }

        protected override void OnInitializeValueProvider(Object target)
        {
            m_valueProvider = (target as GameObject).GetComponent<Afterburner>();
        }
    }
}