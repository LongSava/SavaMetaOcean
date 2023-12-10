using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : Singleton<Test>
{
    public static int RemainPlayer = -1;
    public int NumberPlayer;
    public NetworkRunner Runner;
    public Cube Cube;
    private List<NetworkRunner> _runners = new List<NetworkRunner>();
    private Dictionary<PlayerRef, NetworkObject> _players = new Dictionary<PlayerRef, NetworkObject>();

    private void Start()
    {
        AddRunners();
    }

    private async void AddRunners()
    {
        RemainPlayer = NumberPlayer;

        await AddRunner(GameMode.Server);

        while (RemainPlayer > 0)
        {
            await AddRunner(GameMode.Client);
            RemainPlayer--;
        }

        CheckRunner(0);
    }

    private Task<StartGameResult> AddRunner(GameMode gameMode)
    {
        var runner = Instantiate(Runner);
        _runners.Add(runner);
        AddEvents(runner);
        return StartGame(runner, gameMode);
    }

    private void AddEvents(NetworkRunner runner)
    {
        var events = runner.AddComponent<NetworkEvents>();

        events.PlayerJoined = new NetworkEvents.PlayerEvent();
        events.PlayerJoined.AddListener(OnPlayerJoined);

        events.PlayerLeft = new NetworkEvents.PlayerEvent();
        events.PlayerLeft.AddListener(OnPlayerLeft);
    }

    private Task<StartGameResult> StartGame(NetworkRunner runner, GameMode gameMode)
    {
        runner.ProvideInput = true;
        return runner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            CustomLobbyName = "SavaOcean",
            SessionName = "SavaOcean",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = runner.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    private void OnPlayerJoined(NetworkRunner runner, PlayerRef playerRef)
    {
        if (runner.IsServer)
        {
            var player = runner.Spawn(Cube, Vector3.zero, Quaternion.identity, playerRef);
            _players.Add(playerRef, player.Object);
        }
    }

    private void OnPlayerLeft(NetworkRunner runner, PlayerRef playerRef)
    {
        if (runner.IsServer)
        {
            if (_players.TryGetValue(playerRef, out NetworkObject player))
            {
                runner.Despawn(player);
                _players.Remove(playerRef);
                RemoveRunner();
            }
        }
    }

    private void RemoveRunner()
    {
        if (_indexCurrent > 0)
        {
            _runners.RemoveAt(_indexCurrent);
            CheckRunner(_indexCurrent - 1);
        }
    }

    private int _indexCurrent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) CheckRunner(0);
        if (Input.GetKeyDown(KeyCode.Alpha1)) CheckRunner(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) CheckRunner(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) CheckRunner(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) CheckRunner(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) CheckRunner(5);
        if (Input.GetKeyDown(KeyCode.Alpha6)) CheckRunner(6);
        if (Input.GetKeyDown(KeyCode.Alpha7)) CheckRunner(7);
        if (Input.GetKeyDown(KeyCode.Alpha8)) CheckRunner(8);
        if (Input.GetKeyDown(KeyCode.Alpha9)) CheckRunner(9);
    }

    private void CheckRunner(int index)
    {
        if (index < _runners.Count)
        {
            _runners.ForEach(runner => ShowRunner(runner, false));
            ShowRunner(_runners[index], true);
            _indexCurrent = index;
        }
    }

    private void ShowRunner(NetworkRunner runner, bool active)
    {
        runner.IsVisible = active;
        runner.ProvideInput = active;
        var gameObjects = runner.SimulationUnityScene.GetRootGameObjects();
        foreach (var gameObject in gameObjects)
        {
            var cameras = gameObject.GetComponentsInChildren<Camera>();
            if (cameras.Length > 0) foreach (var camera in cameras) camera.enabled = active;
        }
    }
}
