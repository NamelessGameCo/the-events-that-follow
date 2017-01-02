/*
 * Author: Isaiah Mann
 * Desc: Controls display of dialogue
 */

using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : UIElement {
	[SerializeField]
	Text dialogeText;
	[SerializeField]
	Text speakerName;

	public override void Show () {
		toggleCanvasGroup(canvas, active:true);	
	}

	public override void Hide () {
		toggleCanvasGroup(canvas, active:false);
	}

	public override void SetText (string text) {
		this.dialogeText.text = text;
	}

	public void SetSpeaker (string speakerName) {
		this.speakerName.text = speakerName;
	}
}
