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

    private void Update()
    {
        ShowInsepctor();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            TrackingImageVisualizer visualizer = null;

            var ran = Random.Range(0, TrackingImageVisualizerPrefab.Obj.Count);

            m_Visualizers.TryGetValue(ran, out visualizer);

            if (visualizer == null)
            {
                visualizer = Instantiate(TrackingImageVisualizerPrefab);
                m_Visualizers.Add(ran, visualizer);
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; (i < key.Count || i < value.Count); ++i)
            {
                if(i < key.Count)
                {
                    Debug.Log("key: " + key[i] );
                }
                if (i < value.Count)
                {
                    Debug.Log("\tvalue: " + value[i]);
                }

            }

        }
    }

    void ShowInsepctor()
    {
        key.Clear();
        value.Clear();

        foreach (var pair in m_Visualizers)
        {
            key.Add(pair.Key);
            value.Add(pair.Value);
        }
    }
}

