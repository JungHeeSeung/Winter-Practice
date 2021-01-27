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


    // Grid <-> Texture 변경용

    public Material mat;    // Grid용

    private List<Material> m_Mat = new List<Material>();    // 원본

    private bool isChange = false;

    private List<MeshRenderer> Childrens = new List<MeshRenderer>();    // 대상

    // Grid <-> Texture 변경용

    // Update is called once per frame
    void Update()
    {
        target = trackingImage.data;

        foreach (var TargetVal in target.Values)
        {
            if (TargetVal.Image != null)
            {
                var img = TargetVal.Image.GetDataBaseIndex();

                //      Rotation & Single Touch                      //
                if (Input.touchCount == 1)
                {
                    RotateObj(TargetVal.Obj[img]);

                }
                //      Rotation With Single Touch                   //



                //      Zoom in & out With Double Touch              // 
                if (Input.touchCount == 2)
                {
                    ZoomInAndOutObj(TargetVal.Obj[img]);

                }
                //      Zoom in & out With Double Touch              // 



                //      Grid <-> Texture With Home Button            //
                if (NRInput.GetButtonDown(ControllerButton.HOME))
                {
                    SwapTextureObj(TargetVal.Obj[img]);
                }
                //      Grid <-> Texture With Home Button            //

                if (NRInput.GetButtonDown(ControllerButton.APP))
                {
                    ResetObj(TargetVal.Obj[img]);
                }
            }
        }
    }


    void SwapTextureObj(GameObject obj)
    {
        if (Childrens.Count == 0)
        {
            Childrens.AddRange(obj.GetComponentsInChildren<MeshRenderer>());
        }

        foreach (var child in Childrens)
        {
            m_Mat.Add(child.material);
        }

        for (int i = 0; i < Childrens.Count; ++i)
        {
            if (false == isChange)
            {
                Childrens[i].material = mat;

            }
            else
            {
                Childrens[i].material = m_Mat[i];
            }
        }
        isChange = !isChange;
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

    void ResetObj(GameObject Obj)
    {
        Obj.transform.rotation = Quaternion.identity;
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}