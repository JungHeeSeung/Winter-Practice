using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRKernal.NRExamples
{
    // 충동 횟수 UI
    public class CollisionUI : MonoBehaviour
    {
        public Player player;
        private Image image;
        private float remove;

        void Start()
        {
            image = GetComponent<Image>();
            remove = 0.35f;
        }

        void Update()
        {
            image.fillAmount = (player.collisionCount * remove);
        }
    }

}