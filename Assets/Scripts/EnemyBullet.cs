using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Vector2 _direction;
    private bool _isReady = false;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;

        _isReady = true;
    }

    void Update()
    {
        if(_isReady)
        {
            Vector2 position = transform.position;

            position += _direction * _speed * Time.deltaTime;

            transform.position = position;

            Vector2 minPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 maxPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if((transform.position.x > maxPosition.x) || (transform.position.x < minPosition.x) || (transform.position.y > maxPosition.y) || (transform.position.y < minPosition.y))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
