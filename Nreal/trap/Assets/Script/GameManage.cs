using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public Player player;
    public Trap trapManger;

    public Text text;

    public float trapRange = 0.5f;

    private void Update()
    {
        for(int i=0; i<trapManger.traps.Count; ++i)
        {
            var trapPos = trapManger.GetTrapPos(i);

            if (trapRange > Vector2.Distance(player.GetPlayerPosXZ(), trapPos))
            {
                text.text = "Player Touched Trap [" + i + "]";
            }
            else
            {
                text.text = "Player Touched Nothing";
            }
        }
    }


}
