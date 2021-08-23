using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{


    public PlayerData playerData;

    [HideInInspector]
    public bool inTutorial = true;

    public Camera fps;
    public int range;

    public float gravity = 8f;
    PlayerCheckpoint relive;
    public Image lowHealthImage;

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

    float healthWarnTimer = 0f;


    KeyCode[] inventoryKeys = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4
    };

    NotificationsManager notifications;

   public Text helpText;
    bool toggleHelpText;

    void Start()
    {
        playerData.Start();

        notifications = GameObject.FindObjectOfType<NotificationsManager>();
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        muzzleLight = GetComponent<Light>();

        
        notifications.SendNotification("Welcome to Course Breakout!", 3);
        notifications.SendNotification("Follow your companion's instructions.", 3, 4);

        Invoke("EndTutorial", 1f); // original is 14

        relive = GetComponent<PlayerCheckpoint>();
        //scores = GetComponent<BossRadios>();
        //boom = GetComponent<EnemyBomber>();


        distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;

       // SetDefaultHelpText();
    }

    void EndTutorial() {
        inventoryManager.AddItem(grappleHook.GetComponent<Item>());
        inventoryManager.AddItem(companionHeal.GetComponent<Item>());
        inventoryManager.AddItem(companionShield.GetComponent<Item>());
        inventoryManager.AddItem(boostedJump.GetComponent<Item>());

        inventoryManager.SelectItem(0);

        inTutorial = false;
        
        notifications.SendNotification("Go to the vents and grab the green key.", 4);
        notifications.SendNotification("Hold Right Click to use your ability.", 4, 6);
        notifications.SendNotification("Use 1-4 keys to switch abilities.", 4, 10);
        notifications.SendNotification("Press E to open the door.", 4, 25);

        FindObjectOfType<AudioManager>().PlayClip("GameBg");
    }

    void SetDefaultHelpText()
    {
        helpText.text = "Move: WASD \n" +
        "Jump: Space Bar \n" +
        "Interact: E \n" +
        "Shoot: Left Click \n" +
        "Use Selected Ability: Right Click \n" +
        "Select Ability with keys: 1, 2, 3, 4";
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.H)) {
            toggleHelpText = !toggleHelpText;

            if (!toggleHelpText)
                helpText.text = "Press H for Help";
            else 
                SetDefaultHelpText();
        }


        if (inTutorial)
            return;


        WarnLowHealthHUD();

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
       
        if (playerData.health <= 0) 
        {
           //GetComponent<PlayerCheckpoint>().Respawn();
           relive.Respawn();
        }
        PoisonPlayer();
       
        if(score >= 4)  {
            score = 0;
            transform.position = new Vector3(27.5f, 72.1f, -11535f);

            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            FindObjectOfType<AudioManager>().PlayClip("WinBg");
            Invoke("BackToMenu", 15f);
        }
    }
    

    private void WarnLowHealthHUD() {
        if (playerData.health < playerData.maxHealth / 2) {
            lowHealthImage.gameObject.SetActive(true);
             healthWarnTimer += Time.deltaTime;
            if (healthWarnTimer < 0.8f)
                lowHealthImage.CrossFadeColor(new Color32(255, 255, 255, 10), 0.8f, true, true);
            else if (healthWarnTimer < 2f)
                lowHealthImage.CrossFadeColor(new Color32(255, 255, 255, 255), 0.8f, true, true);
            else
                healthWarnTimer = 0;
        } else if (lowHealthImage.IsActive())
            lowHealthImage.gameObject.SetActive(false);
    }

    public void BackToMenu() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    float[] dirs = {
        -0.2f, 0.2f, 0, -0.1f, 0.1f, -0.3f, 0.3f
    };

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
            float airMultiplier = 0.8f;
            moveDirection = new Vector3(moveDirection.x * airMultiplier, moveDirection.y, moveDirection.z * airMultiplier);

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

    void updateGrounded()
    {
        if (isGrounded)
            return;

        for (int i = 0; i < dirs.Length; i++)
        {
            Vector3 dir = new Vector3(dirs[i], 0, 0);
            isGrounded = Physics.Raycast(transform.position, Vector3.down + dir, distanceToGround + 0.5f);
            if (isGrounded)
                break;
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


    private void OnCollisionStay(Collision collision)
    {
        updateGrounded();
    }


    private void OnCollisionEnter(Collision collision)
    {
        updateGrounded();

        if (collision.gameObject.CompareTag("EnemyOrProjectilesOrBullets"))
        {
            EnemyDamage(20);
        }
        if (collision.gameObject.CompareTag("Poison") /*&& companion.shieldPlayer == false*/)
        {
            ActivatePoison();
            Destroy(collision.gameObject);
        }
    }
    public void EnemyDamage(float amount)
    {
        playerData.health -= amount;
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
            playerData.health -= 0.2f;
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
            playerData.health += 50f;
            Destroy(other.gameObject);
            HealthVFX.Play();
        }
        if (other.gameObject.CompareTag("HealthKit2"))
        {
            playerData.health += 50f;
            Destroy(other.gameObject);
            HealthVFX2.Play();
        }
        if (other.gameObject.CompareTag("HealthKit3"))
        {
            playerData.health += 50f;
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
