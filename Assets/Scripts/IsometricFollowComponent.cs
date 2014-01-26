using UnityEngine;
using System.Collections;

public class IsometricFollowComponent : MonoBehaviour
{
	public GameObject TargetObj;
	public Vector3 Offset;

	public float IntroTime = 5f;
	public float ActiveTime = 0.5f;
	public float LerpTime = 5f;

	private bool Intro = true;

	public float IntroDelay = 0.1f;
	public bool AutoStart = true;
	public bool Running = false;

	public float ActiveSize = 5f;

	private void Start()
	{
		LerpTime = IntroTime;
		Intro = true;
		Running = false;

		if (AutoStart)
		{
			Begin();
		}
	}
	
	public void Begin()
	{
		StartCoroutine(DoStart());
	}

	private IEnumerator DoStart()
	{
		yield return new WaitForSeconds(IntroDelay);
		Running = true;
	}

	void FixedUpdate ()
	{
		if (Running)
		{
			if (Intro && (this.gameObject.transform.position == TargetObj.transform.position + Offset))
			{
				Intro = false;
				Camera.main.orthographicSize = ActiveSize;
				LerpTime = ActiveTime;
			}
			else if (Intro)
			{
				Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, ActiveSize, Time.deltaTime / LerpTime);
			}

			this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, TargetObj.transform.position + Offset, Time.deltaTime / LerpTime);
		}
	}
}
