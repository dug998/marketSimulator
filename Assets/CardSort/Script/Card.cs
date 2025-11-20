using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    public ColorType _color;
    public void Init(ColorType color)
    {
        _color = color;
    }
    public Sequence PlayAnimation(Slot targetSlot, float offset, float delay)
    {
        var duration = LevelSetup.Instance.totalDuration;
        float height = LevelSetup.Instance.height;
        Ease e = LevelSetup.Instance.ease;


        var rotationTarget = new Vector3();
        var currentRotation = transform.rotation.eulerAngles;

        var dirType = DirectionHelper.GetMovementDirection((targetSlot.transform.position - transform.position));
        rotationTarget = currentRotation + DirectionHelper.GetDirectionAngle(dirType);

        var position = targetSlot.transform.position;
        var p = new Vector3(position.x, offset, position.z);


        Sequence j = transform.DOJump(p, height, 1, duration).SetEase(e).SetDelay(delay);
        transform.DORotate(rotationTarget, duration).SetEase(e).SetDelay(delay).OnComplete(() =>
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        });

        return j;

    }
}
