using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedActions
{
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
        protected EActionTypes m_type;
        protected float m_waitTime;
        protected string m_name;

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

        public EActionTypes actionType { get { return m_type; } }
        public float waitTime { get { return m_waitTime; } }
        public string actionName { get { return m_name; } }
    }
}
