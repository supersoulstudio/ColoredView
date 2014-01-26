using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorShiftComponent : MonoBehaviour
{
	public Texture2D RedOverlay;
	public Texture2D GreenOverlay;
	public Texture2D BlueOverlay;

	private bool lDown = false;
	private bool rDown = false;

	private Color TargetColor;
	private bool ColorChanging = false;

	private void Update ()
	{
		if (Game.Player.InputEnabled)
		{
			if (Input.GetAxis("Xbox360ControllerTriggersL") > 0.2f)
			{
				lDown = true;
			}
			else if ((lDown) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Comma))
			{
				UpdateColor(-1, true);
				lDown = false;
			}

			if (Input.GetAxis("Xbox360ControllerTriggersR") > 0.2f)
			{
				rDown = true;
			}
			else if ((rDown) || Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.Period))
			{
				UpdateColor(1, true);
				rDown = false;
			}
		}
	}

	private void Start()
	{
		Camera.main.backgroundColor = Color.white;
		TargetColor = Color.white;
	}
	
	private void FixedUpdate()
	{
		if (ColorChanging)
		{
			Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, TargetColor, Time.deltaTime / 0.2f);

			//if (Camera.main.backgroundColor == TargetColor)
			//{
				switch (CurrentColor)
				{
					case ColorShiftComponent.ColorType.Red:
						Game.Overlay.texture = RedOverlay;
						break;
						
					case ColorShiftComponent.ColorType.Green:
						Game.Overlay.texture = GreenOverlay;
						break;
						
					case ColorShiftComponent.ColorType.Blue:
						Game.Overlay.texture = BlueOverlay;
						break;				
				}
				Game.Overlay.enabled = true;
				//ColorChanging = false;
			//}
			//else
			//{
			//	Game.Overlay.enabled = false;
			//}
		}
	}

	public void Begin()
	{
		CurrentColor = ColorType.Red;
		UpdateColor(0, false);
	}

	private void UpdateColor(int Delta, bool Animate)
	{
		if (CurrentColor + Delta > ColorType.Blue)
		{
			CurrentColor = ColorType.Red;
		}
		else if (CurrentColor + Delta < ColorType.Red)
		{
			CurrentColor = ColorType.Blue;
		}
		else
		{
			CurrentColor += Delta;
		}

		//Debug.Log(CurrentColor);
		//Debug.Log((int)CurrentColor);

		switch (CurrentColor)
		{
			case ColorShiftComponent.ColorType.Red:
				ToggleList(RedList, false);
				ToggleList(BlueList, true);
				ToggleList(GreenList, true);
				TargetColor = new Color(0.71f, 0.2f, 0.2f);
				if (Animate)
				{
					Game.HUD.SetBool("ToBlue", false);
					Game.HUD.SetBool("ToGreen", false);
					Game.HUD.SetBool("ToRed", true);
				}
				break;

			case ColorShiftComponent.ColorType.Green:
				ToggleList(RedList, true);
				ToggleList(BlueList, true);
				ToggleList(GreenList, false);
				TargetColor = new Color(0.2f, 0.71f, 0.2f);
				if (Animate)
				{
					Game.HUD.SetBool("ToRed", false);
					Game.HUD.SetBool("ToBlue", false);
					Game.HUD.SetBool("ToGreen", true);
				}
				break;

			case ColorShiftComponent.ColorType.Blue:
				ToggleList(RedList, true);
				ToggleList(BlueList, false);
				ToggleList(GreenList, true);
				TargetColor = new Color(0.2f, 0.2f, 0.71f);
				if (Animate)
				{
					Game.HUD.SetBool("ToRed", false);
					Game.HUD.SetBool("ToGreen", false);
					Game.HUD.SetBool("ToBlue", true);
				}
				break;
		}

		ColorChanging = true;
	}

	private ColorType CurrentColor;

	private void ToggleList(List<GameObject> list, bool Enabled)
	{
		for (int i = 0; i < list.Count; i++)
		{
			list[i].SetActive(Enabled);
		}
	}

	private List<GameObject> RedList = new List<GameObject>();

	public void AddRed(GameObject obj)
	{
		RedList.Add(obj);
	}

	private List<GameObject> GreenList = new List<GameObject>();
	
	public void AddGreen(GameObject obj)
	{
		GreenList.Add(obj);
	}

	private List<GameObject> BlueList = new List<GameObject>();
	
	public void AddBlue(GameObject obj)
	{
		BlueList.Add(obj);
	}

	public enum ColorType {Black = 0, White = 1, Red = 2, Green = 3, Blue = 4};
}
