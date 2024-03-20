using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private float maxHp = 10f;
    [SerializeField] private bool destroyOnDeath = false;
    [SerializeField] private GameObject prefabToSpawnOnDeath;
    [SerializeField] HoverTextManager htman;
    private DoomCC myDcc;
    private float hp = 10f;
    private bool dead = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        hp = maxHp;
        myDcc = gameObject.GetComponent<DoomCC>();
        if(gameObject.tag == "Player")
        {
            print("gameObject player status: myDcc is null? " + (myDcc==null));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void Reset() {
        dead = false;
        hp = maxHp;
    }

    public void OnKilled() {
        // do the killed thing!
        // deactivate player capsule
        // show respawn button
        // print(gameObject.name + " has received fatal damage");
        if(prefabToSpawnOnDeath != null) {
            Instantiate(prefabToSpawnOnDeath, transform.position, Quaternion.identity);
        }
        // print("destroying " + gameObject.name);
        gameObject.SendMessage("playerDeath", null, SendMessageOptions.DontRequireReceiver);
        if(myDcc != null)
        {
            print("calling player death from DamageReceiver");
            myDcc.playerDeath();
        }
        if(destroyOnDeath) {
            Destroy(gameObject);
        }
    }

    public virtual void ReceiveDamage(float amount) {
        if(htman != null) {
            htman.ShowDamagedOverlay();
        }
        hp -= amount;
        if(hp <= 0) {
            dead = true;
            OnKilled();
        }
    }

    public string getDisplayText() {
        if(dead) {
            return "DEAD";
        }
        return (hp + " / " + maxHp);
    }
}
