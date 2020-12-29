using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public Player player;
    public Trap trapManger;

    public Text text;

  
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    //private void Update()
    //{
    //    //for(int i=0; i<trapManger.traps.Count; ++i)
    //    //{
    //    //    var trapPos = trapManger.GetTrapPos(i);

    //    //    if (trapRange > Vector2.Distance(player.GetPlayerPosXZ(), trapPos))
    //    //    {

    //    //    }

    //    //}


    //}

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}
