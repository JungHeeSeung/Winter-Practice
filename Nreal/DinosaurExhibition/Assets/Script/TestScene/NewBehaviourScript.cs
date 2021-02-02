using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NRKernal.NRExamples;



public class NewBehaviourScript : MonoBehaviour
{

    public TrackingImageVisualizer TrackingImageVisualizerPrefab;


    public Dictionary<int, TrackingImageVisualizer> m_Visualizers = new Dictionary<int, TrackingImageVisualizer>();

    public List<int> key = new List<int>();
    public List<TrackingImageVisualizer> value = new List<TrackingImageVisualizer>();


}

