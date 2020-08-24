using System;
using MyUnityLibrary.Enums;
using MyUnityLibrary.Patterns;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyUnityLibrary.Managers
{
    public class ScreenInputManager : MonoSingleton<ScreenInputManager>
    {
        [SerializeField] private float _swipeDeadzone = 100.0f;
        [SerializeField] private float _doubleTapTime = 0.5f;
        [SerializeField] private float _longPressTime = 1f;
        [SerializeField] private bool _isDeviceSimulator = false;
    
    
        private bool _tap, _doubleTap, _longPress, _leftScreenTap, _rightScreenTap, _bottomScreenTap, _topScreenTap, _swipeLeft, _swipeRight, _swipeUp, _swipeDown;
    
        private float _sqrSwipeDeadzone;

        private Vector2 _touchBeginPosition;
        private Vector2 _touchEndPosition;

        private float _lastTouchBeginTime;
        private bool _isAlreadyLongPressed;

        public bool IsIgnoreInputToUI { get; set; } = true;
        public bool IsIgnoreAllInput { get; set; } = false;

        public int TouchCount
        {
            get
            {
                int touchCount = 0;
                foreach (var touch in Input.touches)
                {
                    if (IsIgnoreInputToUI && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        _touchBeginPosition = Vector2.zero;
                        continue;
                    }

                    ++touchCount;
                }

                return touchCount;
            }
        }

        private void Start()
        {
            _sqrSwipeDeadzone = _swipeDeadzone * _swipeDeadzone;
        }

        private void Update()
        {
            _tap = _doubleTap = _longPress = _leftScreenTap =
                _rightScreenTap = _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;

            if (IsIgnoreAllInput)
            {
                return;
            }
        
#if UNITY_EDITOR
            if (_isDeviceSimulator)
            {
                UpdateMobile();
            }
            else
            {
                UpdateStandalone();
            }

#elif UNITY_ANDROID || UNITY_IOS
            UpdateMobile();
#endif
        }

        public bool GetDoubleTap()
        {
            return _doubleTap;
        }

        public bool GetLongPress()
        {
            return _longPress;
        }
    
        public bool GetTap()
        {
            return _tap;
        }

        public bool GetLeftScreenTap()
        {
            return _leftScreenTap;
        }
    
        public bool GetRightScreenTap()
        {
            return _rightScreenTap;
        }

        public bool GetTopScreenTap()
        {
            return _topScreenTap;
        }
    
        public bool GetBottomScreenTap()
        {
            return _bottomScreenTap;
        }
    
        public bool GetSwipe(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.Left:
                    return _swipeLeft;
                case EDirection.Right:
                    return _swipeRight;
                case EDirection.Up:
                    return _swipeUp;
                case EDirection.Down:
                    return _swipeDown;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void UpdateStandalone()
        {
            float currentTime = Time.time;
        
            if (IsIgnoreInputToUI && EventSystem.current.IsPointerOverGameObject())
            {
                _touchBeginPosition = Vector2.zero;
                return;
            }
        
            if (Input.GetMouseButtonDown(0))
            {
                _touchBeginPosition = Input.mousePosition;
                _isAlreadyLongPressed = false;
            
                if (currentTime - _lastTouchBeginTime < _doubleTapTime)
                {
                    float x = _touchEndPosition.x - _touchBeginPosition.x;
                    float y = _touchEndPosition.y - _touchBeginPosition.y;
                
                    if (Mathf.Abs(x) == 0f && Mathf.Abs(y) == 0f)
                    {
                        _doubleTap = true;
                    }
                }
            
                _lastTouchBeginTime = currentTime;
            }
            else if (Input.GetMouseButton(0))
            {
                if (!_isAlreadyLongPressed && currentTime - _lastTouchBeginTime >= _longPressTime)
                {
                    Vector2 deltaPosition = _touchBeginPosition - (Vector2) Input.mousePosition;
                    if (deltaPosition.sqrMagnitude == 0f)
                    {
                        // Debug.Log($"Long Press");
                        _longPress = true;
                        _isAlreadyLongPressed = true;
                    }
                }
            }
        
            else if (Input.GetMouseButtonUp(0))
            {
                _touchEndPosition = Input.mousePosition;
                Vector2 deltaPosition = _touchEndPosition - _touchBeginPosition;
                float x = deltaPosition.x;
                float y = deltaPosition.y;

                if (_touchBeginPosition == Vector2.zero)
                {
                    return;
                }

                if (_isAlreadyLongPressed)
                {
                    return;
                }
            
                if (deltaPosition.sqrMagnitude == 0f)
                {
                    _tap = true;
                
                    if (_touchEndPosition.x < Screen.width / 2.0f)
                    {
                        _leftScreenTap = true;
                    }
                    else
                    {
                        _rightScreenTap = true;
                    }
                
                    if (_touchEndPosition.y < Screen.height / 2.0f)
                    {
                        _bottomScreenTap = true;
                    }
                    else
                    {
                        _topScreenTap = true;
                    }
                }
                else if (deltaPosition.sqrMagnitude > _sqrSwipeDeadzone)
                {
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x > 0)
                        {
                            _swipeRight = true;
                        }
                        else
                        {
                            _swipeLeft = true;
                        }
                    }
                    else
                    {
                        if (y > 0)
                        {
                            _swipeUp = true;
                        }
                        else
                        {
                            _swipeDown = true;
                        }
                    }
                }
            }
        }
    
        private void UpdateMobile()
        {
            float currentTime = Time.time;
        
            if (Input.touchCount > 0)
            {
                Touch firstTouch = Input.GetTouch(0);    
            
                if (IsIgnoreInputToUI && EventSystem.current.IsPointerOverGameObject(firstTouch.fingerId))
                {
                    _touchBeginPosition = Vector2.zero;
                    return;
                }
            
                if (firstTouch.phase == TouchPhase.Began)
                {
                    _touchBeginPosition = firstTouch.position;
                    _isAlreadyLongPressed = false;
                
                    if (currentTime - _lastTouchBeginTime < _doubleTapTime)
                    {
                        float x = _touchEndPosition.x - _touchBeginPosition.x;
                        float y = _touchEndPosition.y - _touchBeginPosition.y;
                    
                        if (Mathf.Abs(x) == 0f && Mathf.Abs(y) == 0f)
                        {
                            _doubleTap = true;
                        }
                    }
                
                    _lastTouchBeginTime = currentTime;
                }
                else if (firstTouch.phase == TouchPhase.Stationary)
                {
                    if (!_isAlreadyLongPressed && currentTime - _lastTouchBeginTime >= _longPressTime)
                    {
                        Vector2 deltaPosition = _touchBeginPosition - firstTouch.position;
                        if (deltaPosition.sqrMagnitude == 0f)
                        {
                            _longPress = true;
                            _isAlreadyLongPressed = true;
                        }
                    }
                }
            
                else if (firstTouch.phase == TouchPhase.Ended)
                {
                    _touchEndPosition = firstTouch.position;
                    Vector2 deltaPosition = _touchEndPosition - _touchBeginPosition;
                    float x = deltaPosition.x;
                    float y = deltaPosition.y;

                    if (_touchBeginPosition == Vector2.zero)
                    {
                        return;
                    }

                    if (_isAlreadyLongPressed)
                    {
                        return;
                    }
                
                    if (deltaPosition.sqrMagnitude < _sqrSwipeDeadzone * 0.25f)//== 0f)
                    {
                        _tap = true;
                    
                        if (_touchEndPosition.x < Screen.width / 2.0f)
                        {
                            _leftScreenTap = true;
                        }
                        else
                        {
                            _rightScreenTap = true;
                        }
                    }
                    else if (deltaPosition.sqrMagnitude > _sqrSwipeDeadzone)
                    {
                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            if (x > 0)
                            {
                                _swipeRight = true;
                            }
                            else
                            {
                                _swipeLeft = true;
                            }
                        }
                        else
                        {
                            if (y > 0)
                            {
                                _swipeUp = true;
                            }
                            else
                            {
                                _swipeDown = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
