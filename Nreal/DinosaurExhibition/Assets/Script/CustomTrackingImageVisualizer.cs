namespace NRKernal.NRExamples
{
    using System.Collections.Generic;
    using UnityEngine;


    /// <summary>
    /// Uses 4 frame corner objects to visualize an TrackingImage.
    /// </summary>
    public class CustomTrackingImageVisualizer : MonoBehaviour
    {
        // The TrackingImage to visualize.
        public NRTrackableImage Image;

        // A model for the lower left corner of the frame to place when an image is detected.
        public GameObject FrameLowerLeft;

        // A model for the lower right corner of the frame to place when an image is detected.
        public GameObject FrameLowerRight;

        // A model for the upper left corner of the frame to place when an image is detected.
        public GameObject FrameUpperLeft;

        // A model for the upper right corner of the frame to place when an image is detected.
        public GameObject FrameUpperRight;

    
        public GameObject canvas;

        public canvasUI ui;

        public List<GameObject> Obj;

        [HideInInspector]
        public int idx;

        public void Update()
        {
            if (Image == null || Image.GetTrackingState() != TrackingState.Tracking)
            {
                FrameLowerLeft.SetActive(false);
                FrameLowerRight.SetActive(false);
                FrameUpperLeft.SetActive(false);
                FrameUpperRight.SetActive(false);

                canvas.SetActive(false);

                /// 가지고 있는 모든 오브젝트 비활성화
                foreach (var target in Obj)
                {
                    if (target != null)
                    {
                        target.SetActive(false);
                    }
                }
                return;
            }

            float halfWidth = Image.ExtentX / 2;
            float halfHeight = Image.ExtentZ / 2;
            FrameLowerLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);
            FrameLowerRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.back);
            FrameUpperLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.forward);
            FrameUpperRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);

            var center = Image.GetCenterPose();
            transform.position = center.position;
            transform.rotation = center.rotation;

            FrameLowerLeft.SetActive(true);
            FrameLowerRight.SetActive(true);
            FrameUpperLeft.SetActive(true);
            FrameUpperRight.SetActive(true);

            var pos = canvas.transform.position;
            pos.y = center.position.y - halfHeight / 2;
            canvas.transform.position = pos;
            canvas.transform.rotation = Quaternion.identity;

            canvas.SetActive(true);

            /// 간혹 특정 오브젝트가 비활성화 되지가 않아서
            /// 지금 활성화하는 오브젝트 빼고 전부 비활성화
            foreach (var target in Obj)
            {
                if (target != null)
                {
                    if ((idx < Obj.Count) &&
                        (target == Obj[idx]))
                    {
                        target.transform.position = center.position;
                        target.SetActive(true);
                    }
                    else
                    {
                        target.SetActive(false);
                    }
                }
            }
            ///
        }
    }
}
