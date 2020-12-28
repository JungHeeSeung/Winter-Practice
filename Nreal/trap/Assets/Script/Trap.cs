using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Trap : MonoBehaviour
{ 
    public List<GameObject> traps;
    public Text text;

    public void GetTrap(GameObject newObj)
    {
        newObj.transform.Rotate(90, 0, 0);
        traps.Add(newObj);
    }

    public void ShowTrapPos()
    {
        string info = null;
        for (int i =0; i<traps.Count; ++i)
        {
            info += "Trap [" + i + "]" + " pos: " + traps[i].transform.position + 
                "Rotation: " + traps[i].transform.rotation.eulerAngles + "\n";

        }
        text.text = info;
    }

    private void Update()
    {
        ShowTrapPos();
    }

    public Vector2 GetTrapPos(int index)
    {
        var newPos = new Vector2(traps[index].transform.position.x, traps[index].transform.position.z);

        return newPos;
    }
}
