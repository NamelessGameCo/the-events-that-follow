/*
 * Author(s): Isaiah Mann
 * Description: Represents a conversation (with a knowledge of the conversation flow
 */

public class LConversationTree : LTree<LText> {
	public LConversationTreeNode CurrentNode;
	public LConversationTree(LText rootText) : base(rootText){
		CurrentNode = this.Root as LConversationTreeNode;
	}
}
