﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.transform.position;
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
