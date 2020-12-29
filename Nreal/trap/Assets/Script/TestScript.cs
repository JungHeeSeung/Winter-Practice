using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject other;

    public float range = 0.1f;

    public float dis = 0f;

    public Material floorMaterial;

    public Material trapMaterial;


    // Update is called once per frame
    void Update()
    {
        if (other != null)
        {
            var newPos = new Vector2(this.transform.position.x, this.transform.position.z);
            var newPosOther = new Vector2(other.transform.position.x, other.transform.position.z);

            dis = Vector2.Distance(newPos, newPosOther);

            if (range >= dis)
            {
                Debug.Log("둘의 거리가" + dis + " 만큼 가깝다!");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var mt = GetComponent<MeshRenderer>();

            //var oldMT = mt.material;

            Debug.Log(floorMaterial.name + ", " + trapMaterial.name + ", " + mt.material.name);

            //mt.material = trapMaterial;
            //Debug.Log("Space 눌렸당");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }

   
}

