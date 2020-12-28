using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NRKernal;

public class VideoCaputre : MonoBehaviour
{
    public UnityEvent AppButtonTrigger;
    public UnityEvent HomeButtonTrigger;

    // Update is called once per frame
    void Update()
    {
        if(NRInput.GetButtonDown(ControllerButton.APP))
        {
            AppButtonTrigger.Invoke();
        }
        if(NRInput.GetButtonDown(ControllerButton.HOME))
        {
            HomeButtonTrigger.Invoke();
        }
    }
}
