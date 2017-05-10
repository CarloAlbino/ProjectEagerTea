using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedActions;

public class Combatant : MonoBehaviour {

    // Is the combatant the player
    [SerializeField]
    private bool m_isPlayer = false;
    // The actions that the player can choose from
    private Action[] m_actions;

    void Start ()
    {
        // Create all the actions
        // This needs to be done in a better way so that it is customizable
        // Leaving it as is to start with
        m_actions = new Action[(int)EActionTypes.ActionCount];
        for (int i = 0; i < (int)EActionTypes.ActionCount; i++)
        {
            m_actions[i] = new Action((EActionTypes)i, 1);
        }
    }

    // Is the combatant the player?
    public bool isPlayer { get { return m_isPlayer; } }
    // Get the action at an index
    public Action GetAction(int num) { return m_actions[num]; }

}
