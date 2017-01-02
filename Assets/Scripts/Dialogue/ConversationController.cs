/*
 * Author(s): Isaiah Mann
 * Description: Controls the texting system. Loads data
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationController : SingletonController<ConversationController>, IConversationController {
	protected StoryController story;
	const char JOIN_CHAR = '_';
	const string TEXTING_PATH = "Texting";
	Dictionary<string, DialogueGroup> allConversations;

	public bool TryGetConversation (string conversationName, out DialogueGroup conversation) {
		return allConversations.TryGetValue(conversationName, out conversation);
	}

	protected override void SetReferences () {
		base.SetReferences ();
		allConversations = parseAllTexts();
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		story = StoryController.Instance;
	}

	Dictionary<string, DialogueGroup> parseAllTexts () {
		TextAsset[] textAssets = Resources.LoadAll<TextAsset>(TEXTING_PATH);
		Dictionary<string, DialogueGroup> allTexts = new Dictionary<string, DialogueGroup>();
		foreach (TextAsset text in textAssets) {
			DialogueGroup newGroup;
			// Adds the group to the dictionary and sets a local ref to it on a single line
			allTexts.Add(text.name, newGroup = JsonUtility.FromJson<DialogueGroup>(text.text));

			// Creates a conversation graph
			newGroup.CreateConversationGraph();
		}
		return allTexts;
	}

	string getConversationName (CharacterDescriptor contact) {
		throw new System.NotImplementedException();
	}

	public ConversationGraph GetConversation (CharacterDescriptor contact) {
		DialogueGroup group;
		if (allConversations.TryGetValue(getConversationName(contact), out group)) {
			return group.Conversation;
		} else {
			return null;
		}
	}

	// Checks against the current time
	public bool HasConversationForContactAtTime (CharacterDescriptor contact) {
		return GetConversation(contact) != null;
	}
}
