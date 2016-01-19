﻿using UnityEngine;
using UnityEngine.UI;

namespace Colony.UI
{
    public class ForceCreatePanelController : MonoBehaviour
    {
        private Text messageBox;
        private float createWithinTime;

        void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.name == "Message")
                {
                    messageBox = child.GetComponent<Text>();
                }
            }
        }

        public void SetMessage(string message)
        {
            messageBox.text = message;
        }

        public void SetCreateWithinTime(float time)
        {
            createWithinTime = time;
        }

        public void OnButtonClick()
        {
            //Spawn countdown
        }
    }
}