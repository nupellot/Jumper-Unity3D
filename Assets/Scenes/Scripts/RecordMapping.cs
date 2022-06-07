using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordMapping : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject.transform.Translate(10000, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Record: " + PlayerPrefs.GetInt("Record").ToString();
    }
}
