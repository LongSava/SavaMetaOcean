using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerController : Singleton<RunnerController>
{
    public static int NumberPlayer = -1;
    private List<NetworkRunner> _runners = new List<NetworkRunner>();
    private Dictionary<PlayerRef, NetworkObject> _players = new Dictionary<PlayerRef, NetworkObject>();
    private CameraFollower CameraFollower;

    private void Start()
    {
        AddRunners();
    }

    private Task<StartGameResult> AddRunner(GameMode gameMode)
    {
        var runner = Instantiate(Config.Data.Runner);
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
            CustomLobbyName = "SavaOcean2",
            SessionName = "SavaOcean2",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = runner.AddComponent<NetworkSceneManagerDefault>(),
            Initialized = OnInit
        });
    }

    private void OnInit(NetworkRunner runner)
    {
        runner.AddComponent<EventScene>();

        if (runner.IsServer)
        {
            runner.Spawn(Config.Data.UI.Loading);

            var totalFish = 0;
            Config.Data.FishAreas.FishAreas.ForEach(config => totalFish += config.NumberFish);
            for (int i = 0; i < totalFish; i++)
            {
                runner.Spawn(Config.Data.Fish.Objects[Random.Range(0, Config.Data.Fish.Objects.Count)]);
            }

            runner.Spawn(Config.Data.FishAreas.Object);

            CameraFollower = runner.InstantiateInRunnerScene(Config.Data.CameraFollower);
        }
        else if (runner.IsPlayer)
        {
            runner.InstantiateInRunnerScene(Config.Data.Particle.BubbleCommon);
            runner.InstantiateInRunnerScene(Config.Data.Particle.SunLight);
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

    private void OnPlayerJoined(NetworkRunner runner, PlayerRef playerRef)
    {
        if (runner.IsServer)
        {
            var position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)) + Config.Data.Player.PositionSpawned;
            var rotation = Quaternion.Euler(0, -90, 0);
            var player = runner.Spawn(Config.Data.Player.Object, position, rotation, playerRef);
            _players.Add(playerRef, player.GetComponent<NetworkObject>());
        }
    }

    private async void AddRunners()
    {
        if (NumberPlayer == -1)
        {
            if ((int)Config.Data.NumberPlayer == -1)
            {
                await AddRunner(GameMode.Client);
                DisableRunnerJustAdded();
            }
            else
            {
                NumberPlayer = (int)Config.Data.NumberPlayer;

                await AddRunner(GameMode.Server);
                DisableRunnerJustAdded();

                while (NumberPlayer > 0)
                {
                    await AddRunner(GameMode.Client);
                    DisableRunnerJustAdded();
                    NumberPlayer--;
                }
            }
        }

        if (_runners.Count > 1)
        {
            ChangeRunner(1);
        }
        else
        {
            ChangeRunner(0);
        }
    }

    private void DisableRunnerJustAdded()
    {
        ShowRunner(_runners[_runners.Count - 1], false);
    }

    private void RemoveRunner()
    {
        if (_indexCurrent > 0)
        {
            _runners.RemoveAt(_indexCurrent);
            ChangeRunner(_indexCurrent - 1);
        }
    }

    private int _indexCurrent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) ChangeRunner(0);
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeRunner(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeRunner(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeRunner(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeRunner(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeRunner(5);
        if (Input.GetKeyDown(KeyCode.Alpha6)) ChangeRunner(6);
        if (Input.GetKeyDown(KeyCode.Alpha7)) ChangeRunner(7);
        if (Input.GetKeyDown(KeyCode.Alpha8)) ChangeRunner(8);
        if (Input.GetKeyDown(KeyCode.Alpha9)) ChangeRunner(9);

        if (Input.GetKeyDown(KeyCode.Keypad1)) ChangeCameraFollower(1);
        if (Input.GetKeyDown(KeyCode.Keypad2)) ChangeCameraFollower(2);
    }

    private void ChangeCameraFollower(int index)
    {
        if (index > 0)
        {
            var gameObjects = _runners[0].SimulationUnityScene.GetRootGameObjects();
            List<Player> players = new List<Player>();
            foreach (var gameObject in gameObjects)
            {
                var player = gameObject.GetComponentInChildren<Player>();
                if (player != null)
                {
                    players.Add(player);
                }
            }
            if (players.Count > 0 && index <= players.Count)
            {
                CameraFollower.Target = players[index - 1].HeadDevice;
            }
        }
    }

    private void ChangeRunner(int index)
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
