namespace FreakGames.Unity.Core.FlightModel
{
    public class GlocEventCollector : AbstractEventCollector<bool>
    {
        void OnGloc(bool value)
        {
            OnEvent(value);
        }
    }
}