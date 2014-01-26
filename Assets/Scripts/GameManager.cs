using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float StartDelay = 3f;
	public bool AutoStart = true;
	
	private void Awake ()
	{
		Game.Manager = this;
		Game.Color = this.GetComponent<ColorShiftComponent>();
		Game.Overlay = Camera.main.GetComponent<ScreenOverlay>();
		Game.Camera = Camera.main.GetComponent<IsometricFollowComponent>();
		Game.HUD = GameObject.Find("HUD").GetComponent<Animator>();
		Game.Player = GameObject.Find("Player").GetComponent<ThirdPersonUserControl>();
	}

	private void Start()
	{
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
		yield return new WaitForSeconds(StartDelay);
		Game.Player.InputEnabled = true;
		Game.Color.Begin();
		Game.HUD.transform.parent.GetComponent<Camera>().enabled = true;
	}

	// Update is called once per frame
	void Update () {
	
	}
}

public class Game
{
	public static GameManager Manager;
	public static ColorShiftComponent Color;
	public static ScreenOverlay Overlay;
	public static Animator HUD;
	public static ThirdPersonUserControl Player;
	public static IsometricFollowComponent Camera;
}
