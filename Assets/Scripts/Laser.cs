using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserVelocity = 8.0f;

    // Update is called once per frame
    void Update()
    {
        // Moves laser up when spawned.
        transform.Translate(Vector3.up * _laserVelocity * Time.deltaTime);

        // Destroy Laser when off screen.
        if (transform.position.y > 8f) {
            Destroy(this.gameObject);
        }
    }
}
