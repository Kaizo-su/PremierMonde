using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesRotation : MonoBehaviour

{
	private Transform Camera;
	
    // Start is called before the first frame update
    void Start()
    {
	    Camera = GameObject.Find("Camera Offset").transform;
	    
    }

    // Update is called once per frame
    void Update()
    {
	    this.transform.LookAt(Camera);
	    this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
    }
}
