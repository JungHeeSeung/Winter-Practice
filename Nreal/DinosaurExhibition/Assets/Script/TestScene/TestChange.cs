using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChange : MonoBehaviour
{
    public float spd = 0.1f;

    public Material mat;

    [SerializeField]
    private List<Material> m_Mat = new List<Material>();

    [SerializeField]
    private bool isChange = false;

    public List<MeshRenderer> Childrens = new List<MeshRenderer>();

    [SerializeField]
    public List<GameObject> target = new List<GameObject>();

    public int targetIndex = 0;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (targetIndex + 1 < target.Count)
            {
                targetIndex++;
            }
            else
            {
                targetIndex = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {

            List<MeshRenderer> temp = new List<MeshRenderer>();
            temp.AddRange(target[targetIndex].GetComponentsInChildren<MeshRenderer>());


            if (Childrens.Count == 0)
            {
                Childrens = temp;
            }
            else
            {
               if(false == Childrens.Equals(temp))
                {
                    Childrens.Clear();
                    Childrens = temp;
                }
            }


            foreach (var child in Childrens)
            {
                m_Mat.Add(child.material);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {

            Vector3 delta = new Vector3(spd, spd, spd);

            transform.localScale += delta;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 delta = new Vector3(spd, spd, spd);

            transform.localScale -= delta;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {

            transform.Rotate(Vector3.up, spd * 10);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.Rotate(Vector3.up, spd * 10);
        }



        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
