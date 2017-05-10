using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedActions
{
    // Enumerate the available actions here
    public enum EActionTypes
    {
        Attack, 
        Defend, 
        Heal, 
        Retreat,
        Wait,
        ActionCount
    }

    public class Action
    {
        protected EActionTypes m_type;  // The type the action is
        protected float m_waitTime;     // The time to wait after the action is activated
        protected string m_name;        // The name of the Action

        // Constructors
        public Action()
        {
            m_type = EActionTypes.Wait;
            m_waitTime = 0.0f;
            m_name = m_type.ToString();
        }

        public Action(EActionTypes type, float waitTime)
        {
            m_type = type;
            m_waitTime = waitTime;
            m_name = m_type.ToString();
        }
        // Constructors

        // The type of the action
        public EActionTypes actionType { get { return m_type; } }
        // The time to wait after the action is activated
        public float waitTime { get { return m_waitTime; } }
        // The name of the action
        public string actionName { get { return m_name; } }
    }
}
