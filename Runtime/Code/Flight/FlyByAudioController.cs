using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class FlyByAudioController : AbstractAudioController
    {
        void OnTriggerEnter(Collider other)
        {
            Play();
        }
    }
}