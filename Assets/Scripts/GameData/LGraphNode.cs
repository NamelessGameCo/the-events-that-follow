/*
 * Author: Isaiah Mann
 * Description: A generic graphc node
 */

using System.Collections.Generic;

public class LGraphNode<LType> : Data {
	public LType Value;
	public List<LGraphNode<LType>> Neighbors = new List<LGraphNode<LType>>();

	bool isBidirectional;

	public LGraphNode (LType value, LGraphNode<LType>[] neighbours = null, bool isBidirectional = false) {
		this.Value = value;
		this.isBidirectional = isBidirectional;
		if (neighbours != null) {
			AddNeighbours(neighbours);
		}
	}

	public void AddNeighbours (params LGraphNode<LType> [] neigbhors ) {
		foreach (LGraphNode<LType> node in neigbhors) {
			AddNeighbour(node);
		}
	}

	public void AddNeighbour (LGraphNode<LType> neighbour) {
		Neighbors.Add(neighbour);
		if (isBidirectional) {
			neighbour.AddNeighbour(this);
		}
	}
}
