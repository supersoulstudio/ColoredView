using UnityEngine;
using System.Collections;

public class ColorComponent : MonoBehaviour {

	public ColorShiftComponent.ColorType TypeOfColor;

	private void Start()
	{
		switch (TypeOfColor)
		{
			case ColorShiftComponent.ColorType.Red:
				Game.Color.AddRed(this.gameObject);
				break;
			case ColorShiftComponent.ColorType.Blue:
				Game.Color.AddBlue(this.gameObject);
				break;
			case ColorShiftComponent.ColorType.Green:
				Game.Color.AddGreen(this.gameObject);
				break;

		}
	}
}
