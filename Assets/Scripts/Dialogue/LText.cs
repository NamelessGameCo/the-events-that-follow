/*
 * Author: Isaiah Mann
 * Description: Represents a text in the mobile game Lucid
 */

[System.Serializable]
public class LText : Data {
	public string ID;
	public int IDNum {
		get {
			int idNum;
			if (tryParseFirstInt(ID, out idNum)) {
				return idNum;
			} else {
				return DEFAULT_INT_VALUE;
			}
		}
	}
	public string choice;
	public string Sender;
	public string Receiever;
	public string Body;
	public string[] Responses;
	public bool FromPlayer;

	// Used to determine equality
	public override string ToString () {
		return string.Format ("[LText: ID={0}, Body={1}, Sender={2}, Receiver={3}, Responses={4}]", ID, Body, Sender, Receiever, ArrayUtil.ToString(Responses));
	}

	public override int GetHashCode () {
		return ToString().GetHashCode();
	}

	public override bool Equals (object obj) {
		if (obj is LText) {
			return (obj as LText).ToString().Equals(this.ToString());
		} else {
			return base.Equals (obj);
		}
	}
}
