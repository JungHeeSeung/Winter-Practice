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

    public GameObject bigPrefab;

    public Text text;

    private int numOfTrap =7;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("Manager");
        text = gameManager.transform.Find("Debug/DebugTxt3").GetComponent<Text>();
        exitManager = gameManager.GetComponent<ExitManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();


        MakeTrap();
    }

    public void ShowTrapPos()
    {
        for (int i = 0; i < traps.Count; ++i)
        {
            text.text += "Trap [" + i + "]" + " pos: " + traps[i].transform.position + "\n";

        }
    }

    public void MakeTrap()
    {
        var exitPos = exitManager.exit.transform.position;
        var playerPos = player.transform.position;

        // 랜덤으로 함정 만들기
        int count = 0;
      
        while (traps.Count < numOfTrap)
        {
            count++;

            var exitXZ = new Vector2(exitPos.x, exitPos.z);
            var playerXZ = new Vector2(playerPos.x, playerPos.z);
            var center = (exitXZ + playerXZ) / 2;
            var size = Vector2.Distance(exitXZ, playerXZ) / 2;

            Vector3 spawnPos = new Vector3(center.x, exitPos.y, center.y)  /* 플레이어와 출구의 중간 지점. 단, y값은 출구에 고정 */
                             + new Vector3(Random.Range(-size / 2, size / 2),   /* 그리고 랜덤 벡터값을 더하는데 두 점 절반지점 거리까지만 더한다 */
                               0,                                                   /* 그러면 사각형 범위 안에서 점을 랜덤으로 구할 수 있다 */
                               Random.Range(-size / 2, size / 2));

            var layer = (1 << LayerMask.NameToLayer("Player"))
                | (1 << LayerMask.NameToLayer("Exit"))
                | (1 << LayerMask.NameToLayer("Trap"));

            var collision = Physics.OverlapSphere(spawnPos, 0.5f /* 여기 부분 왜 상수로 하면 잘 되는거지... */, layer);
                                                                 /* 이 값으로 얼마나 떨어져 있을지 정함*/
            if (collision.Length == 0)  // 주변에 겹치는 게 없다면
            {
                var newTrap = Instantiate(trapPrefab, spawnPos, Quaternion.identity);

                traps.Add(newTrap);
            }
            if (count > 100000)        // 무한 루프 방지용... 이 부분은 수정될 가능성이 큼
            {
                break;
            }
        }
        ShowTrapPos();
        // 랜덤으로 함정 만들기

        var newPos = (playerPos + exitPos) / 2;

        Instantiate(bigPrefab, new Vector3(newPos.x, playerPos.y - 10f, newPos.z)
            , Quaternion.identity);
    }
}
