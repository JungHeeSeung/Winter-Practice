using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이어 총알 개수 UI
public class BulletCountUI : MonoBehaviour
{
    public BulletCountText text;
    private Image image;
    private float remove;
    void Start()
    {
        image = GetComponent<Image>();
        remove = 0.1f;
    }

    void Update()
    {
        image.fillAmount = text.count * remove;
    }
}
