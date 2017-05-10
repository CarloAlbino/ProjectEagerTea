using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedActions;

public class Combatant : MonoBehaviour {

    [SerializeField]
    private bool m_isPlayer = false;
    private Action[] m_actions;

    void Start ()
    {
        m_actions = new Action[(int)EActionTypes.ActionCount];

        for (int i = 0; i < (int)EActionTypes.ActionCount; i++)
        {
            m_actions[i] = new Action((EActionTypes)i, 1);
        }
    }

    public bool isPlayer { get { return m_isPlayer; } }
    public Action GetAction(int num) { return m_actions[num]; }

}
