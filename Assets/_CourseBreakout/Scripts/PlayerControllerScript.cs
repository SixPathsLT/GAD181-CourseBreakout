using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public Camera fps;
    public int range;

    public float playerHealth = 300;

    public ParticleSystem muzzleFlash;
    private Light muzzleLight;

    float timer;
    public float maxtime = 0.4f;

    public float sensitivity, speed, jumpForce;

    bool isGrounded = false;
    float xRotation, yRotation = 0f;

    void Start()
    {
        Cursor.visible = false;
        muzzleLight = GetComponent<Light>();
    }


    void Update()
    {
        //player movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = ((transform.right * x) + (transform.forward * z)).normalized * speed;

        moveDirection *= Time.deltaTime;
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
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }


    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fps.transform.position, fps.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyBattleSys enemy = hit.transform.GetComponent<EnemyBattleSys>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
        }
    }


}