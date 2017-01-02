/*
 * Author(s): Isaiah Mann
 * Description: Handles tracking progress in the game's narrative
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryController : SingletonController<StoryController>, IStoryController {
	DataController data;
	CharacterController characters;
	ConversationController messaging;

	List<Conversation> activeConversations = new List<Conversation>();
	[SerializeField]
	CharacterDescriptor player;
	public Conversation[] Conversations {
		get {
			return activeConversations.ToArray();
		}
	}

	protected override void SetReferences () {
		base.SetReferences ();
		Reset();
	}

	protected override void FetchReferences () {
		base.FetchReferences ();
		data = DataController.Instance;
		characters = CharacterController.Instance;
		messaging = ConversationController.Instance;
	}
		
	public CharacterDescriptor Player {
		get {
			return player;
		}
	}

	public void Set (Conversation[] conversations) {
		this.activeConversations = new List<Conversation>(conversations);
	}

	public void Reset () {
		activeConversations = new List<Conversation>();
	}

	public void TrackConversation (Conversation conversation) {
		// Remove any previous iterations of the conversation
		untrackConversation(conversation);
		activeConversations.Add(conversation);
		if (data) {
			data.Save();
		}
	}

	void untrackConversation (Conversation conversation) {
		Conversation tracked = activeConversations.Find(convo => convo.ID.Equals(conversation.ID));
		if (tracked != null) {
			activeConversations.Remove(tracked);
		}
	}

	public bool IsTrackingConversation (Conversation conversation) {
		return activeConversations.Find(convo => convo.ID.Equals(conversation.ID)) != null;
	}

	// Checks for whether all the conversations for the day have been complete
	public bool ReadyToAdvanceDayPhase () {
		foreach (CharacterDescriptor contact in characters.IContacts.Elements) {
			// Some contacts do not have any messages during certain day phases
			if (messaging.HasConversationForContactAtTime(contact)) {
				Conversation conversation = activeConversations.Find(convo => convo.ID .Equals(contact.Name));
				// Case: Conversation not yet begun or not yet complete
				if (conversation == null || !conversation.CheckIsComplete()) {
					return false;
				}
			}
		}
		return true;
	}

	public bool TryLoadConversation (string conversationID, out Conversation convo) {
		convo = activeConversations.Find(conversation => conversation.ID.Equals(conversationID));
		return convo != null;
	}

}
