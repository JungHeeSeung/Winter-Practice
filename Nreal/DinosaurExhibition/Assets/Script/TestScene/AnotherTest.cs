using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTest : MonoBehaviour
{
    // Grid <-> Texture 변경용

    public Material mat;    // Grid용

    private Dictionary<int, List<Material>> originMat = new Dictionary<int, List<Material>>();    // 원본


    public List<MeshRenderer> Childrens = new List<MeshRenderer>();    // 대상

    public bool isTexture = true;


    public int idx = 0;
    public GameObject obj;

   
    public List<Material> vals = new List<Material>();

    void renewal()
    {
        vals.Clear();
        foreach(var temp in originMat.Values)
        {
            foreach(var realtemp in temp)
            {
                vals.Add(realtemp);
            }
        }
    }

    void extractTexture(int idx, GameObject obj)
    {
        List<Material> mt = null;


        if (false == originMat.TryGetValue(idx, out mt))
        {
            mt = new List<Material>();
        }
        else
        {
            return;
        }


        Childrens.Clear();
        Childrens.AddRange(obj.GetComponentsInChildren<MeshRenderer>());

        foreach (var child in Childrens)
        {
            mt.Add(child.material);
        }
        originMat.Add(idx, mt);


    }

    void DrawTexture(int idx, GameObject obj)
    {
        extractTexture(idx, obj);

        for (int i = 0; i < Childrens.Count; ++i)
        {
            if (originMat[idx].Count <= i)
            {
                Childrens[i].material = originMat[idx][originMat[idx].Count - 1];
            }
            else
            {
                Childrens[i].material = originMat[idx][i];
            }
        }
    }

    void DrawGrid(int idx, GameObject obj)
    {
        extractTexture(idx, obj);

        for (int i = 0; i < Childrens.Count; ++i)
        {
            Childrens[i].material = mat;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isTexture = !isTexture;
            if (isTexture)
            {
                DrawTexture(idx, obj);
            }
            else
            {
                DrawGrid(idx, obj);
            }
            renewal();
        }
    }
}
