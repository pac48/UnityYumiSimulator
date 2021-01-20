using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Mirror;
using RosSharp.RosBridgeClient;

public class YumiMalePlayer : NetworkBehaviour
{
    protected List<Action> functions = new List<Action>();
    private static PlayerType playerType;
    
   public void Start()
    {
        functions.Add(WaitForPlayerAssign);
    }

   void WaitForROSToConnect(){
       var rosConnector = GetComponent<RosConnector>();
       if (rosConnector.RosSocket == null)
           return;
       functions.Remove(WaitForROSToConnect);
       EnablePublishersSubscribers();
       
   }
   void EnableROS()
   {
       var component = GetComponent<RosConnector>();
       if (component != null){
           component.Activate();
            functions.Add(WaitForROSToConnect);
            }
       }
   void EnablePublishersSubscribers()
   {
       var rosPubSubList =
           (GetComponents<UnityPublisher<RosSharp.RosBridgeClient.MessageTypes.Std.Float32MultiArray>>().Cast<MonoBehaviour>())
           .Concat(GetComponents<UnityPublisher<RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped>>().Cast<MonoBehaviour>())
           .Concat(GetComponents<UnitySubscriber<RosSharp.RosBridgeClient.MessageTypes.Std.Float32MultiArray>>().Cast<MonoBehaviour>())
           .Concat(GetComponents<UnitySubscriber<RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped>>().Cast<MonoBehaviour>())
           .ToList();
       foreach (var script in rosPubSubList)
       {
           script.enabled = true;
       }
   }
    void WaitForPlayerAssign(){
        if ((!Server2Client.playerAssigned || Server2Client.playerName.Length == 0) && !isServer){
            return;
        }
        functions.Remove(WaitForPlayerAssign);
        if (Server2Client.playerName.Contains("male") && !isServer){
            functions.Add(FixedUpdateHuman);
            playerType = PlayerType.Male;
        } else if (Server2Client.playerName.Contains("yumi") && !isServer){
            functions.Add(FixedUpdateYumi);
            playerType = PlayerType.Yumi;
            EnableROS();
        } else{
            functions.Add(FixedUpdateServer);
            playerType = PlayerType.Server;
        }
        PostAssignPlayer();
    }

    public void FixedUpdate()
    {
        int ind = 0;
        while (ind < functions.Count)
        {
            functions[ind]();
            ind++;
        } 
    }
    public virtual void FixedUpdateHuman(){}
    public virtual void FixedUpdateYumi() {}
    public virtual void FixedUpdateServer() {}
    public virtual void PostAssignPlayer(){}
         
}
