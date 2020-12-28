using UnityEngine;
using UnityEngine.EventSystems;


namespace NRKernal.NRExamples
{
    // Droid의 총알 class
    public class DroidBullet : MonoBehaviour
    {
        private MeshRenderer m_MeshRender;
        void Awake()
        {
            m_MeshRender = transform.GetComponent<MeshRenderer>();
        }
        public void OnEnable()
        {
            //5초 사용
            Invoke("Destory", 5.0f);
        }
        void Destory()
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 플레이어와 닿으면 사용 중지
            if (collision.gameObject.name == "Player")
            {
                gameObject.SetActive(false);
            }
        }
    }

}