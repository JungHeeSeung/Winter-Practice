using UnityEngine;

namespace NRKernal.NRExamples
{
    // 플레이어 총알
    public class Bullet : MonoBehaviour
    {
        // Start is called before the first frame update
        public void OnEnable()
        {
            Invoke("Destory", 3.0f);
        }
        void Destory()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}