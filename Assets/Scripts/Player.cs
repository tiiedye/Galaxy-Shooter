﻿/*
 * There are a lot of comments in this code that are notes for educational purposes,
 * and lines of code that have been commented out for me to refer back to as examples of alternative ways
 * to accomplish the same thing. I am still learning, and having examples to refer back to is a good
 * way for me to able to broaden my coding horizons. Generally I wouldn't have as many "obvious"
 * comments, otherwise. Also keep in mind this is my first game ever, and please enjoy!
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    // public || private reference, data type, variable name, optional value
    // if private add _ to start of variable name, to easily distinguish private variables at a glance.
    // serialize field attribute allows developer to read & modify private variables in the unity inspector, but other game objects
        // and scripts cannot touch it.

    // Player variables
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    [SerializeField]
    private GameObject _rightWingDmg, _leftWingDmg;
    private float _Iframe = 0.5f;
    private float _dmgBuffer = -1.0f;

    // Laser variables
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = .15f;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _explosionPrefab;

    // Triple Shot variables
    private bool _tripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    // Speed Boost Variables
    private bool _speedBoostActive = false;
    private float _speedBoostMultiplier = 2f;

    // Shield Variables
    private bool _shieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;

    // Spawn variables
    private SpawnManager _spawnManager;

    // UI Variables
    private UIManager _uiManager;

    // Audio Variables
    [SerializeField]
    private AudioClip _laserAudioClip;
    private AudioSource _audioSource;

    void Start()
    {
        // Zeroes out start position.
        transform.position = new Vector3(0, 0, 0);

        // Gets access to SpawnManager script
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.LogError("Spawn Manager is NULL");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) {
            Debug.LogError("UI Manager is NULL");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.LogError("Audio Source on the Player is NULL");
        } else {
            _audioSource.clip = _laserAudioClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) {
            FireLaser();
        }
    }

    void CalculateMovement() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // NOTE:
        // transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
                // above code does the same as below code:
        // transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
                // and the same again...:
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // Player bounds for y axis. NOTE: Can use Clamping or an if/else statement.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5), 0);

        // Player bounds for x axis, allows for player wrapping. Cannot use Clamping b/c of player wrapping.
        if (transform.position.x > 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x < -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        // Spawns Laser, and dictates Laser cooldown.
        _canFire = Time.time + _fireRate;

        // Determines whether to fire Triple Shot or normal Laser.
        if (_tripleShotActive == true) {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        } else {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        // play laser audio clip
        _audioSource.Play();

    }

    public void Damage()
    {

        if (Time.time > _dmgBuffer) {
            _dmgBuffer = Time.time + _Iframe;

            // If shields is active, take no damage and deactivate shields.
            if (_shieldActive == true) {
                _shieldActive = false;
                _shieldVisualizer.SetActive(false);
                return;
            } else {
                // Decrements number of lives.
                _lives--;
            }
        }


        _uiManager.UpdateLives(_lives);

        if (_lives == 2) {
            // right engine damage
            _rightWingDmg.SetActive(true);
        } else if (_lives == 1) {
            // left engine damage
            _leftWingDmg.SetActive(true);
        } else if (_lives < 1) {
            // communicate w/ spawn manager, stop spawning enemies
            _spawnManager.OnPlayerDeath();
            // Destroys player
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        // activates Triple Shot and begins to power down.
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }

    IEnumerator TripleShotPowerDown()
    {
        while (_tripleShotActive == true) {
            yield return new WaitForSeconds(5.0f);
            _tripleShotActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _speed *= _speedBoostMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        while (_speedBoostActive == true) {
            yield return new WaitForSeconds(5.0f);
            _speedBoostActive = false;
            _speed /= _speedBoostMultiplier;
        }
    }

    public void ShieldActive()
    {
        _shieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
}
