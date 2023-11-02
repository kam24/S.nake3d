using UnityEngine;

public class InputRouter
{
    private DynamicJoystick _joystick;
    private SnakeHead _snakeHead;

    public InputRouter(DynamicJoystick joystick, SnakeHead snakeHead)
    {
        _joystick = joystick;
        _snakeHead = snakeHead;
    }

    public void Update()
    {
        Vector2 input = Utilities.GetInputDirection(_joystick);
        _snakeHead.Rotate(input);
    }
}

