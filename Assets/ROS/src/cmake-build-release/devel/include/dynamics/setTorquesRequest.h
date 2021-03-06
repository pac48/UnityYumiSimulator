// Generated by gencpp from file dynamics/setTorquesRequest.msg
// DO NOT EDIT!


#ifndef DYNAMICS_MESSAGE_SETTORQUESREQUEST_H
#define DYNAMICS_MESSAGE_SETTORQUESREQUEST_H


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
struct setTorquesRequest_
{
  typedef setTorquesRequest_<ContainerAllocator> Type;

  setTorquesRequest_()
    : tau()  {
    }
  setTorquesRequest_(const ContainerAllocator& _alloc)
    : tau(_alloc)  {
  (void)_alloc;
    }



   typedef std::vector<double, typename ContainerAllocator::template rebind<double>::other >  _tau_type;
  _tau_type tau;





  typedef boost::shared_ptr< ::dynamics::setTorquesRequest_<ContainerAllocator> > Ptr;
  typedef boost::shared_ptr< ::dynamics::setTorquesRequest_<ContainerAllocator> const> ConstPtr;

}; // struct setTorquesRequest_

typedef ::dynamics::setTorquesRequest_<std::allocator<void> > setTorquesRequest;

typedef boost::shared_ptr< ::dynamics::setTorquesRequest > setTorquesRequestPtr;
typedef boost::shared_ptr< ::dynamics::setTorquesRequest const> setTorquesRequestConstPtr;

// constants requiring out of line definition



template<typename ContainerAllocator>
std::ostream& operator<<(std::ostream& s, const ::dynamics::setTorquesRequest_<ContainerAllocator> & v)
{
ros::message_operations::Printer< ::dynamics::setTorquesRequest_<ContainerAllocator> >::stream(s, "", v);
return s;
}


template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator==(const ::dynamics::setTorquesRequest_<ContainerAllocator1> & lhs, const ::dynamics::setTorquesRequest_<ContainerAllocator2> & rhs)
{
  return lhs.tau == rhs.tau;
}

template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator!=(const ::dynamics::setTorquesRequest_<ContainerAllocator1> & lhs, const ::dynamics::setTorquesRequest_<ContainerAllocator2> & rhs)
{
  return !(lhs == rhs);
}


} // namespace dynamics

namespace ros
{
namespace message_traits
{





template <class ContainerAllocator>
struct IsMessage< ::dynamics::setTorquesRequest_<ContainerAllocator> >
  : TrueType
  { };

template <class ContainerAllocator>
struct IsMessage< ::dynamics::setTorquesRequest_<ContainerAllocator> const>
  : TrueType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::dynamics::setTorquesRequest_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::dynamics::setTorquesRequest_<ContainerAllocator> const>
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::dynamics::setTorquesRequest_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::dynamics::setTorquesRequest_<ContainerAllocator> const>
  : FalseType
  { };


template<class ContainerAllocator>
struct MD5Sum< ::dynamics::setTorquesRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "6488f72361a7c3710faf21208a8c781c";
  }

  static const char* value(const ::dynamics::setTorquesRequest_<ContainerAllocator>&) { return value(); }
  static const uint64_t static_value1 = 0x6488f72361a7c371ULL;
  static const uint64_t static_value2 = 0x0faf21208a8c781cULL;
};

template<class ContainerAllocator>
struct DataType< ::dynamics::setTorquesRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "dynamics/setTorquesRequest";
  }

  static const char* value(const ::dynamics::setTorquesRequest_<ContainerAllocator>&) { return value(); }
};

template<class ContainerAllocator>
struct Definition< ::dynamics::setTorquesRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "float64[] tau\n"
;
  }

  static const char* value(const ::dynamics::setTorquesRequest_<ContainerAllocator>&) { return value(); }
};

} // namespace message_traits
} // namespace ros

namespace ros
{
namespace serialization
{

  template<class ContainerAllocator> struct Serializer< ::dynamics::setTorquesRequest_<ContainerAllocator> >
  {
    template<typename Stream, typename T> inline static void allInOne(Stream& stream, T m)
    {
      stream.next(m.tau);
    }

    ROS_DECLARE_ALLINONE_SERIALIZER
  }; // struct setTorquesRequest_

} // namespace serialization
} // namespace ros

namespace ros
{
namespace message_operations
{

template<class ContainerAllocator>
struct Printer< ::dynamics::setTorquesRequest_<ContainerAllocator> >
{
  template<typename Stream> static void stream(Stream& s, const std::string& indent, const ::dynamics::setTorquesRequest_<ContainerAllocator>& v)
  {
    s << indent << "tau[]" << std::endl;
    for (size_t i = 0; i < v.tau.size(); ++i)
    {
      s << indent << "  tau[" << i << "]: ";
      Printer<double>::stream(s, indent + "  ", v.tau[i]);
    }
  }
};

} // namespace message_operations
} // namespace ros

#endif // DYNAMICS_MESSAGE_SETTORQUESREQUEST_H
