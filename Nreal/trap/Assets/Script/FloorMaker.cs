using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NRKernal;

public class FloorMaker : MonoBehaviour
{
    public List<GameObject> floors;

    public Text text;

    public Material floorMt;
    public Material trapMt;


    public void GetFloor(GameObject newObj)
    {
       
    }

    public void ShowFloorPos()
    {
        string info = null;
        for (int i = 0; i < floors.Count; ++i)
        {
            info += "Floor [" + i + "]" + " pos: " + floors[i].transform.position +
                "Rotation: " + floors[i].transform.rotation.eulerAngles + "\n";

        }
        text.text = info;
    }

    // Update is called once per frame
    void Update()
    {
        ShowFloorPos();

       
       if(NRInput.GetButtonDown(ControllerButton.HOME))
        {
            for(int i=0; i< floors.Count; ++i)
            {
                var mt = floors[i].GetComponent<MeshRenderer>();
                mt.material = trapMt; 
            }
        }
    }
}
