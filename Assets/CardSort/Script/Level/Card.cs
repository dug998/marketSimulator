using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;

public class Card : MonoBehaviour
{
    public ColorType _color;

    public GameObject _view;

    private bool _isHighlighted;
    public void Init(ColorType color)
    {
        _color = color;
        SetHighlighted(false);
    }
    public Sequence PlayAnimationJump(Slot targetSlot, float offset, float delay)
    {
        SetHighlighted(false);
        var duration = LevelManager.LevelSetup.DurationMoveCard;
        float height = LevelManager.LevelSetup.height;
        Ease e = LevelManager.LevelSetup.ease;


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
    public void SetHighlighted(bool on, float animDuration = 0f)
    {
        _view.transform.DOKill();
        if (on)
        {
            _view.transform.DOLocalMoveY(0.1f, animDuration);
        }
        else
        {
            _view.transform.DOLocalMoveY(0, animDuration);
        }
    }
}
