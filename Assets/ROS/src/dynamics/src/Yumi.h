//
// Created by paul on 1/7/20.
//
#ifndef YUMI_H
#define YUMI_H

#include "Robot.cpp"

class Yumi : public Robot
{
    
public:
    std::vector<std::string> jointNames = {"yumi_joint_1_r", "yumi_joint_2_r",
                                           "yumi_joint_7_r", "yumi_joint_3_r",
                                           "yumi_joint_4_r", "yumi_joint_5_r",
                                           "yumi_joint_6_r", "yumi_joint_1_l",
                                           "yumi_joint_2_l", "yumi_joint_7_l",
                                           "yumi_joint_3_l", "yumi_joint_4_l",
                                           "yumi_joint_5_l", "yumi_joint_6_l"};
    Yumi() : Robot("Yumi")
    {
        auto q = this->dynamics->getPosition();
        q[0] = (46.0 / 180.0) * 3.14;
        q[1] = (-121.0 / 180.0) * 3.14;
        q[2] = (-87.0 / 180.0) * 3.14;
        q[3] = (4.0 / 180.0) * 3.14;
        q[4] = (44.0 / 180.0) * 3.14;
        q[5] = (54.0 / 180.0) * 3.14;
        q[6] = (-3.0 / 180.0) * 3.14;
        this->dynamics->setPosition(q);
    }

    static void updateJoints(const std_msgs::Float32MultiArray::ConstPtr &msg)
    { //subpub
    Yumi* robot = RobotFactory::getRobot<Yumi>();
        std::vector<float> data = msg->data;
        auto q = robot->floatVec2MathVec(data);
        for (int i = 0; i < data.size(); i++)
        {
            q[i] = data[i];
        }
        robot->dynamics->setPosition(q);
        robot->dynamics->forwardPosition();
       // opPostionMsgs[0]->data = robot->getOperationalPosition(0);
      //  opPostionMsgs[1]->data = robot->getOperationalPosition(1);
    }

    static void rvizUpdateJoints(const std_msgs::Float32MultiArray::ConstPtr &msg)
    { //subpub
    Yumi* robot = RobotFactory::getRobot<Yumi>();
        std::vector<double> tmp;
        for (int i = 0; i < msg->data.size(); i++)
        {
            double val = msg->data[i];
            tmp.push_back(val);
        }
        //rvizMsgs->position = tmp;
  // rvizMsgs->name = robot->jointNames;
     //   rvizMsgs->header.stamp = ros::Time::now();
    }

    static void sendOpJointVelocities(const std_msgs::Float32MultiArray::ConstPtr &opVelMsg, std::vector<std_msgs::Float32MultiArray *> jointVelMsgs)
    { //subpub
    Yumi* robot = RobotFactory::getRobot<Yumi>();
        std::vector<float> opvelL;
        std::vector<float> opvelR;
        for (int i = 0; i < 6; i++)
        {
            opvelR.push_back(opVelMsg->data[i]);
            opvelL.push_back(opVelMsg->data[i + 6]);
        }
        jointVelMsgs[0]->data = robot->xd2jd(0, opvelR);
        jointVelMsgs[1]->data = robot->xd2jd(1, opvelL);
    }

     static bool getStaticTorques(dynamics::getStaticTorques::Request  &req, dynamics::getStaticTorques::Response &res){ // serviceRobot
         // Robot* robot = RobotFactory::getRobot<Robot>();
          Yumi* robot = RobotFactory::getRobot<Yumi>();
          auto q = robot->dynamics->getPosition();
        auto qd = robot->dynamics->getVelocity();
        auto qdd = robot->dynamics->getAcceleration();
        auto qtmp = robot->dynamics->getPosition();
        auto qdtmp = robot->dynamics->getVelocity();
        auto qddtmp = robot->dynamics->getAcceleration();
        for (int i=0;i<req.q.size();i++){
            q[i]=req.q[i];
            qd[i]=0;
            qdd[i]=0;
        }
        robot->dynamics->setPosition(q);
        robot->dynamics->setVelocity(qd);
        robot->dynamics->setAcceleration(qdd);
        robot->dynamics->inverseDynamics();
        auto tau = robot->dynamics->getTorque();
        res.tau=req.q;
        for (int i=0;i<req.q.size();i++) {
            res.tau[i] = tau[i];
        }
        robot->dynamics->setPosition(qtmp);
        robot->dynamics->setVelocity(qdtmp);
        robot->dynamics->setAcceleration(qddtmp);
        robot->dynamics->forwardDynamics();
        return true;
    }

};

#endif //YUMI_WS_YUMI_H
