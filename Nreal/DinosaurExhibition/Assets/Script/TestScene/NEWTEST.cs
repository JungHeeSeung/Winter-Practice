using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWTEST : MonoBehaviour
{

    public int idx = 0;

    public Dictionary<int, GameObject> dict = new Dictionary<int, GameObject>();

    public List<int> keys = new List<int>();
    public List<GameObject> values = new List<GameObject>();

    public List<GameObject> objs = new List<GameObject>();


    public GameObject mt = null;
    void Renew()
    {
        keys.Clear();
        values.Clear();

        var temp = new List<int>(dict.Keys);
        keys = temp;

        var vList = new List<GameObject>(dict.Values);
        values = vList;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mt = null;

            if (true == dict.TryGetValue(idx, out mt))
            {
                Debug.Log("찾았당");
            }
            else
            {
                Debug.Log("못 찾았당");
                dict.Add(idx, mt);
            }

            if (mt == null)
            {
                Debug.Log("그리고 난 NULL");
            }
            else
            {
                Debug.Log("아 NULL 아니라고~");
            }
            Renew();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            dict.Add(idx, objs[idx]);
            Renew();
        }
    }


}
