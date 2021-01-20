using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Server2Client : NetworkBehaviour
{

        [SyncVar(hook = "OnChange")] 
        public GameObject player;
        public static bool playerAssigned = false;
        public static string playerName = "";
        
        public void OnChange(GameObject playerold, GameObject playerNew)
        {
                if (playerAssigned || playerNew == null)
                        return;
                playerName = player.name;
                playerAssigned = true;
        }


}
