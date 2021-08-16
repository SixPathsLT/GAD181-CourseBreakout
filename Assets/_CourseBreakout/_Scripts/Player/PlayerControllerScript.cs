using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{

    bool inTutorial = true;

    public Camera fps;
    public int range;

    public float gravity = 8f;
    public float playerHealth = 300;
    PlayerCheckpoint relive;

    public ParticleSystem muzzleFlash;
    private Light muzzleLight;

    float timer;
    public float maxtime = 0.4f;
    public float maxHeight = 3f;

    public float speed, jumpForce;

    public Settings setting;

    bool isPoisoned;

    public bool isGrounded = false;
    [HideInInspector]
   public float xRotation, yRotation = 0f;

    public Text notificationText;
    public GameObject grappleHook;
    public GameObject companionHeal;
    public GameObject companionShield;
    public GameObject boostedJump;

    InventoryManager inventoryManager;

    public GameObject blueShield;

    public Companion companion;

    public ParticleSystem HealthVFX;
    public ParticleSystem HealthVFX2;
    public ParticleSystem HealthVFX3;

    public RawImage PoisonWarning;
    float distanceToGround;


    public int score;


    KeyCode[] inventoryKeys = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
    };

    NotificationsManager notifications;


    void Start()
    {
        notifications = GameObject.FindObjectOfType<NotificationsManager>();
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        muzzleLight = GetComponent<Light>();

        
        notifications.SendNotification("Welcome to Course Breakout!", 3);
        notifications.SendNotification("Follow your companion's instructions.", 3, 4);

        Invoke("EndTutorial", 1f);

        relive = GetComponent<PlayerCheckpoint>();
        //scores = GetComponent<BossRadios>();
        //boom = GetComponent<EnemyBomber>();


        distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    void EndTutorial() {
        inventoryManager.AddItem(grappleHook.GetComponent<Item>());
        inventoryManager.AddItem(companionHeal.GetComponent<Item>());
        inventoryManager.AddItem(companionShield.GetComponent<Item>());
        inventoryManager.AddItem(boostedJump.GetComponent<Item>());

        inventoryManager.SelectItem(0);

        inTutorial = false;
        
        notifications.SendNotification("Go to the vents and grab the green key.", 4);
        notifications.SendNotification("Press E to interact with objects.", 4, 4);
    }

    void Update()
    {
        if (inTutorial)
            return;

        //inventory keys
        for (int i =0; i < inventoryKeys.Length; i++) {
            KeyCode keyCode = inventoryKeys[i];

            if (Input.GetKeyDown(keyCode))
                inventoryManager.SelectItem(i);
        }



        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            muzzleFlash.Play();
            muzzleLight.enabled = true;
        }
        if (timer > maxtime)
        {
            muzzleLight.enabled = false;
            timer = 0;
        }
        timer += Time.deltaTime;
       
        if (playerHealth <= 0) 
        {
           //GetComponent<PlayerCheckpoint>().Respawn();
           relive.Respawn();
           playerHealth = 300;
        }
        PoisonPlayer();
       
        if(score >= 4)
        {
            transform.position = new Vector3(27.5f, 72.1f, -11535f);
        }
    }

    void FixedUpdate()
    {
        if (inTutorial)
            return;

        //player movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        


        bool jump = Input.GetKey("space");


         Vector3 moveDirection = ((transform.right * x) + (transform.forward * z)).normalized * speed;

         Rigidbody playerBody = GetComponent<Rigidbody>();
        // playerBody.MovePosition(transform.position + moveDirection);



        if (!isGrounded)
        {
            if (moveDirection.x == 0)
                moveDirection.x = playerBody.velocity.x / 1.005f;
            if (moveDirection.z == 0)
                moveDirection.z = playerBody.velocity.z / 1.005f;
        }

        var controlledVelocity = playerBody.velocity;
        controlledVelocity.x = Mathf.Clamp(controlledVelocity.x, -speed, speed);
        controlledVelocity.z = Mathf.Clamp(controlledVelocity.z, -speed, speed);


        var requiredVelocity = moveDirection - controlledVelocity;


       // if ((x != 0 || isGrounded) && (playerBody.velocity.x < speed && playerBody.velocity.x > -speed))
            requiredVelocity.x = Mathf.Clamp(requiredVelocity.x, -speed, speed);
       // if ((z != 0 || isGrounded) && (playerBody.velocity.z < speed && playerBody.velocity.z > -speed))
            requiredVelocity.z = Mathf.Clamp(requiredVelocity.z, -speed, speed);
            
         requiredVelocity.y = 0;

         playerBody.AddForce(requiredVelocity, ForceMode.VelocityChange);



        if (!isGrounded)
         {
            playerBody.AddForce(new Vector3(0, -gravity, 0));
            //playerBody.velocity += new Vector3(0, -gravity * Time.fixedDeltaTime, 0);
         }


        // camera movement
        float mouseX = Input.GetAxisRaw("Mouse X") * setting.sensetivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * setting.sensetivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -60, 60);

        transform.Find("Top").gameObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);


      if (jump && playerBody != null && isGrounded)
        {
            Jump(jumpForce, ForceMode.VelocityChange);
           // playerBody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

            if (playerBody.velocity.magnitude > maxHeight) {
                playerBody.velocity = new Vector3(playerBody.velocity.x, maxHeight, playerBody.velocity.z);
            }
        }

    }

    public void Jump(float jumpForce, ForceMode forceMode)
    {
        if (!isGrounded)
            return;

        isGrounded = false;

        Rigidbody playerBody = GetComponent<Rigidbody>();

        playerBody.AddForce(Vector3.up * jumpForce, forceMode);
        FindObjectOfType<SoundEffect>().jump.Play();
    }
 
    private void OnCollisionEnter(Collision collision)
    {


        isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.5f);


        if (collision.gameObject.CompareTag("EnemyOrProjectilesOrBullets") && companion.shieldPlayer == false)
        {
            EnemyDamage(20);
        }
        if (collision.gameObject.CompareTag("Poison") && companion.shieldPlayer == false)
        {
            ActivatePoison();
            Destroy(collision.gameObject);
        }
    }
    public void EnemyDamage(float amount)
    {
        playerHealth -= amount;
    }
   // Everything below this is for the Poison Effect
    void ActivatePoison()
    {
        isPoisoned = true;
        Invoke(nameof(PoisonEnd), 3);
    }
    void PoisonPlayer()
    {
        if (isPoisoned)
        {
            playerHealth -= 0.2f;
            PoisonWarning.gameObject.SetActive(true);
        }
    }
    void PoisonEnd()
    {
        isPoisoned = false;
        PoisonWarning.gameObject.SetActive(false);
    }
    // Until here
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthKit"))
        {
            playerHealth += 50f;
            Destroy(other.gameObject);
            HealthVFX.Play();
        }
        if (other.gameObject.CompareTag("HealthKit2"))
        {
            playerHealth += 50f;
            Destroy(other.gameObject);
            HealthVFX2.Play();
        }
        if (other.gameObject.CompareTag("HealthKit3"))
        {
            playerHealth += 50f;
            Destroy(other.gameObject);
            HealthVFX3.Play();
        }
    }



    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fps.transform.position, fps.transform.forward, out hit, range))
        {
            if (hit.collider.CompareTag("BombBody"))
            {
                hit.collider.gameObject.GetComponentInParent<EnemyBomber>().Explode();
                // GetComponentInParent<EnemyBomber>().Explode(); 
            }
            
               
            //Debug.Log(hit.transform.name);
            EnemyBattleSys enemy = hit.transform.GetComponent<EnemyBattleSys>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }   
        }
    }


}
