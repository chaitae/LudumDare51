using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, GameEvent> gameEventsDirectory = new Dictionary<string, GameEvent>();
    public AudioSource audioSource;
    // List<GameEvent> gameEventsList = new List<GameEvent>();
    public static GameManager _instance;

    private IEnumerator DelaySceneChangeHelper(float waitTime, string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
    public void DelaySceneChange(string sceneName)
    {
        IEnumerator coroutine = DelaySceneChangeHelper(2f, sceneName);
        StartCoroutine(coroutine);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void Awake()
    {
        if (_instance == null)
        {
            audioSource = GameObject.FindObjectOfType<AudioSource>();
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnChangeMusic(float level)
    {
        audioSource.volume = level;
    }
    public void ChangeScene(string str)
    {
        SceneManager.LoadScene(str);
    }
    void Start()
    {
        var gameEvents = Resources.LoadAll<GameEvent>("GameEvents");
        foreach (GameEvent gameEvent in gameEvents)
        {
            gameEventsDirectory.Add(gameEvent.name, gameEvent);
            gameEvent.eventRaised = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
