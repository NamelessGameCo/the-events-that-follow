/*
 * Author: Isaiah Mann
 * Description: Controls the UI for The Events That Follow
 */

using UnityEngine;

public class ETFUIController : UIController {
	public static ETFUIController Instance {get; private set;}
	[SerializeField]
	WorldTextBox worldTextBoxPrefab;
	[SerializeField]
	Vector2 textBoxSize;

	DialogueDisplay dialogue;

	public WorldTextBox CreateWorldTextBox (string text, Vector3 position) {
		WorldTextBox textBox = Instantiate(worldTextBoxPrefab);
		textBox.Setup(text, position, textBoxSize);
		return textBox;
	}

	public void TeardownWorldTextBox (WorldTextBox textBox) {
		Destroy(textBox.gameObject);
	}

	protected override void SetReferences () {
		Instance = this;
		base.SetReferences ();
		dialogue = GetComponentInChildren<DialogueDisplay>();
	}
}
