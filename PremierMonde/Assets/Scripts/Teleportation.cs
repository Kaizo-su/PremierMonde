using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
	public string scenename;
	public Color loadToColor = Color.black;
	public float transitionSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
	    /*  SceneManager.LoadScene("AbstractDream"); */
	    Initiate.Fade(scenename,loadToColor,transitionSpeed);
    }
}
