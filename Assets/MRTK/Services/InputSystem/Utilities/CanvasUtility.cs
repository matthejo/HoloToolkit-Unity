// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Microsoft.MixedReality.Toolkit.Input.Utilities
{
    /// <summary>
    /// Helper class for setting up canvases for use in the MRTK.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    [AddComponentMenu("Scripts/MRTK/Services/CanvasUtility")]
    public class CanvasUtility : MonoBehaviour, IMixedRealityPointerHandler
    {
        public void OnPointerClicked(MixedRealityPointerEventData eventData) { }

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            if (!(eventData.Pointer is IMixedRealityNearPointer) && eventData.Pointer.Controller.IsRotationAvailable)
            {
                PointerUpHandler.TrackPointerUp(eventData.Pointer);
            }
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

        public void OnPointerUp(MixedRealityPointerEventData eventData) { }

        private void Start()
        {
            VerifyCanvasConfiguration();
        }

        /// <summary>
        /// Verifies and updates MRTK related canvas configuration.
        /// </summary>
        public void VerifyCanvasConfiguration()
        {
            Canvas canvas = GetComponent<Canvas>();
            Debug.Assert(canvas != null);

            if (canvas.worldCamera == null)
            {
                Debug.Assert(CoreServices.InputSystem?.FocusProvider?.UIRaycastCamera != null, this);
                canvas.worldCamera = CoreServices.InputSystem?.FocusProvider?.UIRaycastCamera;

                if (EventSystem.current == null)
                {
                    Debug.LogError("No EventSystem detected. UI events will not be propagated to Unity UI.");
                }
            }
            else
            {
                Debug.LogError("World Space Canvas should have no camera set to work properly with Mixed Reality Toolkit. At runtime, they'll get their camera set automatically.");
            }
        }

        private class PointerUpHandler : IMixedRealityPointerHandler
        {
            private readonly IMixedRealityPointer pointer;
            private readonly bool oldIsTargetPositionLockedOnFocusLock;

            public static void TrackPointerUp(IMixedRealityPointer pointer)
            {
                var handler = new PointerUpHandler(pointer);
                CoreServices.InputSystem.RegisterHandler<IMixedRealityPointerHandler>(handler);
            }

            private PointerUpHandler(IMixedRealityPointer pointer)
            {
                this.pointer = pointer;
                this.oldIsTargetPositionLockedOnFocusLock = pointer.IsTargetPositionLockedOnFocusLock;

                this.pointer.IsTargetPositionLockedOnFocusLock = false;
            }

            public void OnPointerClicked(MixedRealityPointerEventData eventData)
            {
            }

            public void OnPointerDown(MixedRealityPointerEventData eventData)
            {
            }

            public void OnPointerDragged(MixedRealityPointerEventData eventData)
            {
            }

            public void OnPointerUp(MixedRealityPointerEventData eventData)
            {
                if (eventData.Pointer == this.pointer)
                {
                    this.pointer.IsTargetPositionLockedOnFocusLock = this.oldIsTargetPositionLockedOnFocusLock;

                    CoreServices.InputSystem.UnregisterHandler<IMixedRealityPointerHandler>(this);
                }
            }
        }
    }
}
