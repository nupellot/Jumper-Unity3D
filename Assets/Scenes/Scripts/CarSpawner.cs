using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject Car1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCar());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnCar() {
        while (true) {
            Debug.Log("Sosi");
            yield return new WaitForSeconds(1.5f);
        }
    }
}
