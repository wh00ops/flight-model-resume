using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class Helper_AutoAfterburner : MonoBehaviour
    {
        public float reactionCallsCount;
        public bool reaction;

        private void OnReaction(bool reaction)
        {
            reactionCallsCount++;
            this.reaction = reaction;
        }
    }
}
