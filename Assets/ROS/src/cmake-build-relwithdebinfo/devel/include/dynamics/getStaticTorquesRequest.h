// Generated by gencpp from file dynamics/getStaticTorquesRequest.msg
// DO NOT EDIT!


#ifndef DYNAMICS_MESSAGE_GETSTATICTORQUESREQUEST_H
#define DYNAMICS_MESSAGE_GETSTATICTORQUESREQUEST_H


#include <string>
#include <vector>
#include <map>

#include <ros/types.h>
#include <ros/serialization.h>
#include <ros/builtin_message_traits.h>
#include <ros/message_operations.h>


namespace dynamics
{
template <class ContainerAllocator>
struct getStaticTorquesRequest_
{
  typedef getStaticTorquesRequest_<ContainerAllocator> Type;

  getStaticTorquesRequest_()
    : q()  {
    }
  getStaticTorquesRequest_(const ContainerAllocator& _alloc)
    : q(_alloc)  {
  (void)_alloc;
    }



   typedef std::vector<double, typename ContainerAllocator::template rebind<double>::other >  _q_type;
  _q_type q;





  typedef boost::shared_ptr< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> > Ptr;
  typedef boost::shared_ptr< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> const> ConstPtr;

}; // struct getStaticTorquesRequest_

typedef ::dynamics::getStaticTorquesRequest_<std::allocator<void> > getStaticTorquesRequest;

typedef boost::shared_ptr< ::dynamics::getStaticTorquesRequest > getStaticTorquesRequestPtr;
typedef boost::shared_ptr< ::dynamics::getStaticTorquesRequest const> getStaticTorquesRequestConstPtr;

// constants requiring out of line definition



template<typename ContainerAllocator>
std::ostream& operator<<(std::ostream& s, const ::dynamics::getStaticTorquesRequest_<ContainerAllocator> & v)
{
ros::message_operations::Printer< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >::stream(s, "", v);
return s;
}


template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator==(const ::dynamics::getStaticTorquesRequest_<ContainerAllocator1> & lhs, const ::dynamics::getStaticTorquesRequest_<ContainerAllocator2> & rhs)
{
  return lhs.q == rhs.q;
}

template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator!=(const ::dynamics::getStaticTorquesRequest_<ContainerAllocator1> & lhs, const ::dynamics::getStaticTorquesRequest_<ContainerAllocator2> & rhs)
{
  return !(lhs == rhs);
}


} // namespace dynamics

namespace ros
{
namespace message_traits
{





template <class ContainerAllocator>
struct IsMessage< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
  : TrueType
  { };

template <class ContainerAllocator>
struct IsMessage< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> const>
  : TrueType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> const>
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> const>
  : FalseType
  { };


template<class ContainerAllocator>
struct MD5Sum< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "ab94b9bcaaa12f74def43e4b33992df1";
  }

  static const char* value(const ::dynamics::getStaticTorquesRequest_<ContainerAllocator>&) { return value(); }
  static const uint64_t static_value1 = 0xab94b9bcaaa12f74ULL;
  static const uint64_t static_value2 = 0xdef43e4b33992df1ULL;
};

template<class ContainerAllocator>
struct DataType< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "dynamics/getStaticTorquesRequest";
  }

  static const char* value(const ::dynamics::getStaticTorquesRequest_<ContainerAllocator>&) { return value(); }
};

template<class ContainerAllocator>
struct Definition< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "float64[] q\n"
;
  }

  static const char* value(const ::dynamics::getStaticTorquesRequest_<ContainerAllocator>&) { return value(); }
};

} // namespace message_traits
} // namespace ros

namespace ros
{
namespace serialization
{

  template<class ContainerAllocator> struct Serializer< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
  {
    template<typename Stream, typename T> inline static void allInOne(Stream& stream, T m)
    {
      stream.next(m.q);
    }

    ROS_DECLARE_ALLINONE_SERIALIZER
  }; // struct getStaticTorquesRequest_

} // namespace serialization
} // namespace ros

namespace ros
{
namespace message_operations
{

template<class ContainerAllocator>
struct Printer< ::dynamics::getStaticTorquesRequest_<ContainerAllocator> >
{
  template<typename Stream> static void stream(Stream& s, const std::string& indent, const ::dynamics::getStaticTorquesRequest_<ContainerAllocator>& v)
  {
    s << indent << "q[]" << std::endl;
    for (size_t i = 0; i < v.q.size(); ++i)
    {
      s << indent << "  q[" << i << "]: ";
      Printer<double>::stream(s, indent + "  ", v.q[i]);
    }
  }
};

} // namespace message_operations
} // namespace ros

#endif // DYNAMICS_MESSAGE_GETSTATICTORQUESREQUEST_H
