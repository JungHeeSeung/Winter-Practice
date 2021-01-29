using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChange : MonoBehaviour
{
    public float spd = 0.1f;

    public Material mat;
    [SerializeField]
    private bool isChange = false;

    public List<MeshRenderer> Childrens = new List<MeshRenderer>();

    [SerializeField]
    public List<GameObject> target = new List<GameObject>();

    public int targetIndex = 0;


    private void Start()
    {
        // List 에 LIst 넣기...
        // 생각보다 까다로움

        // 두개만 신경쓰자
        // 초기화를 신경써줄것 --> NULL이라도 넣어야 에러가 안 뜬다
        // NULL 체크 신경써줄것 --> NULL을 넣었는데 접근하려고 하면 에러가 뜸

        // 코드 리팩토링 할 때가 온 것 같다...
        for (int i=0;i<target.Count;++i)
        {
            //tarMat[i].list.Add(null);
        }

        // 구조 다시 잡기..
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
                   // Childrens[i].material = tarMat[targetIndex].list[i];
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

            List<MeshRenderer> temp = new List<MeshRenderer>();
            temp.AddRange(target[targetIndex].GetComponentsInChildren<MeshRenderer>());


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

            

            //if (tarMat[targetIndex].list.Count == 0)
            //{
            //    foreach (var child in Childrens)
            //    {
            //        tarMat[targetIndex].list.Add(child.material);
            //    }
            //}
        }


        // UP & DOWN : Scale || LEFT & RIGHT : Rotation
        {
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
        }
        //


        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
