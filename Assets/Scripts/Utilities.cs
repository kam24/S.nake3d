using UnityEngine;

public class Utilities
{
    public static Vector3 GetRandomDirection()
    {
        Vector3 random = new()
        {
            x = Random.Range(-1f, 1f),
            y = Random.Range(-1f, 1f),
            z = Random.Range(-1f, 1f)
        };
        return random;
    }

    public static Vector2 GetInputDirection(DynamicJoystick joystick)
    {
        return new(-joystick.Horizontal, -joystick.Vertical);
    }
}

