using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TurnBasedActions;

public class Battle : MonoBehaviour {

    public Text m_textDisplay = null;
    public Button[] m_buttons;

    private int m_turn = 0;
    private bool m_canAct = true;

    private int m_numberOfCombatants = 0;
    private List<Combatant> m_combatants = new List<Combatant>();

	void Start ()
    {
        m_turn = 0;
        m_canAct = true;
        ActivateButtons(m_canAct);
    }

    public void AddCombatant(Combatant c)
    {
        m_combatants.Add(c);
        m_numberOfCombatants++;
        SetButtonText();
        SetDeafultTextDisplay();
    }

    public void ActivateAction(int type)
    {
        if (m_canAct || !m_combatants[m_turn].isPlayer)
        {
            m_canAct = false;
            ActivateButtons(m_canAct);

            if(m_combatants[m_turn].isPlayer)
                m_textDisplay.text = "You are " + m_combatants[m_turn].GetAction(type).actionName + "ing";
            else
                m_textDisplay.text = "Player " + (m_turn + 1) + " is " + m_combatants[m_turn].GetAction(type).actionName + "ing";

            StartCoroutine(WaitFor(m_combatants[m_turn].GetAction(type).waitTime));

            m_turn++;
            if (m_turn >= m_combatants.Count)
                m_turn = 0;
        }
    }

    private IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SetDeafultTextDisplay();
        if(!m_combatants[m_turn].isPlayer)
        {
            yield return new WaitForSeconds(1);
            ActivateAction(Random.Range(0, m_buttons.Length));
        }
    }

    private void SetDeafultTextDisplay()
    {
        if (m_combatants[m_turn].isPlayer)
        {
            m_textDisplay.text = "Choose...";
            m_canAct = true;
            ActivateButtons(m_canAct);
        }
        else
        {
            m_textDisplay.text = "Player " + (m_turn + 1) + " is choosing...";
        }
    }

    private void SetButtonText()
    {
        foreach (Combatant c in m_combatants)
        {
            if (c.isPlayer)
            {
                for (int i = 0; i < (int)EActionTypes.ActionCount; i++)
                {
                    if (i < m_buttons.Length)
                        m_buttons[i].GetComponentInChildren<Text>().text = c.GetAction(i).actionName;
                }
                return;
            }
        }
    }

    private void ActivateButtons(bool b)
    {
        foreach (Button button in m_buttons)
        {
            button.interactable = b;
        }
    }
}
