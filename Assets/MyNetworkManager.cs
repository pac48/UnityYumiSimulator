using Mirror;
using UnityEngine;


    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class MyNetworkManager : NetworkManager
    {
        //public static GameObject player; 

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

          //var c = GetComponent<NetworkManager>();
          //c.playerPrefab = player;
          
           NetworkServer.AddPlayerForConnection(conn, player);
           GameObject.Find("server2client").GetComponent<Server2Client>().player = player;

           // spawn ball if two players
           //   if (numPlayers == 2)
           //  {
           //     ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
           //    NetworkServer.Spawn(ball);
           // }
        }

        


        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // destroy ball
        //    if (ball != null)
         //       NetworkServer.Destroy(ball);

            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
