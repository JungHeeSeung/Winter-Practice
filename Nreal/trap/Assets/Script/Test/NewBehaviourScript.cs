using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject exit;
    public List<GameObject> objs;
    public GameObject objPrefab;

    void Make()
    {
        var exitPos = exit.transform.position;
        var playerPos = player.transform.position;


        var exitXZ = new Vector2(exitPos.x, exitPos.z);
        var playerXZ = new Vector2(playerPos.x, playerPos.z);
        var center = (exitXZ + playerXZ) / 2;
        var size = Vector2.Distance(exitXZ,playerXZ)/2;
      
        Vector3 spawnPos = new Vector3(center.x, exitPos.y, center.y)  /* 플레이어와 출구의 중간 지점. 단, y값은 출구에 고정 */
                         + new Vector3(Random.Range(-size / 2, size / 2),   /* 그리고 랜덤 벡터값을 더하는데 두 점 절반지점 거리까지만 더한다 */
                           0,                                                   /* 그러면 사각형 범위 안에서 점을 랜덤으로 구할 수 있다 */
                           Random.Range(-size / 2, size / 2));
        //Vector3 spawn Pos = new Vector3(Random.Range(player.transform.position.x, exit.transform.position.x),
        //   exit.transform.position.y,
        //   Random.Range(player.transform.position.z, exit.transform.position.z));


        var collision = Physics.OverlapSphere(spawnPos, 0.5f /* 여기 부분 왜 상수로 하면 잘 되는거지... */ );
        /* Trap의 사이즈값*/
        if (collision.Length == 0)  // 주변에 겹치는 게 없다면
        {
            var newObj = Instantiate(objPrefab, spawnPos, Quaternion.identity);

            objs.Add(newObj);
        }

        
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Make();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
           foreach(var obj in objs)
            {
                Destroy(obj);
            }

            objs.Clear();
        }
    }
}
