using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class HashIDsDragons : MonoBehaviour
{

    [HideInInspector]    public int verticalHash;
    [HideInInspector]    public int horizontalHash;
    [HideInInspector]    public int updownHash;

    [HideInInspector]    public int standHash;

    [HideInInspector]    public int jumpHash;
    [HideInInspector]    public int flyHash;
    [HideInInspector]    public int dodgeHash;
    [HideInInspector]    public int fallHash;
    [HideInInspector]    public int groundedHash;
    [HideInInspector]    public int fowardPressedHash;
    [HideInInspector]    public int shiftHash;
    [HideInInspector]    public int flySpeedHash;

    [HideInInspector]    public int attack1Hash;
     [HideInInspector]    public int attack2Hash;
    [HideInInspector]    public int deathHash;
    
    [HideInInspector]    public int injuredHash;
    [HideInInspector]    public int stunnedHash;

    [HideInInspector]    public int intDragonHash;
    [HideInInspector]    public int floatDragonHash;
    void Awake()
    {
        verticalHash = Animator.StringToHash("Vertical");
        horizontalHash = Animator.StringToHash("Horizontal");
        updownHash = Animator.StringToHash("UpDown");

        standHash = Animator.StringToHash("Stand");
       
        jumpHash = Animator.StringToHash("Jump");
        flyHash = Animator.StringToHash("Fly");
        fallHash = Animator.StringToHash("Fall");
        groundedHash = Animator.StringToHash("Grounded");

        dodgeHash = Animator.StringToHash("Dodge");

        fowardPressedHash = Animator.StringToHash("FowardPressed");
        shiftHash = Animator.StringToHash("Shift");

        flySpeedHash = Animator.StringToHash("FlySpeed");
        deathHash = Animator.StringToHash("Death");
        attack1Hash = Animator.StringToHash("Attack1");
        attack2Hash = Animator.StringToHash("Attack2");

        injuredHash = Animator.StringToHash("Damaged");
        stunnedHash = Animator.StringToHash("Stunned");

        intDragonHash = Animator.StringToHash("DragoInt");
        floatDragonHash = Animator.StringToHash("DragoFloat");

        
    }

}

