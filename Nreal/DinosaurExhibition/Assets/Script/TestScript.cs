using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public float rotSpd = 1f;

    Vector3 oldPos;

    bool isMouseDown = false;

    float rotX;
    float rotY;

    // Update is called once per frame
    void Update()
    {
        // 마우스가 눌림
        if (Input.GetMouseButtonDown(0))
        {
            oldPos = Input.mousePosition;
            isMouseDown = true;
        }

        // 마우스가 떼짐
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        if (isMouseDown)
        {
            var newPos = Input.mousePosition;

            float deltaX = newPos.x - oldPos.x;
            float deltaY = newPos.y - oldPos.y;

            rotX += deltaX * rotSpd * Time.deltaTime;
            rotY += deltaY * rotSpd * Time.deltaTime;

            gameObject.transform.Rotate(Vector3.up, rotX);
            gameObject.transform.Rotate(Vector3.forward, rotY);
           
            //gameObject.transform.rotation = Quaternion.Euler(rotY, rotX, 0f);

            oldPos = newPos;

        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(transform.localScale);
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            Debug.Log(transform.localScale);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.up);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.right);
    }
}
