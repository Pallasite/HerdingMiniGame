using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Runner : MonoBehaviour
{
    public int num_chickens = 15;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("Chicken");
        for(int i = 0; i < num_chickens; i++)
        {
            GameObject c = GameObject.Instantiate(g);
            c.name = "Chicken_" + i;


            c.GetComponent<Rigidbody>().transform.position = new Vector3 ((i / (float)num_chickens * 10) - 5, 2.5f, -5.0f);
            c.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
