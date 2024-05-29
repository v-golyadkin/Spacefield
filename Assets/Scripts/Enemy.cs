using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    [SerializeField] private float _speed = 4f;
    [SerializeField] private int _health;

    private GameObject _scoreText;

    private void Awake()
    {
        _scoreText = GameObject.FindGameObjectWithTag("Score Text");
    }

    private void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - _speed * Time.deltaTime);

        transform.position = position;

        Vector2 minPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if(transform.position.y < minPosition.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player") || (collision.tag == "Player Bullet"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health--;

        if (_health <= 0)
        {
            SoundManager.instance.PlaySound2D("Explosion");

            Explosion();

            _scoreText.GetComponent<GameScore>().AddScore(10);

            Destroy(gameObject);
        }
    }

    private void Explosion()
    {
        GameObject explosion = (GameObject)Instantiate(_explosionPrefab);

        explosion.transform.position = transform.position;
    }
}
