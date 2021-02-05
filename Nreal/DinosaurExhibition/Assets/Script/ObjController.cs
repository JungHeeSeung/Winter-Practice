using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using NRKernal.NRExamples;

using UnityEngine.UI;

public class ObjController : MonoBehaviour
{
    public TrackingImageExampleController trackingImage;

    public float rotSpd = 10f;
    public float zoomSpd = 10f;

    private Dictionary<int, TrackingImageVisualizer> target;


    // Grid <-> Texture 변경용

    public Material mat;    // Grid용

    private List<Material> originMat = new List<Material>();    // 원본


    private List<MeshRenderer> Childrens = new List<MeshRenderer>();    // 대상

    // Grid <-> Texture 변경용

    public Text text;
    private int count = 0;

    void Update()
    {
        target = trackingImage.data;

        foreach (var TargetVal in target.Values)
        {
            var obj = TargetVal.Obj;


            RotateObjWithTouch(obj);
            ZoomInAndOutObjWithTouch(obj);


            if (true == TargetVal.ui.Rotate.isOn)
            {
                RotateObj(obj);
            }

            if (true == TargetVal.ui.In.isDown)
            {
                ZoomIn(obj);
            }

            if (true == TargetVal.ui.Out.isDown)
            {
                ZoomOut(obj);
            }

            if (true == TargetVal.ui.DrawMode.isOn)
            {
                DrawTexture(obj);
            }
            else
            {
                DrawGrid(obj);
            }


            if (true == TargetVal.ui.reset.isDown)
            {
                ResetObj(obj);

                if (TargetVal.ui.DrawMode.isOn == false)
                {
                    TargetVal.ui.DrawMode.isOn = true;
                }
            }

        }
    }

    void extractTexture(GameObject obj)
    {
        Childrens.Clear();
        Childrens.AddRange(obj.GetComponentsInChildren<MeshRenderer>());

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
            if (originMat.Count <= i)
            {
                Childrens[i].material = originMat[originMat.Count - 1];
            }
            else
            {
                Childrens[i].material = originMat[i];
            }

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

    void ZoomIn(GameObject obj)
    {
        var diff = Time.deltaTime * zoomSpd;


        Vector3 delta = new Vector3(diff, diff, diff);

        obj.transform.localScale = (obj.transform.localScale + delta).magnitude < 10f ?
              (obj.transform.localScale + delta) : obj.transform.localScale;
    }

    void ZoomOut(GameObject obj)
    {
        var diff = Time.deltaTime * zoomSpd;

        Vector3 delta = new Vector3(diff, diff, diff);

        obj.transform.localScale = (obj.transform.localScale - delta).magnitude > 0.5f ?
              (obj.transform.localScale - delta) : obj.transform.localScale;

    }

    void RotateObj(GameObject obj)
    {
        float rotX = Time.deltaTime * rotSpd * 10;

        obj.transform.Rotate(Vector3.up, rotX);
    }

    void ZoomInAndOutObjWithTouch(GameObject obj)
    {
        if (Input.touchCount != 2)
        {
            return;
        }

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

    void RotateObjWithTouch(GameObject obj)
    {
        if (Input.touchCount != 1)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {

            float deltaX = touch.deltaPosition.x;
            float deltaY = touch.deltaPosition.y;

            float rotX = deltaX * Time.deltaTime * rotSpd;
            float rotY = deltaY * Time.deltaTime * rotSpd;

            obj.transform.Rotate(Vector3.up, rotX);
            obj.transform.Rotate(Vector3.forward, rotY);
        }
    }

    public void ResetObj(GameObject Obj)
    {
        Obj.transform.rotation = Quaternion.identity;
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}