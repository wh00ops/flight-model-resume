using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class OverG_Helper : MonoBehaviour
    {
        public bool gloc { get; private set; }
        public bool isOnGlocReceived { get; set; }
        private void OnGloc(bool gloc)
        {
            isOnGlocReceived = true;
            this.gloc = gloc;
        }
    }
}
