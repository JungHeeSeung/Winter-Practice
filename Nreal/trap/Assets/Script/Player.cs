using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Text text;

    public Vector3 pos;

    public UnityEvent Victory;
    public UnityEvent Dead;

    void Update()
    {
        pos = this.transform.position;
        text.text = "Scene Name: " + SceneManager.GetActiveScene().name + "\nPos:" + pos;
    }


    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.layer == LayerMask.NameToLayer("Portal"))
        {
            Debug.Log("Portal하고 충돌함");
            Victory.Invoke();
        }    
        if (other.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            Debug.Log("Trap하고 충돌함");
            Dead.Invoke();
        }
    }

  
}
