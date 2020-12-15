using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 1.5f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;

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

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.LogError("AudioSource is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire) {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;

            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++) {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
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
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }

        // Destroys Enemy & Laser if they collide, and updates score.
        if (other.tag == "Laser") {
            Laser laser = other.transform.GetComponent<Laser>();

            if (laser._isEnemyLaser == false) {
                Destroy(other.gameObject);

                if (_player != null) {
                    _player.AddScore();
                }

                _anim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _audioSource.Play();

                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.5f);
            }
        }

    }
}
