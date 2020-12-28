using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace NRKernal.NRExamples
{
    // 평면 트래킹 UI
    public class SetFloorUI : MonoBehaviour
    {
        public Text mainInfoText;
        public Button button;
        private Color plus;
        public PlaneDetector planeDetector;
        public HelloMRController helloMRController;
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

        // 평면 트래킹 이 끝난 후
        public void GotoGetCubePos()
        {
            mainInfoText.color = new Color(1, 1, 1, 0);
            mainInfoText.text = "Click on the floor to set the enemy's position.";
            planeDetector.ok = true;
            helloMRController.startGetPos = true;

            button.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            button.onClick.AddListener(GotoGameStart);
        }

        // pool position 선택이 끝난 후 게임 로드
        public void GotoGameStart()
        {
            foreach (var o in GameObject.FindObjectsOfType<NRTrackableBehaviour>())
            {
                o.gameObject.SetActive(false);
            }

            SceneManager.LoadScene("gogo");
        }
    }

}