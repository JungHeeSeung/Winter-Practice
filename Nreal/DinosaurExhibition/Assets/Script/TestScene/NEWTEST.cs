using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWTEST : MonoBehaviour
{
    [System.Serializable]
    public class PointList
    {
        public List<Vector3> list = new List<Vector3>();
    }

    public List<PointList> pointLists = new List<PointList>();

    public int count = 0;

    private void Start()
    {
        if(pointLists.Count < count)
        {
            for(int i = pointLists.Count; i<=count; ++i)
            {
                pointLists.Add(null);
                Debug.Log(i);
            }
        }
        if(pointLists[0] == null)
        {
            pointLists[0] = new PointList();
        }
       
    }

}
