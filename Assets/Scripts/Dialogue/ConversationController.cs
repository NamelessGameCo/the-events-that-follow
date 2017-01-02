/*
 * Author(s): Isaiah Mann
 * Description: Controls the texting system. Loads data
 */

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ConversationController : SingletonController<ConversationController>, IConversationController {
	protected StoryController story;
	const char JOIN_CHAR = '_';
	const string JSON_DIR = "JSON";
	const string DIALOGUE_DIR = "Dialogue";

	Dictionary<string, DialogueGroup> allConversations;

	public bool TryGetConversation (string conversationName, out DialogueGroup conversation) {
		return allConversations.TryGetValue(conversationName, out conversation);
	}

	protected override void SetReferences () {
		base.SetReferences ();
		allConversations = parseAllDialogue();
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		story = StoryController.Instance;
	}

	Dictionary<string, DialogueGroup> parseAllDialogue () {
		TextAsset[] textAssets = Resources.LoadAll<TextAsset>(Path.Combine(JSON_DIR, DIALOGUE_DIR));
		Dictionary<string, DialogueGroup> allDialogues = new Dictionary<string, DialogueGroup>();
		foreach (TextAsset text in textAssets) {
			DialogueGroup newGroup;
			// Adds the group to the dictionary and sets a local ref to it on a single line
			allDialogues.Add(text.name, newGroup = JsonUtility.FromJson<DialogueGroup>(text.text));
			// Creates a conversation graph
			newGroup.CreateConversationGraph();
		}
		return allDialogues;
	}

	public bool TryGetConversation (string conversationID, out ConversationGraph graph) {
		DialogueGroup group;
		if (allConversations.TryGetValue(conversationID, out group)) {
			graph = group.Conversation;
			return true;
		} else {
			graph = null;
			return false;
		}
	}
		
}
