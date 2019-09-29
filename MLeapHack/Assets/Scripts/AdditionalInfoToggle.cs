using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicLeap
{
    [DisallowMultipleComponent]
    public class AdditionalInfoToggle : MediaPlayerButton
    {
        #region Unity Methods
        protected override void OnEnable()
        {
            OnControllerTriggerDown += HandleTriggerDown;

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            OnControllerTriggerDown -= HandleTriggerDown;

            base.OnDisable();
        }
        #endregion

        #region Event Handlers
        private void HandleTriggerDown(float triggerValue)
        {
            TranscriptManager._instance.ToggleAdditionalInfoPanel(true);
        }
        #endregion

    }
}
