using UnityEngine;
using System.Collections;

public class NextLevelComponent : MonoBehaviour
{
	public float Delay = 2f;

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player")
		{
			StartCoroutine(DoTransition());
		}

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			StartCoroutine(DoTransition());
		}
	}

private IEnumerator DoTransition()
{
		Game.Player.InputEnabled = false;
		Game.Color.FinishLevel();
	yield return new WaitForSeconds(Delay);
	Application.LoadLevelAsync(Application.loadedLevel + 1);
}
}
