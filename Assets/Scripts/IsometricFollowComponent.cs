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

	public static int LastScene = -1;

	private void Start()
	{
		LerpTime = IntroTime;
		Intro = true;
		Running = false;

		if (LastScene == Application.loadedLevel)
		{
			this.gameObject.transform.position = TargetObj.transform.position + Offset;
			Game.Manager.StartDelay /= 5;
		}

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
		LastScene = Application.loadedLevel;
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

/*
	public Texture2D fadeTexture;
	public float fadeSpeed = 0.2f;
	public int drawDepth = -1000;
	
	private float alpha = 1.0f; 
	private int fadeDir = -1;
	
	private void OnGUI()
	{
		
		alpha += fadeDir * fadeSpeed * Time.deltaTime;  
		alpha = Mathf.Clamp01(alpha);   
		
		GUI.color = new Color alpha;
		
		GUI.depth = drawDepth;
		
		GUI.DrawTexture(Rect(0, 0, Screen.width, Screen.height), fadeTexture);
	}
	*/
}
