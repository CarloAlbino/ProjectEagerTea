using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    public Battle m_battle = null;
    public Combatant[] m_combatants;

    void Start()
    {
        // Add the combatants to the battle
        foreach(Combatant c in m_combatants)
        {
            m_battle.AddCombatant(c);
        }
    }

    // Used by UI buttons to choose an attack by the player
    public void Button_Action(int action)
    {
        m_battle.ActivateAction(action);
    }

}

#region Explanation

/*
 * Most of the logic happens in the battle class, the battle controller class only holds the reference to all the combatants and
 * the battle itself.  The battle controller also has the function that buttons can call for the player to use an attack.
 * 
 * The combatants are added to the battle in the Start() of the battle controller.  Creating and adding combatants as well as 
 * creating the battle needs to be done in a more standardized and expandable way than it currently is.  Right now the way it is done is
 * only for the demo.
 * 
 * If the player is the first combatant in the battle (this is not always the case) then the battle begins with Button_Action when the
 * player clicks a button.  If the first combatant is not the player then a coroutine is called to start the game after 2 seconds (need to find
 * a better way, see comments of the AddCombatant function in battle class).
 * 
 * The ActivateAction is called by either the Button_Action function or the coroutine.  The ActivateAction function does any actions or logic
 * that needs to be done before the action takes place (animations, calculations, logic, etc.) it then calls a coroutine to add a delay (the
 * delay can be set in the action when it is created).  After the delay any actions or logic to be done after the action is complete is done
 * (animations, logic, etc.).  
 * 
 * After the action is activated the turn count is incremented.  The next combatant can choose their attack after the delay based on the 
 * turn count.
 */

#endregion Explanation