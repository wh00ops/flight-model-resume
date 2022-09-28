using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FreakGames.Unity.Core.FlightModel.Tests
{
    public class Tests_Flight
    {
        Rigidbody rigidbody;
        Flight flight;

        [SetUp]
        public void Setup()
        {
            flight = new GameObject("flight").AddComponent<Flight>();
            rigidbody = flight.gameObject.AddComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            flight.sensivityAngle = 1f;
            flight.movementDumping = 9f;
            flight.torqueDumping = 9f;
            flight.maxSpeed = 10f;
            flight.torqueSpeedDrop.AddKey(0f, 0f);
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.Destroy(flight.gameObject);
        }

        IEnumerator CheckUntil(System.Func<bool> @while, float maxCheckTime = 5f)
        {
            float endTime = Time.time + maxCheckTime;
            while (!@while.Invoke() && Time.time < endTime)
                yield return null;
        }

        [UnityTest]
        public IEnumerator Reset()
        {
            flight.enabled = false;

            rigidbody.velocity = UnityEngine.Random.rotation.eulerAngles.normalized;
            rigidbody.angularVelocity = UnityEngine.Random.rotation.eulerAngles.normalized;
            rigidbody.useGravity = true;

            flight.SendMessage("OnReset");

            yield return null;

            Assert.AreEqual(0f, flight.throttle);
            Assert.AreEqual(Vector3.zero, (flight as IForcePower).forcePower);
            Assert.AreEqual(Vector3.zero, (flight as ITorquePower).torquePower);
            Assert.IsFalse(rigidbody.useGravity);
            Assert.IsTrue(flight.enabled);
        }

        [UnityTest]
        public IEnumerator Throttle()
        {
            flight.throttle = 1f;
            yield return CheckUntil(() => (flight as IForcePower).forcePower.z >= flight.throttle - 0.01f);
            Assert.AreEqual(flight.throttle, (flight as IForcePower).forcePower.z, 0.01f);
        }

        [UnityTest]
        public IEnumerator MaxSpeed()
        {
            flight.throttle = 1f;
            yield return CheckUntil(() => rigidbody.velocity.z >= flight.maxSpeed - 1f);
            Assert.AreEqual(rigidbody.velocity.z, flight.maxSpeed, 1f);
        }

        [UnityTest]
        public IEnumerator MaxAngularSpeed_X_positive()
        {
            flight.maxTorque = new Vector3(0.1f, 0f, 0f);
            flight.targetPoint = Vector3.down;

            yield return CheckUntil(() => rigidbody.angularVelocity.x >= flight.maxTorque.x - 0.01f);

            Assert.AreEqual(flight.maxTorque.x, rigidbody.angularVelocity.x, 0.01f);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(flight.maxTorque.x, rigidbody.angularVelocity.x, 0.01f);
        }

        [UnityTest]
        public IEnumerator MaxAngularSpeed_X_negative()
        {
            flight.maxTorque = new Vector3(0.1f, 0f, 0f);
            flight.targetPoint = Vector3.up;

            yield return CheckUntil(() => rigidbody.angularVelocity.x <= -flight.maxTorque.x + 0.01f);

            Assert.AreEqual(flight.maxTorque.x, -rigidbody.angularVelocity.x, 0.01f);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(flight.maxTorque.x, -rigidbody.angularVelocity.x, 0.01f);
        }

        [UnityTest]
        public IEnumerator MaxAngularSpeed_Y_positive()
        {
            flight.maxTorque = new Vector3(0f, 0.1f, 0f);
            flight.targetPoint = Vector3.right;

            yield return CheckUntil(() => rigidbody.angularVelocity.y >= flight.maxTorque.y - 0.01f);

            Assert.AreEqual(flight.maxTorque.y, rigidbody.angularVelocity.y, 0.01f);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(flight.maxTorque.y, rigidbody.angularVelocity.y, 0.01f);
        }

        [UnityTest]
        public IEnumerator MaxAngularSpeed_Y_negative()
        {
            flight.maxTorque = new Vector3(0f, 0.1f, 0f);
            flight.targetPoint = Vector3.left;

            yield return CheckUntil(() => rigidbody.angularVelocity.y <= -flight.maxTorque.y + 0.01f, 100f);

            Assert.AreEqual(flight.maxTorque.y, -rigidbody.angularVelocity.y, 0.01f);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(flight.maxTorque.y, -rigidbody.angularVelocity.y, 0.01f);
        }

        [UnityTest]
        public IEnumerator MaxAngularSpeed_Z_positive()
        {
            flight.maxTorque = new Vector3(0f, 0f, 0.1f);
            flight.targetPoint = Vector3.left;

            yield return CheckUntil(() => rigidbody.angularVelocity.z >= flight.maxTorque.z - 0.01f);

            Assert.AreEqual(flight.maxTorque.z, rigidbody.angularVelocity.z, 0.01f);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(flight.maxTorque.z, rigidbody.angularVelocity.z, 0.01f);
        }

        [UnityTest]
        public IEnumerator MaxAngularSpeed_Z_negative()
        {
            flight.maxTorque = new Vector3(0f, 0f, 0.1f);
            flight.targetPoint = Vector3.left;

            yield return CheckUntil(() => rigidbody.angularVelocity.z >= flight.maxTorque.z - 0.01f);

            Assert.AreEqual(flight.maxTorque.z, rigidbody.angularVelocity.z, 0.01f);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(flight.maxTorque.z, rigidbody.angularVelocity.z, 0.01f);
        }

        [UnityTest]
        public IEnumerator TorqueSpeedDrop()
        {
            flight.torqueSpeedDrop.RemoveKey(0);
            flight.torqueSpeedDrop.AddKey(0f, 0.5f);

            flight.maxTorque = new Vector3(0.1f, 0f, 0f);
            flight.targetPoint = Vector3.down;

            yield return CheckUntil(() => rigidbody.angularVelocity.x >= flight.maxTorque.x * flight.torqueSpeedDrop[0].value - 0.01f);

            Assert.AreEqual(flight.maxTorque.x * flight.torqueSpeedDrop[0].value, rigidbody.angularVelocity.x, 0.01f);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(flight.maxTorque.x * flight.torqueSpeedDrop[0].value, rigidbody.angularVelocity.x, 0.01f);
        }

        [UnityTest]
        public IEnumerator Alignment()
        {
            flight.sensivityAngle = 30f;
            flight.maxTorque = new Vector3(2f, 0f, 0f);
            flight.targetPoint = Vector3.up;

            yield return CheckUntil(() => Vector3.Angle(flight.transform.forward, flight.targetPoint) < 1f);

            Assert.Less(Vector3.Angle(flight.transform.forward, flight.targetPoint), 1f);
        }

        [UnityTest]
        public IEnumerator AutoHorizont()
        {
            flight.autoHorizontFactor = 3f;
            flight.targetPoint = Vector3.forward;
            flight.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

            yield return CheckUntil(() => Vector3.Angle(flight.transform.right, Vector3.right) < 1f);

            Assert.Less(Vector3.Angle(flight.transform.right, Vector3.right), 1f);
        }
    }
}