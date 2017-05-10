using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TurnBasedActions;

public class Battle : MonoBehaviour {

    // UI references
    public Text m_textDisplay = null;
    public Button[] m_buttons;

    // Whose turn is it?
    private int m_turn = 0;
    // Can the player make an action?
    private bool m_canAct = true;

    // Combatant info
    private int m_numberOfCombatants = 0;
    private List<Combatant> m_combatants = new List<Combatant>();

	void Start ()
    {
        m_turn = 0;                 // Set the turn to 0
    }

    // Add a combatant to the battle
    public void AddCombatant(Combatant c)
    {
        m_combatants.Add(c);        // Add to the list
        m_numberOfCombatants++;     // Increment the count 
        if (c.isPlayer)
        {
            SetButtonText();        // Set the button text only if the passed in combatant is the player
            if (m_numberOfCombatants == 1)  // if the player is the first combatant
            {
                m_canAct = true;            // Allow the player to act
                ActivateButtons(m_canAct);  // Set the buttons to be interactable
            }
        }
        else
        {
            if (m_numberOfCombatants == 1)  // if c is the first combatant and not the player start the game
            {  
                StartCoroutine(WaitFor(2)); // NOTE: This needs to be done in a better way.  Need to start when all combatants are added
                m_canAct = false;           // Disallow the player to act
                ActivateButtons(m_canAct);  // Set the buttons to be non-interactable
            }
        }
        SetDeafultTextDisplay();    // Set the Default text display
    }

    // Activate an action, the type is the index of the action on the combatant
    public void ActivateAction(int type)
    {
        if (m_canAct || !m_combatants[m_turn].isPlayer) // if the player clicks on a button or if it is a non player combatant's turn
        {
            // Prevent the player from attacking
            m_canAct = false;           
            ActivateButtons(m_canAct);

            // Change the text display
            // Any effects of the battle needs to be called here
            if(m_combatants[m_turn].isPlayer)
                m_textDisplay.text = "You are " + m_combatants[m_turn].GetAction(type).actionName + "ing";
            else
                m_textDisplay.text = "Player " + (m_turn + 1) + " is " + m_combatants[m_turn].GetAction(type).actionName + "ing";

            // Add a delay between actions
            StartCoroutine(WaitFor(m_combatants[m_turn].GetAction(type).waitTime));

            // Increment turns
            m_turn++;
            if (m_turn >= m_combatants.Count)
                m_turn = 0;
        }
    }

    // Wait for some time before continuing actions
    private IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Change the text display
        // Any effects of after an action needs to be called here
        SetDeafultTextDisplay();

        // Activate a new action if the next combatant is not the player
        if(!m_combatants[m_turn].isPlayer)
        {
            yield return new WaitForSeconds(1);
            // This is where any AI for the enemies or NPCs are called
            ActivateAction(Random.Range(0, m_buttons.Length));
        }
    }

    // Set the display to whose turn it is
    private void SetDeafultTextDisplay()
    {
        if (m_combatants[m_turn].isPlayer)
        {
            // Allow the player to choose an action
            m_textDisplay.text = "Choose...";
            m_canAct = true;
            ActivateButtons(m_canAct);
        }
        else
        {
            // Indicate that a non-player combatant is choosing an attack
            m_textDisplay.text = "Player " + (m_turn + 1) + " is choosing...";
        }
    }

    // Set the button text to the player's actions
    private void SetButtonText()
    {
        foreach (Combatant c in m_combatants)
        {
            if (c.isPlayer) // Find the player in the combatant list
            {
                for (int i = 0; i < (int)EActionTypes.ActionCount; i++)
                {
                    if (i < m_buttons.Length)
                        m_buttons[i].GetComponentInChildren<Text>().text = c.GetAction(i).actionName;
                }
                return; // exit because there should only be 1 player
            }
        }
    }

    // Set buttons interactable to b
    private void ActivateButtons(bool b)
    {
        foreach (Button button in m_buttons)
        {
            button.interactable = b;
        }
    }
}
