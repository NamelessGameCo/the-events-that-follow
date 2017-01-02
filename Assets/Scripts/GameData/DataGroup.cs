/*
 * Author: Isaiah Mann
 * Description: Generic structure to store serialized data for Lucid
 */

[System.Serializable]
public class DataGroup<T> : SerializableDataCollection<T> {
	public string GroupID;
}
