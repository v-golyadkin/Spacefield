using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Rigidbody2D _body;
    private PlayerInputAction _playerInputAction;
    private PlayerAnimationVisual _playerVisual;

    [SerializeField] Image[] lifesIcon;

    [SerializeField] private GameObject _booster;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private GameObject _explosionPrefab;

    [SerializeField] private GameObject _gameStateController;

    private Vector2 _yBorder = new Vector2(1f, 10f);

    private bool _isMoving;
    [SerializeField] private float _speed = 4f;
    private int _health = 3;

    private void Start()
    {
        _playerVisual.InitAnimator(GetComponent<Animator>(), _booster.GetComponent<Animator>());
    }

    public void Init()
    {
        ResetLifes();

        gameObject.SetActive(true);

        gameObject.transform.position = new Vector3 (0, -3.5f, 0);
    }

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();

        _playerVisual = new PlayerAnimationVisual();
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();

        _playerInputAction.Player.Shoot.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SoundManager.instance.PlaySound2D("Player Shoot");

            GameObject bullet = Instantiate(_bulletPrefab, this.transform.position, transform.rotation, transform);

            Destroy(bullet, 1f);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = _playerInputAction.Player.Movement.ReadValue<Vector2>();

        _isMoving = inputVector.x != 0;
        _playerVisual.Moving(_isMoving);
        
        if(_isMoving)
        {
            _playerVisual.Move(inputVector);
        }

        _body.velocity = _speed * inputVector;
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
        _health--;
        
        if (_health <= 0)
        {

            gameObject.SetActive(false);

            _gameStateController.GetComponent<GameStateController>().SetGameState(GameStateController.GameState.GameOver);

            SoundManager.instance.PlaySound2D("Explosion");

            Explosion();
        }
        else
        {
            SoundManager.instance.PlaySound2D("Take Damage");
        }

        lifesIcon[_health].enabled = false;
    }

    private void ResetLifes()
    {
        _health = 3;

        lifesIcon[lifesIcon.Length - 1].enabled = true;
        lifesIcon[lifesIcon.Length - 2].enabled = true;
        lifesIcon[lifesIcon.Length - 3].enabled = true;
    }

    private void Explosion()
    {
        GameObject explosion = (GameObject) Instantiate(_explosionPrefab);

        explosion.transform.position = transform.position;
    }
}

public class PlayerAnimationVisual
{
    private Animator _playerAnimator;
    private Animator _boosterAnimator;

    public void InitAnimator(Animator player, Animator booster)
    {
        _playerAnimator = player;
        _boosterAnimator = booster;
    }

    public void Move(Vector2 motionVector)
    {
        _playerAnimator.SetFloat("Horizontal", motionVector.normalized.x);
        _boosterAnimator.SetFloat("Horizontal", motionVector.normalized.x);
    }

    public void Moving(bool isMoving)
    {
        _playerAnimator.SetBool("isMove", isMoving);
        _boosterAnimator.SetBool("isMove", isMoving);
    }
}
