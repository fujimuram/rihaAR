using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 100;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            GetComponent<Rigidbody>().AddForce(Vector3.left * speed, ForceMode.Force);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            GetComponent<Rigidbody>().AddForce(Vector3.right * speed, ForceMode.Force);
        }
    }
}
