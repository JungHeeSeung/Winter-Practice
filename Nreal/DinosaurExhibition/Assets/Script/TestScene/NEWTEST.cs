using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWTEST : MonoBehaviour
{
    public float rotSpd = 100f;

    private void Update()
    {
        float rotX = Time.deltaTime * rotSpd;

        transform.Rotate(Vector3.up, rotX);
    }

}
