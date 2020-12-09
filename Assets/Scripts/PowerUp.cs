using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 3.0f;
    [SerializeField] // 0 = TripleShot, 1 = SpeedBoost, 2 = Shields
    private int powerUpID;
    [SerializeField]
    private AudioClip _powerUpClip;

    // Update is called once per frame
    void Update()
    {
        // Moves PowerUp down and destroys it if it goes off screen.
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (transform.position.y < -4.5f) {
            Destroy(this.gameObject);
        }
    }

    // Destroys PowerUp if collected by Player, and activates the TripleShot PowerUp.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            // communicate w/ player script
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);

            if(player != null) {
                switch (powerUpID) {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
