using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTest : MonoBehaviour
{
    public List<GameObject> target = new List<GameObject>();

    public List<canvasUI> vList;

    public canvasUI UI;

    public enum State
    {
        None,
        rotate,
        scale,
        texture,
        grid
    }

    private void Start()
    {
        for (int i = 0; i < target.Count; ++i)
        {
            var ui = Instantiate(UI);

            ui.transform.SetParent(target[i].transform, false);
            ui.transform.position = target[i].transform.position;

            ui.rotate.onValueChanged.RemoveAllListeners();
            ui.scale.onValueChanged.RemoveAllListeners();
            ui.texture.onValueChanged.RemoveAllListeners();
            ui.wireFrame.onValueChanged.RemoveAllListeners();

            ui.rotate.onValueChanged.AddListener(SetRotation);
            ui.scale.onValueChanged.AddListener(SetScale);
            ui.texture.onValueChanged.AddListener(SetTexture);
            ui.wireFrame.onValueChanged.AddListener(SetGrid);


            vList.Add(ui);
        }

    }

    public State state = State.None;

    public State DrawState = State.texture;

    void SetRotation(bool isSelected)
    {
        if (isSelected)
        {
            state = State.rotate;
        }
    }
    void SetScale(bool isSelected)
    {
        if (isSelected)
        {
            state = State.scale;
        }
    }
    void SetTexture(bool isSelected)
    {
        if (isSelected)
        {
            DrawState = State.texture;
        }
    }
    void SetGrid(bool isSelected)
    {
        if (isSelected)
        {
            DrawState = State.grid;
        }
    }

    public void ResetState()
    {
        state = State.None;
        DrawState = State.texture;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            state = State.None;

            foreach (var lst in vList)
            {
                lst.rotate.isOn = !lst.rotate.isOn;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(state);
            Debug.Log(DrawState);

        }
    }
}
