using Mirror;
using UnityEngine;
using UnityEngine.Rendering;


// Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class MyNetworkManager : NetworkManager
    {
        //public static GameObject player; 
        void Awake()
        {
            if (IsHeadless()) {
                print("headless mode detected");
                StartServer();
            }
            else 
            {
                print("automatic connect");
                networkAddress = "18.188.185.82";
                StartClient();
            }
        }
        
        bool IsHeadless() {
            return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // add player at correct spawn position
           // Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
           // GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
          GameObject player;
          if (numPlayers == 0 && Application.platform != RuntimePlatform.WebGLPlayer ){
              player = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "yumiNetwork"));
          } else{
              player = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "male"));
          }
          NetworkServer.AddPlayerForConnection(conn, player);
           GameObject.Find("server2client").GetComponent<Server2Client>().player = player;
        }

    }
