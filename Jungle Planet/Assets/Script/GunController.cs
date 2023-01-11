using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject GunRoot;
    [SerializeField] private GameObject GunModel;
    [SerializeField] private GameObject hitParticlesPrefab;
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private float RPM;
    private float fireInterval = 1f;
    private float fireCooldown = 0f;
    [SerializeField] private int MagSize;
    private int currMag;
    [SerializeField] private float ReloadTime;
    private float reloadCooldown;
    private bool isReloading;
    [SerializeField] private float FireRange;
    private RaycastHit hit;
    [SerializeField] private float Damage;
    [SerializeField] private Light MuzzleFlash;
    private float muzzleFlashBrightness;
    [SerializeField] private HoverTextManager htman;
    private bool gunIsEquipped = false;
    [SerializeField] private float KnockbackForce = 5f;

    [SerializeField] private Animator recoilAnimator;
    // Start is called before the first frame update
    void Start()
    {
        fireInterval = 60f/RPM;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading) {
            reloadCooldown -= Time.deltaTime;
            if(reloadCooldown <= 0f) {
                currMag = MagSize;
                isReloading = false;
            }
        }
        else if(fireCooldown > 0f) {
            fireCooldown -= Time.deltaTime;
        }
        else if(Input.GetButton("Fire1")) {
            // fire bullet
            Fire();
            // Instantiate(smokePrefab,MuzzleFlash.gameObject.transform.position,Quaternion.identity);
            muzzleFlashBrightness = 1f;
            fireCooldown += fireInterval;
            currMag -= 1;
            if(currMag <= 0) {
                Reload();
            }
        }
        if(Input.GetKeyDown("r")) {
            Reload();
        }
        muzzleFlashBrightness = Mathf.Clamp(muzzleFlashBrightness - (Time.deltaTime*8f),0f,1f); // 5f is muzzle flash decay speed
        MuzzleFlash.intensity = muzzleFlashBrightness * 2f; // 2f is intensity of muzzle flash
    }

    void Reload() {
        isReloading = true;
        reloadCooldown = ReloadTime;
        // reload
    }

    void Fire() {
        recoilAnimator.Play("Base Layer.GunRecoil", 0, 0f);
        if(!Physics.Raycast(transform.position, GunRoot.transform.forward, out hit, FireRange)) {
            return;
        }
        GameObject targ = hit.collider.gameObject;
        if(targ == null) {
            return;
        }
        if(targ.GetComponent<DamageReceiver>() == null) {
            return;
        }
        if(targ.GetComponent<Renderer>() == null) {
            return;
        }
        Debug.DrawLine(GunRoot.transform.position, hit.point, Color.red);
        GameObject particles = Instantiate(hitParticlesPrefab, hit.point, Quaternion.identity);
        particles.GetComponent<ParticleSystemRenderer>().material = targ.GetComponent<Renderer>().sharedMaterial;
        // particles.GetComponent<ParticleSystem>()
        targ.GetComponent<DamageReceiver>().ReceiveDamage(Damage);
        Rigidbody otherrb = targ.GetComponent<Rigidbody>();
        if(otherrb != null) {
            otherrb.AddForce((transform.forward).normalized * KnockbackForce, ForceMode.Impulse);
        }
        // create particle effect
    }

    public void SetGunEquipped(bool i) {
        gunIsEquipped = i;
        GunRoot.SetActive(i);
        htman.ShowWeaponText(i);
        htman.ShowPlayerHpText(i);
    }

    public string getDisplayText() {
        if(isReloading) {
            return "RELOADING";
        }
        return currMag + " / " + MagSize;
    }
}
