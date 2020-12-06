using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    private Player _player;
    private Animator _anim;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();  
        
        if (_player == null) {
            Debug.LogError("Player is null");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null) {
            Debug.LogError("Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Moves Enemy sprite down.
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        // if Enemy reaches the bottom of the screen, it re-spawns at the top of the screen at a new random position.
        if (transform.position.y < -5f) {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if Enemy collides with Player, Damage the Player and Destroy Enemy.

        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();

            if (player != null) {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.5f);
        }

        // Destroys Enemy & Laser if they collide, and updates score.
        if (other.tag == "Laser") {
            Destroy(other.gameObject);

            if (_player != null) {
                _player.AddScore();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.5f);
        }
    }
}
