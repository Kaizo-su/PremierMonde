using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class AbstractDreamMusicMaster : MonoBehaviour
{
    public int melodyDuration;
    public int validationRange;
    public int silenceBetweenLoops;

    [Range(0, 1)]
    public float[] Partition;

    private int timer;
    private int index = 0;
    private int validation = 0;
    private bool isPlaying = false;
    private bool isActivated = false;

    private ParticleSystem EchoWaveGenerator;
    private StudioEventEmitter fmodMelody;

    // Start is called before the first frame update
    void Start()
    {
        fmodMelody = this.GetComponent<StudioEventEmitter>();

        this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

        timer = - validationRange;
        silenceBetweenLoops -= validationRange;
        EchoWaveGenerator = this.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        if (isActivated)
        {
            // Manage time
            if (timer >= melodyDuration + silenceBetweenLoops)
            {
                timer = -validationRange;
                index = 0;
                validation = 0;
            }

            if (index < Partition.Length && timer == (int)(Partition[index] * melodyDuration) - validationRange)
            {
                EchoWaveGenerator.Play();
            }


            // Manage Melody

            if (timer == 0)
            {
                fmodMelody.Play();
            }

            if (index < Partition.Length && timer == (int)(Partition[index] * melodyDuration))
            {

                isPlaying = true;
                index++;
            }
            else
            {
                isPlaying = false;
            }

            // Manage Clock
            timer++;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActivated)
        {
            fmodMelody.Play();
            EchoWaveGenerator.Play();
        }

        if (isPlaying)
        {
            

            // Victory validation
            if (validation == Partition.Length)
            {

            }
        }
    }

    public void Active()
    {
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        isActivated = true;
    }
}
