using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugUI : MonoBehaviour
{
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string contents = "Input: (" + Mathf.Floor(Input.GetAxis("Horizontal")) + ", " + Mathf.Floor(Input.GetAxis("Vertical")) + ")";
        contents += "\nMouse: (" + Mathf.Floor(Input.GetAxis("Mouse X")*10f) + ", " + Mathf.Floor(Input.GetAxis("Mouse Y")*10f) + ")";
        text.text = contents;
    }
}
