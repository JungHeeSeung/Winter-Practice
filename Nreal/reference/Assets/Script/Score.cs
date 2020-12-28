using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NRKernal.NRExamples
{
    public class Score : MonoBehaviour
    {
        private Text text;
        public float score = 0;

        void Start()
        {
            text = GetComponent<Text>();
            // 2초마다 점수 상승
            StartCoroutine(Repeat(2.0f));
        }
        void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
            text.text = "Score : " + score.ToString();
        }

        private IEnumerator Repeat (float sec)
        {
            WaitForSeconds waitForSec = new WaitForSeconds(sec);
            while (true)
            {
                score += 10;
                yield return waitForSec;
            }
        }
    }
}