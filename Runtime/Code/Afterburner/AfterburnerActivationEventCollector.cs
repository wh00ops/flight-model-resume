namespace FreakGames.Unity.Core.FlightModel
{
    public class AfterburnerActivationEventCollector : AbstractEventCollector<bool>
    {
        void OnAfterburnerActivation(bool value)
        {
            OnEvent(value);
        }
    }
}