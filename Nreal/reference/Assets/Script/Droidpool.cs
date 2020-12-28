using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace NRKernal.NRExamples
{
    public class Droidpool : MonoBehaviour
    {
        //프리팹
        public Droid prefab_cube;

        public Player player;
        // Pool
        public List<Droid> cubePool = new List<Droid>();

        //내가 생성할 갯수
        private readonly int cubeMaxCount = 15;

        //현재 장전된 인덱스
        private int curCubeIndex = 0;
        private IEnumerator coroutine;

        void Awake()
        {
            // 10개 미리 생성
            for (int i = 0; i < cubeMaxCount; ++i)
            {
                Droid b = Instantiate<Droid>(prefab_cube);

                //발사하기 전까지는 비활성화 해준다.
                b.gameObject.SetActive(false);

                cubePool.Add(b);
                b.transform.parent = this.transform;
            }
            player = GameObject.Find("Player").GetComponent<Player>();
            coroutine = FireCube();
        }

        private void Start()
        {
            StartCoroutine(coroutine);
        }

        private void Update()
        {
            foreach(Droid droid in cubePool)
            {
                if (droid.gameObject.activeSelf)
                    return;
            }
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }

        // 발사
        IEnumerator FireCube()
        {
            WaitForSeconds waitForSec = new WaitForSeconds(Random.Range(5.0f, 9.0f));
            while (true)
            {
                //발사되어야할 순번이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
                if (cubePool[curCubeIndex].gameObject.activeSelf)
                {
                    yield return waitForSec;
                }
                // 활성화 해주기
                cubePool[curCubeIndex].gameObject.SetActive(true);

                // 초기 위치
                //cubePool[curCubeIndex].gameObject.transform.position = this.transform.position;
                cubePool[curCubeIndex].arrivalPoint = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-0.5f, 0.5f),
                    Random.Range((player.transform.position - transform.position).normalized.z, (player.transform.position - transform.position).normalized.z*2));
                // 발사
                //if (ray.HoverEventData.pointerCurrentRaycast.isValid)
                //    bulletPool[curBulletIndex].GetComponent<Rigidbody>().AddForce(ray.HoverEventData.pointerCurrentRaycast.worldPosition.normalized * 500);
                //else

                //cubePool[curCubeIndex].GetComponent<Rigidbody>().AddForce((player.transform.position - cubePool[curCubeIndex].transform.position) * Random.Range(8.0f, 15.0f));

                if (curCubeIndex >= cubeMaxCount - 1)
                {
                    curCubeIndex = 0;
                }
                else
                {
                    curCubeIndex++;
                }
                yield return waitForSec;
            }
        }
    }
}