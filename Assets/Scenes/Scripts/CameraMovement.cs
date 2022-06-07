using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    // Игрок, за движением которого мы наблюдаем.
    public float height = 5;
    public float xShift = 10;
    public float zShift = 10;
    public float xAngle = 0;
    public float yAngle = 0;
    public float zAngle = 0;
    public float CameraSpeed = 0.04f;

    private GameObject Player;

    void Start()
    {
        Player = GameObject.Find("_Dude_");
    }

    // Update is called once per frame
    void Update()
    {
        // Находим насколько камера отстала от игрока.
        // Vector3 Difference = dude.position - transform.position;
        // // Difference.z = 0;  // Камера не должна прыгать вслед за игроком.
        // transform.Translate(Difference - new Vector3(0, Difference.y, 0));  // Перемещаем камеру вслед за игроком.
        // transform.Translate(new Vector3(10, 0, 10));

        float RubberSpeedMultiplier = Player.transform.position.z - this.transform.position.z;
        if (RubberSpeedMultiplier < 0) {
            RubberSpeedMultiplier *= 5;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x + xShift, height, Player.transform.position.z - zShift), Time.deltaTime * Math.Abs(RubberSpeedMultiplier) * CameraSpeed);
        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
    }
}
