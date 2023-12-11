using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using FMODUnity;
using FMODUnityResonance;

public class AbstractDreamMusicPlayer : MonoBehaviour
{
    public int melodyDuration;
    public int validationRange;
	public int silenceBetweenLoops;
	//public string melody;

    [Range(0, 1)]
    public float[] Partition;

    private int timer;
    private int id;
    private int index = 0;
    private int validation = 0;
	private bool isPlaying = false;


	private ParticleSystem EchoWaveGenerator;
	private StudioEventEmitter fmodMelody;

    private static GameObject MusicMaster;
    private static bool[] IDs;

    // Start is called before the first frame update
    void Start()
    {
        if(MusicMaster == null)
        {
            MusicMaster = GameObject.Find("AbstractDreamMusicMaster");
            MusicMaster.SetActive(false);
        }

        id = (IDs == null ? 0 : IDs.Length);
	    Array.Resize(ref IDs, id + 1);
        
	    fmodMelody = this.GetComponent<StudioEventEmitter>();

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
	    
	    if (timer == 0 ){
	    	//Debug.Log ("quoicoubeh");
	    	//FMODUnity.RuntimeManager.PlayOneShot("event:/" + melody);
	    	fmodMelody.Play();
	    	
	    }
        
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
                    this.GetComponent<Renderer>().material.color = Color.green;

                    foreach(bool i in IDs)
                    {
                        Debug.Log(i);

                        if (!i)
                        {
                            return;
                        }
                    }
                    MusicMaster.SetActive(true);
                }
            }
        }
    }
}
