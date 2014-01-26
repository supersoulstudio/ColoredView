using UnityEngine;
using System.Collections;

public class NextLevelComponent : MonoBehaviour
{
	public float Delay = 2f;
    public AudioClip TransitionSound;
	private bool ShowResults = false;

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
		Results = ((int)(Time.time - Game.Manager.StartTime)).ToString() + " seconds\n \n" + Game.Manager.ChangeCount.ToString() + " color changes";
		ShowResults = true;
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

	public GUIStyle style;
	
	private string Results;

	private void OnGUI () 
	{	
		if (ShowResults)
		{
			GUI.Label(new Rect(400,75,50,50), Results,style);
			Debug.Log("test");
		}
	}
}
