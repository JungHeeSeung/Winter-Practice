using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //public GameObject target;
    //public canvasUI ui;

    //public BtnDownCheck In;
    //public BtnDownCheck Out;

    //public float zoomSpd = 1f;
    //public float rotSpd = 10f;

    //private void Start()
    //{
    //    ui.Rotate.onValueChanged.AddListener(RotateObj);
    //    ui.ZoomIn.onClick.AddListener(ZoomIn);
    //    ui.ZoomOut.onClick.AddListener(ZoomOut);

    //    In = ui.ZoomIn.GetComponent<BtnDownCheck>();
    //    Out = ui.ZoomOut.GetComponent<BtnDownCheck>();

    //}

    //private void Update()
    //{
    //    ZoomIn();
    //    ZoomOut();
    //}

    //void ZoomIn()
    //{
    //    if (In.isDown)
    //    {

    //        var diff = Time.deltaTime * zoomSpd;

    //        Vector3 delta = new Vector3(diff * 0.01f, diff * 0.01f, diff * 0.01f);

    //        target.transform.localScale += delta;
    //    }
    //}

    //void ZoomOut()
    //{
    //    if (Out.isDown)
    //    {
    //        var diff = Time.deltaTime * zoomSpd;

    //        Vector3 delta = new Vector3(diff * 0.01f, diff * 0.01f, diff * 0.01f);

    //        target.transform.localScale -= delta;
    //    }
    //}

    //void RotateObj(bool isOn)
    //{
    //    if (isOn)
    //    {
    //        float rotX = Time.deltaTime * rotSpd;

    //        target.transform.Rotate(Vector3.up, rotX);
    //    }
    //}
}

