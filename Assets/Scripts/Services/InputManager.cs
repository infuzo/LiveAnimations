using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Runner.Services
{

    public interface IInputManager
    {

        InputManager.UnityEventInputAction OnInputAction { get; }

    }

    public class InputManager : IInputManager
    {
        public enum InputManagerType
        {
            Touch,
            Keyborad
        }

        public enum InputType
        {
            LeftCommand,
            RightCommand
        }

        public class UnityEventInputAction : UnityEvent<InputType> { }

        public UnityEventInputAction OnInputAction { get; set; }

        public InputManager()
        {
            OnInputAction = new UnityEventInputAction();
        }

    }

    public class TouchInputManager : InputManager
    {

        class TouchInputManagerWorker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
        {

            /// <summary>
            /// Percent of screen width.
            /// </summary>
            const float minSwipeDistanceForTouchRegister = .25f;

            int? nowPointerId = null;
            float startSwipePositionX = 0;

            public UnityEventInputAction OnInputAction { get; set; }

            public void OnBeginDrag(PointerEventData eventData)
            {
                if (nowPointerId != null) { return; }

                nowPointerId = eventData.pointerId;
                startSwipePositionX = eventData.position.x;
            }

            public void OnDrag(PointerEventData eventData)
            {
                if (nowPointerId != eventData.pointerId) { return; }

                if (Mathf.Abs(startSwipePositionX - eventData.position.x) >= minSwipeDistanceForTouchRegister * Screen.width)
                {
                    if (startSwipePositionX - eventData.position.x < 0)
                    {
                        OnInputAction.Invoke(InputType.RightCommand);
                    }
                    else
                    {
                        OnInputAction.Invoke(InputType.LeftCommand);
                    }
                    nowPointerId = null;
                }
            }

            public void OnEndDrag(PointerEventData eventData)
            {
                if (nowPointerId == eventData.pointerId)
                {
                    nowPointerId = null;
                }
            }
        }

        TouchInputManagerWorker worker;

        public TouchInputManager() : base()
        {
            worker = MainContext.InjectionBinder.GetInstance<UI.IGameUIManager>().
                InstantiateLikeUIElement(Models.UI.GameUIModel.Instance.TouchInputManagerPrefab).AddComponent<TouchInputManagerWorker>();
            worker.OnInputAction = OnInputAction;
        }

    }

    public class KeyBoradInputManager : InputManager
    {

        class KeyBoradInputManagerWorker : MonoBehaviour
        {

            public UnityEventInputAction OnInputAction { get; set; }

            private void Update() 
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    OnInputAction.Invoke(InputType.LeftCommand);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    OnInputAction.Invoke(InputType.RightCommand);
                }
            }

        }

        KeyBoradInputManagerWorker keyBoradInputManagerWorker;

        public KeyBoradInputManager() : base()
        {
            keyBoradInputManagerWorker = new GameObject("KeyBoradInputManagerWorker").AddComponent<KeyBoradInputManagerWorker>();
            keyBoradInputManagerWorker.OnInputAction = OnInputAction;
        }

    }

}
