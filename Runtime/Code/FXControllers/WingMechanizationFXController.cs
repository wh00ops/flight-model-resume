using System.Text;
using UnityEngine;

namespace FreakGames.Unity.Core.FlightModel
{
    public class WingMechanizationFXController : MonoBehaviour
    {
        public enum MechanizationType
        {
            none = 0,
            pitch,
            yaw,
            roll
        }

        public Transform[] mechanization;
        public string pathToMechanization = "MECHANIZATION-PATH";

        public MechanizationType main;
        public MechanizationType mix;

        public Vector3 rotationAxis = Vector3.right;
        public float maxAttackAngle = 30f;

        ITorquePower m_torque;
        Quaternion[] m_wingsInitialRotations;
        Transform m_transform;

        StringBuilder m_builder = new StringBuilder();

        private void Awake()
        {
            m_transform = transform;
        }

        private void Start()
        {
            m_torque = m_transform.root.GetComponentInChildren<ITorquePower>();

            if (mechanization.Length > 0)
                Configure();
        }

        private void OnAssetInstantiated(GameObject asset)
        {
            Transform fxScheme = asset.transform.Find(pathToMechanization);

            mechanization = GetChilds(fxScheme.transform);

            m_transform.position = fxScheme.transform.position;
            m_transform.rotation = fxScheme.transform.rotation;
            m_transform.localScale = fxScheme.transform.localScale;

            Configure();
        }

        private void Configure()
        {
            m_wingsInitialRotations = new Quaternion[mechanization.Length];
            for (int i = 0; i < m_wingsInitialRotations.Length; i++)
                m_wingsInitialRotations[i] = mechanization[i].localRotation;
        }

        private void LateUpdate()
        {
            float mainRotationAngle = GetValue(main, m_torque.torquePower) * maxAttackAngle;
            mainRotationAngle = Mathf.Clamp(mainRotationAngle, -maxAttackAngle, maxAttackAngle);

            float mixRotationAngle = 0f;

            if (mix != MechanizationType.none)
            {
                mixRotationAngle = GetValue(mix, m_torque.torquePower) * maxAttackAngle;
                mixRotationAngle = Mathf.Clamp(mixRotationAngle, -maxAttackAngle, maxAttackAngle);
            }

            for (int i = 0; i < mechanization.Length; i++)
            {
                Transform wing = mechanization[i];
                float rotationAngle = mainRotationAngle * GetPositionSign(main, wing.localPosition) + mixRotationAngle * GetPositionSign(mix, wing.localPosition);
                rotationAngle = Mathf.Clamp(rotationAngle, -maxAttackAngle, maxAttackAngle);
                wing.localRotation = m_wingsInitialRotations[i] * Quaternion.AngleAxis(rotationAngle, rotationAxis);
            }
        }

        private float GetPositionSign(MechanizationType mechanization, Vector3 value)
        {
            float result = 0f;

            if (mechanization == MechanizationType.roll)
                result = -value.x;
            else if (mechanization == MechanizationType.pitch)
                result = value.z;
            else if (mechanization == MechanizationType.yaw)
                result = value.z;

            return Mathf.Sign(result);
        }

        private float GetValue(MechanizationType mechanization, Vector3 value)
        {
            float result = 0f;

            if (mechanization == MechanizationType.roll)
                result = value.z;
            else if (mechanization == MechanizationType.pitch)
                result = value.x;
            else if (mechanization == MechanizationType.yaw)
                result = value.y;

            return result;
        }

        private Transform[] GetChilds(Transform container)
        {
            Transform[] result = new Transform[container.childCount];
            for (int i = 0; i < container.childCount; i++)
                result[i] = container.GetChild(i);
            return result;
        }

        private void OnBecameVisible()
        {
            enabled = true;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }
    }
}