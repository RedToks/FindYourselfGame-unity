using UnityEngine;

public class RoomDarkening : MonoBehaviour
{
    private SpriteRenderer roomMask;

    private void Start()
    {
        roomMask = GetComponent<SpriteRenderer>();
        roomMask.enabled = true; 
    }

    public void EnterRoom()
    {
        roomMask.enabled = false;
    }
}
