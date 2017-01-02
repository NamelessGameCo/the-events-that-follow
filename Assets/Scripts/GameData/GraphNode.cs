/*
 * Author: Isaiah Mann
 * Description: A generic graphc node
 */

using System.Collections.Generic;

public class GraphNode<DataType> : Data {
	public DataType Value;
	public List<GraphNode<DataType>> Neighbors = new List<GraphNode<DataType>>();

	bool isBidirectional;

	public GraphNode (DataType value, GraphNode<DataType>[] neighbours = null, bool isBidirectional = false) {
		this.Value = value;
		this.isBidirectional = isBidirectional;
		if (neighbours != null) {
			AddNeighbours(neighbours);
		}
	}

	public void AddNeighbours (params GraphNode<DataType> [] neigbhors ) {
		foreach (GraphNode<DataType> node in neigbhors) {
			AddNeighbour(node);
		}
	}

	public void AddNeighbour (GraphNode<DataType> neighbour) {
		Neighbors.Add(neighbour);
		if (isBidirectional) {
			neighbour.AddNeighbour(this);
		}
	}
}
