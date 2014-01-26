using UnityEngine;
using System.Collections;

public class NextLevelComponent : MonoBehaviour {
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player")
			Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			Application.LoadLevel(Application.loadedLevel + 1);
	}
	
}
