using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float maxSpeed = 10f;
    public bool isFacingRight = true;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpforce = 700f;

    public int maxJetPackUse = 1400;
    public int jetPackPower = 70;
    public bool doubleJump = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce));
        }

        if (!grounded && Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            doubleJump = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce));
        }

        if (!grounded && Input.GetKey(KeyCode.Space) && !doubleJump && maxJetPackUse > 0)
        {
            UseJetPack();
        }
    }

    void FixedUpdate(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (grounded) { doubleJump = true; }


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
        if (Input.GetKey(KeyCode.Space) && maxJetPackUse > 0)
        {
            maxJetPackUse--;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetPackPower));
        }

    }
}
