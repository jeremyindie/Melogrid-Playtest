using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyLady : Controller
{
    protected override void OnSuccessfulMove(Vector2 moveDelta)
    {
        
    }

    protected override bool MoveUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance;
        if (StartMove())
        {
            AudioManager.Instance.Play("UpSound");
            return true;
        }
        return false;

    }

    protected override bool MoveDiagonallyLeftUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance - Vector3.right * _movementDistance;
        if (StartMove())
        {
            AudioManager.Instance.Play("LeftSound");
            return true;
        }
        return false;
    }

    protected override bool MoveDiagonallyRightUp()
    {
        _newPosition = _originalPosition + Vector3.up * _movementDistance + Vector3.right * _movementDistance;
        if (StartMove())
        {
            AudioManager.Instance.Play("RightSound");
            return true;

        }
        return false;
    }
}
