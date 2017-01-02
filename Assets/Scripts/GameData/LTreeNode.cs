/*
 * Author(s): Isaiah Mann
 * Description: A generic tree node
 */

using System.Collections.Generic;

public class LTreeNode<LType> : LNode<LType> {
	public LType Value;
	public List<LTreeNode<LType>> Children = new List<LTreeNode<LType>>();

	public LTreeNode (LType value) {
		this.Value = value;
	}


	public void AddChild(LType child) {
		AddChild(new LTreeNode<LType>(child));
	}

	public void AddChildren(params LType[] children) {
		foreach (LType child in children) {
			AddChild(child);
		}
	}

	public void AddChild(LTreeNode<LType> child) {
		Children.Add(child);
	}

	public void AddChildren(params LTreeNode<LType>[] children) {
		foreach (LTreeNode<LType> child in children) {
			AddChild(child);
		}
	}
}
