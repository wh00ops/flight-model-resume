using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class Tests_OverG
    {
        OverG overG;
        OverG_Helper helper;
        Rigidbody rigidbody;

        [SetUp]
        public void SetUp()
        {
            overG = new GameObject("over-g", typeof(Rigidbody)).AddComponent<OverG>();
            helper = overG.gameObject.AddComponent<OverG_Helper>();
            rigidbody = overG.gameObject.GetComponent<Rigidbody>();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(overG.gameObject);
        }
        IEnumerator CheckUntil(System.Func<bool> @while, float maxCheckTime = 5f)
        {
            float endTime = Time.time + maxCheckTime;
            while (!@while.Invoke() && Time.time < endTime)
                yield return null;
        }

        [UnityTest]
        public IEnumerator OnReset()
        {
            overG.g = Random.Range(0.1f, 1);
            overG.gloc = true;

            overG.SendMessage("OnReset");

            Assert.AreEqual(overG.g, 0);
            Assert.IsFalse(overG.gloc);

            yield return null;
        }

        [UnityTest]
        public IEnumerator OnGloc()
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 10000, rigidbody.velocity.z);

            yield return CheckUntil(() => { return helper.isOnGlocReceived; });

            Assert.IsTrue(true);
        }
    }
}
