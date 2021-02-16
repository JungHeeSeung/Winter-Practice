using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using NRKernal;
public class Player : MonoBehaviour
{
    private static Transform m_CenterCamera;
    public static Transform CenterCamera
    {
        get
        {
            if (m_CenterCamera == null)
            {
                if (NRSessionManager.Instance.CenterCameraAnchor != null)
                {
                    m_CenterCamera = NRSessionManager.Instance.CenterCameraAnchor;
                }
                else if (Camera.main != null)
                {
                    m_CenterCamera = Camera.main.transform;
                }
            }
            return m_CenterCamera;
        }
    }
}
