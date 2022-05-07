using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class govnopad : MonoBehaviour
{

    public GameObject govno;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(govno, govno.transform.position, govno.transform.rotation);
    }

    private float nextActionTime = 0.0f;
    public float period = 0.001f;

    void Update () {
        if (Time.time > nextActionTime ) {
        nextActionTime += period;
          Instantiate(govno, govno.transform.position + new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)), govno.transform.rotation);
        }
    }
}
