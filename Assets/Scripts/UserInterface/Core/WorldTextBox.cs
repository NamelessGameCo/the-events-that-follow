/*
* Author: Isaiah Mann
* Desc: Controls a text box in the world
*/

using UnityEngine;

public class WorldTextBox : UIElement {
	public void Setup (string text, Vector3 location, Vector2 size) {
		SetText(text);
		transform.position = location;
		transform2D.sizeDelta = size;
	}
}
