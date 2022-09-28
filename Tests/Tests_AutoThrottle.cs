using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class Tests_AutoThrottle
    {
        AutoThrottle autoThrottle;
        Heir_AutoThrottle heir_AutoThrottle;
        Flight flight;

        [SetUp]
        public void SetUp()
        {
            flight = new GameObject("auto-throttle").AddComponent<Flight>();
            autoThrottle = flight.gameObject.AddComponent<AutoThrottle>();
            autoThrottle.gameObject.AddComponent<Rigidbody>();
            heir_AutoThrottle = autoThrottle.gameObject.AddComponent<Heir_AutoThrottle>();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(autoThrottle.gameObject);
        }

        [UnityTest]
        public IEnumerator Reset()
        {
            autoThrottle.enabled = false;
            autoThrottle.SendMessage("OnReset");

            yield return null;

            Assert.IsTrue(autoThrottle.enabled);
        }

        [UnityTest]
        public IEnumerator OnReactionWithTrue()
        {
            autoThrottle.SendMessage("OnReaction", true);

            yield return null;

            Assert.AreEqual(autoThrottle.highThrottle, flight.throttle);
        }

        [UnityTest]
        public IEnumerator OnReactionWithFalse()
        {
            autoThrottle.SendMessage("OnReaction", false);

            yield return null;

            Assert.AreEqual(autoThrottle.lowThrottle, flight.throttle);
        }

        [UnityTest]
        public IEnumerator Ready_True()
        {
            autoThrottle.SendMessage("OnAlignment", true);

            Assert.IsTrue(heir_AutoThrottle.Ready);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Ready_False()
        {
            autoThrottle.SendMessage("OnAlignment", false);

            Assert.IsFalse(heir_AutoThrottle.Ready);

            yield return null;
        }
    }
}
