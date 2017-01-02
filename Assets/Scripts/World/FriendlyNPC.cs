/*
 * Author: Isaiah Mann
 * Desc: AI that can talk to player
 */

using UnityEngine;

public class FriendlyNPC : AIAgent {
	[SerializeField]
	string primaryConversationID;
	SpriteRenderer display;
	ConversationController dialogue;
	ConversationGraph currentConversation = null;
	DialogueLine currentLine;

	protected override void SetReferences () {
		base.SetReferences ();
		display = GetComponent<SpriteRenderer>();
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		dialogue = ConversationController.Instance;
	}

	void OnMouseUp () {
		if (InConversation()) {
			AdvanceConversation();
		} else {
			TryStartConversation(primaryConversationID);
		}
	}

	public void AdvanceConversation (int responseIndex = 0) {
		if (currentLine.Responses.Length == 0) { 
			EndConversation();
		} else {
			currentLine = currentConversation.GetResponses(currentLine)[responseIndex];
			ShowLine(currentLine);
		}
	}

	public bool TryStartConversation (string conversationID) {
		ConversationGraph conversation;
		if (dialogue.TryGetConversation(conversationID, out conversation)) {
			currentConversation = conversation;
			currentLine = currentConversation.GetFirstMessage().Value;
			ShowLine(currentLine);
			return true;
		} else {
			return false;
		}
	}

	public void ShowLine (DialogueLine line) {
		ui.ShowDialogue(this, line);
	}

	public void EndConversation () {
		currentConversation = null;
		ui.HideDialogue();
	}

	public bool InConversation () {
		return currentConversation != null;
	}

	public override bool TryGetPortrait (out Sprite portrait) {
		portrait = display.sprite;
		return true;
	}
}
