using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTest : MonoBehaviour
{
    public Dictionary<int, GameObject> dict;

    public List<GameObject> target = new List<GameObject>();

<<<<<<< HEAD

    private void Start()
    {
        var cnt = target.Count;
=======
    private void Start()
    {
        for (int i = 0; i < target.Count; ++i)
        {
            var ui = Instantiate(UI);

            ui.transform.position = target[i].transform.position;
            ui.transform.parent = target[i].transform;

            vList.Add(ui);
        }
>>>>>>> parent of 4220a2b5... 2/1
    }
    
}
