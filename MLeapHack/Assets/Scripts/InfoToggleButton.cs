using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicLeap
{
    [DisallowMultipleComponent]
    public class InfoToggleButton : MediaPlayerButton
    {
        public Vector3 offsetVector;
        public GameObject mediaPlayerParent;

        bool videoToggle = false;

        #region Unity Methods
        protected override void OnEnable()
        {
            OnControllerTriggerDown += HandleTriggerDown;

            base.OnEnable();

            mediaPlayerParent = GameObject.FindGameObjectWithTag("MediaPlayer");
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
            if(!videoToggle)
            {
                AppSequenceHandler.instance.SetupMediaPlayer();
                videoToggle = true;
            }
            else
            {
                AppSequenceHandler.instance.DisableMediaPlayer();
                videoToggle = false;
            }
        }
        #endregion

    }
}