using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Rifle : MonoBehaviour
{
    //adjustable variables.
    [Range(1f, 100f)]
    public float damage = 10f;
    [Range(1, 10)]
    public int numBulletsPerShot = 1;
    [Range(5f, 200f)]
    public float force = 10f;
    [Range(20f, 50f)]
    public float totalCharge = 30f;
    [Range(1f, 5f)]
    public float chargeRate = 3f;
    [Range(100f, 1000f)]
    public float range;
    [Range(1f, 1000f)]
    public float dropoff1;
    [Range(1f, 1000f)]
    public float dropoff2;
    [Range(.5f, 5f)]
    public float inaccuracy;
    [Range(.1f, 100f)]
    public float fireRate = 10f;

    //nonadjustable variables
    [HideInInspector]
    public float currCharge = 0;
    private int sign;
    private int sign2;
    private float actualAccuracy;
    private bool isCooling = false;
    private float actualDmg;
    private float nextToFire = 0f;
    private static bool gunIsGrabbed;

    //references
    public GameObject rifleEnd;
    public GameObject mark;
    public AudioClip shotSound;
    public AudioClip chargeCooldownSound;
    public AudioClip readySound;
    public AudioSource laserSound;
    public GameObject lineRenderer;
    public LayerMask lm;

    void Start()
    {
        actualDmg = damage;
    }


    void Update()
    {
        //check if gun is currently reloading
        if (isCooling)
        {
            return;
        }
        //Debug.Log(gunIsGrabbed);
        //if mouse1 pressed fire
        //also limits fire presses based on rate of fire values and current charge
        if (Input.GetButton("Fire1") && Time.time >= nextToFire && currCharge < totalCharge)
        {
            laserSound.PlayOneShot(shotSound, 1f);
            
            currCharge += 1f;

            //altering firerate based on the charge of the weapon
            //essentially the higher the currCharge the slower the firerate
            nextToFire = Time.time + 1f / fireRate;
            if (currCharge <= totalCharge * .5f)
            {
                nextToFire = Time.time + 1f / fireRate;
            }
            else if (currCharge <= totalCharge * .8f)
            {
                nextToFire = Time.time + 1.5f / fireRate;
            }
            else
            {
                nextToFire = Time.time + 2f / fireRate;
            }

            //shoot # of shots based on numBulletsPerShot
            for (int i = 0; i < numBulletsPerShot; i++)
            {
                actualAccuracy = Random.Range(-inaccuracy, inaccuracy);
                Shoot(actualAccuracy);
            }


        }


        //cool down the gun after gun stops firing
        if (!Input.GetButton("Fire1") && currCharge > 0 && Time.time >= nextToFire)
        {
            currCharge -= chargeRate * Time.deltaTime;
        }

        //force reload if current charge is larger than capacity
        if (currCharge >= totalCharge)
        {
            StartCoroutine(CoolDown());
            return;
        }
    }

    public void Shoot(float accuracy)
    {
        
        //assigning randomness to placement of shot.
        sign = Random.Range(1, 10);
        if (sign >= 5)
        {
            sign = 1;
        }
        else
        {
            sign = -1;
        }
        sign2 = Random.Range(1, 10);
        if (sign2 >= 5)
        {
            sign2 = 1;
        }
        else
        {
            sign2 = -1;
        }

        //calculate a random angle
        Quaternion spreadAngle = Quaternion.AngleAxis(accuracy, new Vector3(0, sign, sign2));
        RaycastHit hit;

        //shoots ray out and returns true if ray hits an object
        //can also add effective range float value
        bool hasHit = Physics.Raycast(rifleEnd.transform.position, rifleEnd.transform.forward, out hit, range);
        print(hasHit);
        //if targeted object has rigidbody apply a force 
        // if (hit.rigidbody)
        // {
        //     hit.rigidbody.AddForce(-hit.normal * force);
        // }
        if (hit.collider)
        {
            print("!");
            float distance = Vector3.Distance(this.gameObject.transform.position, hit.transform.gameObject.transform.position);

            if (distance >= dropoff2)
            {
                damage = damage * .2f;
            }
            else if (distance >= dropoff1)
            {
                damage = damage * .8f;
            }
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().DamageEnemy(damage);
            }
        }
        if (lineRenderer)
        {
            GameObject line = Instantiate(lineRenderer);
            line.GetComponent<LineRenderer>().SetPositions(new Vector3[] {rifleEnd.transform.position, hasHit ? hit.point : rifleEnd.transform.position + rifleEnd.transform.forward * 100});
            Destroy(line, .5f);
        }



        //Debug.Log("Distance: " + distance + "m, Damage: " + damage);
        //DamageHandler.DealDamage(damage, hit.transform.gameObject);

        damage = actualDmg;

        //leave effect where ray hit object and delete that object 1 second later
        // GameObject bullet = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
        // Destroy(bullet, 1f);

        //leave bullet mark and delete object 1 second later
        if (mark) 
        {
            GameObject laserMark = Instantiate(mark, hit.point, Quaternion.LookRotation(hit.normal));
            laserMark.transform.position += laserMark.transform.forward / 1000;
            Destroy(laserMark, 1f);
        }
            
    }

    //cool down the gun after gun stops firing
    IEnumerator CoolDown()
    {
        laserSound.PlayOneShot(chargeCooldownSound, 1f);
        isCooling = true;
        yield return new WaitForSeconds((int)(totalCharge * .15f));
        currCharge = 0;
        isCooling = false;
        laserSound.PlayOneShot(readySound, 1f);
    }
}