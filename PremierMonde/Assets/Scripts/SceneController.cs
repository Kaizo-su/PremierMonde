using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
 
 void OnTriggerEnter(Collider other)
 
 {
 	
	 if(other.CompareTag("Player"))
	 {
		SceneManager.LoadScene(scenename);
	 }
 }
 
}
