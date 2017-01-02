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
	PlayerController player;
	DialogueDisplay dialogue;
	FriendlyNPC currentNPCInConversation;

	public void SetPlayer (PlayerController player) {
		this.player = player;
	}

	public WorldTextBox CreateWorldTextBox (string text, Vector3 position) {
		WorldTextBox textBox = Instantiate(worldTextBoxPrefab);
		textBox.Setup(text, position, textBoxSize);
		return textBox;
	}

	public void TeardownWorldTextBox (WorldTextBox textBox) {
		Destroy(textBox.gameObject);
	}

	Sprite getAgentPortrait (Agent agent) {
		Sprite portrait;
		if (agent.TryGetPortrait(out portrait)) {
			return portrait;
		} else {
			return null;
		}
	}

	Sprite getPortraitFromLine (DialogueLine line) {
		if (line.IsPlayer) {
			return getAgentPortrait(player);
		} else {
			return getAgentPortrait(currentNPCInConversation);
		}
	}

	public void ShowDialogue (FriendlyNPC fromNPC, DialogueLine line) {
		this.currentNPCInConversation = fromNPC;
		ShowDialogue(getPortraitFromLine(line), line);
	}

	public void ShowDialogue (Sprite sprite, DialogueLine line) {
		this.dialogue.Show();
		this.dialogue.SetSpeaker(line.Speaker, sprite);
		this.dialogue.SetText(line.Body);
	}

	public void HideDialogue () {
		this.dialogue.Hide();
		currentNPCInConversation = null;
	}

	public void AdvanceCurrentConversation () {
		if (currentNPCInConversation) {
			currentNPCInConversation.AdvanceConversation();
		}
	}

	protected override void SetReferences () {
		Instance = this;
		base.SetReferences ();
		dialogue = GetComponentInChildren<DialogueDisplay>();
	}
}
