using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interactable : MonoBehaviour
{
    public string[] methodsToCallOnInteract;
    public string description = "a door";
    public GameObject objectToSendMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract() {
        foreach (string s in methodsToCallOnInteract) {
            objectToSendMessage.SendMessage(s);
        }
    }

    public string GetDescription() {
        return description;
    }

    public void SetDescription(string s) {
        description = s;
    }
}
