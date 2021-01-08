﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NRKernal;
using NRKernal.NRExamples;

public class GameManage : MonoBehaviour
{
    public Player player;
    public ExitManager exitManger;

    private bool isRecord = false;

    public UnityEngine.Events.UnityEvent startCapture;
    public UnityEngine.Events.UnityEvent stopCapture;

    private PlaneDetector pd;
    private HelloMRController helloMR;

    public UI ui;

    private void Awake()
    {
        var objs = FindObjectsOfType<GameManage>();

        if (objs.Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }


        pd = GetComponent<PlaneDetector>();
        helloMR = GetComponent<HelloMRController>();
    }


    public void LoadMainScene()
    {
        // 씬 넘어가면서 평면트래킹과 MRcontroller를 비활성화 시킨다
        pd.enabled = false;
        helloMR.enabled = false;

        SceneManager.LoadScene("MainScene");
    }

    public void LoadStartScene()
    {
        // 다시 시작 화면으로 돌아오면 활성화 시켜준다
        pd.enabled = true;
        helloMR.enabled = true;

        SceneManager.LoadScene("StartScene");
    }

    void StartAndStopRecord()
    {
        if (false == isRecord)
        {
            startCapture.Invoke();
        }
        else
        {
            stopCapture.Invoke();
        }
        isRecord = !isRecord;
    }

    private void Update()
    {
        if (NRInput.GetButtonDown(ControllerButton.APP))
        {
            StartAndStopRecord();
        }
        if (NRInput.GetButtonDown(ControllerButton.HOME) || (Input.GetKeyDown(KeyCode.Return)))
        {
            if (SceneManager.GetActiveScene().name == "StartScene")
            {
                LoadMainScene();
            }
            else if (SceneManager.GetActiveScene().name == "MainScene")
            {
                LoadStartScene();
            }
        }
    }

    public void PlayerVictory()
    {
        ui.text.text = "Victory!";
    }

    public void PlayerDead()
    {
        ui.text.text = "You Died";
    }
}
