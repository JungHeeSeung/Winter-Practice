using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public Text text;

    public Vector3 pos;



    void Update()
    {
        pos = this.transform.position;
        text.text = "Scene Name: " + SceneManager.GetActiveScene().name + "\nPos:" + pos;
    }

    public Vector2 GetPlayerPosXZ()
    {
        var newPos = new Vector2(pos.x, pos.z);

        return newPos;
    }
}
