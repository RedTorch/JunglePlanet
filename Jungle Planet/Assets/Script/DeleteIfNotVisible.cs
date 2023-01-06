using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIfNotVisible : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private float killTime = 1f;
    private float renderTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckRenderers();
    }

    void CheckRenderers() {
        foreach(Renderer r in renderers) {
            if(r.isVisible) {
                renderTimer = 0f;
                return;
            }
        }
        renderTimer += Time.deltaTime;
        if(renderTimer>=killTime) {
            Destroy(gameObject);
        }
    }
}
