using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class Tests_Afterburner
    {
        GameObject owner;
        Afterburner component;
        Helper_Afterburner helper;

        [SetUp]
        public void Setup()
        {
            owner = new GameObject(GetType().Name);
            component = owner.AddComponent<Afterburner>();
            helper = owner.AddComponent<Helper_Afterburner>();
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

            Assert.IsFalse(component.activation);
            Assert.IsFalse(component.restoring);
            Assert.IsFalse(component.enabled);
        }

        [UnityTest]
        public IEnumerator OnAfterburnerActivation_True()
        {
            yield return null;
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);
            Assert.IsTrue(component.enabled);
        }

        [UnityTest]
        public IEnumerator OnAfterburnerActivation_False()
        {
            yield return null;
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, false);
            Assert.IsFalse(component.enabled);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_Activation_Ignore_Activation()
        {
            component.activation = true;
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);
            yield return null;
            Assert.AreEqual(0f, helper.throttle);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_Activation_Ignore_Restore()
        {
            component.restoring = true;
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);
            yield return null;
            Assert.AreEqual(0f, helper.throttle);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_Activation()
        {
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);

            yield return null;

            Assert.AreEqual(component.throttle, helper.throttle);
            Assert.IsTrue(component.activation);
            Assert.IsTrue(helper.activation);
            Assert.AreEqual(1, helper.afterburnerCallsCount);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_Disable()
        {
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);

            yield return null;

            component.enabled = false;

            yield return null;

            Assert.IsFalse(component.activation);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_ChangeThrottle()
        {
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);

            yield return null;

            helper.throttle = -1f;

            yield return null;

            Assert.IsFalse(component.activation);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_Duration()
        {
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);
            yield return new WaitForSeconds(component.duration * 1.1f);
            Assert.IsFalse(component.activation);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_Restoring_Start()
        {
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);

            yield return null;

            component.enabled = false;

            yield return null;

            Assert.IsTrue(component.restoring);
        }

        [UnityTest]
        public IEnumerator ActivationProcess_Restoring_Complete()
        {
            component.SendMessage(FlightModelUtility.ON_AFTERBURNER_ACTIVATION_METHOD, true);

            yield return null;

            component.enabled = false;

            yield return null;

            yield return new WaitForSeconds(component.cooldown * 1.1f);

            Assert.IsFalse(component.restoring);
        }
    }
}
