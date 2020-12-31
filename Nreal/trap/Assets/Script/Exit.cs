using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public string debugTxt
    {
        set;

        get;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        count++;
    //        debugTxt = "Player Touched: " + count;
    //    }
    //}


    private void Start()
    {
        debugTxt = "생성 됐당";
    }

  
}
