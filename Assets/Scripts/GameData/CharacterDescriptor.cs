/*
 * Author: Joseph Gillen
 * Initial Date: 12th October 2016
 * Description: This class is used to create contacts
 * Initialization: To initialize a contact using JSON use the follow,
 * instanceOfLContact.ContactWithJson(jsonFileName);
 */

using UnityEngine;

[System.Serializable]
public class CharacterDescriptor {
    public string ID; // Some way to reference contact
    public string Name; // Name to be displayed on screen
    public int MessagesRecieved; // The total number of messages a contact has recieved
    public int MessagesSent; // The total number of messsages a contact has sent
	public string SpriteName;
	[System.NonSerialized]
	public Sprite ContactImage; // An image to represent a contact
    public bool IsMessageUnread; // A bool to represent if a contacts message is sitting unread
    public bool IsContact; // Is contact known to the player, .i.e. not annoymous


    // Getters and Setters
    public string ContactID {
        get { return ID; }
        set { ID = value; }
    }
    public string ContactName
    {
        get { return Name; }
        set { Name = value; }
    }
    public int NumberOfMessagesRecieved {
        get { return MessagesRecieved; }
        set { MessagesRecieved = value; }
    }
    public int NumberOfMessagesSent {
        get { return MessagesSent; }
        set { MessagesSent = value; }
    }
    public Sprite SpriteContactImage
    {
        get { return ContactImage; }
        set { ContactImage = value; }
    }
    public bool BoolIsMessageUnread
    {
        get { return IsMessageUnread; }
        set { IsMessageUnread = value; }
    }
    public bool BoolIsContact
    {
        get { return IsContact; }
        set { IsContact = value; }
    }
    // A function to load JSON data into a contact object
    public void ContactWithJson(string data)
    {
        JsonUtility.FromJsonOverwrite(data, this);
    }
    // A function to return a stringified version of LConacts JSON
    public string GetAsJson()
    {
        return JsonUtility.ToJson(this);
    }
}
