using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject Car1;
    private GameObject CarClone1;
    private GameObject CarClone2;
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
        // while (true) {
            Debug.Log("One more iteration");
            CarClone1 = Instantiate(Car1, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            CarClone2 = Instantiate(Car1, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            Debug.Log("After return");
            Destroy(CarClone2);
            yield return new WaitForSeconds(2.5f);
            Destroy(CarClone1);
        // }
    }
}

// Require the rocket to be a rigidbody.
// This way we the user can not assign a prefab without rigidbody
// public Rigidbody rocket;
// public float speed = 10f;
//
// void FireRocket ()
// {
//     Rigidbody rocketClone = (Rigidbody) Instantiate(rocket, transform.position, transform.rotation);
//     rocketClone.velocity = transform.forward * speed;
//
//     // You can also access other components / scripts of the clone
//     rocketClone.GetComponent<MyRocketScript>().DoSomething();
// }
//
// // Calls the fire method when holding down ctrl or mouse
// void Update ()
// {
//     if (Input.GetButtonDown("Fire1"))
//     {
//         FireRocket();
//     }
// }
