using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum RoomType
{
    Home,
    School,
    Gallery
}

[System.Serializable]
public class Room
{
    public RoomType type;
    public RoomComponent roomRefs;
}

public class RoomBehaviour : MonoBehaviour
{
    public List<Room> rooms;

    public void OpenRoom(string type)
    {
        GameObject currentRoom = null;
        foreach (var room in rooms)
        {
            if (Enum.Parse<RoomType>(type) == room.type)
            {
                currentRoom = room.roomRefs.gameObject;
            }

            DOTween.Kill(room.roomRefs.UI);
            room.roomRefs.UI.gameObject.SetActive(Enum.Parse<RoomType>(type) == room.type);
            room.roomRefs.UI.alpha = 0;
            room.roomRefs.UI.DOFade(1, 1f);
            room.roomRefs.environment.gameObject.SetActive(Enum.Parse<RoomType>(type) == room.type);
            room.roomRefs.logic.SetActive(Enum.Parse<RoomType>(type) == room.type);
        }
        transform.DOMove(-currentRoom.transform.localPosition, 1);
    }
}
