using System.Collections;
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

 
    public UnityEngine.Events.UnityEvent startCapture;
    public UnityEngine.Events.UnityEvent stopCapture;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    
    public void LoadMainScene()
    {
        var pd = GetComponent<PlaneDetector>();
        var MRct = GetComponent<HelloMRController>();

        // 씬 넘어가면서 평면트래킹과 MRcontroller를 비활성화 시킨다
        pd.enabled = false;
        MRct.enabled = false;

        SceneManager.LoadScene("MainScene");
    }

    private void Update()
    {
        if(NRInput.GetButtonDown(ControllerButton.APP) || (Input.GetKeyDown(KeyCode.Return)))
        {
            stopCapture.Invoke();
        }
        if (NRInput.GetButtonDown(ControllerButton.HOME) || (Input.GetKeyDown(KeyCode.Space)))
        {
            startCapture.Invoke();
        }
        if(Input.touchCount >=2 )
        {
            LoadMainScene();
        }
    }

  
}
