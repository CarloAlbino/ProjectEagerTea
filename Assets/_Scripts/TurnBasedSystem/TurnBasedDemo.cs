using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedActions;
using UnityEngine.UI;

public class TurnBasedDemo : MonoBehaviour {

    public Text m_textDisplay = null;
    public Button[] m_buttons;
    private Action[] m_actions;
    private bool m_canAct = true;

	void Start ()
    {
        m_actions = new Action[(int)EActionTypes.ActionCount];
        
        for(int i = 0; i < (int)EActionTypes.ActionCount; i++)
        {
            m_actions[i] = new Action((EActionTypes)i, 1);
        }

        m_textDisplay.text = "Choose...";
        SetButtonText();
        m_canAct = true;
        ActivateButtons(m_canAct);
    }

    public void ActivateAction(int type)
    {
        if (m_canAct)
        {
            m_canAct = false;
            ActivateButtons(m_canAct);
            m_textDisplay.text = m_actions[type].actionName;
            StartCoroutine(WaitFor(m_actions[type].waitTime));
        }
    }

    private IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_textDisplay.text = "Choose...";
        m_canAct = true;
        ActivateButtons(m_canAct);
    }

    private void SetButtonText()
    {
        for (int i = 0; i < (int)EActionTypes.ActionCount; i++)
        {
            if (i < m_buttons.Length)
                m_buttons[i].GetComponentInChildren<Text>().text = m_actions[i].actionName;
            else
                return;
        }
    }

    private void ActivateButtons(bool b)
    {
        foreach(Button button in m_buttons)
        {
            button.interactable = b;
        }
    }
}
