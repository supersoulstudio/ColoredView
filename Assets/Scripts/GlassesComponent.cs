using UnityEngine;
using System.Collections;

public class GlassesComponent : MonoBehaviour
{
	private Color TargetColor = Color.red;
	private Light LeftGlasses;
	private Light RightGlasses;

	private bool Started = false;
	private bool TurnDone = false;

	void Start()
	{
		LeftGlasses = GameObject.Find("LeftGlasses").light;
		RightGlasses = GameObject.Find("RightGlasses").light;

		StartCoroutine(DoChangeColor());
	}

	private IEnumerator DoChangeColor()
	{
		TargetColor = Color.red;
		yield return new WaitForSeconds(2);
		TargetColor = Color.green;
		yield return new WaitForSeconds(2);
		TargetColor = Color.blue;
		yield return new WaitForSeconds(2);
		StartCoroutine(DoChangeColor());
	}

	void Update()
	{
		if (Input.anyKey && !Started)
		{
			Started = true;
			Game.Manager.Begin();
			Game.Camera.Begin();
			TurnDone = false;
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
	}
	

}
