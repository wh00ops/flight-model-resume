using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class Flight : MonoBehaviour, IForcePower, ITorquePower, IThrottle, ISensivityPoint
    {
        public Vector3 targetPoint;
        public float throttle;

        public float maxSpeed = 300f;
        public Vector3 maxTorque = new Vector3(0.5f, 0.25f, 1f);
        public float sensivityAngle = 30f;
        public float engineSensivity = 1f;
        public float steeringSensivity = 1f;
        public float movementDumping = 0.1f;
        public float torqueDumping = 0.5f;
        public float autoHorizontFactor = 1f;
        public AnimationCurve torqueSpeedDrop = new AnimationCurve();

        public bool debug;

        float ISensivityPoint.sensivityPoint { get => sensivityAngle; set => sensivityAngle = value; }
        float IThrottle.throttle { get => throttle; set => throttle = value; }
        Vector3 IForcePower.forcePower { get => m_force / maxSpeed; }
        Vector3 ITorquePower.torquePower { get => new Vector3(m_torque.x / maxTorque.x, m_torque.y / maxTorque.y, m_torque.z / maxTorque.z); }

        Vector3 m_force;
        Vector3 m_torque;

        Transform m_transform;
        Rigidbody m_rigidbody;

        private void Awake()
        {
            m_transform = transform;
        }

        private void Start()
        {
            m_rigidbody = m_transform.GetComponentInParent<Rigidbody>();
            OnReset();
        }

        private void OnReset()
        {
            m_force = Vector3.zero;
            m_torque = Vector3.zero;

            if (m_rigidbody != null)
            {
                m_rigidbody.velocity = Vector3.zero;
                m_rigidbody.angularVelocity = Vector3.zero;
                m_rigidbody.useGravity = false;
            }

            enabled = true;
        }

        private void OnTargetPointSet(Vector3 targetPoint)
        {
            this.targetPoint = targetPoint;
        }

        private void OnThrottleSet(float throttle)
        {
            this.throttle = throttle;
        }

        private void Update()
        {
            Vector3 localDirection = m_transform.InverseTransformPoint(targetPoint).normalized;
            float align = Mathf.Clamp(Vector3.Angle(Vector3.forward, localDirection) / (sensivityAngle * 0.5f), -1f, 1f);

            Vector3 torque = new Vector3(-localDirection.y, localDirection.x, -localDirection.x);

            if (torque.magnitude != 0f)
                torque = NormalizePower(torque);

            torque = torque * align;
            torque.z = Mathf.Clamp(torque.z - m_transform.right.y * autoHorizontFactor * (1f - align), -1f, 1f);

            Vector3 torqueLimit = maxTorque - maxTorque * torqueSpeedDrop.Evaluate(m_rigidbody.velocity.magnitude / maxSpeed);

            torque.x *= torqueLimit.x;
            torque.y *= torqueLimit.y;
            torque.z *= torqueLimit.z;

            float deltaTime = Time.deltaTime;

            m_force += (Vector3.forward * maxSpeed * throttle - m_force) * engineSensivity * deltaTime;
            m_torque += (torque - m_torque) * steeringSensivity * deltaTime;
        }

        private void LateUpdate()
        {
            if (!debug)
                return;

            Debug.DrawLine(m_transform.position, m_transform.position + m_transform.forward * 100f, Color.red, 0f);
            Debug.DrawLine(m_transform.position, targetPoint, Color.green, 0f);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;

            m_rigidbody.drag = movementDumping;
            m_rigidbody.angularDrag = torqueDumping;

            float dragFactor = (1 - deltaTime * m_rigidbody.drag);
            float angularDragFactor = (1 - deltaTime * m_rigidbody.angularDrag);

            m_rigidbody.AddRelativeForce((m_force * m_rigidbody.drag) / dragFactor, ForceMode.Acceleration);
            m_rigidbody.AddRelativeTorque((m_torque * m_rigidbody.angularDrag) / angularDragFactor, ForceMode.Acceleration);
        }

        private void OnRuined()
        {
            enabled = false;

            m_rigidbody.drag = 0.1f;
            m_rigidbody.angularDrag = 0.1f;
            m_rigidbody.useGravity = true;
        }

        private Vector3 NormalizePower(Vector3 power)
        {
            float max = Mathf.Max(Mathf.Abs(power.x), Mathf.Abs(power.y), Mathf.Abs(power.z));
            power.x /= max;
            power.y /= max;
            power.z /= max;
            return power;
        }
    }
}