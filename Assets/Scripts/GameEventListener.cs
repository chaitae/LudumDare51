using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // 1

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    // private GameEvent gameEvent; // 2
    public List<GameEvent> gameEvents;
    Dictionary<GameEvent, bool> checkGameEvents = new Dictionary<GameEvent, bool>();
    [SerializeField]
    private UnityEvent response; // 3

    private void OnEnable() // 4
    {
        foreach (GameEvent gameEvent in gameEvents)
        {
            gameEvent.RegisterListener(this);
            checkGameEvents.Add(gameEvent, false);
        }
    }

    private void OnDisable() // 5
    {
        foreach (GameEvent gameEvent in gameEvents)
        {
            gameEvent.UnregisterListener(this);
        }
    }
    bool CheckAllEventsTriggered()
    {
        foreach (GameEvent tempGameEvent in gameEvents)
        {
            if (checkGameEvents[tempGameEvent] == false)
            {
                return false;
            }

        }
        return true;
    }
    public void OnEventRaised(GameEvent gameEvent) // 6
    {
        checkGameEvents[gameEvent] = true;
        if(CheckAllEventsTriggered())
        {
            response.Invoke();
        }
        //if all the gameEvents
        // response.Invoke();
    }
}