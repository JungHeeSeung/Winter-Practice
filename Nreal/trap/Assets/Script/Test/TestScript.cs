using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float radius = 1f;

    public GameObject player;

    public GameObject exit;

    public GameObject trapPrefab;

    public List<GameObject> traps;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var exitPos = exit.transform.position;
            var playerPos = player.transform.position;

            var dis = exitPos - playerPos;

            Debug.Log(dis);

            //MakeTrap();
        }

    }

    public void MakeTrap()
    {
        var exitPos = exit.transform.position;
        var playerPos = player.transform.position;

        for (int i = 0; i < 10; ++i)
        {
            var ranPosX = Random.Range(exitPos.x, playerPos.x);
            var ranPosZ = Random.Range(exitPos.z, playerPos.z);

            Vector3 spawnPos = new Vector3(ranPosX, exitPos.y, ranPosZ);
            var layer = (1 << LayerMask.NameToLayer("Trap"))
                + (1 << LayerMask.NameToLayer("Portal")) + (1 << LayerMask.NameToLayer("Player"));


            var bound = trapPrefab.GetComponent<MeshRenderer>().bounds.size;
            var collision = Physics.OverlapSphere(spawnPos, bound.x, layer);

            if (collision.Length == 0)  // 주변에 겹치는 게 없다면
            {
                var newTrap = Instantiate(trapPrefab, spawnPos, Quaternion.identity);
                newTrap.transform.Rotate(90, 0, 0);

                traps.Add(newTrap);
            }
            else
            {
                foreach (var temp in collision)
                {
                   Debug.Log(LayerMask.LayerToName(temp.gameObject.layer) + "하고 충돌했음\n");
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

