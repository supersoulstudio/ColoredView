using UnityEngine;
using System.Collections;

public class GlassesComponent : MonoBehaviour
{
	private Color TargetColor = Color.red;
	private Light LeftGlasses;
	private Light RightGlasses;

	private bool Started = false;
	private bool TurnDone = false;

	public AudioClip MusicClip;

	public static bool IntroDone = false;

	void Start()
	{
		LeftGlasses = GameObject.Find("LeftGlasses").light;
		RightGlasses = GameObject.Find("RightGlasses").light;

		if (!IntroDone)
		{
			StartCoroutine(DoChangeColor());
		}
		else
		{
			Game.Camera.camera.orthographicSize = 7;
			Game.Manager.StartDelay /= 2; 
			Destroy(LeftGlasses);
			Destroy(RightGlasses);
			DoStart();
			TurnDone = true;
		}
	}

	private IEnumerator DoChangeColor()
	{
		TargetColor = Color.red;
		yield return new WaitForSeconds(1);
		TargetColor = Color.green;
		yield return new WaitForSeconds(1);
		TargetColor = Color.blue;
		yield return new WaitForSeconds(1);
		StartCoroutine(DoChangeColor());
	}

	private void DoStart()
	{
		Started = true;
		Game.Manager.Begin();
		Game.Camera.Begin();
		TurnDone = false;
		GameObject.Find("Title Camera").SetActive(false);

		if (Game.Music == null)
		{
			Game.Music = new GameObject();
			Game.Music.AddComponent("AudioSource");
			Game.Music.audio.clip = MusicClip;
			Game.Music.audio.volume = 0.1f;
			Game.Music.audio.loop = true;
			Game.Music.audio.Play();
			GameObject.DontDestroyOnLoad(Game.Music);
		}
	}

	void Update()
	{
		if (Input.anyKey && !Started)
		{
			DoStart();
			StartCoroutine(DoTurn());
		}
		else if (Started && !TurnDone)
		{
			Game.Player.IntroTurnAround();
		}
		if (!TurnDone)
		{
			LeftGlasses.color = Color.Lerp(LeftGlasses.color, TargetColor, Time.deltaTime / 0.5f);
			RightGlasses.color = Color.Lerp(RightGlasses.color, TargetColor, Time.deltaTime / 0.5f);
		}

	}

	private IEnumerator DoTurn()
	{
		yield return new WaitForSeconds(0.5f);
		TurnDone = true;
		yield return new WaitForSeconds(3f);
		Destroy(LeftGlasses);
		Destroy(RightGlasses);
		IntroDone = true;
	}
	

}
