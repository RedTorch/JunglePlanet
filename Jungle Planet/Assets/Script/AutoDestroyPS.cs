using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyPS : MonoBehaviour
{
    [SerializeField] private float timer = 20f;
    [SerializeField] private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        // If there is a particle system, override the given Time and destroy when particle system is done
        ps = gameObject.GetComponent<ParticleSystem>();
        if(gameObject.GetComponent<Animator>() != null) {
            timer = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ps == null) {
            timer -= Time.deltaTime;
            if(timer <= 0f) {
                Destroy(gameObject);
            }
        }
        else {
            if (!ps.IsAlive()) {
                Destroy(gameObject);
            }
        }
    }
}
