using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public int vitesse;

    private bool night;
    private Vector3 Rotation;

    // Start is called before the first frame update
    void Start()
    {
        Rotation = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(InstantSwitchCycle());
    }

    IEnumerator InstantSwitchCycle()
    {
        if (night)
        {
            Vector3 angles = transform.rotation.eulerAngles;
            angles.x += 180;
            transform.rotation = Quaternion.Euler(angles);
        }
        else
        {
            Vector3 angles = transform.rotation.eulerAngles;
            angles.x += 180;
            transform.rotation = Quaternion.Euler(angles);
        }
        yield return new WaitForSeconds(1);
        night = !night;
    }

    IEnumerator ToDay()
    {

        yield return new WaitForFixedUpdate();
    }

    IEnumerator ToNight()
    {

        yield return new WaitForFixedUpdate();
    }
}
