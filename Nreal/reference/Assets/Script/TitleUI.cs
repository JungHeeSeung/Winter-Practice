using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 타이틀 화면 UI
public class TitleUI : MonoBehaviour
{
    public Text mainInfoText;
    private Color plus;
    private void OnEnable()
    {
        mainInfoText.color = new Color(1, 1, 1, 0);
        plus = new Color(0, 0, 0, 0.01f);
    }

    void Update()
    {
        // text fade in
        mainInfoText.color += plus;
        FollowMainCam();
    }

    private void FollowMainCam()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
    }
}
