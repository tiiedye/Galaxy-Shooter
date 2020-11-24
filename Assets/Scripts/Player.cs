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
    [SerializeField]
    private float _speed = 3.5f;

    void Start()
    {
        // take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
