using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private const int SUPER_SPEED = 7;
    private const float TIMER = .5f;
    private const int MIN_PASS = 3;

    public Transform helixPosition;

    public Button b;

    public ForceMode forceMode;
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f;
    private Vector3 startPos;

    public bool notExploded = true;
    public int perfectPass;
    public bool superSpeedActive;
    public bool jump = true;
    private bool explodable = false;
    public GameObject platformPrefab;

    public float upwardsModifier = 0.0f;

    //splash
    public GameObject Splash_prefab;

    public ParticleSystem Particle;
    public bool enableEmission;

    private ParticleSystem ps;
    public bool moduleEnabled;

    public Text failed;


    public int counter = 0;
    void Awake()
    {
        startPos = transform.position;
        Particle.transform.position = startPos;
        Particle.Stop();
        jump = true;
        Time.timeScale =1f;
    }

    //   this function deals with the balls behaviour when it collides with a platform
    private void OnCollisionEnter(Collision collision)
    {


        if (ignoreNextCollision)
        {

            return;
        }


        Particle.Stop();
   

        var emission = Particle.emission;
        emission.enabled = false;

        if (superSpeedActive && notExploded)
        {



            FindObjectOfType<BallController>().GetComponent<TrailRenderer>().enabled = true;
            jump = false;


            if (!collision.transform.GetComponent<Goal>())
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 0, 1.15f), 1f);

                Invoke("pass", .2f);
                foreach (Collider col in hitColliders)
                {



                    if (col.tag == "h_part")
                    {
                        col.gameObject.AddComponent<Rigidbody>();

                        col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(90f, transform.position + new Vector3(0, 0, 1.15f), 2f, upwardsModifier, forceMode);
                        StartCoroutine(destroyRigidBody(col.gameObject, 2f));

                    }

                    else if (col.tag == "helix_platform")
                    {
                        Destroy(col);
                    }
                }
                superSpeedActive = false;
                notExploded = false;
                perfectPass = 0;
            }

        }
        else
        {
            // Reset level when hit on death part
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
            {
                this.GetComponent<TrailRenderer>().time = -1;

                b.gameObject.SetActive(true);

                Invoke("ResetTrails", 0.01f);
                deathPart.HitDeathPart();

            }
        }


        if (jump)
        {
           
            //set speed for ball and add velocity
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
            
            //set splash
            GameObject splash = Instantiate(Splash_prefab);
            Vector3 pos = transform.position;
            pos.y = pos.y- 0.1f;
            splash.transform.position = pos;
            splash.transform.SetParent(GameObject.Find("Helix").transform);

            gameObject.GetComponent<AudioSource>().Play();
;
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * impulseForce, ForceMode.Acceleration);
            jump = true;

        }

        ignoreNextCollision = true;
        perfectPass = 0;
        superSpeedActive = false;
        Invoke("AllowCollision", .2f);
        Invoke("AllowJump", .2f);

    }

    public IEnumerator destroyRigidBody(GameObject g, float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Destroy(g);
    }

    private void AllowJump()
    {
        jump = true;
    }
    public void FixedUpdate()
    {
        if (perfectPass >= MIN_PASS && !superSpeedActive && notExploded)
        {
            superSpeedActive = true;
            rb.AddForce(Vector3.down * SUPER_SPEED, ForceMode.Impulse);
        }

        if (perfectPass >= 2)
        {
            Particle.gameObject.SetActive(true);
            FindObjectOfType<BallController>().GetComponent<TrailRenderer>().enabled = false;
            var emission = Particle.emission;
            emission.enabled = true;
            Instantiate(Particle, transform.position, transform.rotation);
            Particle.transform.parent = this.transform;
        }

    }

    //this function sets the ball collision to false
    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }




    //this function resets the balls position to it's starting position 
    public void ResetBall()
    {
      
        transform.position = startPos;
        b.gameObject.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<BallController>().GetComponent<TrailRenderer>().enabled = true;
        perfectPass = 0;
        superSpeedActive = false;
        jump = false;

    }


    public void ResetTrails()
    {
        FindObjectOfType<BallController>().GetComponent<TrailRenderer>().time = .2f;
    }

    public void passed()
    {
        if (!notExploded)
        {
            perfectPass = 0;
            superSpeedActive = false;
            notExploded = true;
        }

        perfectPass++;


    }

}

