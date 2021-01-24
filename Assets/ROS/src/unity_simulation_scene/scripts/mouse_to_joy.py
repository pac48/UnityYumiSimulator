#!/usr/bin/env python

# Siemens AG, 2018
# Author: Berkay Alp Cakal (berkay_alp.cakal.ct@siemens.com)
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# <http://www.apache.org/licenses/LICENSE-2.0>.
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

import rospy
import numpy
from std_msgs.msg import Float32MultiArray
from geometry_msgs.msg import PoseStamped
from geometry_msgs.msg import Twist
from Xlib import display
from Xlib.ext import randr
import win32api
# from tf.transformations import quaternion_matrix
import math
import numpy as np

_EPS = numpy.finfo(float).eps * 4.0

def quaternion_matrix(quaternion):
    """Return homogeneous rotation matrix from quaternion.
    """
    q = numpy.array(quaternion[:4], dtype=numpy.float64, copy=True)
    nq = numpy.dot(q, q)
    if nq < _EPS:
        return numpy.identity(4)
    q *= math.sqrt(2.0 / nq)
    q = numpy.outer(q, q)
    return numpy.array((
        (1.0-q[1, 1]-q[2, 2],     q[0, 1]-q[2, 3],     q[0, 2]+q[1, 3], 0.0),
        (    q[0, 1]+q[2, 3], 1.0-q[0, 0]-q[2, 2],     q[1, 2]-q[0, 3], 0.0),
        (    q[0, 2]-q[1, 3],     q[1, 2]+q[0, 3], 1.0-q[0, 0]-q[1, 1], 0.0),
        (                0.0,                 0.0,                 0.0, 1.0)
        ), dtype=numpy.float64)

msg_robot_hand = None
msg_human_hand = None

def callback(msg):
    global msg_robot_hand
    msg_robot_hand = msg

def callback2(msg):
    global msg_human_hand
    msg_human_hand = msg

def mouseToJoy():
    # initialize node
    rospy.init_node('mouseToJoy', anonymous=True)
    global msg_robot_hand
    global msg_human_hand
    #### Setup MouseToJoy Publisher
    mouseToJoyPublisher = rospy.Publisher("hand_vel_command_R", Float32MultiArray, queue_size=5)
    mouseToJoyPublisherL = rospy.Publisher("joint_vel_command_L", Float32MultiArray, queue_size=5)
    rospy.Subscriber("robot_hand2_R", Float32MultiArray, callback)
    rospy.Subscriber("human_hand_R", PoseStamped, callback2)
    rate = rospy.Rate(60)  # 10hz
    msg = Float32MultiArray()

    while not rospy.is_shutdown():
        #### Initialize joy msg every loop
        if msg_human_hand != None and msg_robot_hand != None:
            data = np.array(msg_robot_hand.data)
            hand_r = data[0:3]
            z = data[3:6]
            pos_x, pos_y = win32api.GetCursorPos()
            pos_x -= 1920/2
            pos_y -= 1080/2
            pos_y = - pos_y 
            pos_x *= (5/1920)
            pos_y *= (5/1080)
          #  z = matrix[0:3, 2]
            print(z)
            # theta = np.acos(np.dot(z, np.array([1.0, 0.0, 0.0])))
            rot = .41*np.cross(z, np.array([0.0, 0.0, -1.0]))
           # rot[2] = 4
            #### Start Mapping from Mouse Position to Joy
            msg.data = [-1*(pos_y-hand_r[2]*0), (msg_human_hand.pose.position.z*0+2.0-hand_r[2]), 1*(pos_x-hand_r[0]*0), rot[0], rot[1], rot[2]]
            rospy.loginfo(msg)
            mouseToJoyPublisher.publish(msg)
            msg.data = [.5, .5, .5, 0, 0, 0, 0]
            mouseToJoyPublisherL.publish(msg)
            msg_robot_hand = None
            msg_human_hand = None
            rate.sleep()


if __name__ == '__main__':
    mouseToJoy()
