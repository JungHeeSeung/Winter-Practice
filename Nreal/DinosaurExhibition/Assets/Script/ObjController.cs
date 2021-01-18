using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NRKernal;
using NRKernal.NRExamples;

public class ObjController : MonoBehaviour
{
    public TrackingImageExampleController trackingImage;

    private Touch OldTouch;

    public float rotSpd = 0.1f;
   
    // Update is called once per frame
    void Update()
    {
        var target = trackingImage.data;

        if (target != null)
        {
            // 딕셔너리의 밸류를 가져왔음
            // 아마도.. 여러 종류 이미지 트래킹이 필요하면 바뀔지도...??

            foreach (var TargetVal in target.Values)
            {
                if (TargetVal.Image != null)
                {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            OldTouch = touch;
                        }
                        else if(touch.phase == TouchPhase.Moved)
                        {

                            float deltaX = OldTouch.position.x - touch.position.x;
                            float deltaY = OldTouch.position.y - touch.position.y;

                            float rotX = deltaX * Time.deltaTime * rotSpd;
                            float rotY = deltaY * Time.deltaTime * rotSpd;
                         
                            TargetVal.Obj.transform.Rotate(Vector3.up, rotX);
                            TargetVal.Obj.transform.Rotate(Vector3.forward, rotY);

                            OldTouch = touch;
                        }
                    }
                }
            }

            if(NRInput.GetButton(ControllerButton.HOME))
            {
                foreach (var TargetVal in target.Values)
                {
                    TargetVal.Obj.transform.rotation = Quaternion.identity;
                }
            }
        }
    }

   
       
   
}
