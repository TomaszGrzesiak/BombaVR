using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace Unity.VRTemplate
{
    /// <summary>
    /// A simplified interactable knob that rotates along the Z-axis.
    /// </summary>
    public class XRKnobSimplifiedZ : XRBaseInteractable
    {
        [Serializable]
        public class ValueChangeEvent : UnityEvent<float> { }

        [SerializeField]
        [Tooltip("The object that is visually grabbed and manipulated")]
        private Transform m_Handle = null;

        [SerializeField]
        [Tooltip("Rotation of the knob at value '0'")]
        private float m_MinAngle = -180.0f;

        [SerializeField]
        [Tooltip("Rotation of the knob at value '1'")]
        private float m_MaxAngle = 180.0f;

        [SerializeField]
        [Tooltip("The current value of the knob")]
        [Range(0.0f, 2.0f)]
        private float m_Value = 0.5f;

        [SerializeField]
        [Tooltip("Event triggered when the value changes")]
        private ValueChangeEvent m_OnValueChange = new ValueChangeEvent();

        private IXRSelectInteractor m_Interactor;

        /// <summary>
        /// The value of the knob (normalized between 0 and 1).
        /// </summary>
        public float Value
        {
            get => m_Value;
            set
            {
                m_Value = Mathf.Clamp01(value);
                UpdateKnobRotation();
                m_OnValueChange.Invoke(m_Value);
            }
        }

        private void Start()
        {
            UpdateKnobRotation();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            selectEntered.AddListener(OnSelectEntered);
            selectExited.AddListener(OnSelectExited);
        }

        protected override void OnDisable()
        {
            selectEntered.RemoveListener(OnSelectEntered);
            selectExited.RemoveListener(OnSelectExited);
            base.OnDisable();
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            m_Interactor = args.interactorObject;
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            m_Interactor = null;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (isSelected && m_Interactor != null && updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                UpdateKnobValue();
            }
        }

        private void UpdateKnobValue()
        {
            // Calculate the new value based on the interactor's position
            var localOffset = transform.InverseTransformPoint(m_Interactor.GetAttachTransform(this).position);
            float newValue = Mathf.InverseLerp(m_MinAngle, m_MaxAngle, localOffset.x * 100); // Map X-axis offset to normalized value
            Value = newValue;
        }

        private void UpdateKnobRotation()
        {
            if (m_Handle != null)
            {
                float angle = Mathf.Lerp(m_MinAngle, m_MaxAngle, m_Value);
                m_Handle.localEulerAngles = new Vector3(angle, 270f, 0f); // Rotate around the Z-axis
            }
        }
    }
}
