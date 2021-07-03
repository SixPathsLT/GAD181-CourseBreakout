using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{

    bool inTutorial = true;

    public Camera fps;
    public int range;

    public float playerHealth = 300;

    public ParticleSystem muzzleFlash;
    private Light muzzleLight;

    float timer;
    public float maxtime = 0.4f;
    public float maxHeight = 3f;

    public float sensitivity, speed, jumpForce;

    bool isGrounded = false;
    float xRotation, yRotation = 0f;

    public Text notificationText;
    public GameObject grappleHook;
    public GameObject companionHeal;
    public GameObject companionShield;
    public GameObject boostedJump;

    InventoryManager inventoryManager;

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

        
        notifications.SendNotification("Welcome to Course Breakout!", 2);

        
        inventoryManager.AddItem(grappleHook.GetComponent<Item>());
        inventoryManager.AddItem(companionHeal.GetComponent<Item>());
        inventoryManager.AddItem(companionShield.GetComponent<Item>());
        inventoryManager.AddItem(boostedJump.GetComponent<Item>());

        inventoryManager.SelectItem(0);

        Invoke("EndTutorial", 12f);
    }

    void EndTutorial() {
        inTutorial = false;
        
        notifications.SendNotification("Grab the key to open the door.", 4);
        notifications.SendNotification("Press E to Interact with objects.", 4, 5);
    }

    void Update()
    {

        //inventory keys
        for (int i =0; i < inventoryKeys.Length; i++) {
            KeyCode keyCode = inventoryKeys[i];

            if (Input.GetKeyDown(keyCode))
                inventoryManager.SelectItem(i);
        }

        if (inTutorial)
            return;

            //player movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = ((transform.right * x) + (transform.forward * z)).normalized * speed * Time.deltaTime;
        
        transform.position += moveDirection;

        // camera movement
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -60, 60);

        transform.Find("Top").gameObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

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
    }

    void FixedUpdate()
    {
        bool jump = Input.GetKey("space");

        Rigidbody playerBody = GetComponent<Rigidbody>();
        if (jump && playerBody != null && isGrounded)
        {
            isGrounded = false;
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (playerBody.velocity.magnitude > maxHeight) {
                playerBody.velocity = new Vector3(0, maxHeight, 0);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }


    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fps.transform.position, fps.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            EnemyBattleSys enemy = hit.transform.GetComponent<EnemyBattleSys>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
        }
    }


}