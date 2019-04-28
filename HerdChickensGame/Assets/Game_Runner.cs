using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Runner : MonoBehaviour
{
    public int num_chickens;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("Chicken");
        for (int i = 0; i < num_chickens; i++)
        {
            GameObject c = GameObject.Instantiate(g);
            c.name = "Chicken_" + i;

            //float x_val = (i / (float)num_chickens * 5) - 5;
            float y_val = Random.Range(1.0f, 4.0f);
            float z_val;
            float x_val;

            if (i >= (num_chickens / 2))
            {
                z_val = -3.0f;
                x_val = Random.Range(-8.0f, 2.5f);
            }
            else
            {
                z_val = -5.0f;
                x_val = Random.Range(-8.0f, 5.0f);
            }

            c.GetComponent<Rigidbody>().transform.position = new Vector3(x_val, y_val, z_val);
            //c.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void Decrement_Num_Chickens()
    {
        num_chickens--;
    }

    public int Get_Num_Chickens()
    {
        return num_chickens;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
