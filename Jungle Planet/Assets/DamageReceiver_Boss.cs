using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver_Boss : DamageReceiver
{
    private BossFight1 myBf;
    void Start()
    {
        myBf = GameObject.FindWithTag("Boss").GetComponent<BossFight1>();
    }
    public override void ReceiveDamage(float amount)
    {
        myBf.BossTakeDamage(amount);
    }
}
