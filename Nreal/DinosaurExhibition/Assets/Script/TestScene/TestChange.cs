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

    private void Start()
    {
        Childrens.AddRange(GetComponentsInChildren<MeshRenderer>());

        foreach (var child in Childrens)
        {
            m_Mat.Add(child.material);
        }

        Debug.Log(Childrens.ToString());
    }



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

        if(Input.GetKey(KeyCode.RightArrow))
        {

            transform.Rotate(Vector3.up, spd*10);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.Rotate(Vector3.up, spd*10);
        }



        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
