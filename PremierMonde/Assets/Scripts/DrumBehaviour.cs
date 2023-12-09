using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumBehaviour : MonoBehaviour
{
    private ParticleSystem Echos;
    private charactercontroller1stPerson charactercontroller1stPerson;

    // Start is called before the first frame update
    void Start()
    {
        Echos = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        charactercontroller1stPerson = GameObject.Find("PlayerVR").GetComponent<charactercontroller1stPerson>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Drum")
        {
            if (charactercontroller1stPerson.Action1())
            {
                Echos.Play();
            }
        }
    }
}
