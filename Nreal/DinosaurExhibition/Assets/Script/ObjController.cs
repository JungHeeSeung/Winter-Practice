using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NRKernal;
using NRKernal.NRExamples;

public class ObjController : MonoBehaviour
{
    public TrackingImageExampleController trackingImage;

    public float rotSpd = 0.1f;

    private Dictionary<int, TrackingImageVisualizer> target;

    // Update is called once per frame
    void Update()
    {
        target = trackingImage.data;

        if (target != null)
        {
            // 딕셔너리의 밸류를 가져왔음
            // 아마도.. 여러 종류 이미지 트래킹이 필요하면 바뀔지도...??

            foreach (var TargetVal in target.Values)
            {
                if (TargetVal.Image != null)
                {
                    // Rotation & Single Touch //
                    if (Input.touchCount == 1)
                    {
                        if (false == TargetVal.Obj.activeSelf && true == TargetVal.GridObj.activeSelf)
                        {
                            RotateObj(TargetVal.GridObj);
                        }
                        else if (false == TargetVal.GridObj.activeSelf && true == TargetVal.Obj.activeSelf)
                        {
                            RotateObj(TargetVal.Obj);
                        }
                    }
                    // Rotation With Single Touch //


                    // Zoom in & out With Double Touch // 
                    if (Input.touchCount == 2)
                    {
                        if (false == TargetVal.Obj.activeSelf && true == TargetVal.GridObj.activeSelf)
                        {
                            ZoomInAndOutObj(TargetVal.GridObj);
                        }
                        else if (false == TargetVal.GridObj.activeSelf && true == TargetVal.Obj.activeSelf)
                        {
                            ZoomInAndOutObj(TargetVal.Obj);
                        }
                    }
                    // Zoom in & out With Double Touch // 

                    // -----------------------------테스트용

                    if(Input.touchCount == 3)
                    {
                        TargetVal.Obj.SetActive(!TargetVal.Obj.activeSelf);
                    }






                    // -----------------------------테스트용

                    // -----------------------------에러 나서 죽음

                    // Change Texture <-> Grid Mode With Triple Touch // 
                    //if (Input.touchCount == 3)
                    //{
                    //    if (false == TargetVal.Obj.activeSelf && true == TargetVal.GridObj.activeSelf)
                    //    {
                    //        TargetVal.Obj.SetActive(true);
                    //        TargetVal.GridObj.SetActive(false);
                    //    }
                    //    else if (false == TargetVal.GridObj.activeSelf && true == TargetVal.Obj.activeSelf)
                    //    {
                    //        TargetVal.GridObj.SetActive(true);
                    //        TargetVal.Obj.SetActive(false);
                    //    }
                    //    ResetObj();
                    //}
                    // Change Texture <-> Grid Mode With Triple Touch // 

                    // ----------------------------에러 나서 죽음

                }
            }

            if (NRInput.GetButton(ControllerButton.HOME))
            {
                ResetObj();
            }
        }
    }

    void ZoomInAndOutObj(GameObject obj)
    {
        Touch touchOne = Input.GetTouch(0);
        Touch touchTwo = Input.GetTouch(1);

        Vector2 oldPosTouchOne = touchOne.position - touchOne.deltaPosition;
        Vector2 oldPosTouchTwo = touchTwo.position - touchTwo.deltaPosition;

        float oldDis = (oldPosTouchOne - oldPosTouchTwo).magnitude;
        float newDis = (touchOne.position - touchTwo.position).magnitude;

        float diff = newDis - oldDis;

        Vector3 delta = new Vector3(diff * 0.01f, diff * 0.01f, diff * 0.01f);

        obj.transform.localScale += delta;
    }

    void RotateObj(GameObject obj)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            // deltaPosition으로 한 방에 해결이 가능한가?
            float deltaX = touch.deltaPosition.x;
            float deltaY = touch.deltaPosition.y;

            float rotX = deltaX * Time.deltaTime * rotSpd;
            float rotY = deltaY * Time.deltaTime * rotSpd;

            obj.transform.Rotate(Vector3.up, rotX);
            obj.transform.Rotate(Vector3.forward, rotY);
        }
    }

    void ResetObj()
    {
        foreach (var tar in target.Values)
        {
            if (true == tar.Obj.activeSelf)
            {
                tar.Obj.transform.rotation = Quaternion.identity;
                tar.Obj.transform.localScale = new Vector3(1f, 1f, 1f);
            }

            if (true == tar.GridObj.activeSelf)
            {
                tar.GridObj.transform.rotation = Quaternion.identity;
                tar.GridObj.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

}
