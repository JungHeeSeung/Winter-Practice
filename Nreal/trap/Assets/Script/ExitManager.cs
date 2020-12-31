using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExitManager : MonoBehaviour
{
    public GameObject exit;
    public Text text;


    public void GetTrap(GameObject newObj)
    {
        if (exit != null)
        {
            Destroy(exit);
        }
        exit = newObj;
    }

    public void ShowExitPos()
    {
        string info = null;

        info += "Exit pos: " + exit.transform.position + " ";
        info += exit.GetComponent<Exit>().debugTxt + "\n";
        text.text = info;
    }


    private void Update()
    {
        if (exit != null)
        {
            ShowExitPos();

        }
    }
}
