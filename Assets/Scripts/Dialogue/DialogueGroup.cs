/*
 * Author: Isaiah Mann
 * Description: Represents a group of texts in the mobile game Lucid
 */

using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class DialogueGroup : DataGroup<DialogueLine> {
	public ConversationGraph Conversation {get; private set;}

	public void CreateConversationGraph () {
		List<DialogueLine> allTexts = new List<DialogueLine>(this.Elements);
		Dictionary<DialogueLine, DialogueLine[]> responses = new Dictionary<DialogueLine, DialogueLine[]>();
		foreach (DialogueLine text in allTexts) {
			string[] responseIds = text.Responses;
			List<DialogueLine> matchingResponses = allTexts.FindAll(response => ArrayUtil.Contains<string>(responseIds, response.ID));
			responses.Add(text, matchingResponses.ToArray());
		}
		Dictionary<DialogueLine, GraphNode<DialogueLine>> textNodes = new Dictionary<DialogueLine, GraphNode<DialogueLine>>();
		foreach (DialogueLine text in responses.Keys) {
			textNodes.Add(text, new GraphNode<DialogueLine>(text));
		}
		foreach (DialogueLine text in responses.Keys) {
			foreach (DialogueLine response in responses.Keys) {
				if (text.Responses.Contains(response.ID)) {
					textNodes[text].AddNeighbour(textNodes[response]);
				}
			}
		}
		Conversation = new ConversationGraph(textNodes.Values.ToArray());
	}

}
