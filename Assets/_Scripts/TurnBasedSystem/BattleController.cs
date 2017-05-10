using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    public Battle m_battle = null;
    public Combatant[] m_combatants;

    void Start()
    {
        foreach(Combatant c in m_combatants)
        {
            m_battle.AddCombatant(c);
        }
    }

    public void Button_Action(int action)
    {
        m_battle.ActivateAction(action);
    }

}
