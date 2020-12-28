using System.Collections.Generic;
using UnityEngine;

namespace NRKernal.NRExamples
{
    // Droid의 fireball pool
    public class DroidBulletpool : MonoBehaviour
    {
        //fireball 프리팹
        public DroidBullet prefab_cube;

        public Player player;

        //fireball Pool
        public List<DroidBullet> cubePool = new List<DroidBullet>();

        //생성할 fireball 갯수
        private readonly int cubeMaxCount = 5;

        //현재 장전된 fireball의 인덱스
        private int curCubeIndex = 0;

        void Awake()
        {
            //fireball 미리 생성
            for (int i = 0; i < cubeMaxCount; ++i)
            {
                DroidBullet b = Instantiate<DroidBullet>(prefab_cube);

                //fireball 발사하기 전까지는 비활성화 해준다.
                b.gameObject.SetActive(false);

                cubePool.Add(b);
                transform.parent = this.transform;
            }
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        private void OnEnable()
        {
            // Droid 생성 후 1초 뒤에 fireball 발사
            Invoke("Fireball", 1.0f);
        }

        //fireball 발사
        void Fireball()
        {
            //발사되어야할 순번의 fireball이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
            if (!transform.parent.gameObject.activeSelf)
                return;
            if (cubePool[curCubeIndex].gameObject.activeSelf)
            {
                return;
            }
            //fireball 활성화 해주기
            cubePool[curCubeIndex].gameObject.SetActive(true);

            //fireball 초기 위치
            cubePool[curCubeIndex].gameObject.transform.position = this.transform.position;

            // 플레이어를 향해 fireball 발사
            cubePool[curCubeIndex].GetComponent<Rigidbody>().AddForce((player.transform.position - cubePool[curCubeIndex].transform.position).normalized * Random.Range(180.0f, 250.0f));

            // fireball을 다 사용하면 초기화
            if (curCubeIndex >= cubeMaxCount - 1)
            {
                curCubeIndex = 0;
            }
            else
            {
                curCubeIndex++;
            }
            // fireball이 발사되면 색을 하얗게 함
            transform.parent.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
            Invoke("Fireball", Random.Range(2.5f, 3.5f));
        }
    }
}