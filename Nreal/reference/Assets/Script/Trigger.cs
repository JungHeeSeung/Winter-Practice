using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 타이틀, 게임 오버 씬에서 trigger를 쏴서 맞추어 게임 씬 넘기기
public class Trigger : MonoBehaviour
{
    public Player player;
    private bool triggerOn = false;

    void Update()
    {
        if (!triggerOn)
        {
            transform.Rotate(Vector3.up, 1f);
            transform.position = player.transform.position + player.transform.forward * 7f; ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 큐브를 맞추면 setFloor 씬 로드
        Invoke("ChangeScene", 1.0f);
        GetComponent<ParticleSystem>().Play();
        triggerOn = true;
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("SetFloor");
    }
}
