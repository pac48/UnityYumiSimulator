using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RosSharp.RosBridgeClient
{
    public class YumiHandVelSubscriber : UnitySubscriber<MessageTypes.Std.Float32MultiArray>
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
                Yumi.handVelL = message.data.ToList();
            }
            else
            {
                Yumi.handVelR = message.data.ToList();
            }
        }
    }
}