using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class DragonController : MonoBehaviour
{

    #region Variables 
    #region Monster Components 
    private Animator anim;
    private Transform DragoTransform;
    private Rigidbody dragoRigidBody;
    private CapsuleCollider dragoCollider;
    private HashIDsDragons hashs;  //Hash ID For the animator
    #endregion

    #region Animator Parameters Variables
    private float speed, direction, flyspeed = 1f;
    private float maxSpeed = 1f; //0 Walking, 1 Running
    private bool Jump, ShiftK, wounded, fly, dodge, fall;

    private float horizontal, vertical, upDown;

    float jumpPoint, dragoFloat;
    int dragoInt;
    private float jumpingCurve;
    private bool stand = true, grounded;
    private bool attack1, attack2, Stun;

    private bool fowardPressed = false;
    #endregion

    #region Modify_the_Rotation_Variables
    public LayerMask GroundLayer;
    public float TurnSpeed = 0.6f;
    public int GotoSleep;
    #endregion

    #region Modify_the_Position_Variables

    RaycastHit hit_Hip, hit_Chest;
    Vector3 Drago_Hip, Drago_Chest;
    float scaleFactor;
    Pivots[] pivots;
    float maxHeight;
    [HideInInspector]
    public int Tired = 0;
    //private Vector3 ColliderCenter;
    #endregion

    #region Properties
    public float JumpPoint
    {
        set { jumpPoint = value; }
        get { return this.jumpPoint; }
    }

    public float MaxSpeed
    {
        set { maxSpeed = value; }
        get { return this.maxSpeed; }
    }


    public float MaxHeight
    {
        set { maxHeight = value; }
        get { return this.maxHeight; }
    }

    public int DragoInt
    {
        set { dragoInt = value; }
        get { return this.dragoInt; }
    }

    public float DragoFloat
    {
        set { dragoFloat = value; }
        get { return this.dragoFloat; }
    }
    #endregion

    #endregion

    // Use this for initialization
    void Start()
    {
        
        anim = GetComponent<Animator>();
        hashs = GetComponent<HashIDsDragons>();
        DragoTransform = transform;
        dragoCollider = GetComponent<CapsuleCollider>();
        dragoRigidBody = GetComponent<Rigidbody>();

        // ColliderCenter = dragoCollider.center;
        Tired = 0;
        pivots = GetComponentsInChildren<Pivots>(); //Pivots are Strategically Transform objects use to cast rays used by the drago
        scaleFactor = DragoTransform.localScale.y;  //TOTALLY SCALABE DRAGO

    }



    //----------------------Link the buttons pressed with correspond variables-------------------------------------------------------------------------------

    void getButtons()
    {
        fowardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); //If foward is pressed

#if UNITY_ANDROID || UNITY_IOS

                        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
                            fowardPressed = true;
                        else fowardPressed = false;
#endif

        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        vertical = CrossPlatformInputManager.GetAxis("Vertical");

        ShiftK = Input.GetKey(KeyCode.LeftShift);
        attack1 = CrossPlatformInputManager.GetButton("Fire1");            //Get the Attack button
        attack2 = CrossPlatformInputManager.GetButton("Fire2");


        if (Input.GetKeyDown(KeyCode.Q))
        {
            fly = !fly;   //Toogle Fly
            dragoInt = 0;
        }

        dodge = Input.GetKey(KeyCode.E);
        wounded = Input.GetKeyDown(KeyCode.H);
        Jump = CrossPlatformInputManager.GetButton("Jump");
        Stun = Input.GetMouseButton(2);

    }

    //----------------------linking  the Parameters-------------------------------------------------------------------------------

    void LinkingAnimator(Animator anim_)
    {
        anim_.SetFloat(hashs.verticalHash, vertical * speed);
        anim_.SetFloat(hashs.horizontalHash, direction);
        anim_.SetFloat(hashs.updownHash, upDown);
        anim_.SetFloat(hashs.flySpeedHash, Mathf.Lerp(anim_.GetFloat(hashs.flySpeedHash), flyspeed, Time.deltaTime * 5f));
        anim_.SetBool(hashs.shiftHash, ShiftK);
        anim_.SetBool(hashs.standHash, stand);
        anim_.SetBool(hashs.jumpHash, Jump);
        anim_.SetBool(hashs.attack1Hash, attack1);
        anim_.SetBool(hashs.attack2Hash, attack2);
        anim_.SetBool(hashs.fowardPressedHash, fowardPressed);
        anim_.SetBool(hashs.injuredHash, wounded);
        anim_.SetBool(hashs.flyHash, fly);
        anim_.SetBool(hashs.fallHash, fall);
        anim_.SetBool(hashs.dodgeHash, dodge);
        anim_.SetBool(hashs.stunnedHash, Stun);

        if (fly)
        {
            anim_.SetFloat(hashs.floatDragonHash, dragoFloat);
        }
        anim_.SetBool(hashs.groundedHash, grounded);



        if (Input.GetKeyDown(KeyCode.K))
            anim_.SetTrigger(hashs.deathHash); //Triggers the Death
    }

    //--Add more Rotations to the current turn animations  usic the public turnSpeed float--------------------------------------------
    void TurnAmount()
    {
        //For going Foward and Backward
        if (vertical >= 0)
        {
            DragoTransform.Rotate(DragoTransform.up, TurnSpeed * 3 * horizontal * Time.deltaTime);
        }
        else
        {
            DragoTransform.Rotate(DragoTransform.up, TurnSpeed * 3 * -horizontal * Time.deltaTime);
        }
        //More Rotation when jumping and falling
      if (isJumping() || fall && !fly) 
        {
            if (vertical >= 0)
                DragoTransform.Rotate(DragoTransform.up,  100 * horizontal * Time.deltaTime);
            else
                DragoTransform.Rotate(DragoTransform.up,  100 * -horizontal * Time.deltaTime);
        }

    }


    //------------------------------------------Terrain Logic----------------------------------

    void FixPosition()
    {
        Drago_Hip = pivots[0].transform.position;
        Drago_Chest = pivots[1].transform.position;

        //Ray From Hip to the ground
        if (Physics.Raycast(Drago_Hip, -DragoTransform.up, out hit_Hip, 0.5f * scaleFactor, GroundLayer))
        {
            Debug.DrawRay(hit_Hip.point, hit_Hip.normal * 0.02f, Color.blue);
        }


        //Ray From Chest to the ground
        if (Physics.Raycast(Drago_Chest, -DragoTransform.up, out hit_Chest, 0.5f * scaleFactor, GroundLayer))
        {
            Debug.DrawRay(hit_Chest.point, hit_Chest.normal * 0.02f, Color.red);
        }



        //Smoothy rotate until is Aling with the Horizontal
        if (fly)
        {
            float angleTerrain = Vector3.Angle(DragoTransform.up, Vector3.up);

            Quaternion finalRot = Quaternion.FromToRotation(DragoTransform.up, Vector3.up) * dragoRigidBody.rotation;

            if (angleTerrain > 0.1f)
                DragoTransform.rotation = Quaternion.Lerp(DragoTransform.rotation, finalRot, Time.deltaTime * 10);
            else
                DragoTransform.rotation = finalRot;
        }
        else
        {
            //------------------------------------------------Terrain Adjusment-----------------------------------------

            //----------Calculate the Align vector of the terrain------------------
            Vector3 direction = (hit_Chest.point - hit_Hip.point).normalized;
            Vector3 Side = Vector3.Cross(Vector3.up, direction).normalized;
            Vector3 SurfaceNormal = Vector3.Cross(direction, Side).normalized;
            float angleTerrain = Vector3.Angle(DragoTransform.up, SurfaceNormal);

            // ------------------------------------------Orient To Terrain-----------------------------------------  
            Quaternion finalRot = Quaternion.FromToRotation(DragoTransform.up, SurfaceNormal) * dragoRigidBody.rotation;

            // If the dragon is falling, jumping or flying smoothly aling with the horizontal
            if (fall || isJumping(0.7f, true))
            {
                finalRot = Quaternion.FromToRotation(DragoTransform.up, Vector3.up) * dragoRigidBody.rotation;
                DragoTransform.rotation = Quaternion.Lerp(DragoTransform.rotation, finalRot, Time.deltaTime * 10f);
            }
            else
            {
                // if the terrain changes hard smoothly adjust to the terrain 
                if (angleTerrain > 0.2f)
                {
                    DragoTransform.rotation = Quaternion.Lerp(DragoTransform.rotation, finalRot, Time.deltaTime * 10f);
                }
                else
                {
                    DragoTransform.rotation = finalRot;
                }
            }
        }
    }


    //--------------------------------------Falling Logic----------------------------------------------------------
    void Falling()
    {
        RaycastHit hitGrounded;
        RaycastHit hitpos;

        //if the horse stay stucked while falling move foward  ... basic solution
        if (fall && dragoRigidBody.velocity.magnitude < 0.1 && !fly)
        {
            DragoTransform.position = Vector3.Lerp(DragoTransform.position, DragoTransform.position + DragoTransform.forward * 2, Time.deltaTime);
        }



        if (Physics.Raycast(dragoCollider.bounds.center, -DragoTransform.up, out hitpos, 0.9f*scaleFactor))
        {
            fall = false;
        }
        else
        {
            fall = true;
        }


        if (Physics.Raycast(DragoTransform.position, -DragoTransform.up, out hitGrounded, .1f))
        {
            if (isJumping(0.5f,true))
            {
                grounded = false;
            }
            else
            {
                grounded = true;
            }
        }
        else
        {
           grounded = false;
        }
    }



    //--------------------------Check if the in the Jumping State------------------------------------------
    bool isJumping(float normalizedtime, bool half)
    {
        if (half)  //if is jumping the first half
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < normalizedtime)
                    return true;
            }

            if (anim.GetNextAnimatorStateInfo(0).IsTag("Jump"))
            {
                if (anim.GetNextAnimatorStateInfo(0).normalizedTime < normalizedtime)
                    return true;
            }
        }
        else //if is jumping the second half
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > normalizedtime)
                    return true;
            }

            if (anim.GetNextAnimatorStateInfo(0).IsTag("Jump"))
            {
                if (anim.GetNextAnimatorStateInfo(0).normalizedTime > normalizedtime)
                    return true;
            }
        }


        return false;

    }
    bool isJumping()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
                return true;
        }
        if (anim.GetNextAnimatorStateInfo(0).IsTag("Jump"))
        {
                return true;
        }
        return false;
    }

    //--------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        getButtons(); //GET the Input Buttons
        FixPosition();
        Falling();
        TurnAmount();

        if ((horizontal != 0) || (vertical != 0) || Tired >= GotoSleep)
            stand = false;
        else stand = true;

        //Change velocity on ground!!
        if (!fly)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))   maxSpeed = 1f;
            if (Input.GetKeyDown(KeyCode.Alpha2))   maxSpeed = 2f;
            if (Input.GetKeyDown(KeyCode.Alpha3))   maxSpeed = 3f;
        }
        else  //Change velocity on air!!
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))   flyspeed = 1f;
            if (Input.GetKeyDown(KeyCode.Alpha2))   flyspeed = 1.25f;
            if (Input.GetKeyDown(KeyCode.Alpha3))   flyspeed = 1.35f;
        }


        int shiftSpeed = 1;
        float directionmult = 1; // for Strafe in air in horizontal 

        //SHift Ket Changes velocity
        if (ShiftK)
        {
            if (maxSpeed!=3)
            {
                shiftSpeed = 2;
            }
            if (fly)
            {
                directionmult = 2;
                DragoFloat = Mathf.Lerp(DragoFloat, 1, Time.deltaTime * 5f); //Glide on
            }
        }
        else
        {
            if (fly)
                DragoFloat = Mathf.Lerp(DragoFloat, 0, Time.deltaTime * 5f); //Glide off
        }

        speed = Mathf.Lerp(speed, maxSpeed * shiftSpeed, Time.deltaTime * 2f);            //smoothly transitions bettwen velocities
        direction = Mathf.Lerp(direction, horizontal*directionmult, Time.deltaTime * 8f);  //smoothly transitions bettwen directions



        if (Jump || attack2 || wounded || Stun) stand = false; //Stand False when doing some action
       
        if (fly)  //--------------------Controls the Fly Movement Up and Down
        {
            if (Jump)
            {
                upDown = Mathf.Lerp(upDown, 1, Time.deltaTime * 2);
            }

            else if (Input.GetKey(KeyCode.C))
            {
                upDown = Mathf.Lerp(upDown, -1, Time.deltaTime * 2);
            }

            else
            {
                upDown = Mathf.Lerp(upDown, 0, Time.deltaTime * 2);
            }
        }

        if (grounded)
        {
            fly = false;
            upDown = 0;
        }

        //Reset Sleep

      if (!stand || attack1 || attack2 || Jump || ShiftK)
        {
            Tired = 0;
        }
        LinkingAnimator(anim);
    }
}

