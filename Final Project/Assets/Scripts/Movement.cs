using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float maxSpeed = 10f;
    public bool isFacingRight = true;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.5f;
    public LayerMask whatIsGround;
    public float jumpforce = 700f;

    public int maxJetPackUse = 1400;
    public int jetPackPower = 70;
    public bool doubleJump = false;
    bool jumpedOnce = false;
    bool allowDoubleJump = false;
    float jetPack;

    // Use this for initialization
    void Start () {
         jetPack = maxJetPackUse;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (grounded && Input.GetAxis("Jump") != 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce));
            jumpedOnce = true;
        }

        if(Input.GetAxis("Jump") == 0 && jumpedOnce)
        {
            allowDoubleJump = true;
        }

        if (!grounded && Input.GetAxis("Jump") != 0 && doubleJump && allowDoubleJump)
        {
            doubleJump = false;
            allowDoubleJump = false;
            jumpedOnce = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce));
        }

        if(grounded && jetPack < maxJetPackUse)
        {
            jetPack += 0.1f;
        }

        if (!grounded && Input.GetAxis("Jump") != 0 && !doubleJump && maxJetPackUse > 0)
        {
            UseJetPack();
        }
    }

    void FixedUpdate(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if(GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            grounded = false;
        }

        if (grounded)
        {
            allowDoubleJump = false;
            jumpedOnce = false;
            doubleJump = true;
        }


        float move = Input.GetAxis("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();
    }

    void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void UseJetPack()
    {
        if (Input.GetKey(KeyCode.Space) && jetPack > 0)
        {
            jetPack--;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetPackPower));
        }

    }
}
