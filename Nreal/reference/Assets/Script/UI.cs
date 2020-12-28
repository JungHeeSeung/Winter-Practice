using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// gogo 인게임 UI
public class UI : MonoBehaviour
{
    public Player player;
    public Text mainInfoText;

    void Update()
    {
        mainInfoText.text = player.collisionCount.ToString();
        FollowMainCam();
    }
    private void FollowMainCam()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
    }
}
