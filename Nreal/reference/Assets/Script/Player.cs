using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace NRKernal.NRExamples
{
    public class Player : MonoBehaviour
    {
        //충동 횟수
        public int collisionCount = 0;
        
        // 충돌했을 때 타격 이미지
        public Image damaged;

        private void Awake()
        {
            transform.position = Camera.main.transform.position;
            transform.rotation = Camera.main.transform.rotation;
        }

        private void Update()
        {
            transform.position = Camera.main.transform.position;
            transform.rotation = Camera.main.transform.rotation;

            //3번 부딪히면 gameover
            if(collisionCount > 2)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "DroidBullet(Clone)")
            {
                damaged.gameObject.SetActive(true);
                collisionCount += 1;
                // 0.7초 동안 타격 피해 이미지 표시
                Invoke("damegedImageControll", 0.7f);
            }
        } 
        private void damegedImageControll()
        {
            damaged.gameObject.SetActive(false);
        }
    }
}