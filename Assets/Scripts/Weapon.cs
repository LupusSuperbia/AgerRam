using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // [System.Serializable]
    [Header("Weapon")]
    [SerializeField] public float weaponDamage; // Damage
    [SerializeField] public int bullets;
    [SerializeField] public int currentBullets;
    [SerializeField] public float hitForce;
    [SerializeField] public float fireRate = 0.2f;
    [SerializeField] public float weaponRange = 75f;
    public Transform gunEnd;


    [SerializeField] private Camera fpsCam;
    private float nextFire;
    public WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    [SerializeField] public GameObject owner;
    [SerializeField] private AudioSource weaponAudio;
    [SerializeField] private LineRenderer laserLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Rigidbody r;
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        weaponAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        // ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // r = GetComponent<Rigidbody>();
        // r.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        // r.freezeRotation = true;
        // r.useGravity = false;
    }

    void Update()
    {
    }
    public void TryShoot()
    {
        if (Time.time >= nextFire)
        {
            Debug.Log("Ay me tiraste un tiro");
            nextFire = Time.time + fireRate;
            Shooting();
        }

    }
    public void Shooting()
    {
        if (fpsCam == null) return;
        StartCoroutine(ShootEffect());
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);
        if (Physics.Raycast(ray.origin, fpsCam.transform.forward, out hit, 100f))
        {
            laserLine.SetPosition(1, hit.point);
            Debug.DrawLine(ray.origin, hit.point, Color.red, 0.25f);
            if (hit.collider.TryGetComponent(out IDamageable objDamageable))
            {
                DamageInfo info = new DamageInfo(weaponDamage, hit.point, ray.origin, owner);
                objDamageable.TakeDamage(info);
                Debug.DrawLine(ray.origin, hit.point, Color.green, 0.25f);
                GameObject attackedObject = hit.collider.gameObject;
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce, ForceMode.Impulse);
                }
                Debug.Log("El objeto atacado fue" + attackedObject.name);
            }
        }
    }

    private IEnumerator ShootEffect()
    {
        weaponAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
