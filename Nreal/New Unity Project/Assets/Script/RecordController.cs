using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NRKernal;

public class RecordController : MonoBehaviour
{
    public UnityEvent OnRecord;
    public UnityEvent OffRecord;

    bool isRecord = false;

    // Update is called once per frame
    void Update()
    {
        if (NRInput.GetButton(ControllerButton.APP))
        {
            if (false == isRecord)
            {
                OnRecord.Invoke();
            }
            else
            {
                OffRecord.Invoke();
            }
            isRecord = !isRecord;
        }
    }
}
