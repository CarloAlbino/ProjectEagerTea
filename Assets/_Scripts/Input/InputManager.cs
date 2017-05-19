using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public enum EInput
    {
        Up,
        Down,
        Left,
        Right,
        Accept,
        Cancel,
        Pause
    }

    public struct GameModeInputEvent
    {
        public EInput inputType;
        public EGameMode gameMode;
    }

    public class InputManager : MonoBehaviour
    {
        private GameController m_gameController;
        private Dictionary<EInput, UnityEvent> eventDictionary;
        private static InputManager eventManager;

        public static InputManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(InputManager)) as InputManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;
            }
        }

        void Start()
        {
            m_gameController = GetComponent<GameController>();
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<EInput, UnityEvent>();
            }
        }

        public static void StartListening(EInput eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(EInput eventName, UnityAction listener)
        {
            if (eventManager == null) return;
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(EInput eventName)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }

    }
}
