using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKBController : MonoBehaviour
{
    public static PlayerKBController instance;
   
    public Transform viewPoint;
    public float viewPointOffset;
    public float mouseSensitivity = 1f;
    private float verticalRotation;
    private Vector2 mouseInput;
    public bool invertLook;
    private float invertMultiplier;


    public float moveSpeed = 5f, runSpeed = 9f;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    
    public CharacterController charCont;

    public float standHeight = 2f;
    public float ccYStand = 0f;
    public float crouchHeight = 1f;
    public float ccYCrouched = .4f;
    private bool crouched;
    public Camera cam;
    public float coyoteTime;
    private float coyoteTimer;

    public int maxJumps;
    public int jumpsLeft;
    public float jumpForce = 12f, gravityMod = 2.5f;
    

    public float maxGlideTime = 2f;
    public float glideTimer;

    private bool grounded;
    private bool moving;

    private float shotCounter;
    public float muzzleDisplayTime;
    private float muzzleCounter;

    public bool inControl;



    //sliding
    public float slopeSpeed = 8f;
    private Vector3 hitPointNormal;
    private bool isSliding
    {
        get
        {
            if (charCont.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 4f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > charCont.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;

        inControl = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (!inControl)
        {
            return;
        }

        invertMultiplier = invertLook ? 1 : -1;

        grounded = charCont.isGrounded;
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y") * invertMultiplier) * mouseSensitivity;
        mouseInput.x = Mathf.Clamp(mouseInput.x, -15, 15);
        mouseInput.y = Mathf.Clamp(mouseInput.y, -15, 15);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        verticalRotation += mouseInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -85f, 85f);



        viewPoint.rotation = Quaternion.Euler(verticalRotation, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);


        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;

        }
        else
        {
            activeMoveSpeed = moveSpeed;
        }


        float yVel = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
        movement.y = yVel;

        if (grounded)
        {
            movement.y = 0f;
            coyoteTimer = coyoteTime;

            if (jumpsLeft < maxJumps || glideTimer < maxGlideTime)
            {
                jumpsLeft = maxJumps;
                glideTimer = maxGlideTime;
            }
 
            
        } else if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
        }

        HandleCrouching();
        HandleJump();

        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;

        if (isSliding)
        {
            movement += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
        }
        if (new Vector2(movement.x, movement.z).magnitude > .5f)
        {
            moving = true;

        }
        else
        {
            moving = false;

        }
        charCont.Move(movement * Time.deltaTime);
        
    }



    private void LateUpdate()
    {
        cam.transform.position = viewPoint.position + new Vector3(0, viewPointOffset, 0);
        cam.transform.rotation = viewPoint.rotation;
        
    }

    private void HandleCrouching()
    {

        if (!crouched && Input.GetKeyDown(KeyCode.LeftControl))
        {
            charCont.height = crouchHeight;
            charCont.center = new Vector3(0, ccYCrouched, 0f);
            crouched = true;
        }

        if (crouched && Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.position += new Vector3(0, 1f, 0f);
            charCont.height = standHeight;
            charCont.center = new Vector3(0, ccYStand, 0f);
            crouched = false;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && ((coyoteTimer > 0 && jumpsLeft == maxJumps) || jumpsLeft > 0))
        {
            movement.y = jumpForce;
            coyoteTimer = 0;
            jumpsLeft--;
            glideTimer = maxGlideTime;
        }

        if (Input.GetButton("Jump") && glideTimer > 0)
        {
            if (!grounded && movement.y < 0)
            {
                movement.y = 0;
                glideTimer -= Time.deltaTime;
            }
        }
    }

    public void GivePlayerControl(bool isGivingControl)
    {
        if (isGivingControl)
        {
            inControl = true;
        }
        else
        {
            inControl = false;
        }
    }

    public void SetMouseSensitivity(float newSensValue)
    {
        mouseSensitivity = newSensValue;
    }

    public void SetViewPointHeight(float newHeightValue)
    {
        viewPointOffset = newHeightValue;
        
    }

}
