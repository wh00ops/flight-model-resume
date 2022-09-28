using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class Tests_AutoAfterburner
    {

        // Flight flight;
        // Afterburner afterburner;
        // AutoAfterburner autoAfterburner;
        // Heir_AutoAfterburner heir_AutoAfterburner;

        // [SetUp]
        // public void SetUp()
        // {
        //     flight = new GameObject("flight").AddComponent<Flight>();
        //     flight.gameObject.AddComponent<Rigidbody>();

        //     afterburner = flight.gameObject.AddComponent<Afterburner>();
        //     afterburner.enabled = false;

        //     autoAfterburner = flight.gameObject.AddComponent<AutoAfterburner>();
        //     heir_AutoAfterburner = flight.gameObject.AddComponent<Heir_AutoAfterburner>();
        // }

        // [TearDown]
        // public void TearDown()
        // {
        //     GameObject.Destroy(flight.gameObject);
        // }

        // [UnityTest]
        // public IEnumerator CheckOnResetAutoAfterBurner()
        // {
        //     autoAfterburner.enabled = false;

        //     autoAfterburner.SendMessage("OnReset");

        //     yield return null;

        //     Assert.IsTrue(autoAfterburner.enabled);
        // }

        // [UnityTest]
        // public IEnumerator CheckOnReaction()
        // {
        //     autoAfterburner.SendMessage("OnReaction", true);

        //     yield return null;

        //     Assert.IsTrue(afterburner.enabled);
        // }

        // [UnityTest]
        // public IEnumerator Ready()
        // {
        //     autoAfterburner.SendMessage("OnAlignment", true);
        //     autoAfterburner.SendMessage("OnRange", false);

        //     autoAfterburner.SendMessage("OnReaction", heir_AutoAfterburner.Ready);

        //     Assert.IsTrue(afterburner.enabled);

        //     yield return null;
        // }

        GameObject owner;
        AutoAfterburner component;
        Helper_AutoAfterburner helper;

        [SetUp]
        public void Setup()
        {
            owner = new GameObject(GetType().Name);
            component = owner.AddComponent<AutoAfterburner>();
            helper = owner.AddComponent<Helper_AutoAfterburner>();
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(owner);
        }

        IEnumerator CheckUntil(System.Func<bool> @until, float maxCheckTime = 5f)
        {
            float endTime = Time.time + maxCheckTime;
            while (!@until.Invoke() && Time.time < endTime)
                yield return null;
        }

        [UnityTest]
        public IEnumerator OnReset()
        {
            yield return null;
            component.SendMessage("OnReset");

            Assert.IsTrue(component.enabled);
            Assert.IsFalse(component.inRange);
            Assert.IsFalse(component.inAlignment);
        }

        [UnityTest]
        public IEnumerator OnAlignment()
        {
            yield return null;
            component.SendMessage("OnAlignment", true);
            Assert.IsTrue(component.inAlignment);
        }

        [UnityTest]
        public IEnumerator OnRange()
        {
            yield return null;
            component.SendMessage("OnRange", true);
            Assert.IsTrue(component.inRange);
        }

        [UnityTest]
        public IEnumerator OnReaction()
        {
            yield return null;
            component.SendMessage("OnReaction", true);
            Assert.IsTrue(helper.reaction);
        }
    }
}
