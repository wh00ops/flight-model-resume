using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class AfterburnerAudioController : AbstractAudioController
    {
        private void OnAfterburner(bool activation)
        {
            if (activation)
                Play();
        }
    }
}