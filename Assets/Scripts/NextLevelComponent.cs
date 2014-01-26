using UnityEngine;
using System.Collections;

public class NextLevelComponent : MonoBehaviour
{
	public float Delay = 2f;
    public AudioClip TransitionSound;

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
		AudioSource.PlayClipAtPoint (TransitionSound, Camera.main.transform.position);
	yield return new WaitForSeconds(Delay);
		if (Application.loadedLevel + 1 >= Application.levelCount)
		{
			GlassesComponent.IntroDone = false;
			Application.LoadLevelAsync(0);
		}
		else
		{
			Application.LoadLevelAsync(Application.loadedLevel + 1);
		}
}
}
