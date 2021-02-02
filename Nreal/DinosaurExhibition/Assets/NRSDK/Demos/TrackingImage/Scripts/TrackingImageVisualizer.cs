namespace NRKernal.NRExamples
{
    using UnityEngine;

    public enum State
    {
        None,
        rotate,
        scale,
        texture,
        grid
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

        // 이 부분을 아마도 배열같은 자료구조로 처리해야할 듯
        public GameObject Obj;

        public GameObject ui;

        public canvasUI but;

        public State state = State.None;

        public State DrawState = State.texture;

        void SetRotation(bool isSelected)
        {
            if(isSelected)
            {
                state = State.rotate;
            }
            else
            {
                state = State.None;
            }
        }
        void SetScale(bool isSelected)
        {
            if (isSelected)
            {
                state = State.scale;
            }
            else
            {
                state = State.None;
            }
        }
        void SetTexture(bool isSelected)
        {
            if (isSelected)
            {
                DrawState = State.texture;
            }
        }
        void SetGrid(bool isSelected)
        {
            if (isSelected)
            {
                DrawState = State.grid;
            }
        }

        public void ResetState()
        {
            state = State.None;
            DrawState = State.texture;

            but.rotate.isOn = false;
            but.scale.isOn = false;
            but.texture.isOn = true;
            but.wireFrame.isOn = false;
        }

        private void Start()
        {
            but = ui.GetComponent<canvasUI>();

            but.rotate.onValueChanged.RemoveAllListeners();
            but.scale.onValueChanged.RemoveAllListeners();
            but.texture.onValueChanged.RemoveAllListeners();
            but.wireFrame.onValueChanged.RemoveAllListeners();

            but.rotate.onValueChanged.AddListener(SetRotation);
            but.scale.onValueChanged.AddListener(SetScale);
            but.texture.onValueChanged.AddListener(SetTexture);
            but.wireFrame.onValueChanged.AddListener(SetGrid);
        }

        public void Update()
        {
            if (Image == null || Image.GetTrackingState() != TrackingState.Tracking)
            {
                ResetState();
                Obj.transform.rotation = Quaternion.identity;
                Obj.transform.localScale = new Vector3(1f, 1f, 1f);
                ui.transform.rotation = Quaternion.identity;
                ui.transform.localScale = new Vector3(1f, 1f, 1f);


                FrameLowerLeft.SetActive(false);
                FrameLowerRight.SetActive(false);
                FrameUpperLeft.SetActive(false);
                FrameUpperRight.SetActive(false);
                Obj.SetActive(false);
                ui.SetActive(false);
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

            Obj.SetActive(true);
            ui.SetActive(true);
        }
    }
}
