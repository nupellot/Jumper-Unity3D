using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

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
      Debug.Log(PlayerPrefs.GetInt("CurrentZPosition") + " " + PlayerPrefs.GetInt("Record"));
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
        this.transform.position = GameObject.Find("Directional Light").GetComponent<MapInitializer>().Spawn;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (PlayerPrefs.GetInt("CurrentZPosition") - 1 >  PlayerPrefs.GetInt("Record")) {
            PlayerPrefs.SetInt("Record", PlayerPrefs.GetInt("CurrentZPosition") - 1);
        }
        PlayerPrefs.SetInt("CurrentZPosition", 0);
        Input.ResetInputAxes();
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
