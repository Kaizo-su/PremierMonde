using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller1stPerson : MonoBehaviour
{
    public byte sensibility = 13;
    private int soundsDuration = 20;
    private int timer;

    private Transform T_Camera;
    private Collider SoundsRange;
    private CharacterController CC_3rdPerson;

    // Start is called before the first frame update
    void Start()
    {
        T_Camera = this.transform.GetChild(0);
        SoundsRange = this.transform.GetChild(2).GetComponent<Collider>();

        timer = soundsDuration;

        CC_3rdPerson = this.GetComponent<CharacterController>();

        SoundsRange.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Contrôle de la touche A pour lancer un echo
        if (Input.GetButtonUp("Fire1"))
        {
            if (SoundsRange.enabled == true)
            {
                StopCoroutine(Drum());
                SoundsRange.enabled = false;
            }
                

            StartCoroutine(Drum());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Contrôle de la camera
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            T_Camera.localEulerAngles = new Vector3(T_Camera.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensibility, T_Camera.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility, 0);
        }

        // Contrôle les déplacements
        if (Math.Abs(Input.GetAxis("Horizontal")) > 0.1 || Math.Abs(Input.GetAxis("Vertical")) > 0.1)
        {
            Vector3 Direction = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) / 5);

            CC_3rdPerson.Move(Quaternion.Euler(0, T_Camera.localEulerAngles.y, 0) * Direction);
            CC_3rdPerson.Move(Vector3.down);
        }
    }

    IEnumerator Drum()
    {
        timer = soundsDuration;
        SoundsRange.enabled = true;

        while (timer > 0)
        {
            timer--;
            yield return new WaitForFixedUpdate();
        }

        SoundsRange.enabled = false;
    }
}
