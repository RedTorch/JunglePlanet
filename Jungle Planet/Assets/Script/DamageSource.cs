using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    private DamageReceiver dreceiver;
    // Start is called before the first frame update
    void Start()
    {
        dreceiver = gameObject.GetComponent<DamageReceiver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDamage() {
        return damageAmount;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<DamageReceiver>() != null && other.gameObject.tag != gameObject.tag) {
            other.gameObject.GetComponent<DamageReceiver>().ReceiveDamage(damageAmount);
            if(dreceiver != null) {
                dreceiver.OnKilled();
            }
        }
    }
}
