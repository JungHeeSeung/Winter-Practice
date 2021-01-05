using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player;

    public List<GameObject> traps;

    private void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var newTrap = Instantiate(prefab, transform.position, Quaternion.identity);
            //newTrap.transform.Rotate(90, 0, 0);
            //newTrap.transform.LookAt(player.transform);

            traps.Add(newTrap);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowInfo();
        }
      
    }

    void ShowInfo()
    {
        foreach(var obj in traps)
        {
            Debug.Log("Forward: " + obj.transform.forward);
            Debug.Log("Up: " + obj.transform.up);
        }
    }

}

