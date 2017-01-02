/*
 * Author: Isaiah Mann
 * Description: Represents a group of texts in the mobile game Lucid
 */

using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class LTextGroup : LDataGroup<LText> {
	public LTime Time {get; private set;}
	public LConversationGraph Conversation {get; private set;}
	public void ParseTime () {
		Time = new LTime(GroupID);
	}

	public void CreateConversationGraph () {
		List<LText> allTexts = new List<LText>(this.Elements);
		Dictionary<LText, LText[]> responses = new Dictionary<LText, LText[]>();
		foreach (LText text in allTexts) {
			string[] responseIds = text.Responses;
			List<LText> matchingResponses = allTexts.FindAll(response => ArrayUtil.Contains<string>(responseIds, response.ID));
			responses.Add(text, matchingResponses.ToArray());
		}
		Dictionary<LText, LGraphNode<LText>> textNodes = new Dictionary<LText, LGraphNode<LText>>();
		foreach (LText text in responses.Keys) {
			textNodes.Add(text, new LGraphNode<LText>(text));
		}
		foreach (LText text in responses.Keys) {
			foreach (LText response in responses.Keys) {
				if (text.Responses.Contains(response.ID)) {
					textNodes[text].AddNeighbour(textNodes[response]);
				}
			}
		}
		Conversation = new LConversationGraph(textNodes.Values.ToArray());
	}

}
