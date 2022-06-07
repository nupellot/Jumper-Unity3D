using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("CurrentZPosition") < 0) {
            KillThePlayer();
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            KillThePlayer();
        }
    }

    void OnCollisionEnter(Collision col) {
        if (this.gameObject.CompareTag("Player") && col.gameObject.CompareTag("Car")) {
            KillThePlayer();
        }
    }

    void KillThePlayer() {
        Debug.Log("You're Dead");
        this.transform.position = new Vector3(0, GetComponent<Renderer>().bounds.size.y / 2 + PlayerPrefs.GetFloat("GapBetweenZeroAndPlayer"), 0);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        PlayerPrefs.SetInt("CurrentZPosition", 0);
        Input.ResetInputAxes();
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
