using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DirectionGUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI blah;
    void OnTaskChange(GameEvent gameEvent)
    {
        string action = gameEvent.previewSetting[0];
        if(gameEvent.isSwitch)
        {
            if(gameEvent.isOn)
            {
                //chose the second not on version
                action = gameEvent.previewSetting[1];
            }
            else
            {
                action = gameEvent.previewSetting[0];
            }
        }
        //do little type writer animation with the string here
        //blah.text = $"OoOoOo oh spirit if you hear me please {action} the {gameEvent.name}";
        StartCoroutine("TypeWriter", $"OoOoOo oh spirit if you hear me please {action} the {gameEvent.name}");
    }
    IEnumerator TypeWriter(string str)
    {
        blah.text = "";

        for (int i =0;i<str.Length;i++)
        {
            blah.text += str[i];
            yield return new WaitForSeconds(.05f);
        }
    }
    private void OnEnable()
    {
        GameEventListener.OnChangeTask += OnTaskChange;
    }
    private void OnDisable()
    {

        GameEventListener.OnChangeTask -= OnTaskChange;
    }
}
