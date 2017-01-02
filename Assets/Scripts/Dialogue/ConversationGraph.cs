/*
 * Author(s): Isaiah Mann
 * Description: A graph that holds conversations
 */

using UnityEngine;
using System.Collections.Generic;

public class ConversationGraph : Graph<DialogueLine> {
	Dictionary<DialogueLine, GraphNode<DialogueLine>> textLookup;

	public ConversationGraph (params GraphNode<DialogueLine>[] texts) {
		this.Nodes = texts;
		initLookup(texts);
	}

	// Currently just returns a random response because most AI texts only have one option (can be overriden)
	public virtual DialogueLine ChooseResponse (DialogueLine toText) {
		DialogueLine[] potentialResponses = GetResponses(toText);
		if (potentialResponses != null && potentialResponses.Length > 0) {
			return potentialResponses[Random.Range(0, potentialResponses.Length)];
		} else {
			return null;
		}
	}

	public DialogueLine[] GetResponses (DialogueLine toText) {
		GraphNode<DialogueLine> node;
		if (textLookup.TryGetValue(toText, out node)) {
			DialogueLine[] respones = new DialogueLine[node.Neighbors.Count];
			for (int i = 0; i < respones.Length; i++) {
				respones[i] = node.Neighbors[i].Value;
			}
			return respones;
		} else {
			return null;
		}
	}

	public GraphNode<DialogueLine> GetFirstMessage () {
		int lowestIdNum = int.MaxValue;
		int indexOfLowestId = -1;
		for (int i = 0; i < this.Nodes.Length; i++) {
			if (Nodes[i].Value.IDNum < lowestIdNum) {
				lowestIdNum = Nodes[i].Value.IDNum;
				indexOfLowestId = i;
			}
		}
		if (indexOfLowestId == -1) {
			return null;
		} else {
			return Nodes[indexOfLowestId];
		}

	}

	void initLookup (GraphNode<DialogueLine> [] texts) {
		textLookup = new Dictionary<DialogueLine, GraphNode<DialogueLine>>();
		foreach (GraphNode<DialogueLine> textNode in texts) {
			textLookup.Add(textNode.Value, textNode);
		}
	}
}
