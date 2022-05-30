using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
        // GameObject Player = this.gameObject;
    // public Vector3 InitialPosition = new Vector3(Player.GetComponent<Renderer>().bounds.size.x / 2, gameObject.GetComponent<Renderer>().bounds.size.y / 2 + 0.1, gameObject.GetComponent<Renderer>().bounds.size.z / 2);

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col) {
        // Debug.Log("Collision");
        if (this.gameObject.CompareTag("Player") && col.gameObject.CompareTag("Car")) {
            Debug.Log("You're Dead");
            this.transform.position = new Vector3(0, 5, 0);
        }
    }
}
