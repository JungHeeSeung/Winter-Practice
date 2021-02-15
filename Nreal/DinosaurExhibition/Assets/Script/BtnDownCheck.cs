using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDownCheck : MonoBehaviour
{
    public bool isDown = false;

    public void PointerDown()
    {
        isDown = true;
    }

    public void PointerUp()
    {
        isDown = false;
    }
}
