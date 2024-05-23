using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerInputAction playerInputAction;
    private PlayerAnimationVisual playerVisual;

    [SerializeField] Image[] lifesIcon;

    [SerializeField] private GameObject booster;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private GameObject gameStateController;

    private Vector2 yBorder = new Vector2(1f, 10f);

    private bool isMoving;
    [SerializeField] private float speed = 4f;
    private int health = 3;

    private void Start()
    {
        playerVisual.InitAnimator(GetComponent<Animator>(), booster.GetComponent<Animator>());
    }

    public void Init()
    {
        ResetLifes();

        gameObject.SetActive(true);

        gameObject.transform.position = new Vector3 (0, -3.5f, 0);
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        playerVisual = new PlayerAnimationVisual();
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Shoot.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SoundManager.instance.PlaySound2D("Shoot");

            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, transform.rotation, transform);

            Destroy(bullet, 1f);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = playerInputAction.Player.Movement.ReadValue<Vector2>();

        isMoving = inputVector.x != 0;
        playerVisual.Moving(isMoving);
        
        if(isMoving)
        {
            playerVisual.Move(inputVector);
        }

        body.velocity = speed * inputVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Enemy") || (collision.tag == "Enemy Bullet"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health--;
        
        if (health <= 0)
        {

            gameObject.SetActive(false);

            gameStateController.GetComponent<GameStateController>().SetGameState(GameStateController.GameState.GameOver);

            SoundManager.instance.PlaySound2D("Explosion");

            Explosion();
        }
        else
        {
            SoundManager.instance.PlaySound2D("Take Damage");
        }

        lifesIcon[health].enabled = false;
    }

    private void ResetLifes()
    {
        health = 3;

        lifesIcon[lifesIcon.Length - 1].enabled = true;
        lifesIcon[lifesIcon.Length - 2].enabled = true;
        lifesIcon[lifesIcon.Length - 3].enabled = true;
    }

    private void Explosion()
    {
        GameObject explosion = (GameObject) Instantiate(explosionPrefab);

        explosion.transform.position = transform.position;
    }
}

public class PlayerAnimationVisual
{
    private Animator playerAnimator;
    private Animator boosterAnimator;

    public void InitAnimator(Animator player, Animator booster)
    {
        playerAnimator = player;
        boosterAnimator = booster;
    }

    public void Move(Vector2 motionVector)
    {
        playerAnimator.SetFloat("Horizontal", motionVector.normalized.x);
        boosterAnimator.SetFloat("Horizontal", motionVector.normalized.x);
    }

    public void Moving(bool isMoving)
    {
        playerAnimator.SetBool("isMove", isMoving);
        boosterAnimator.SetBool("isMove", isMoving);
    }
}
