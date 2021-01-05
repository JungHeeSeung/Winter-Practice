using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NRKernal;

public class TrapMaker : MonoBehaviour
{
    public List<GameObject> traps;

    public GameObject gameManager;

    public ExitManager exitManager;

    public Player player;

    public GameObject trapPrefab;

    public Text text;


    private int numOfTrap = 5;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("Manager");
        text = gameManager.transform.Find("Debug/DebugTxt3").GetComponent<Text>();
        if(gameManager != null)
        {
            text.text = "gameManager 찾음";
        }

        exitManager = gameManager.GetComponent<ExitManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(player != null)
        {
            text.text += "Player 찾음";
        }
         

        text.text = "Scene 시작";
        MakeTrap();
    }


    public void ShowTrapPos()
    {
        string info = null;

        text.text = "";
        for (int i = 0; i < traps.Count; ++i)
        {
            info += "Trap [" + i + "]" + " pos: " + traps[i].transform.position + "\n";
          
        }
        text.text = info;
    }

    public void MakeTrap()
    {
        var exitPos = exitManager.exit.transform.position;
        var playerPos = player.transform.position;

        //// 랜덤으로 만들기 전에 일단 고정 생성 해보자
        //var dis = (new Vector2(exitPos.x, exitPos.z) - player.GetPlayerPosXZ());
        //var middlePos = new Vector3(dis.x, exitPos.y, dis.y);
        //middlePos = middlePos / 2;
        //middlePos.y = exitPos.y;

        //var newTrap = (Instantiate(trapPrefab, middlePos, Quaternion.identity));
        //newTrap.transform.Rotate(90, 0, 0);
        //traps.Add(newTrap);
        //// 랜덤으로 만들기 전에 일단 고정 생성 해보자


        // 랜덤으로 함정 만들기
        int count = 0;

        while (traps.Count < numOfTrap)
        {
            count++;
            var ranPosX = Random.Range(exitPos.x, playerPos.x);
            //var ranPosY = Random.Range(exitPos.y, playerPos.y);
            var ranPosZ = Random.Range(exitPos.z, playerPos.z);

            Vector3 spawnPos = new Vector3(ranPosX, exitPos.y, ranPosZ);
            var layer = (1 << LayerMask.NameToLayer("Trap"))
                | (1 << LayerMask.NameToLayer("Portal")) | (1 << LayerMask.NameToLayer("Player"));


            var collision = Physics.OverlapSphere(spawnPos, 0.5f, layer);

            if (collision.Length == 0)  // 주변에 겹치는 게 없다면
            {
                var newTrap = Instantiate(trapPrefab, spawnPos, Quaternion.identity);


                traps.Add(newTrap);
            }
            if (count > 1000)        // 무한 루프 방지용... 이 부분은 수정될 가능성이 큼
            {
                text.text = "으악 무한루프다";
                break;
            }
        }
        // 랜덤으로 함정 만들기


    }

    private void Update()
    {
        ShowTrapPos();
    }


    public Vector2 GetTrapPos(int index)
    {
        var newPos = new Vector2(traps[index].transform.position.x, traps[index].transform.position.z);

        return newPos;
    }
}
