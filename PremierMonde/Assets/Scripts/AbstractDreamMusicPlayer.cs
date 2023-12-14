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
    public int id;
    //public string melody;

    [Range(0, 1)]
    public float[] Partition;

    private int timer;
    private int index = 0;
    private int validation = 0;
	private bool isPlaying = false;


	private ParticleSystem EchoWaveGenerator;
	private ParticleSystem EchoValidationGenerator;
	private ParticleSystem EchoCompletedGenerator;
	private ParticleSystem EchoErrorGenerator;
	private Transform Aura;
	private StudioEventEmitter fmodMelody;

    private static int progression = 0;
    private static bool isResing = false;
    private static GameObject MusicMaster;
    private static Transform Player;
    private static Color Blue = new Color(.16f, .6f, .7f, .3f);
    private static Color Green = new Color(.46f, .8f, .4f, .35f);
    private static Color Red = new Color(.8f, .2f, .2f, .35f);

    // Start is called before the first frame update
    void Start()
    {
        if(MusicMaster == null)
        {
            MusicMaster = GameObject.Find("AbstractDreamMusicMaster");
            //MusicMaster.SetActive(false);
        }
        
	    fmodMelody = this.GetComponent<StudioEventEmitter>();

        timer = - validationRange;
        silenceBetweenLoops -= validationRange;

        if(Player == null)
        Player = GameObject.Find("PlayerVR").transform;

        EchoWaveGenerator = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        EchoValidationGenerator = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        EchoCompletedGenerator = this.transform.GetChild(2).GetComponent<ParticleSystem>();
        EchoErrorGenerator = this.transform.GetChild(3).GetComponent<ParticleSystem>();
        Aura = this.transform.GetChild(4);
    }

    // Update is called once per fixed frame
    void FixedUpdate()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, Player.position));

        if(progression <= id)
        {
            if (Vector3.Distance(this.transform.position, Player.position) < 12)
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
            else
            {
                timer = -validationRange;
                index = 0;
                validation = 0;
            }

        }

        if (isResing)
        {
            StartCoroutine(Reset());
        }
        
    }

    [Obsolete]
    private void OnTriggerStay(Collider other)
    {
        // Collision avec la sphere qui defini la porté du son
        if (isPlaying && progression <= id)
        {
            EchoValidationGenerator.Play();
            validation++;

            // Valide que la musique a été joué entierement.
            if (validation == Partition.Length)
            {
                // Valide que celui qui joue est le bon
                if (id == progression)
                {
                    //this.GetComponent<Renderer>().material.color = Color.green;
                    EchoCompletedGenerator.Play();
                    Aura.GetChild(0).GetComponent<ParticleSystem>().startColor = Green;
                    progression++;

                    if (progression >= 4)
                        MusicMaster.GetComponent<AbstractDreamMusicMaster>().Active();
                }
                else
                {
                    progression = 0;
                    EchoErrorGenerator.Play();
                    Aura.GetChild(0).GetComponent<ParticleSystem>().startColor = Red;
                    isResing = true;
                }
            }
        }
    }

    [Obsolete]
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        Aura.GetChild(0).GetComponent<ParticleSystem>().startColor = Blue;
        isResing = false;
    }
}
