public struct PointerEvent
{
    public Vector2 Position;
}

public static class InputEventHelper
{
    public static bool IsDrag(this InputEvent inputEvent, out PointerEvent pointerEvent)
    {
        if (inputEvent is InputEventScreenDrag inputEventScreenDrag)
        {
            pointerEvent = new PointerEvent
            {
                Position = inputEventScreenDrag.Position,
            };
            return true;
        }
        
        pointerEvent = default;
        return false;
    }
    
    public static bool IsLeftClickOrTouchDown(this InputEvent inputEvent, out PointerEvent pointerEvent)
    {
        if (inputEvent is InputEventScreenTouch { Pressed: true } inputEventScreenTouch)
        {
            pointerEvent = new PointerEvent
            {
                Position = inputEventScreenTouch.Position,
            };
            return true;
        }

        if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: MouseButtonMask.Left } inputEventMouseButton)
        {
            pointerEvent = new PointerEvent
            {
                Position = inputEventMouseButton.Position,
            };
            return true;
        }

        pointerEvent = default;
        return false;
    }
    
    public static bool IsLeftClickOrTouchUp(this InputEvent inputEvent, out PointerEvent pointerEvent)
    {
        if (inputEvent is InputEventScreenTouch { Pressed: false } inputEventScreenTouch)
        {
            pointerEvent = new PointerEvent
            {
                Position = inputEventScreenTouch.Position,
            };
            return true;
        }

        if (inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Left, ButtonMask: 0 } inputEventMouseButton)
        {
            pointerEvent = new PointerEvent
            {
                Position = inputEventMouseButton.Position,
            };
            return true;
        }

        pointerEvent = default;
        return false;
    }
}
