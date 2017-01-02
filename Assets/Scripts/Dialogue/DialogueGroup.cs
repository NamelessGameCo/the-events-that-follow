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
		List<DialogueLine> allLines = new List<DialogueLine>(this.Elements);
		Dictionary<DialogueLine, DialogueLine[]> responses = new Dictionary<DialogueLine, DialogueLine[]>();
		foreach (DialogueLine line in allLines) {
			string[] responseIds = line.Responses;
			List<DialogueLine> matchingResponses = allLines.FindAll(response => ArrayUtil.Contains<string>(responseIds, response.ID));
			responses.Add(line, matchingResponses.ToArray());
		}
		Dictionary<DialogueLine, GraphNode<DialogueLine>> dialogueNodes = new Dictionary<DialogueLine, GraphNode<DialogueLine>>();
		foreach (DialogueLine line in responses.Keys) {
			dialogueNodes.Add(line, new GraphNode<DialogueLine>(line));
		}
		foreach (DialogueLine text in responses.Keys) {
			foreach (DialogueLine response in responses.Keys) {
				if (text.Responses.Contains(response.ID)) {
					dialogueNodes[text].AddNeighbour(dialogueNodes[response]);
				}
			}
		}
		Conversation = new ConversationGraph(dialogueNodes.Values.ToArray());
	}

}
