using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 5.0f;
    private float _asteroidSpeed = 0.25f;
    [SerializeField]
    private GameObject _explosionPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _asteroidSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);

        // if Asteroid reaches the bottom of the screen, it gets destroyed.
        if (transform.position.y < -5f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser") {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 0.15f);
        }

        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();

            if (player != null) {
                player.Damage();
            }

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
        }
    }
}
