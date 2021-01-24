//
// Created by paul on 7/30/19.
//

#ifndef ROS_PROVIDER_H
#define ROS_PROVIDER_H

#include "ros/ros.h"
#include <boost/function.hpp>
#include <tuple>
#include <map>

using namespace std;

class ROSProvider
{
public:
    template <typename T>
    static auto getSubscriberCallBack(void (*userCallback)(T))
    {
        boost::function<void(T msg)> callBack = [=](T msg) { userCallback(msg); };
        return callBack;
    }

    template <typename T>
    static tuple<ros::Publisher, T> getPublisher(ros::NodeHandle n, const string &topic, uint32_t queue_size)
    {
        if (!publisherPairs)
        {
            publisherPairs = new vector<PublisherPair *>();
            atexit(ROSProvider::cleanUp);
        }
        if (!topic2Pub<T>)
        {
            topic2Pub<T> = new map<string, tuple<ros::Publisher, T> *>();
            atexit(ROSProvider::cleanTopic2Pub<T>);
        }
        if (!topic2Pub<T>->operator[](topic))
        {
            ros::Publisher pub = n.advertise<T>(topic, queue_size);
            auto msg = new T();
            publisherPairs->push_back(new PublisherPair{pub, (void *)msg});
            auto tmp = new tuple<ros::Publisher, T> {pub, *msg};
            topic2Pub<T>->operator[](topic) = tmp;
        }
        return *topic2Pub<T>->operator[](topic); //{pub, *msg};
    }

    template <typename T>
    static ros::Subscriber getSubscriber(ros::NodeHandle n, const string &topic, uint32_t queue_size,
                                         void (*userCallback)(T))
    {
        if (!topic2Sub)
            topic2Sub = new map<string, ros::Subscriber *>();
        if (!topic2Sub->operator[](topic))
        {
            auto callback = getSubscriberCallBack(userCallback);
            ros::Subscriber sub = n.subscribe(topic, queue_size, callback);
            topic2Sub->operator[](topic) = &sub;
        }
        return *(topic2Sub->operator[](topic));
    }

    template <typename T1, typename T2>
    static auto getServiceCallBack(bool (*userCallback)(T1, T2))
    {
        boost::function<bool(T1 req, T2 res)> callBack = [=](T1 req, T2 res) { return userCallback(req, res); };
        return callBack;
    }

    template <typename T1, typename T2>
    static ros::ServiceServer getService(ros::NodeHandle n, const string &topic, bool (*userCallback)(T1, T2))
    {
        auto callback = getServiceCallBack(userCallback);
        ros::ServiceServer service = n.advertiseService(topic, callback);
        return service;
    }

    static void publishAll()
    {
        for (auto pp : *publisherPairs)
            ROSProvider::publishOne<std_msgs::Float32>(pp->msg, pp->publisher);
    }

    template <typename T>
    static void publishOne(void *msg, ros::Publisher pub)
    {
        T *msg2 = (T *)msg;
        pub.publish(*msg2);
    }

    static void cleanUp()
    {
        for (auto pp : *publisherPairs)
            delete pp;
        delete publisherPairs;
        delete topic2Sub;
        // delete  topic2Pub;
    }

    template <typename T>
    static void cleanTopic2Pub(){
        for (auto it = topic2Pub<T>->begin(); it != topic2Pub<T>->end(); it++)
                delete it->second;
        delete topic2Pub<T>;
    }

private:
    struct PublisherPair
    {
        ros::Publisher publisher;
        void *msg;
    };
    static inline vector<PublisherPair *> *publisherPairs;
    static inline map<string, ros::Subscriber *> *topic2Sub;
    template <typename T>
    static inline map<string, tuple<ros::Publisher, T> *> *topic2Pub;
};
#endif
