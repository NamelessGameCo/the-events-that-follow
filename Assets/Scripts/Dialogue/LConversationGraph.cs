/*
 * Author(s): Isaiah Mann
 * Description: A graph that holds conversations
 */

using UnityEngine;
using System.Collections.Generic;

public class LConversationGraph : LGraph<LText> {
	Dictionary<LText, LGraphNode<LText>> textLookup;

	public LConversationGraph (params LGraphNode<LText>[] texts) {
		this.Nodes = texts;
		initLookup(texts);
	}

	// Currently just returns a random response because most AI texts only have one option (can be overriden)
	public virtual LText ChooseResponse (LText toText) {
		LText[] potentialResponses = GetResponses(toText);
		if (potentialResponses != null && potentialResponses.Length > 0) {
			return potentialResponses[Random.Range(0, potentialResponses.Length)];
		} else {
			return null;
		}
	}

	public LText[] GetResponses (LText toText) {
		LGraphNode<LText> node;
		if (textLookup.TryGetValue(toText, out node)) {
			LText[] respones = new LText[node.Neighbors.Count];
			for (int i = 0; i < respones.Length; i++) {
				respones[i] = node.Neighbors[i].Value;
			}
			return respones;
		} else {
			return null;
		}
	}

	public LGraphNode<LText> GetFirstMessage () {
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

	void initLookup (LGraphNode<LText> [] texts) {
		textLookup = new Dictionary<LText, LGraphNode<LText>>();
		foreach (LGraphNode<LText> textNode in texts) {
			textLookup.Add(textNode.Value, textNode);
		}
	}
}
