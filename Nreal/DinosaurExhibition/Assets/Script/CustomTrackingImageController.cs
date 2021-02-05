namespace NRKernal.NRExamples
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Controller for TrackingImage example.
    /// </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/image-tracking")]
    public class CustomTrackingImageController : MonoBehaviour
    {
        // A prefab for visualizing an TrackingImage.
        public CustomTrackingImageVisualizer TrackingImageVisualizerPrefab;

        // The overlay containing the fit to scan user guide.
        public GameObject FitToScanOverlay;

        private Dictionary<int, CustomTrackingImageVisualizer> m_Visualizers
            = new Dictionary<int, CustomTrackingImageVisualizer>();

        public Dictionary<int, CustomTrackingImageVisualizer> data
        {
            set => m_Visualizers = value;
            get => m_Visualizers;
        }

        private int cnt = 0;

        private List<NRTrackableImage> m_TempTrackingImages = new List<NRTrackableImage>();

        public void Update()
        {
#if !UNITY_EDITOR
            // Check that motion tracking is tracking.
            if (NRFrame.SessionStatus != SessionState.Running)
            {
                return;
            }
#endif
            // Get updated augmented images for this frame.
            NRFrame.GetTrackables<NRTrackableImage>(m_TempTrackingImages, NRTrackableQueryFilter.New);

            // Create visualizers and anchors for updated augmented images that are tracking and do not previously
            // have a visualizer. Remove visualizers for stopped images.
            foreach (var image in m_TempTrackingImages)
            {
                CustomTrackingImageVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.GetDataBaseIndex(), out visualizer);

                if (image.GetTrackingState() == TrackingState.Tracking && visualizer == null)
                {
                   
                    // Create an anchor to ensure that NRSDK keeps tracking this augmented image.
                    visualizer = (CustomTrackingImageVisualizer)Instantiate(TrackingImageVisualizerPrefab, image.GetCenterPose().position, image.GetCenterPose().rotation);
                    visualizer.Image = image;
                    visualizer.transform.parent = transform;


                    visualizer.idx = image.GetDataBaseIndex();
                    // 항상 사람이 봤을 때 수직으로 서 있게
                    visualizer.Obj[visualizer.idx].transform.rotation = Quaternion.identity;
                    visualizer.ui.transform.rotation = Quaternion.identity;
                    // 

                    m_Visualizers.Add(image.GetDataBaseIndex(), visualizer);
                }
                else if (image.GetTrackingState() != TrackingState.Tracking && visualizer != null)
                {
                    m_Visualizers.Remove(image.GetDataBaseIndex());
                    Destroy(visualizer.gameObject);
                }

                //FitToScanOverlay.SetActive(false);
            }

            foreach (var val in m_Visualizers.Values)
            {
                if (true == val.Obj[val.idx].activeSelf)
                {
                    FitToScanOverlay.SetActive(false);
                    break;
                }

                if(cnt < m_Visualizers.Values.Count - 1)
                {
                    continue;
                }
                else
                {
                    FitToScanOverlay.SetActive(true);
                }
                cnt++;
            }
            cnt = 0;
        }



        public void EnableImageTracking()
        {
            var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            config.ImageTrackingMode = TrackableImageFindingMode.ENABLE;
            NRSessionManager.Instance.SetConfiguration(config);
        }

        public void DisableImageTracking()
        {
            var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            config.ImageTrackingMode = TrackableImageFindingMode.DISABLE;
            NRSessionManager.Instance.SetConfiguration(config);
        }
    }
}
