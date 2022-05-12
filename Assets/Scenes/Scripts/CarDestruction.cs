using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnTriggerEnter(Collider other) {
    //     Debug.Log("Triggered");
    //     if (this.CompareTag("Car") && other.CompareTag("Barrier")) {
    //         Destroy(this.gameObject);
    //     }
    // }

    void OnCollisionEnter(Collision col) {
        Debug.Log("Collision");
        if (this.gameObject.CompareTag("Car") && col.gameObject.CompareTag("Barrier")) {
            Destroy(this.gameObject);
        }
    }
}
