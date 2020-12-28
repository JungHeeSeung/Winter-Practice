using System.Collections.Generic;
using UnityEngine;

namespace NRKernal.NRExamples
{
    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/controller")]
    public class HelloMRController : MonoBehaviour
    {
        /// <summary>
        /// A model to place when a raycast from a user touch hits a plane.
        /// </summary>
        // 표시할 프리팹
        public GameObject AndyPlanePrefab;
        // pool의 위치 저장 List
        public List<Vector3> poolPos = new List<Vector3>();
        // true == 위치 선정 완료
        public bool startGetPos = false;
        private Data data;

        private void Awake()
        {
            data = GameObject.Find("Data").GetComponent<Data>();
        }
        void Update()
        {
            // If the player doesn't click the trigger button, we are done with this update.
            if (!startGetPos)
            {
                return;
            }
            if (!NRInput.GetButtonDown(ControllerButton.TRIGGER))
            {
                return;
            }

            // Get controller laser origin.
            Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : ControllerAnchorEnum.RightLaserAnchor);

            RaycastHit hitResult;
            if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
            {
                // 레이저와 오브젝트가 충돌
                if (hitResult.collider.gameObject != null && hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>() != null)
                {
                    var behaviour = hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>();
                    // 바닥이랑 충돌한 게 아니면 리턴
                    if (behaviour.Trackable.GetTrackableType() != TrackableType.TRACKABLE_PLANE)
                    {
                        return;
                    }
                    // 바닥이랑 충돌한 거면 위치 저장
                    poolPos.Add(hitResult.point);
                    data.poolPos = poolPos;
                }
            }
        }
    }
}
