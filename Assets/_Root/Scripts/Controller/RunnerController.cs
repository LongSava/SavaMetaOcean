using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public enum RoomType
{
    Ocean,
    Titanic
}

public class RunnerController : Singleton<RunnerController>
{
    public RoomType RoomType;
    public Vector3 PositionSpawned;
    public static int NumberPlayer = -1;
    private List<NetworkRunner> _runners = new List<NetworkRunner>();
    private Dictionary<PlayerRef, NetworkObject> _players = new Dictionary<PlayerRef, NetworkObject>();
    private CameraFollower CameraFollower;

    public void Destroy()
    {
        _runners.ForEach(runner => Destroy(runner.gameObject));
        _runners.Clear();

        _players.Clear();

        NumberPlayer = -1;

        Destroy(gameObject);
    }

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
        runner.AddComponent<EventScene>();

        var events = runner.AddComponent<NetworkEvents>();

        events.PlayerJoined = new NetworkEvents.PlayerEvent();
        events.PlayerJoined.AddListener(OnPlayerJoined);

        events.PlayerLeft = new NetworkEvents.PlayerEvent();
        events.PlayerLeft.AddListener(OnPlayerLeft);

        events.OnSceneLoadDone = new NetworkEvents.RunnerEvent();
        events.OnSceneLoadDone.AddListener(OnSceneLoadDone);
    }

    private Task<StartGameResult> StartGame(NetworkRunner runner, GameMode gameMode)
    {
        runner.ProvideInput = true;
        return runner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            CustomLobbyName = RoomType.ToString(),
            SessionName = RoomType.ToString(),
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = runner.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    private void OnSceneLoadDone(NetworkRunner runner)
    {
        switch (RoomType)
        {
            case RoomType.Ocean:
                StartCoroutine(LoadAssetOcean(runner));
                break;
            case RoomType.Titanic:
                StartCoroutine(LoadAssetTitanic(runner));
                break;
        }
    }

    private IEnumerator LoadAssetOcean(NetworkRunner runner)
    {
        if (runner.IsServer)
        {
            var totalFish = 0;
            Config.Data.FishAreas.FishAreas.ForEach(config => totalFish += config.NumberFish);
            for (int i = 0; i < totalFish; i++)
            {
                runner.Spawn(Config.Data.Fish.Objects[Random.Range(0, Config.Data.Fish.Objects.Count)]);
            }

            runner.Spawn(Config.Data.FishAreas.Object);

            CameraFollower = runner.InstantiateInRunnerScene(Config.Data.CameraFollower);

            var handleServer = Addressables.LoadAssetAsync<GameObject>("Ocean");
            yield return handleServer;
            runner.InstantiateInRunnerScene(handleServer.Result);

            handleServer = Addressables.LoadAssetAsync<GameObject>("Ocean2");
            yield return handleServer;
            runner.InstantiateInRunnerScene(handleServer.Result);

            handleServer = Addressables.LoadAssetAsync<GameObject>("Terrain");
            yield return handleServer;
            runner.InstantiateInRunnerScene(handleServer.Result);

            runner.GetComponent<EventScene>().OnAssetLoadDone?.Invoke(RoomType.Ocean);

            handleServer = Addressables.LoadAssetAsync<GameObject>("Gyre");
            yield return handleServer;
            runner.InstantiateInRunnerScene(handleServer.Result);
        }
        else
        {
            var handleClient = Addressables.LoadAssetAsync<GameObject>("Volume");
            yield return handleClient;
            runner.InstantiateInRunnerScene(handleClient.Result);

            handleClient = Addressables.LoadAssetAsync<GameObject>("JellyFishes");
            yield return handleClient;
            runner.InstantiateInRunnerScene(handleClient.Result);

            if (SceneController.Instance.SceneFrom == SceneType.Loading)
            {
                handleClient = Addressables.LoadAssetAsync<GameObject>("Ocean");
                yield return handleClient;
                runner.InstantiateInRunnerScene(handleClient.Result);

                runner.GetComponent<EventScene>().OnAssetLoadDone?.Invoke(RoomType.Ocean);

                handleClient = Addressables.LoadAssetAsync<GameObject>("Ocean2");
                yield return handleClient;
                runner.InstantiateInRunnerScene(handleClient.Result);
            }
            else
            {
                handleClient = Addressables.LoadAssetAsync<GameObject>("Ocean2");
                yield return handleClient;
                runner.InstantiateInRunnerScene(handleClient.Result);

                runner.GetComponent<EventScene>().OnAssetLoadDone?.Invoke(RoomType.Ocean);

                handleClient = Addressables.LoadAssetAsync<GameObject>("Ocean");
                yield return handleClient;
                runner.InstantiateInRunnerScene(handleClient.Result);
            }

            handleClient = Addressables.LoadAssetAsync<GameObject>("Gyre");
            yield return handleClient;
            runner.InstantiateInRunnerScene(handleClient.Result);

            handleClient = Addressables.LoadAssetAsync<GameObject>("ClamShells");
            yield return handleClient;
            runner.InstantiateInRunnerScene(handleClient.Result);

            handleClient = Addressables.LoadAssetAsync<GameObject>("BubblesCommon");
            yield return handleClient;
            runner.InstantiateInRunnerScene(handleClient.Result);

            handleClient = Addressables.LoadAssetAsync<GameObject>("SunLight");
            yield return handleClient;
            runner.InstantiateInRunnerScene(handleClient.Result);
        }
    }

    private IEnumerator LoadAssetTitanic(NetworkRunner runner)
    {
        if (runner.IsServer)
        {
            CameraFollower = runner.InstantiateInRunnerScene(Config.Data.CameraFollower);
        }

        yield return new WaitForSeconds(1);

        runner.GetComponent<EventScene>().OnAssetLoadDone?.Invoke(RoomType.Titanic);
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
            var position = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            switch (SceneController.Instance.SceneFrom)
            {
                case SceneType.Loading:
                    position += Config.Data.Player.PositionSpawnedOcean;
                    break;
                case SceneType.Ocean:
                    position += Config.Data.Player.PositionSpawnedTitanic;
                    break;
                case SceneType.Titanic:
                    position += Config.Data.Player.PositionSpawnedTitanicToOcean;
                    break;
            }
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
            DOTween.Sequence().AppendInterval(1).AppendCallback(() =>
            {
                ChangeCameraFollower(1);
            });
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
