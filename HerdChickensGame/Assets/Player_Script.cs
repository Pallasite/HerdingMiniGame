using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public Rigidbody player;
    public Vector3 player_position;

    void Start()
    {
        player = GetComponent<Rigidbody>();
        player.useGravity = false;
        player.isKinematic = true;
    }

    private void Update()
    {

        Vector3 position = this.transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= 0.15f;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Vector3 position = this.transform.position;
            position.x += 0.15f;
            this.transform.position = position;
        }

        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Vector3 position = this.transform.position;
            position.z += 0.15f;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Vector3 position = this.transform.position;
            position.z -= 0.15f;
            this.transform.position = position;
        }
        */
        

        player_position = position;
    }

    /*
    // Display the changing position of the sphere.
    void OnGUI()
    {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 24;
        GUI.Label(new Rect(20, 20, 300, 50), "Position: " + player_position.ToString("F2"), fontSize);
    }
    */

}