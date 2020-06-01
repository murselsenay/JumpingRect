using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField]

    private Touch touch;
    private Vector2 beginTouchPosition, endTouchPosition;

    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float angle;
    private float swipeDistanceX;
    private float swipeDistanceY;

    public Rigidbody2D playerBody;
    Vector2 playerScale;
    public float moveSpeed = 2f;
    bool isGrounded;
    public float jumpAmount = 7;
    //internal bool death = false;
    int jumpPowerUpTimeRemain;
    private ParticleSystem particle;

    public static Player instance;


    void Start()
    {
        instance = this;
    }
    void Awake()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerScale = transform.localScale;
        particle = GetComponentInChildren<ParticleSystem>();

    }
    void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
       

    }
    void Move()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
            playerScale.x = -0.07f;
            transform.localScale = playerScale;
        }
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            playerBody.velocity = new Vector2(-moveSpeed, playerBody.velocity.y);
            playerScale.x = 0.07f;
            transform.localScale = playerScale;
        }
        if (Input.GetAxisRaw("Vertical") > 0f && isGrounded)
        {
            playerBody.velocity = Vector2.up * jumpAmount;
        }

        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).tapCount == 2 && isGrounded)
                {
                    playerBody.velocity = Vector2.up * jumpAmount;
                }
                if (Input.GetTouch(i).tapCount == 1)
                {
                    Debug.Log("Single tap..");
                }
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    playerBody.velocity = new Vector2(-moveSpeed, playerBody.velocity.y);
                    playerScale.x = 0.07f;
                    transform.localScale = playerScale;
                }

                if (touch.position.x > Screen.width / 2)
                {
                    playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
                    playerScale.x = -0.07f;
                    transform.localScale = playerScale;
                }
            }
        }
    }

    public void PlatformMove(float x)
    {
        playerBody.velocity = new Vector2(x, playerBody.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "SpikyTile" || target.tag == "TopSpike")
        {
            gameObject.transform.localRotation = Quaternion.identity;
            playerBody.isKinematic = true;
            playerBody.velocity = Vector3.zero;
            playerBody.gravityScale = 0;

            StartCoroutine(Death());

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (target.gameObject.tag == "Coin")
        {
            LevelManager.instance.collectedCoin++;
            AudioManager.instance.PlaySound("CoinCollected");
            Destroy(target.gameObject);
        }
        if (target.gameObject.tag == "JumpMore")
        {
            jumpAmount = 9;
            StartCoroutine(jumpPowerUp());
            Destroy(target.gameObject);
        }
    }
    internal IEnumerator Death()
    {
        particle.Play();
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
        SceneManager.LoadScene(0);

    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Tile")
        {
            isGrounded = true;
        }

    }
    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.tag == "Tile")
        {
            isGrounded = false;
        }

    }
    internal IEnumerator jumpPowerUp()
    {
        jumpPowerUpTimeRemain = 0;
        while (jumpPowerUpTimeRemain<3)
        {
            yield return new WaitForSeconds(1);
            jumpPowerUpTimeRemain = jumpPowerUpTimeRemain + 1;
            Debug.Log(jumpPowerUpTimeRemain.ToString());
        }
        jumpAmount = 7;
        yield break;
        
    }
}

   
