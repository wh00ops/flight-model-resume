namespace FreakGames.Unity.Core.FlightModel
{
    public class AfterburnerEventCollector : AbstractEventCollector<bool>
    {
        void OnAfterburner(bool value)
        {
            OnEvent(value);
        }
    }
}