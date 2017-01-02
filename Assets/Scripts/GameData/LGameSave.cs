/*
 * Author(s): Isaiah Mann
 * Description: A game save for Lucid
 */

[System.Serializable]
public class LGameSave : Data {
	public LTime Time;
	public LConversation[] Conversations;

	public LGameSave (LTime time, LConversation[] conversations) {
		this.Time = time;
		this.Conversations = conversations;
	}
}
