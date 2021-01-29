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

    public class MatList
    {
        public List<Material> list = new List<Material>();
    }

    private List<MatList> originMat = new List<MatList>();    // 원본

    private bool isChange = false;

    private List<MeshRenderer> Childrens = new List<MeshRenderer>();    // 대상

    void InitializeList(int cnt)
    {
        if (originMat.Count <= cnt)
        {
            for (int i = originMat.Count; i <= cnt; ++i)
            {
                originMat.Add(null);
            }
        }
    }

    // Grid <-> Texture 변경용



    // Debug
    public Text text;
    public List<int> key = new List<int>();
    public List<TrackingImageVisualizer> value = new List<TrackingImageVisualizer>();
    void ShowInsepctor(Dictionary<int, TrackingImageVisualizer> dat)
    {
        key.Clear();
        value.Clear();

        foreach (var pair in dat)
        {
            key.Add(pair.Key);
            value.Add(pair.Value);
        }
    }
    // Debug

    void Update()
    {
        target = trackingImage.data;



        // key value 쌍에 대해 접근이 잘못되고 있는 것 같음..
        // Key를 바탕으로 Value를 찾아야 하나...??
        foreach (var TargetVal in target.Values)
        {
            if (TargetVal.Image != null)
            {
                var img = TargetVal.Image.GetDataBaseIndex();

                InitializeList(TargetVal.Obj.Count);

                //      Rotation & Single Touch                      //
                if (Input.touchCount == 1)
                {
                    //RotateObj(TargetVal.Obj[img]);


                    // Debug 용....
                    ShowInsepctor(target);

                    for (int i = 0; (i < Mathf.Max(key.Count, value.Count)); ++i)
                    {
                        if (i < key.Count)
                        {
                            text.text += "key: " + key[i];
                        }
                        if (i < value.Count)
                        {
                            text.text += "value's key: " + value[i].Image.GetDataBaseIndex();
                        }
                    }

                    text.text += " " + target.Count + " in Dict \n";
                    // Debug 용....


                }
                //      Rotation With Single Touch                   //



                //      Zoom in & out With Double Touch              // 
                if (Input.touchCount == 2)
                {
                    //ZoomInAndOutObj(TargetVal.Obj[img]);

                    text.text = "";

                }
                //      Zoom in & out With Double Touch              // 



                //      Grid <-> Texture With Home Button            //
                if (NRInput.GetButtonDown(ControllerButton.HOME))
                {
                    SwapTextureObj(img, TargetVal.Obj[img]);
                }
                //      Grid <-> Texture With Home Button            //

                if (NRInput.GetButtonDown(ControllerButton.APP))
                {
                    ResetObj(img, TargetVal.Obj[img]);
                }
            }
            else
            {
                text.text = "Image is Null\n";
            }
        }
    }

    void SwapTextureObj(int idx, GameObject obj, bool reset = false)
    {
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
            if (originMat[idx] == null)
            {
                originMat[idx] = new MatList();
                originMat[idx].list.Add(child.material);
            }
            else
            {
                break;
            }
        }

        if (true == reset)
        {
            isChange = false;
        }


        for (int i = 0; i < Childrens.Count; ++i)
        {
            if (false == isChange)
            {
                Childrens[i].material = mat;

            }
            else
            {
                Childrens[i].material = originMat[idx].list[i];
            }
        }

        if (reset != true)
        {
            isChange = !isChange;
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
        SwapTextureObj(idx, Obj, true);
    }
}