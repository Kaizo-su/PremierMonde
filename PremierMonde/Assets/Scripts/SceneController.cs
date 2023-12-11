using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEditor.XR.Management;

/*public class SceneController : MonoBehaviour
{
	public string scenename;
		
	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene(scenename);
		}
	}
}
*/

public class SceneController : MonoBehaviour

{
	
	public string scenename;
	private InputDevice targetDevice;
	public Color loadToColor = Color.black;
	public float transitionSpeed; 
	
	void OnTriggerEnter(Collider other)
	{
		/*targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue); 
		if(primaryButtonValue == true)
		{
			Debug.Log("Pressing Primary Button");
			SceneManager.LoadScene(scenename);	 
		} */
		
		Initiate.Fade(scenename,loadToColor,transitionSpeed);
		
	}	
 
}
