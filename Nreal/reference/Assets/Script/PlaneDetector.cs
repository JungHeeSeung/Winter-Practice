using System.Collections.Generic;
using UnityEngine;

namespace NRKernal.NRExamples
{
    [HelpURL("https://developer.nreal.ai/develop/discover/introduction-nrsdk")]
    public class PlaneDetector : MonoBehaviour
    {
        public GameObject DetectedPlanePrefab;
        public static float y = 0;

        // A list to hold new planes NRSDK began tracking in the current frame. This object is used across
        // the application to avoid per-frame allocations.
        private List<NRTrackablePlane> m_NewPlanes = new List<NRTrackablePlane>();

        private bool searching = true;
        public bool ok = false;


        public void Update()
        {
            if (ok)
            {
                //생성 중지 & 평면 비표시
                searching = false;    
            }

            if (searching)
            {
                NRFrame.GetTrackables<NRTrackablePlane>(m_NewPlanes, NRTrackableQueryFilter.New);
                for (int i = 0; i < m_NewPlanes.Count; i++)
                {
                    // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
                    // the origin with an identity rotation since the mesh for our prefab is updated in Unity World coordinates.
                    GameObject planeObject = Instantiate(DetectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
                    planeObject.GetComponent<NRTrackableBehaviour>().Initialize(m_NewPlanes[i]);

                }
            }
        }
    }
}