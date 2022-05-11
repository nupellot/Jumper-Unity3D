using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInitializer : MonoBehaviour {

    public GameObject Roadway;  // Дорожное полотно, используемое в построении карты.
    public GameObject GrassSurface;  // Травяная поверхность, используемая в построении карты.
    // public float LineSize = 30;

    private List<GameObject> Roads = new List<GameObject>();
    private List<GameObject> Grasses = new List<GameObject>();
    private float RoadSize;
    private float GrassSurfaceSize;
    private List<int> Map = new List<int>() {0, 0, 0, 0, 0};  // Карта игрового поля.


    void Start()
    {
        RoadSize = Roadway.GetComponent<Renderer>().bounds.size.z;  // Ширина дороги.
        GrassSurfaceSize = GrassSurface.GetComponent<Renderer>().bounds.size.z;  // Ширина травяного покрова.
        GenerateMap(20);  // Получаем двоичную карту.

        Debug.Log(Map.Count);
        Debug.Log(System.String.Join(", ", Map));

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
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateMap(int MapSize = 10) {
        System.Random random = new System.Random();
        for (int i = 0; i < MapSize; i++) {
            int kek = random.Next(0, 2);
            // Debug.Log(kek);
            Map.Add(kek);
        }
    }


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
