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
    public GameObject Tree;
    public float CarSpeed = 10;
    public float MinCarSpawnTime = 1;
    public float MaxCarSpawnTime = 10;
    public int GenerationGap = 20;
    public int InitialPlayerZ = 5;
    public float StandardDeviation = 1;
    public float ExpectedValue = 0;
    // public float LineSize = 30;

    System.Random random = new System.Random();
    private List<GameObject> Roads = new List<GameObject>();
    private List<GameObject> Grasses = new List<GameObject>();
    private List<GameObject> Cars = new List<GameObject>();
    private List<GameObject> Trees = new List<GameObject>();
    private GameObject Player;
    private float RoadSize;
    private float GrassSurfaceSize;
    private float LineSize;
    private float LineLength;
    private float LineRatio;
    private int InitialFieldLength = 10;
    private int CurrentFieldLength = 0;
    private List<int> Map = new List<int>();  // Карта игрового поля.


    void Start()
    {
        // CarDestruction.singleton.CarIsDeleted += DeleteCar;
        // Создаём карту.
        RoadSize = Roadway.GetComponent<Renderer>().bounds.size.z;  // Ширина дороги.
        GrassSurfaceSize = GrassSurface.GetComponent<Renderer>().bounds.size.z;  // Ширина травяного покрова.
        LineSize = Roadway.GetComponent<Renderer>().bounds.size.z;
        LineLength = Roadway.GetComponent<Renderer>().bounds.size.x;
        LineRatio = LineLength / LineSize;
        // GenerateMap(InitialFieldLength);  // Получаем двоичную карту.
        // // Record.text;
        //
        CreateMap();
                // // Создаём само полотно.
        // float CurrentZ = 0;  // Переменная, отслеживающая крайнюю позицию карты.
        // for (int i = 0; i < Map.Count; i++) {
        //     if (Map[i] == 1) {  // Дорога.
        //         CurrentZ += RoadSize;
        //         // Создаём новый элемент дороги и сразу же засовываем его в массив.
                // Roads.Add(Instantiate(Roadway, new Vector3(0, 0, CurrentZ), Quaternion.identity));
                // Roads.Last().name = "Line" + i;  // Задаём имя, отоборажающееся в инспекторе.
        //         Debug.Log("Roadway at " + i);
        //     } else if (Map[i] == 0) {  // Травяной покров.
                // CurrentZ += GrassSurfaceSize;
                // Grasses.Add(Instantiate(GrassSurface, new Vector3(0, 0, CurrentZ), Quaternion.identity));
                // Grasses.Last().name = "Line" + i;
        //         Debug.Log("GrassSurface at " + i);
        //     } else {
        //         Debug.Log("Wrong map element " + i + ": " + Map[i]);
        //     }
        // }
        //
        // Делаем барьеры для автомобилей.
        GameObject LeftCarBarrier = Instantiate(CarBarrier, new Vector3(0 - GrassSurface.GetComponent<Renderer>().bounds.size.x / 2, 0, 0 + CarBarrier.GetComponent<Renderer>().bounds.size.x / 2), Quaternion.Euler(0, 0, 90));
        GameObject RightCarBarrier = Instantiate(CarBarrier, new Vector3(0 + GrassSurface.GetComponent<Renderer>().bounds.size.x / 2, 0, 0 + CarBarrier.GetComponent<Renderer>().bounds.size.x / 2), Quaternion.Euler(0, 0, 90));

        // Спавним игрока.
        Player = Instantiate(PlayerTemplate, new Vector3(0, PlayerTemplate.GetComponent<Renderer>().bounds.size.y / 2, InitialPlayerZ), Quaternion.Euler(0, 90, 0));
        Player.name = "_Dude_";

        // Спавним машины.
        // for (int i = 0; i < Roads.Count; i++) {
        //     Cars.Add(Instantiate(Car, Roads[i].transform.position - Roads[i].GetComponent<Renderer>().bounds.size / 2 + Car.GetComponent<Renderer>().bounds.size / 2, Quaternion.identity));
        //     Cars.Last().name = "Car" + i;
        // }
        // StartCarSpawn();
    }

    // Update is called once per frame
    void Update() {
        EnlargeMap();
        CleanDeletedCars();
        SetCarsSpeed();
        UpdateRecord();
    }

    void CreateMap() {
        for (int i = 0; i < InitialFieldLength; i++) {
            Map.Add(0);
            Grasses.Add(Instantiate(GrassSurface, new Vector3(0, 0, LineSize * CurrentFieldLength), Quaternion.identity));

            // Debug.Log("LineRatio " + LineRatio);
            for (int j = 0; j < LineRatio; j++) {
                if (random.Next(0, 1000000) < GetChanceOfSpawn(j - LineRatio / 2) * 1000000) {
                    Trees.Add(Instantiate(Tree, new Vector3(LineLength / LineRatio * j - LineLength / 2, 0, LineSize * CurrentFieldLength), Quaternion.identity));
                }
            }
            CurrentFieldLength++;
        }
    }

    void EnlargeMap() {
        Debug.Log("Map.Count " + Map.Count);
        if (Map.Count - ((PlayerPrefs.GetInt("CurrentZPosition"))) < GenerationGap) {
            Debug.Log("EnlargeMap");
            Map.Add(random.Next(0, 2));

            if (Map[Map.Count - 1] == 1) {  // Дорога.
                Roads.Add(Instantiate(Roadway, new Vector3(0, 0, LineSize * CurrentFieldLength), Quaternion.identity));
                Roads.Last().name = "Line" + CurrentFieldLength;  // Задаём имя, отоборажающееся в инспекторе.
                StartCarSpawner(Roads[Roads.Count - 1]);
            } else
            if (Map[Map.Count - 1] == 0) {  // Трава.
                Grasses.Add(Instantiate(GrassSurface, new Vector3(0, 0, LineSize * CurrentFieldLength), Quaternion.identity));
                Grasses.Last().name = "Line" + CurrentFieldLength;

                for (int j = 0; j < LineRatio; j++) {
                    if (random.Next(0, 1000000) < GetChanceOfSpawn(j - LineRatio / 2) * 1000000) {
                        Trees.Add(Instantiate(Tree, new Vector3(LineLength / LineRatio * j - LineLength / 2, 0, LineSize * CurrentFieldLength), Quaternion.identity));
                    }
                }
            }
            CurrentFieldLength++;
        }
    }


    void StartCarSpawn() {
        foreach (GameObject Road in Roads) {
            StartCoroutine(CarCoroutine(Road));
        }
    }

    void StartCarSpawner(GameObject Road) {
        StartCoroutine(CarCoroutine(Road));
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

    public void DeleteCar(GameObject Car) {
        Cars.Remove(Car);
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

    void UpdateRecord() {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerPrefs.SetInt("Record", 0);
            Debug.Log("Рекорд сброшен");
        }
    }

    // bool YesOrNo(float Probability) {
    //     if ()
    // }

    double NormalDistribution(float x) {
        return (1 / (System.Math.Sqrt(2 * System.Math.PI) * StandardDeviation) * System.Math.Pow(System.Math.E, -System.Math.Pow(x - ExpectedValue, 2) / (2 * System.Math.Pow(StandardDeviation, 2))));
    }

    double GetChanceOfSpawn(float x) {
        return (1 - NormalDistribution(x) / NormalDistribution(0));
    }

}
