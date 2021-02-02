using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTest : MonoBehaviour
{
    public List<GameObject> target = new List<GameObject>();

    public List<canvasUI> vList;

    public canvasUI UI;

    private void Start()
    {
        for (int i = 0; i < target.Count; ++i)
        {
            var ui = Instantiate(UI);

            ui.transform.position = target[i].transform.position;
            ui.transform.parent = target[i].transform;

            vList.Add(ui);
        }
    }
    
}
