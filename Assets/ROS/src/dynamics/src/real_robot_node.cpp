//
// Created by paul on 7/26/19.
//
#include <iostream>
#include "dynamics/getStaticTorques.h"
#include "std_msgs/Float32MultiArray.h"
#include "sensor_msgs/JointState.h"
#include "Yumi.h"
#include "RobotFactory.h"
#include "ROSProvider.h"

int main(int argc, char *argv[])
{
    ros::init(argc, argv, "real_robot_node");
    ros::NodeHandle n;

    auto yumi = RobotFactory::getRobot<Yumi>();

    ros::ServiceServer getStaticJointTorqueService = ROSProvider::getService(n, "getStaticJointTorques", Yumi::getStaticTorques);

    auto [operationalPosPub_L, operationalPosPubMsg_L] = ROSProvider::getPublisher<std_msgs::Float32>(n, "operational_position_L", 1);
    auto [operationalPosPub_R, operationalPosPubMsg_R] = ROSProvider::getPublisher<std_msgs::Float32>(n, "operational_position_R", 1);
    auto jointStateSubscriber = ROSProvider::getSubscriber(n, "joint_states", 1, Yumi::updateJoints);
    auto [jointVelPub_L, jointVelPubMsg_L] = ROSProvider::getPublisher<std_msgs::Float32>(n, "joint_velocity_command_L", 1);
    auto [jointVelPub_R, jointVelPubMsg_R] = ROSProvider::getPublisher<std_msgs::Float32>(n, "joint_velocity_command_R", 1);
    auto [jointRvizPub, jointRvizMsg] = ROSProvider::getPublisher<sensor_msgs::JointState>(n, "rviz/joint_states", 1);
    auto jointStateSubscriberrviz = ROSProvider::getSubscriber(n, "joint_states", 1, Yumi::rvizUpdateJoints);


    ros::Rate loop_rate(250);
    while (ros::ok())
    {
        ROSProvider::publishAll();
        ros::spinOnce();
        loop_rate.sleep();
    }
    int a= 5;
    return 0;
}
