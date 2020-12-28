using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;

// setFloor Scene에서 설정한 position을 적용하여 Droidpoold과 portal 프리팹의 position을 set
public class poolSet : MonoBehaviour
{
    //setFloor에서 넘어온 데이터
    Data Data;

    // 플레이어가 정한 droidpool의 posiotion
    // 여러 곳에 설치 가능
    private List<Vector3> poolPos;

    //portal 프리팹
    public GameObject poolPrefab;
    void Awake()
    {
        // Data를 찾을 수 없으면 position 임의 지정
        if(GameObject.Find("Data") == null)
        {
            transform.position = new Vector3(0, 0, 8);
            Instantiate<GameObject>(poolPrefab, new Vector3(0, 0, 8), Quaternion.identity);
            return;
        }

        Data = GameObject.Find("Data").GetComponent<Data>();
        poolPos = Data.poolPos;

        // Data를 찾았지만, 플레이어가 position을 정하지 않았을 때 position 임의 지정
        if (poolPos.Count == 0)
        {
            transform.position = new Vector3(0, 0, 8);
            Instantiate<GameObject>(poolPrefab, new Vector3(0, 0, 8), Quaternion.identity);
            return;
        }

        // 플레이어가 position을 정했을 때
        foreach (Vector3 Pos in poolPos)
        {
            Vector3 pos = Pos;
            // 플레이어와 눈높이가 맞는 곳에 portal이 생기도록 높이 조정
            pos.y += 1.0f;
            transform.position = pos;
            Instantiate<GameObject>(poolPrefab, pos, Quaternion.identity);
        }
    }
}
