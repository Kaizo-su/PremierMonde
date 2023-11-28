using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller1stPerson : MonoBehaviour
{
    public byte Sensibility = 13;

    private Transform T_Camera;
    private CharacterController CC_3rdPerson;

    // Start is called before the first frame update
    void Start()
    {
        T_Camera = this.transform.GetChild(0);
        CC_3rdPerson = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        // Contrôle de la touche A pour lancer un echo
        /*
        if (Input.GetButtonUp("Fire1"))
        {
            if (Echo == null)
                return;
            GameObject.Instantiate(Echo).position = this.transform.position;
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Contrôle de la camera
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            T_Camera.localEulerAngles = new Vector3(T_Camera.localEulerAngles.x - Input.GetAxis("Mouse Y") * Sensibility, T_Camera.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensibility, 0);
        }

        // Contrôle les déplacements
        if (Math.Abs(Input.GetAxis("Horizontal")) > 0.1 || Math.Abs(Input.GetAxis("Vertical")) > 0.1)
        {
            Vector3 Direction = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) / 5);

            CC_3rdPerson.Move(Quaternion.Euler(0, T_Camera.localEulerAngles.y, 0) * Direction);
            CC_3rdPerson.Move(Vector3.down);
        }
    }
}
