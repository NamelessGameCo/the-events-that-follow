/*
 * Author: Isaiah Mann
 * Description: Generic structure to store serialized data for Lucid
 */

[System.Serializable]
public class LDataGroup<T> : SerializableDataCollection<T> {
	public string GroupID;
}
