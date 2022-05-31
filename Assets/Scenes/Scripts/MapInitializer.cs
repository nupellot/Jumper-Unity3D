using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInitializer : MonoBehaviour {

    public GameObject Roadway;  // Дорожное полотно, используемое в построении карты.
    public GameObject GrassSurface;  // Травяная поверхность, используемая в построении карты.
    public GameObject Car;
    public GameObject CarBarrier;
    public GameObject PlayerTemplate;
    public float CarSpeed = 10;
    public float MinCarSpawnTime = 1;
    public float MaxCarSpawnTime = 10;
    // public float LineSize = 30;

    private List<GameObject> Roads = new List<GameObject>();
    private List<GameObject> Grasses = new List<GameObject>();
    public List<GameObject> Cars = new List<GameObject>();
    private GameObject Player;
    private float RoadSize;
    private float GrassSurfaceSize;
    private List<int> Map = new List<int>() {0, 0, 0, 0, 0};  // Карта игрового поля.


    void Start()
    {
        // Создаём карту.
        RoadSize = Roadway.GetComponent<Renderer>().bounds.size.z;  // Ширина дороги.
        GrassSurfaceSize = GrassSurface.GetComponent<Renderer>().bounds.size.z;  // Ширина травяного покрова.
        GenerateMap(50);  // Получаем двоичную карту.
        Record.text;

        // Создаём само полотно.
        float CurrentZ = 0;  // Переменная, отслеживающая крайнюю позицию карты.
        for (int i = 0; i < Map.Count; i++) {
            if (Map[i] == 1) {  // Дорога.
                CurrentZ += RoadSize;
                // Создаём новый элемент дороги и сразу же засовываем его в массив.
                Roads.Add(Instantiate(Roadway, new Vector3(0, 0, CurrentZ), Quaternion.identity));
                Roads.Last().name = "Line" + i;  // Задаём имя, отоборажающееся в инспекторе.
                Debug.Log("Roadway at " + i);
            } else if (Map[i] == 0) {  // Травяной покров.
                CurrentZ += GrassSurfaceSize;
                Grasses.Add(Instantiate(GrassSurface, new Vector3(0, 0, CurrentZ), Quaternion.identity));
                Grasses.Last().name = "Line" + i;
                Debug.Log("GrassSurface at " + i);
            } else {
                Debug.Log("Wrong map element " + i + ": " + Map[i]);
            }
        }

        // Делаем барьеры для автомобилей.
        GameObject LeftCarBarrier = Instantiate(CarBarrier, new Vector3(0 - GrassSurface.GetComponent<Renderer>().bounds.size.x / 2, 0, 0 + CarBarrier.GetComponent<Renderer>().bounds.size.x / 2), Quaternion.Euler(0, 0, 90));
        GameObject RightCarBarrier = Instantiate(CarBarrier, new Vector3(0 + GrassSurface.GetComponent<Renderer>().bounds.size.x / 2, 0, 0 + CarBarrier.GetComponent<Renderer>().bounds.size.x / 2), Quaternion.Euler(0, 0, 90));

        // Спавним игрока.
        Player = Instantiate(PlayerTemplate, new Vector3(0, PlayerTemplate.GetComponent<Renderer>().bounds.size.y / 2, 5), Quaternion.Euler(0, 90, 0));
        Player.name = "_Dude_";

        // Спавним машины.
        // for (int i = 0; i < Roads.Count; i++) {
        //     Cars.Add(Instantiate(Car, Roads[i].transform.position - Roads[i].GetComponent<Renderer>().bounds.size / 2 + Car.GetComponent<Renderer>().bounds.size / 2, Quaternion.identity));
        //     Cars.Last().name = "Car" + i;
        // }
        StartCarSpawn();
    }

    // Update is called once per frame
    void Update() {
        CleanDeletedCars();
        SetCarsSpeed();

    }

    void StartCarSpawn() {
        foreach (GameObject Road in Roads) {
            StartCoroutine(CarCoroutine(Road));
        }
    }

    IEnumerator CarCoroutine(GameObject Road) {
        while (true) {
            Cars.Add(Instantiate(Car, Road.transform.position - Road.GetComponent<Renderer>().bounds.size / 2 + Car.GetComponent<Renderer>().bounds.size / 2, Quaternion.identity));
            // Cars.Last().name = "Car" + i;
            float delay = Random.Range(MinCarSpawnTime, MaxCarSpawnTime);
            yield return new WaitForSeconds(delay);
        }
    }


    void GenerateMap(int MapSize = 10) {
        System.Random random = new System.Random();
        for (int i = 0; i < MapSize; i++) {
            int kek = random.Next(0, 2);
            // Debug.Log(kek);
            Map.Add(kek);
        }
    }

    void CleanDeletedCars() {
        for (int i = 0; i < Cars.Count; i++) {
            if (Cars[i] == null) {
                Cars.Remove(Cars[i]);
                // Debug.Log("Car is deleted");
            }
        }
        // Debug.Log(Cars.Count + " Машин на карте в данный момент");
    }

    void SetCarsSpeed() {
        for (int i = 0; i < Cars.Count; i++) {
            if (Cars[i] != null) {
                Cars[i].GetComponent<Rigidbody>().velocity = new Vector3(CarSpeed, 0, 0);
            } else
            if (Cars[i] == null) {
                Debug.Log("Moving deleted car");
                // Судя по всему, в обработке удалённой машины нет ничего критичного.
            }
        }
    }
    // void OnCollisionEnter(Collision col) {
    //     Debug.Log("Collision");
    //     if (this.gameObject.CompareTag("Car") && col.gameObject.CompareTag("Barrier")) {
    //         Destroy(this.gameObject);
    //     }
    // }

    // private void OnTriggerEnter(Collider other) {
    //     Debug.Log("Collision");
    //     if (this.CompareTag("Car") && other.CompareTag("Barrier")) {
    //         Destroy(this.gameObject);
    //     }
    // }


    // void Spawn(GameObject Object, Vector3 Coords, int number) {
    //     Instantiate(Object);
    //     Vector3 Size = Object.GetComponent<Renderer>().bounds.size;
    //     // Позиционируем объект, сдвигая на половину его длины, чтобы его начало оказалось в левом нижнем углу.
    //     // Object.transform.position = new Vector3(Coords.x - Size.x / 2, Coords.y - Size.y / 2, Coords.z - Size.z / 2);
    //     Object.transform.position = new Vector3(Coords.x, Coords.y, Coords.z);
    //     Debug.Log(number + " " + " Positioned to " + Coords.x + " " + Coords.z);
    //     Debug.Log("Spawn got " + number);
    //     Object.name = "Line " + number;
    // }
}
