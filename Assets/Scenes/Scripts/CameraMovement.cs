using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Игрок, за движением которого мы наблюдаем.
    [SerializeField] private Transform dude;
    public float height = 5;
    public float xShift = 10;
    public float zShift = 10;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Находим насколько камера отстала от игрока.
        // Vector3 Difference = dude.position - transform.position;
        // // Difference.z = 0;  // Камера не должна прыгать вслед за игроком.
        // transform.Translate(Difference - new Vector3(0, Difference.y, 0));  // Перемещаем камеру вслед за игроком.
        // transform.Translate(new Vector3(10, 0, 10));

        transform.position = new Vector3(dude.position.x + xShift, height, dude.position.z - zShift);
    }
}
