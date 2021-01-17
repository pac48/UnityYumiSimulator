using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RosSharp.RosBridgeClient
{
    public class YumiJointVelSubscriber : UnitySubscriber<MessageTypes.Std.Float32MultiArray>
    {
        private bool left = false;
        protected override void Start()
        {
            base.Start();

            if (base.Topic.Contains("_L"))
            {
                left = true;
            }
        }

        protected override void ReceiveMessage(MessageTypes.Std.Float32MultiArray message)
        {
            if (left)
            {
                Yumi.jointVelL = message.data.ToList();
                Yumi.handVelL = null;
            }
            else {
                Yumi.jointVelR = message.data.ToList();
                Yumi.handVelR = null;
            }
        }
    }
}