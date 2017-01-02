/*
 * Author(s): Isaiah Mann
 * Description: Controls the texting system. Loads data
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LMessageController : SingletonController<LMessageController>, IMessageController {
	LStoryController story;
	const char JOIN_CHAR = '_';
	const string TEXTING_PATH = "Texting";
	Dictionary<string, LTextGroup> allConversations;

	public bool TryGetConversation (string conversationName, out LTextGroup conversation) {
		return allConversations.TryGetValue(conversationName, out conversation);
	}

	protected override void SetReferences () {
		base.SetReferences ();
		allConversations = parseAllTexts();
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		story = LStoryController.Instance;
	}

	Dictionary<string, LTextGroup> parseAllTexts () {
		TextAsset[] textAssets = Resources.LoadAll<TextAsset>(TEXTING_PATH);
		Dictionary<string, LTextGroup> allTexts = new Dictionary<string, LTextGroup>();
		foreach (TextAsset text in textAssets) {
			LTextGroup newGroup;
			// Adds the group to the dictionary and sets a local ref to it on a single line
			allTexts.Add(text.name, newGroup = JsonUtility.FromJson<LTextGroup>(text.text));

			// Parse the time of the group now that we have a ref
			newGroup.ParseTime();

			// Creates a conversation graph
			newGroup.CreateConversationGraph();
		}
		return allTexts;
	}

	string getConversationName (CharacterDescriptor contact) {
		LTime time = story.CurrentTime;
		return string.Format("{0}{4}{1}{2}{4}{3}", contact.ContactName, LTime.DAY, time.Day, time.Phase, JOIN_CHAR);
	}

	public LConversationGraph GetConversation (CharacterDescriptor contact) {
		LTextGroup group;
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
