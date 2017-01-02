/*
 * Author(s): Isaiah Mann
 * Description: A game save for Lucid
 */

[System.Serializable]
public class GameSave : Data {
	public Conversation[] Conversations;

	public GameSave (Conversation[] conversations) {
		this.Conversations = conversations;
	}
}
