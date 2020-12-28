using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace NRKernal.NRExamples
{
    // Droid Class
    public class Droid : MonoBehaviour
    {
        //움직임 도착 지점
        public Vector3 arrivalPoint = Vector3.zero;
        private Player player;
        private Color color;
        void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            transform.position = transform.parent.transform.position;
        }
        public void OnEnable()
        {
            // 10초 사용
            Invoke("PlayDieMotion", 10.0f);
        }
        void Update()
        {
            // 항상 플레이어를 바라보도록 설정
            transform.LookAt(player.transform.position);

            // Droid가 fireball 발사까지 색 변화
            color = transform.GetChild(1).GetComponent<MeshRenderer>().material.color;
            color.g -= 0.005f;
            color.b -= 0.005f;
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = color;

            // 도착지점에 가까워지면 또다른 도착지점 지정
            if ((transform.position - arrivalPoint).sqrMagnitude <= 0.02)
            {
                if ((player.transform.position.z - transform.parent.transform.position.z) > 0)
                    arrivalPoint = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1),
                    UnityEngine.Random.Range(transform.parent.transform.position.z, transform.parent.transform.position.z + 2));
                else
                    arrivalPoint = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1),
                    UnityEngine.Random.Range(transform.parent.transform.position.z -2, transform.parent.transform.position.z));
                return;
            }
            
            // 도착지점으로 이동
            transform.position = Vector3.Slerp(transform.position, arrivalPoint, 0.01f);
        }

        void PlayDieMotion()
        {
            GetComponent<ParticleSystem>().Play();
            GetComponent<Rigidbody>().useGravity = true;
            Invoke("Destory", 0.5f);
        }
        void Destory()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = transform.parent.transform.position;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            //총알에 닿으면 죽음
            if (other.name == "Bullet(Clone)")
            {
                PlayDieMotion();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            // 다른 droid와 닿았을 시에 닿은 부분의 반대로 움직임
            if (other.name == "Droid(Clone)")
            {
                Vector3 vec = (transform.position - other.ClosestPointOnBounds(transform.position)).normalized * 0.1f;
                vec.z = 0;
                arrivalPoint += vec;
            }
        }
    }
}