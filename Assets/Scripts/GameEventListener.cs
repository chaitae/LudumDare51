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
    public UnityEvent onLoseRound;
    public static GenericVoid OnWinRound;
    public static GenericVoid OnRoundLoss;
    public static GenericVoid OnWinGame;

    public List<GameEvent> gameEvents;
    Dictionary<GameEvent, bool> checkGameEvents = new Dictionary<GameEvent, bool>();
    [SerializeField]
    private UnityEvent response; // 3
    [SerializeField]
    private UnityEvent winthing;
    [SerializeField]
    private UnityEvent onMissTask;
    [SerializeField]
    private UnityEvent passedTask;
    public float roundTimer = 0;
    float endRoundTime = 60f;
    int tasksFailed = 0;
    float elapsedtaskTimer = 0;
    [SerializeField]
    float endTaskTimer = 20f;
    private int tooMuchFailCount = 3;
    public bool debug;

    private void Start()
    {
        RandomlyGenerateTask();
    }
    public void Update()
    {
        roundTimer += Time.deltaTime;
        if(roundTimer >= endRoundTime)
        {
            winthing.Invoke();

            //if (OnWinRound != null)
            //{
            //    OnWinRound();
            //    //load next scene
            //    //go to next scene with more objects
            //}
            //if(GameStatus.level == 3)
            //{
            //    if(OnWinGame!=null)OnWinGame();
            //    winthing.Invoke();
            //    //show win game scene
            //    //win the game
            //}
            //if (GameStatus.level != 3)
            //{
            //    GameStatus.level++;
            //    GameManager._instance.ChangeScene(GameStatus.level.ToString());
            //}
            //Debug.Log(" Goodjob u won");
        }

        elapsedtaskTimer += Time.deltaTime;
        if(elapsedtaskTimer >= endTaskTimer)
        {
            Debug.Log("Fail task");
            tasksFailed++;
            CheckLoss();
            RandomlyGenerateTask();
            onMissTask.Invoke();
        }
    }
    [ContextMenu("test invoke")]
    void TestInvoke()
    {
        OnChangeTask?.Invoke(currentGameEventListen);
    }

    void RandomlyGenerateTask()
    {
        int randomIndex = Random.Range(0, gameEvents.Count);
        currentGameEventListen = gameEvents[randomIndex];
        OnChangeTask?.Invoke(currentGameEventListen);
        elapsedtaskTimer = 0;
        //if (OnChangeTask != null) OnChangeTask(currentGameEventListen);
        //StartCoroutine("StartTimerChangeTask");
        //set timer to generate new task maybe. iunno
    }
    void CheckLoss()
    {
        if(tasksFailed >= tooMuchFailCount)
        {
            if(OnRoundLoss != null)
            {
                OnRoundLoss();
            }
            if(!debug)
            onLoseRound.Invoke();
            Debug.Log("round loss");

        }
    }
    private void OnEnable() // 4
    {
        foreach (GameEvent gameEvent in gameEvents)
        {
            gameEvent.isOn = true;
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
        Debug.Log("completed task!");
        //generate new task
        RandomlyGenerateTask();
    }
    public void OnEventRaised(GameEvent gameEvent) // 6
    {
        if(currentGameEventListen == gameEvent)
        {
            OnCompletedTask();
            passedTask.Invoke();
            //completed the thing u need
        }
        else
        {
            //wrong task
        }
        checkGameEvents[gameEvent] = true;
        if(CheckAllEventsTriggered())
        {
            response.Invoke();
        }
    }
}