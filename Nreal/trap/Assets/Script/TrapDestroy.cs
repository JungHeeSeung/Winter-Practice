using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class TrapDestroy : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        // If the player doesn't click the trigger button, we are done with this update.
        if (!NRInput.GetButtonDown(ControllerButton.TRIGGER))
        {
            return;
        }

        // Get controller laser origin.
        Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : ControllerAnchorEnum.RightLaserAnchor);

        RaycastHit hitResult;
        if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
        {
            if (hitResult.collider.gameObject != null && hitResult.collider.gameObject.layer == LayerMask.NameToLayer("Trap"))
            {
                Destroy(hitResult.collider.gameObject);
            }
        }
    }
}
