using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserVelocity = 8.0f;
    private float _enemyLaserVelocity = 4.0f;
    public bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false) {
            MoveUp();
        } else {
            MoveDown();
        }
    }

    void MoveUp()
    {
        // Moves laser up when spawned.
        transform.Translate(Vector3.up * _laserVelocity * Time.deltaTime);

        // Destroy Laser when off screen.
        if (transform.position.y > 8f) {
            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        // Moves Enemy laser down when spawned.
        transform.Translate(Vector3.down * _enemyLaserVelocity * Time.deltaTime);

        // Destroy Laser when off screen.
        if (transform.position.y < -8f) {
            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true) {
            Player player = other.GetComponent<Player>();

            if (player != null) {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
