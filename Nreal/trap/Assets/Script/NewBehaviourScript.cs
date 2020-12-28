using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Rotate(90, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log(this.transform.rotation.eulerAngles);
            //Debug.Log(Quaternion.identity.eulerAngles);
            if(Equals(transform, this.transform))
            {
                Debug.Log("같다!");
            }
            else
            {
                Debug.Log("다르다!");
            }
        }
    }
}
