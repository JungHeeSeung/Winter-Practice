using System.Collections.Generic;
using UnityEngine;

// 플레이어 총알 pool
namespace NRKernal.NRExamples
{
    public class Bulletpool : MonoBehaviour
    {
        //총알 프리팹
        [SerializeField]
        private Bullet prefab_bullet;

        [SerializeField]
        private Player player;

        // 총
        private GameObject gun;

        //총알 Pool
        [SerializeField]
        private List<Bullet> bulletPool = new List<Bullet>();

        //점수
        [SerializeField]
        private GameObject score;

        //내가 생성할 총알 갯수
        private readonly int bulletMaxCount = 10;

        //현재 장전된 총알의 인덱스
        private int curBulletIndex = 0;

        void Awake()
        {
            //총알 미리 생성
            for (int i = 0; i < bulletMaxCount; ++i)
            {
                Bullet b = Instantiate<Bullet>(prefab_bullet);

                //총알 발사하기 전까지는 비활성화 해준다.
                b.gameObject.SetActive(false);

                bulletPool.Add(b);
                b.transform.parent = this.transform;
            }
            gun = GameObject.Find("gun_dummy");
        }

        void Update()
        {
            FireBulet(); 
        }

        //총알 발사
        void FireBulet()
        {
            //클릭 할 때마다 총알 발사
            if (NRInput.GetButtonDown(ControllerButton.TRIGGER))
            {
                //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
                if (bulletPool[curBulletIndex].gameObject.activeSelf)
                {
                    return;
                }

                if (score != null)
                    score.gameObject.GetComponent<Score>().score -= 30;
                
                //총알 활성화 해주기
                bulletPool[curBulletIndex].gameObject.SetActive(true);

                //총알 초기 위치는 플레이어랑 같게
                bulletPool[curBulletIndex].gameObject.transform.position = gun.transform.position;
                bulletPool[curBulletIndex].gameObject.transform.rotation = gun.transform.rotation;


                //총알 발사
                Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : ControllerAnchorEnum.RightLaserAnchor);

                // 레이캐스트 검사
                RaycastHit hitResult;
                if(Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
                    bulletPool[curBulletIndex].GetComponent<Rigidbody>().AddForce((hitResult.point - gun.transform.position).normalized * 2300);
                else
                    bulletPool[curBulletIndex].GetComponent<Rigidbody>().AddForce(laserAnchor.transform.forward  * 2300);


                //방금 9번째 총알을 발사했다면 다시 0번째 총알을 발사할 준비를 한다.
                if (curBulletIndex >= bulletMaxCount - 1)
                {
                    curBulletIndex = 0;
                }
                else
                {
                    curBulletIndex++;
                }
            }
        }
    }

}