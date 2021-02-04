﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using NRKernal.NRExamples;

using UnityEngine.UI;

public class ObjController : MonoBehaviour
{
    public TrackingImageExampleController trackingImage;

    public float rotSpd = 100f;
    public float zoomSpd = 10f;

    private Dictionary<int, TrackingImageVisualizer> target;


    // Grid <-> Texture 변경용

    public Material mat;    // Grid용

    private List<Material> originMat = new List<Material>();    // 원본


    private List<MeshRenderer> Childrens = new List<MeshRenderer>();    // 대상

    // Grid <-> Texture 변경용

    public Text text;

    void Update()
    {
        target = trackingImage.data;

        foreach (var TargetVal in target.Values)
        {
            if (true == TargetVal.ui.Rotate.isOn)
            {
                RotateObj(TargetVal.Obj);
            }

            if (true == TargetVal.ui.In.isDown)
            {
                ZoomIn(TargetVal.Obj);
            }

            if (true == TargetVal.ui.Out.isDown)
            {
                ZoomOut(TargetVal.Obj);
            }

            if (true == TargetVal.ui.DrawMode.isOn)
            {
                DrawTexture(TargetVal.Obj);
            }
            else
            {
                DrawGrid(TargetVal.Obj);
            }

            if (true == TargetVal.ui.reset.isDown)
            {
                ResetObj(TargetVal.Obj);
                TargetVal.ui.DrawMode.isOn = true;
            }

            text.text = "Zoom IN is " + TargetVal.ui.In.isDown;
            text.text += "Texture Mode is " + TargetVal.ui.DrawMode.isOn;
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

    public void ResetObj(GameObject Obj)
    {
        Obj.transform.rotation = Quaternion.identity;
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}