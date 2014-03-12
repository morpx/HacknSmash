using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10;
    public float jumpForce = 500;
    
    private bool facingRight = true;
    
    //variabler som har med huruvida man rör marken att göra
    bool grounded = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    float groundRadius = 0.2f;

    bool attacking = false;

    double deathtimer = 0.0;

    private float currentHeight;

    Animator anim;

    void Start() 
    {
        anim = GetComponent<Animator>();
    }

    public float timer;
    public float time = 10;

    void Update()
    {
        var currentBaseState = anim.GetCurrentAnimatorStateInfo(0).nameHash;
        Debug.LogError(currentBaseState);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(PlayOnce("Attacking"));
        }

        
    }


    private IEnumerator PlayOnce(string paramName)
    {
        
        anim.SetBool(paramName, true);
        yield return null;
        anim.SetBool(paramName, false);
    }

    void FixedUpdate()
    {
        

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        //så här mkt trycker vi på "left/right"-knapparna
        float move = Input.GetAxis("Horizontal");

        //sätter vår animators speed till absoluta värdet av move
        anim.SetFloat("Speed", Mathf.Abs(move));

        //hur mkt vi trycker * maxSpeed, existerande y-hastighet
        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (rigidbody2D.velocity.y < -20 && grounded)
        {
            Debug.LogError("You died LOL");
            Splat();
        }
    }

    void Splat()
    {
        Destroy(gameObject, 0.2f);
    }

    void Flip()
    {
        //byter mellan true/false
        facingRight = !facingRight;

        //plockar ut local scale, flippar den på x-axeln, reassignar
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
