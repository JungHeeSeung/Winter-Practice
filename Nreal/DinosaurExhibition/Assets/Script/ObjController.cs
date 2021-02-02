using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NRKernal;
using NRKernal.NRExamples;

using UnityEngine.UI;

public class ObjController : MonoBehaviour
{
    public TrackingImageExampleController trackingImage;

    public float rotSpd = 0.1f;

    private Dictionary<int, TrackingImageVisualizer> target;


    // Grid <-> Texture 변경용

    public Material mat;    // Grid용

    private List<Material> originMat = new List<Material>();    // 원본

    public State oldState = State.texture;

    private List<MeshRenderer> Childrens = new List<MeshRenderer>();    // 대상

    // Grid <-> Texture 변경용

    void Update()
    {
        target = trackingImage.data;

        var list = new List<TrackingImageVisualizer>(target.Values);

        TrackingImageVisualizer TargetVal = null;

        if (list.Count != 0)
        {
            TargetVal = list[0];

            //      Rotation                       //
            if (TargetVal.state == State.rotate)
            {
                RotateObj(TargetVal.Obj);

            }
            //      Rotation                        //


            //      Zoom in & out              // 
            if (TargetVal.state == State.scale)
            {
                ZoomInAndOutObj(TargetVal.Obj);

            }
            //      Zoom in & out               // 


            //      Grid <-> Texture            //

            SwapTextureObj(TargetVal.Obj, TargetVal.DrawState);

            //      Grid <-> Texture            //

            if (NRInput.GetButtonDown(ControllerButton.HOME))
            {
                ResetObj(TargetVal.Obj);
                TargetVal.ResetState();
            }
        }
    }


    void SwapTextureObj(GameObject obj, State state)
    {
        if(state == oldState)
        {
            return;
        }


        List<MeshRenderer> temp = new List<MeshRenderer>();
        temp.AddRange(obj.GetComponentsInChildren<MeshRenderer>());


        if (Childrens.Count == 0)
        {
            Childrens = temp;
        }
        else
        {
            if (false == Childrens.Equals(temp))
            {
                Childrens.Clear();
                Childrens = temp;
            }
        }

        // 여기 못 고치면 답이 없다
        foreach (var child in Childrens)
        {
            if (originMat.Count == 0)
            {
                originMat.Add(child.material);
            }
            else
            {
                break;
            }
        }


        for (int i = 0; i < Childrens.Count; ++i)
        {
            if (State.grid == state)
            {
                Childrens[i].material = mat;
            }
            else
            {
                Childrens[i].material = originMat[i];
            }
        }

        oldState = state;
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


        if ((obj.transform.localScale + delta).magnitude > 0)
        {
            obj.transform.localScale += delta;
        }
    }

    void RotateObj(GameObject obj)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            // deltaPosition으로 한 방에 해결이 가능한가?
            float deltaX = touch.deltaPosition.x;

            float rotX = deltaX * Time.deltaTime * rotSpd;

            obj.transform.Rotate(Vector3.up, rotX);
        }
    }

    void ResetObj(GameObject Obj)
    {
        Obj.transform.rotation = Quaternion.identity;
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}