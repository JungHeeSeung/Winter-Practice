using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTest : MonoBehaviour
{
    public Dictionary<int, GameObject> dict;

    public List<GameObject> target = new List<GameObject>();


    private void Start()
    {
        var cnt = target.Count;
    }
}
