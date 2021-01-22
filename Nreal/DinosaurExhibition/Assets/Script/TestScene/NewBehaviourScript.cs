using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Mesh mesh;

    public LineRenderer line;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            MakeGrid();
        }
    }

    void MakeGrid()
    {
        var vertex = mesh.vertices;
        var triangle = mesh.triangles;


        foreach (var vtx in vertex)
        {

            {
                line.SetPosition(line.positionCount - 1, vtx);
                line.positionCount++;

            }

        }
    }

}


