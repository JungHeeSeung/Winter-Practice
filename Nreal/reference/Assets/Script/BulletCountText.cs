using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//플레이어 총알 개수 UI 텍스트
public class BulletCountText : MonoBehaviour
{
    private Text text;
    public Bulletpool bulletpool;
    public int count;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        count = 11 - bulletpool.GetComponentsInChildren<Transform>().GetLength(0);
        text.text = count.ToString();
    }
}
