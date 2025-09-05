using com.homemade.pattern.observer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarketShopandRetailSystem
{
    public class Touchpad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public Camera CurrentEventCamera { get; set; }

        public bool PreserveInertia = true;

        public float Friction = 3f;

        private int _lastDragFrameNumber;
        private bool _isCurrentlyTweaking;

        public float HorizontalValue;
        public float VerticalValue;


        public ControlMovementDirection ControlMoveAxis = ControlMovementDirection.Both;

        public virtual void OnDrag(PointerEventData eventData)
        {
            if ((ControlMoveAxis & ControlMovementDirection.Horizontal) != 0)
            {
                HorizontalValue = eventData.delta.x;
            }
            if ((ControlMoveAxis & ControlMovementDirection.Vertical) != 0)
            {
                VerticalValue = eventData.delta.y;
            }
            this.PostEvent(EventID.PlayerEvent.PlayerLookDelta, new Vector2(HorizontalValue, VerticalValue));
            _lastDragFrameNumber = Time.renderedFrameCount;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isCurrentlyTweaking = false;
            if (!PreserveInertia)
            {
                HorizontalValue = 0f;
                VerticalValue = 0f;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isCurrentlyTweaking = true;
            OnDrag(eventData);
        }

        private void Update()
        {
            if (_isCurrentlyTweaking && _lastDragFrameNumber < Time.renderedFrameCount - 2)
            {
                HorizontalValue = 0f;
                VerticalValue = 0f;
                this.PostEvent(EventID.PlayerEvent.PlayerLookDelta, Vector2.zero);
            }

            if (PreserveInertia && !_isCurrentlyTweaking)
            {
                HorizontalValue = Mathf.Lerp(HorizontalValue, 0f, Friction * Time.deltaTime);
                VerticalValue = Mathf.Lerp(VerticalValue, 0f, Friction * Time.deltaTime);
                this.PostEvent(EventID.PlayerEvent.PlayerLookDelta, new Vector2(HorizontalValue, VerticalValue));

            }
        }
    }
}