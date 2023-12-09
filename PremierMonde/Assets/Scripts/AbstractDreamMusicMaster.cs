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
    private int id;
    private int index = 0;
    private int validation = 0;
    private bool isPlaying = false;

    private ParticleSystem EchoWaveGenerator;

    private static bool[] IDs;

    // Start is called before the first frame update
    void Start()
    {
        id = (IDs == null ? 0 : IDs.Length);
        Array.Resize(ref IDs, id + 1);

        timer = - validationRange;
        silenceBetweenLoops -= validationRange;
        EchoWaveGenerator = this.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        // Manage time
        if (timer >= melodyDuration + silenceBetweenLoops)
        {
            timer = - validationRange;
            index = 0;
            validation = 0;
        }

        if (index < Partition.Length && timer == (int) (Partition[index] * melodyDuration) - validationRange)
        {
            EchoWaveGenerator.Play();
        }

        // Manage Melody
        if (index < Partition.Length && timer == (int) (Partition[index] * melodyDuration))
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

    private void OnTriggerStay(Collider other)
    {

        if (isPlaying)
        {
            validation++;


            // Victory validation
            if (validation == Partition.Length)
            {

                if (id == 0 || IDs[id - 1] == true)
                {
                    IDs[id] = true;

                    foreach(bool i in IDs)
                    {
                        if (!i)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
