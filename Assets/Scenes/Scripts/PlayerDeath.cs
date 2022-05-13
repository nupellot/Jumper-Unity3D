using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col) {
        Debug.Log("Collision");
        if (this.gameObject.CompareTag("Player") && col.gameObject.CompareTag("Car")) {
            Debug.Log("You're Dead");
        }
    }
}
