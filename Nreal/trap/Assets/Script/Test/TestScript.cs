using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        //Vector3 target = (transform.position - player.transform.position).normalized;

        //float dot = Vector3.Dot(player.transform.forward, target);

        //float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.LookAt(player.transform);
    }
}

