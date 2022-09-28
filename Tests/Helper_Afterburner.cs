using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class Helper_Afterburner : MonoBehaviour, IThrottle
    {
        public float throttle { get; set; }
        public float afterburnerCallsCount;
        public bool activation;

        private void OnAfterburner(bool activation)
        {
            afterburnerCallsCount++;
            this.activation = activation;
        }
    }
}
