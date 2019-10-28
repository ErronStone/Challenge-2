using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text life;

    private int scoreValue = 0;

    private int lifeValue = 3;

    public Text winText;

    private bool facingRight = true;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicCliptwo;
    Animator Anim;
    private int MoveHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        life.text = "Life: " + lifeValue.ToString();
        winText.text = "";
        Anim = GetComponent<Animator>();
        musicSource.clip = musicClipOne;
        musicSource.Play();
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Anim.SetInteger("state", 1);
            MoveHorizontal = 1;
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Anim.SetInteger("state", 1);
            MoveHorizontal = -1;  
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            Anim.SetInteger("state", 0);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Anim.SetInteger("state", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Anim.SetInteger("state", 2);
        }

        if (facingRight == false && MoveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && MoveHorizontal < 0)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        

       /* if (Input.GetKey("escape"))
        {
            Application.Quit();
        }*/


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector2(42.0f, 1.0f);
                lifeValue = 3;
                life.text = "Life: " + lifeValue.ToString();
            }

            if (scoreValue == 8)
            {
                winText.text = "You Win! Game created by Wyatt Amorin";
                musicSource.clip = musicCliptwo;
                musicSource.Play();
                speed = 0;


            }
        }

        if (collision.collider.tag == "enemy")
        {
            lifeValue -= 1;
            life.text = "Life: " + lifeValue.ToString();
            Destroy(collision.collider.gameObject);

            if (lifeValue == 0)
            {
                winText.text = "You Lose! Game created by Wyatt Amorin";
                gameObject.SetActive(false);
            }
        }

        if (collision.collider.tag == "Ground")
        {
            Anim.SetInteger("state", 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

        if (collision.collider.tag == "death")
        {
            transform.position = new Vector2(42.0f, 1.0f);
            lifeValue -= 1;
            life.text = "Life: " + lifeValue.ToString();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

        
}
