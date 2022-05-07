using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializer : MonoBehaviour {

    public GameObject Roadway;
    public GameObject GrassSurface;
    // public float LineSize = 30;
    private float RoadSize;
    private float GrassSurfaceSize;
    private List<int> Map = new List<int>();  // Карта игрового поля.


    void Start()
    {
        RoadSize = Roadway.GetComponent<Renderer>().bounds.size.z;  // Ширина дороги.
        GrassSurfaceSize = GrassSurface.GetComponent<Renderer>().bounds.size.z;  // Ширина травяного покрова.
        GenerateMap(10);

        // for (int i = 0; i < Map.Count; i++) {
        //     Debug.Log(Map[i]);
        // }

        Debug.Log(Map.Count);
        Debug.Log(System.String.Join(", ", Map));
        float privet = 0;
        for (int i = 0; i < Map.Count; i++) {
            if (Map[i] == 1) {
                // Debug.Log("Roadway at " + i );
                SpawnTerrain(Roadway, new Vector3(0, 0, (privet + RoadSize/2)));
                privet = privet + RoadSize;
                Debug.Log(privet);
            } else if (Map[i] == 0) {
                // Debug.Log("GrassSurface at " + i);
                SpawnTerrain(GrassSurface, new Vector3(0, 0, (privet + GrassSurfaceSize/2)));
                privet = privet + GrassSurfaceSize;
                Debug.Log(privet);
            }
            // else {
            //     Debug.Log("Wrong map element " + i + ": " + Map[i]);
            // }
            // DelayAction((float)2);
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


    void SpawnTerrain(GameObject Object, Vector3 Coords) {
        Instantiate(Object);
        Vector3 Size = Object.GetComponent<Renderer>().bounds.size;
        // Позиционируем объект, сдвигая на половину его длины, чтобы его начало оказалось в левом нижнем углу.
        // Object.transform.position = new Vector3(Coords.x - Size.x / 2, Coords.y - Size.y / 2, Coords.z - Size.z / 2);
        Object.transform.position = new Vector3(Coords.x, Coords.y, Coords.z);
        // Debug.Log(number + " positioned to " + Coords.x + " " + Coords.z);
        // Debug.Log("Spawn got " + number);
        // Object.name = ("Line" + number);
    }

IEnumerator DelayAction(float delayTime)
{
   //Wait for the specified delay time before continuing.
   yield return new WaitForSeconds(delayTime);
   //Do the action after the delay time has finished.
}
}
