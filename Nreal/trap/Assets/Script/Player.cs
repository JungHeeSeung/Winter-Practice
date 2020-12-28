using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text text;

    public Vector3 pos;

    public Vector2 Pos
    {
        get
        {
            var newPos = new Vector2(pos.x, pos.z);

            return newPos;
        }
    }

    void Update()
    {
        pos = this.transform.position;
        text.text = "Pos:" + pos;
    }
}
