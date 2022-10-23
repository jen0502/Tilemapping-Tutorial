using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
     // Score and Lives Text and Integers
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public int scoreValue;
    private int livesValue;
    private bool facingRight = true;
    public AudioClip LevelMusic;
    public AudioClip WinMusic;
    public AudioSource musicSource;

    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;

        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        SetCountText();
        WinTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);

        anim = GetComponent<Animator>();
        musicSource.clip = LevelMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
 void SetCountText()
    {
        countText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            WinTextObject.SetActive(true);
            Destroy(gameObject);
        }

        // Lives reset and Teleport to next level - (BUGGED RIGHT NOW) - Has to do with OnCollisionEnter2D function and SetCountText(); maybe??
        countText.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4) // How can I make this function only perform ONCE?? Cause when score equals 5, this function doesn't run. But as long as its 4, and a player collides with an enemy, it doesn't reduce lives and it teleports player again.
        {
            livesValue = 3;
            transform.position = new Vector2(64.03f, 2.07f);
        }

        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        // This Function Decreases Lives Value
        if (collision.collider.tag == "enemy") //this line is different compared to other tutorial because function is a collider
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetCountText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }

    void Update()
   {

       if (Input.GetKeyDown(KeyCode.W))
            {
           anim.SetInteger("State",2);
            }

       if (Input.GetKeyDown(KeyCode.D))
            {
           anim.SetInteger("State",1);
            }

       if (Input.GetKeyDown(KeyCode.A))
            {
           anim.SetInteger("State",1);
            }

       if (Input.GetKeyDown(KeyCode.LeftShift))
            {
              if (Input.GetKeyDown(KeyCode.D))
               {
               anim.SetInteger("State",2);
               }
            }   

       if (Input.GetKeyDown(KeyCode.LeftShift)) 
            {
              if (Input.GetKeyDown(KeyCode.A)) 
               {
               anim.SetInteger("State",2);
               }
            }   

        if (Input.GetKeyUp(KeyCode.D)) 
            {
                anim.SetInteger("State",0);
            }
        if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetInteger("State",0);
            }
        if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("State",0);
            }    
        if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetInteger("State",0);
            }     
        if (facingRight == false && Input.GetKeyDown(KeyCode.D))
            {
                Flip();
            }
            else if (facingRight == true && Input.GetKeyDown(KeyCode.A))
            {
                Flip();
            }

void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x*-1;
        transform.localScale = Scaler;
    }
   }
}
