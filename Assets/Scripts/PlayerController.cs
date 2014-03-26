using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //vapen-bools
    public bool mace = false;
    public bool sword = true;

    private float maxSpeed = 10;
    private float jumpForce = 500;
    
    private bool facingRight = true;
    
    //variabler som har med huruvida man rör marken att göra
    private bool grounded = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    float groundRadius = 0.2f;

    bool attacking = false;
	bool blocking = false;
	bool divebombing = false;

    double deathtimer = 0.0;

    private float currentHeight;

    Animator anim;
    WeaponController weapon;
    private int attackStateId;

    void Awake()
    {
		//vårt id för attackanimationen
        attackStateId = Animator.StringToHash("Base Layer.Attack");
        //Debug.Log(left_hand);
    }

    void Start() 
    {
        anim = GetComponent<Animator>();
    }

	//körs i sista framen av vår attackanimation
    void resetAttack()
    {
        attacking = false;
    }


    void Update()
    {
        //kolla vapen-bools, byt vapen
        if (mace == true)
        {

        }



		//om attackanimationen har startats sätter vi attack-boolean till false, så att vi inte kan göra en ny attack förrän den animerats färdigt
        if (anim.IsInTransition(0) && anim.GetNextAnimatorStateInfo(0).nameHash == attackStateId)
            anim.SetBool("Attacking", false);

		//om vi inte blockar och hoppar
        if (grounded && Input.GetKeyDown(KeyCode.Space) && blocking == false)
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            
        }

		//om vi trycker på attack och inte redan attackerar
        if (Input.GetKeyDown(KeyCode.Q) && attacking == false)
        {
            anim.SetBool("Attacking", true);
            attacking = true;
        }

		//om vi är på marken och blockar
		if (Input.GetKeyDown(KeyCode.E) && grounded) 
		{
			blocking = true;
			anim.SetBool("Blocking", true);
		}

		//om vi slutar blocka
		if (Input.GetKeyUp(KeyCode.E) && grounded) 
		{
			blocking = false;
			anim.SetBool("Blocking", false);
		}

		//om vi blockar i luften
		if (Input.GetKeyDown(KeyCode.E) && !grounded) 
		{
			divebombing = true;
			anim.SetBool("Divebombing", true);
		}

		if (grounded) 
		{
			divebombing = false;
			anim.SetBool("Divebombing", false);
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

		if(blocking)
			rigidbody2D.velocity = new Vector2(move * maxSpeed/3, rigidbody2D.velocity.y);
        //hur mkt vi trycker * maxSpeed, existerande y-hastighet
		else
        	rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

		
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
