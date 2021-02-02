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


    private List<MeshRenderer> Childrens = new List<MeshRenderer>();    // 대상

    // Grid <-> Texture 변경용



    void Update()
    {
        target = trackingImage.data;

        // key value 쌍에 대해 접근이 잘못되고 있는 것 같음..
        // Key를 바탕으로 Value를 찾아야 하나...??
        foreach (var TargetVal in target.Values)
        {
            if (TargetVal.Image != null)
            {
                switch (TargetVal.state)
                {
                    case State.Rotate:
                        RotateObj(TargetVal.Obj);
                        break;
                    case State.Scaled:
                        ZoomInAndOutObj(TargetVal.Obj);
                        break;
                    default:
                        break;
                }

                switch (TargetVal.drawState)
                {
                    case State.Texture:
                        DrawTexture(TargetVal.Obj);
                        break;
                    case State.WireFrame:
                        DrawGrid(TargetVal.Obj);
                        break;
                    default:
                        break;
                }
            }

        }
    }

    void extractTexture(GameObject obj)
    {
        if (Childrens.Count == 0)
        {
            Childrens.AddRange(obj.GetComponentsInChildren<MeshRenderer>());
        }
        else
        {
            return;
        }

        foreach (var child in Childrens)
        {
            if (originMat.Count == 0)
            {
                originMat.Add(child.material);
            }
            else
            {
                return;
            }
        }
    }

    void DrawTexture(GameObject obj)
    {
        extractTexture(obj);

        for (int i = 0; i < Childrens.Count; ++i)
        {
            Childrens[i].material = originMat[i];
        }
    }

    void DrawGrid(GameObject obj)
    {
        extractTexture(obj);

        for (int i = 0; i < Childrens.Count; ++i)
        {
            Childrens[i].material = mat;
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

            float deltaX = touch.deltaPosition.x;
            float rotX = deltaX * Time.deltaTime * rotSpd;

            obj.transform.Rotate(Vector3.up, rotX);
        }
    }

    void ResetObj(int idx, GameObject Obj)
    {
        Obj.transform.rotation = Quaternion.identity;
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}