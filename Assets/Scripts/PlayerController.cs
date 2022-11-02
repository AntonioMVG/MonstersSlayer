using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public variables
    public int speed;
    public int jumpForce;
    public int lives;
    public float levelTime; // In seconds
    public Canvas canvas;
    #endregion

    #region Private variables
    private Rigidbody2D rb;
    private GameObject foot;
    private SpriteRenderer sprite;
    private Animator playerAnimation;
    private GameManager gameManager;
    private HUDController hud;
    private CollectibleController colController;
    private bool hasJumped;
    private bool sneaking;
    private float initialPosX;
    private float updatedPosX;
    #endregion

    private void Start()
    {
        GameManager.instance.ResumeGame();
        rb = GetComponent<Rigidbody2D>();
        foot = transform.Find("Foot").gameObject;
        initialPosX = transform.position.x;

        // Get the sprite component of the child sprite object
        sprite = gameObject.transform.Find("player-idle-1").GetComponent<SpriteRenderer>();
        
        // Get the animator controller of the sprite child object
        playerAnimation = gameObject.transform.Find("player-idle-1").GetComponent<Animator>();

        // Get the HUD Controller
        hud = canvas.GetComponent<HUDController>();
        hud.SetLivesTxt(lives);
        hud.SetCollectiblesTxt(GameObject.FindGameObjectsWithTag("Collectibles").Length);
    }

    private void FixedUpdate()
    {
        // Get the value from -1 to 1 of the horizontal move
        float inputX = Input.GetAxis("Horizontal");

        // Apply physic velocity to the object with the move value * speed, the Y coordenate is the same
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        // Calculate if the time is finnish
        if (levelTime <= 0f)
        {
            //WinLevel(false);
            GameManager.instance.PauseGame();
            hud.SetTimesUpBox();
        }
        else
        {
            levelTime -= Time.deltaTime;
            hud.SetTimeTxt((int)levelTime);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TouchGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            hasJumped = true;
        }

        // Changing the sprite orientation
        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }

        // If the player press S or KeyDown, the squirrel crouches
        if((Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            sneaking = true;
        }
        else if ((Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.DownArrow)))
        {
            sneaking = false;
        }

        // Player animations
        PlayerAnimate();

        // Count how many steps the character takes according to its direction
        if ((Input.GetKeyUp(KeyCode.RightArrow)) || (Input.GetKeyUp(KeyCode.LeftArrow)))
        {
            updatedPosX = transform.position.x;
            initialPosX = updatedPosX;
        }
    }

    private bool TouchGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(foot.transform.position, Vector2.down, 0.2f);

        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            hasJumped = false;
        }
    }

    private void PlayerAnimate()
    {
        // Player Jumping
        if (!TouchGround())
        {
            playerAnimation.Play("playerJump");
        }
        // Player Running
        else if (TouchGround() && Input.GetAxisRaw("Horizontal") != 0)
        {
            playerAnimation.Play("playerRun");
        }
        // Player Idle
        else if (TouchGround() && Input.GetAxisRaw("Horizontal") == 0 && !sneaking)
        {
            sneaking = false;
            playerAnimation.Play("playerIdle");
        }
        // Player Crouch
        else if (TouchGround() && Input.GetKeyDown(KeyCode.S))
        {
            sneaking = true;
            playerAnimation.Play("playerCrouch");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectibles")
        {
            Destroy(collision.gameObject);
            Invoke(nameof(InfoCollectibles), 0.1f);
            GameManager.instance.Collectibles -= 1;
        }
    }

    private void InfoCollectibles()
    {
        int collectiblesNum = GameObject.FindGameObjectsWithTag("Collectibles").Length;
        hud.SetCollectiblesTxt(collectiblesNum);

        if (collectiblesNum == 0)
        {

        }
    }
}
