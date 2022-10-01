using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public GameObject specialInteractionObject;
    public GameObject settingsCanvas;
    public GameObject hintCanvas;
    public TMP_Text hintText;
    public TMP_Text interactionText;
    public TMP_Text specialInteractionText;
    public GameObject interactionObject;
    public bool disableSettingsMenu = false;

    public GameObject interactionTalkObject;
    string interactionTestDefault = "Press E to interact";
    string specialInteractionTextDefault = "jgdskl;";
    public void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    public void ToggleSettingsMenu()
    {
        settingsCanvas.SetActive(!settingsCanvas.activeSelf);
    }
    public void ShowHintText(string hint)
    {
        if (hint == "")
        {

        }
        else
        {
            hintText.text = hint;
            hintCanvas.SetActive(true);
        }
    }
    public void HideHint()
    {
        hintCanvas.SetActive(false);
    }
    public void ChangeInteractionText(string str)
    {
        if (str == "") interactionText.text = interactionTestDefault;
        else interactionText.text = str;
    }
    public void ChangeSpecialInteractionText(string str)
    {
        if (str == "") specialInteractionText.text = specialInteractionTextDefault;
        else specialInteractionText.text = str;

    }
    public void ShowSpecialInteraction()
    {
        specialInteractionObject.SetActive(true);
    }
    public void HideSpecialInteraction()
    {
        specialInteractionObject.SetActive(false);

        interactionTalkObject.SetActive(false);
    }
    public void ShowInteractionText()
    {
        interactionObject.SetActive(true);
    }
    public void HideInteractionText()
    {
        interactionObject.SetActive(false);
    }
    public void StopCheckingForNearInteractable()
    {
        // CharacterControls.OnNearInteractable -= ShowInteractionText;
    }
    public void ContinueCheckingForNearInteractable()
    {
        // CharacterControls.OnNearInteractable += ShowInteractionText;
    }
    public void OnTaskChanged(GameEvent gameEvent)
    {
        Debug.Log("inside on task changed");
        //example Open Curtains, blow out candles
        string fortuneTellerReadout;
        if(gameEvent != null)
        {
            //first check what type of thingy it is
            if (gameEvent.isSwitch)
            {
                //opn and close
                //just make it second setting
                int index = 0;
                if (gameEvent.isOn)
                {
                    index = 0;
                }
                else
                {
                    index = 1;
                }
                fortuneTellerReadout = $"OooOooo spirit if you hear me {gameEvent.previewSetting[index]} {gameEvent.objectName}";
            }
            else
            {
                //shake table
                Debug.Log(gameEvent);
                fortuneTellerReadout = $"OooOooo spirit if you hear me {gameEvent.previewSetting[0]} {gameEvent.objectName}";

            }
            Debug.Log(fortuneTellerReadout + " fortune teller");

        }
        //convert to task reading
    }
    void OnEnable()
    {
        GameEventListener.OnChangeTask += OnTaskChanged;

        // CharacterControls.OnNearInteractable += ShowInteractionText;
        // CharacterControls.OnLeaveInteractable += HideInteractionText;
    }
    void OnDisable()
    {
        GameEventListener.OnChangeTask -= OnTaskChanged;
        // CharacterControls.OnNearInteractable -= ShowInteractionText;
        // CharacterControls.OnLeaveInteractable -= HideInteractionText;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !disableSettingsMenu)
        {
            ToggleSettingsMenu();
        }
    }
}
