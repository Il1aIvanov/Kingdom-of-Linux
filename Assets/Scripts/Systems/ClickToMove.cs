using KingdomOfLinux.Characters;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 🛡️ Принудительно обнуляем Z еще здесь
            mouseWorld.z = 0f;

            Vector2 target = new Vector2(mouseWorld.x, mouseWorld.y);
            player.MoveTo(target);
        }
    }
}