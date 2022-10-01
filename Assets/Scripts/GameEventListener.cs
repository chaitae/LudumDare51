using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // 1

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    // private GameEvent gameEvent; // 2
    GameEvent currentGameEventListen;
    public delegate void GenericGameEventInput(GameEvent gameEvent);
    public static GenericGameEventInput OnChangeTask;
    public delegate void GenericVoid();
    public static GenericVoid OnEndGame;
    public static GenericVoid OnRoundLoss;


    public List<GameEvent> gameEvents;
    Dictionary<GameEvent, bool> checkGameEvents = new Dictionary<GameEvent, bool>();
    [SerializeField]
    private UnityEvent response; // 3
    float roundTimer = 0;
    float endRoundTime = 120f;
    int tasksFailed = 0;
    private int tooMuchFailCount = 3;

    private void Awake()
    {
        RandomlyGenerateTask();
    }
    public void Update()
    {
        roundTimer += Time.deltaTime;
        if(roundTimer >= endRoundTime)
        {
            if(OnEndGame != null)
            {
                OnEndGame();
                Debug.Log(" Goodjob u won");
            }
        }
    }
    void RandomlyGenerateTask()
    {
        int randomIndex = Random.Range(0, gameEvents.Count);
        currentGameEventListen = gameEvents[randomIndex];
        //Debug.Log($"" + currentGameEventListen);
        StartCoroutine("StartTimerChangeTask");
        if(OnChangeTask != null) OnChangeTask.Invoke(currentGameEventListen);
        //set timer to generate new task maybe. iunno
    }
    void CheckLoss()
    {
        if(tasksFailed >= tooMuchFailCount)
        {
            if(OnRoundLoss != null)
            {
                OnRoundLoss();
                Debug.Log("round loss");
            }
        }
    }
    private IEnumerator StartTimerChangeTask()
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(5f);
        //failed
        tasksFailed++;
        CheckLoss();
        //check lose
        RandomlyGenerateTask();

    }
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
    void OnCompletedTask()
    {
        //generate new task
        RandomlyGenerateTask();
    }
    public void OnEventRaised(GameEvent gameEvent) // 6
    {
        if(currentGameEventListen == gameEvent)
        {
            OnCompletedTask();
            //completed the thing u need
        }
        checkGameEvents[gameEvent] = true;
        if(CheckAllEventsTriggered())
        {
            response.Invoke();
        }
    }
}