namespace NRKernal.NRExamples
{
    using System.Collections.Generic;
    using UnityEngine;


    public enum State
    {
        Rotate,
        Scaled,
        Texture,
        WireFrame
    }

    /// <summary>
    /// Uses 4 frame corner objects to visualize an TrackingImage.
    /// </summary>
    public class TrackingImageVisualizer : MonoBehaviour
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

     
        public State state;
        public State drawState;

        public GameObject  Obj;


        private void Start()
        {
            ui = canvas.GetComponent<canvasUI>();
            SetFunctionUI();
        }

        void IsRotate(bool isSelected)
        {
            if (isSelected)
            {
                state = State.Rotate;
            }
        }

        void IsScale(bool isSelected)
        {
            if (isSelected)
            {
                state = State.Scaled;
            }
        }

        void IsTexture(bool isSelected)
        {
            if (isSelected)
            {
                drawState = State.Texture;
            }
        }

        void IsWireFrame(bool isSelected)
        {
            if (isSelected)
            {
                drawState = State.WireFrame;
            }
        }

        public void SetFunctionUI()
        {
            ui.rotate.onValueChanged.RemoveAllListeners();
            ui.scale.onValueChanged.RemoveAllListeners();
            ui.texture.onValueChanged.RemoveAllListeners();
            ui.wireFrame.onValueChanged.RemoveAllListeners();


            ui.rotate.onValueChanged.AddListener(IsRotate);
            ui.scale.onValueChanged.AddListener(IsScale);
            ui.texture.onValueChanged.AddListener(IsTexture);
            ui.wireFrame.onValueChanged.AddListener(IsWireFrame);
        }

        public void Update()
        {
            if (Image == null || Image.GetTrackingState() != TrackingState.Tracking)
            {
                FrameLowerLeft.SetActive(false);
                FrameLowerRight.SetActive(false);
                FrameUpperLeft.SetActive(false);
                FrameUpperRight.SetActive(false);

                canvas.SetActive(false);

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

            canvas.SetActive(true);
            Obj.SetActive(true);
        }
    }
}
