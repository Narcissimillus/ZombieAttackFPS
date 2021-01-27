using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1f;
    public float jumpSpeed = 8f;
    public float gravity = 9.81f;
    public float fireRate = 0.1f;
    int bullets = 30;
    [HideInInspector] public int totalBullets = 90;
    float nextFireTime = 0f;
    Vector3 moveDirection = Vector3.zero;
    public Slider healthBar;
    public RectTransform crosshair;
    public Text bulletsCounter;
    public ParticleSystem muzzleFlash;
    Transform camTransform;
    CharacterController controller;
    Animator animator;
    void Start()
    {
        camTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            moveDirection = (h * camTransform.right + v * camTransform.forward).normalized;
            moveDirection.y = 0f;
            if (Input.GetKeyDown("space"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * movementSpeed * Time.deltaTime);

        // Get HP
        // healthPoints = healthBar.value;

        HandleFire();

        RaycastHit hit;

        //daca am un obiect pe directia in range
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 50f))
        {
            if (hit.collider.name.StartsWith("Ammo"))
            {
                hit.collider.transform.GetChild(2).gameObject.transform.LookAt(camTransform);
                hit.collider.transform.GetChild(2).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    totalBullets += 30;
                    Destroy(hit.collider.gameObject);
                    bulletsCounter.GetComponent<Text>().text = bullets + "/" + totalBullets;
                }
            }
            if (hit.collider.name.StartsWith("Zombie"))
            {
                hit.collider.gameObject.GetComponentInChildren<Canvas>().enabled = true;
            }
        }

        animator.SetFloat("timeSinceLastHit", animator.GetFloat("timeSinceLastHit") + Time.deltaTime);
        if (animator.GetBool("takeHit"))
        {
            animator.SetBool("takeHit", false);
            animator.SetFloat("timeSinceLastHit", 0);
            healthBar.value -= 15;
        }
    }

    private bool CanFire
    {
        get { return Time.time > nextFireTime; }
    }

    void HandleFire()
    {
        if (Input.GetButtonDown("Fire3") && animator.GetBool("singleFiring") == false)
        {
            animator.SetBool("singleFiring", true);
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            animator.SetBool("singleFiring", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && animator.GetBool("snipe") == false)
        {
            crosshair.gameObject.SetActive(false);
            animator.SetBool("snipe", true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            crosshair.gameObject.SetActive(true);
            animator.SetBool("snipe", false);
        }

        muzzleFlash.Stop();
        if (animator.GetBool("singleFiring") == false)
        {
            if (Input.GetButton("Fire1") && CanFire && bullets > 0 && animator.GetBool("midReload") == false)
            {
                muzzleFlash.Play();
                nextFireTime = Time.time + fireRate;
                bullets--;
                if (totalBullets < 0)
                {
                    bulletsCounter.GetComponent<Text>().text = bullets + "/0";
                }
                else
                {
                    bulletsCounter.GetComponent<Text>().text = bullets + "/" + totalBullets;
                }

                //set animation state
                animator.SetTrigger("firing");
                crosshair.localScale = Vector3.Slerp(crosshair.localScale, new Vector3(1.3f, 1.3f, 1.3f), 0.05f);

                //raycast & everything else ..
                RaycastHit hit;
                if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 500f))
                {
                    if (hit.collider.name.StartsWith("Zombie"))
                    {
                        hit.collider.gameObject.GetComponent<Animator>().SetTrigger("takeHit");
                        hit.collider.gameObject.GetComponent<Animator>().SetInteger("HP", hit.collider.gameObject.GetComponent<Animator>().GetInteger("HP") - 5);
                        hit.collider.gameObject.GetComponentInChildren<Slider>().value -= 5;
                    }
                }
            }
            else if (Input.GetButton("Fire1") == false)
            {
                crosshair.localScale = Vector3.Slerp(crosshair.localScale, new Vector3(0.6f, 0.6f, 0.6f), 0.05f);
            }
        }
        else if (animator.GetBool("singleFiring") == true)
        {
            if (Input.GetButtonDown("Fire1") && CanFire && bullets > 0 && animator.GetBool("midReload") == false)
            {
                muzzleFlash.Play();
                nextFireTime = Time.time + 3 * fireRate;
                bullets--;
                if (totalBullets < 0)
                {
                    bulletsCounter.GetComponent<Text>().text = bullets + "/0";
                }
                else
                {
                    bulletsCounter.GetComponent<Text>().text = bullets + "/" + totalBullets;
                }

                //set animation state
                animator.SetTrigger("firing");
                crosshair.localScale = Vector3.Slerp(crosshair.localScale, new Vector3(2.5f, 2.25f, 2.5f), 0.15f);

                //raycast & everything else ..
                RaycastHit hit;
                if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 500f))
                {
                    if (hit.collider.name.StartsWith("Zombie"))
                    {
                        hit.collider.gameObject.GetComponent<Animator>().SetTrigger("takeHit");
                        hit.collider.gameObject.GetComponent<Animator>().SetInteger("HP", hit.collider.gameObject.GetComponent<Animator>().GetInteger("HP") - 10);
                        hit.collider.gameObject.GetComponentInChildren<Slider>().value -= 10;
                    }
                }
            }
            else
            {
                crosshair.localScale = Vector3.Slerp(crosshair.localScale, new Vector3(0.6f, 0.6f, 0.6f), 0.15f);
            }
        }

        if ((Input.GetKeyDown(KeyCode.R) || bullets == 0) && bullets != 30 && totalBullets > 0 && Input.GetButton("Fire1") == false)
        {
            animator.SetTrigger("reload");
            int copyBullets = bullets;
            if(totalBullets < 30)
            {
                if(totalBullets + bullets > 30)
                {
                    bullets = 30;
                }
                else
                {
                    bullets += totalBullets;
                }
            }
            else
            {
                bullets = 30;
            }
            totalBullets = totalBullets - (30 - copyBullets);
            if(totalBullets < 0)
            {
                bulletsCounter.GetComponent<Text>().text = bullets + "/0";
            }
            else
            {
                bulletsCounter.GetComponent<Text>().text = bullets + "/" + totalBullets;
            }
        }
    }
}
 