// Generated by gencpp from file dynamics/getStaticTorquesResponse.msg
// DO NOT EDIT!


#ifndef DYNAMICS_MESSAGE_GETSTATICTORQUESRESPONSE_H
#define DYNAMICS_MESSAGE_GETSTATICTORQUESRESPONSE_H


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
struct getStaticTorquesResponse_
{
  typedef getStaticTorquesResponse_<ContainerAllocator> Type;

  getStaticTorquesResponse_()
    : tau()  {
    }
  getStaticTorquesResponse_(const ContainerAllocator& _alloc)
    : tau(_alloc)  {
  (void)_alloc;
    }



   typedef std::vector<double, typename ContainerAllocator::template rebind<double>::other >  _tau_type;
  _tau_type tau;





  typedef boost::shared_ptr< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> > Ptr;
  typedef boost::shared_ptr< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> const> ConstPtr;

}; // struct getStaticTorquesResponse_

typedef ::dynamics::getStaticTorquesResponse_<std::allocator<void> > getStaticTorquesResponse;

typedef boost::shared_ptr< ::dynamics::getStaticTorquesResponse > getStaticTorquesResponsePtr;
typedef boost::shared_ptr< ::dynamics::getStaticTorquesResponse const> getStaticTorquesResponseConstPtr;

// constants requiring out of line definition



template<typename ContainerAllocator>
std::ostream& operator<<(std::ostream& s, const ::dynamics::getStaticTorquesResponse_<ContainerAllocator> & v)
{
ros::message_operations::Printer< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >::stream(s, "", v);
return s;
}


template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator==(const ::dynamics::getStaticTorquesResponse_<ContainerAllocator1> & lhs, const ::dynamics::getStaticTorquesResponse_<ContainerAllocator2> & rhs)
{
  return lhs.tau == rhs.tau;
}

template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator!=(const ::dynamics::getStaticTorquesResponse_<ContainerAllocator1> & lhs, const ::dynamics::getStaticTorquesResponse_<ContainerAllocator2> & rhs)
{
  return !(lhs == rhs);
}


} // namespace dynamics

namespace ros
{
namespace message_traits
{





template <class ContainerAllocator>
struct IsMessage< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
  : TrueType
  { };

template <class ContainerAllocator>
struct IsMessage< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> const>
  : TrueType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> const>
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> const>
  : FalseType
  { };


template<class ContainerAllocator>
struct MD5Sum< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
{
  static const char* value()
  {
    return "6488f72361a7c3710faf21208a8c781c";
  }

  static const char* value(const ::dynamics::getStaticTorquesResponse_<ContainerAllocator>&) { return value(); }
  static const uint64_t static_value1 = 0x6488f72361a7c371ULL;
  static const uint64_t static_value2 = 0x0faf21208a8c781cULL;
};

template<class ContainerAllocator>
struct DataType< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
{
  static const char* value()
  {
    return "dynamics/getStaticTorquesResponse";
  }

  static const char* value(const ::dynamics::getStaticTorquesResponse_<ContainerAllocator>&) { return value(); }
};

template<class ContainerAllocator>
struct Definition< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
{
  static const char* value()
  {
    return "float64[] tau\n"
;
  }

  static const char* value(const ::dynamics::getStaticTorquesResponse_<ContainerAllocator>&) { return value(); }
};

} // namespace message_traits
} // namespace ros

namespace ros
{
namespace serialization
{

  template<class ContainerAllocator> struct Serializer< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
  {
    template<typename Stream, typename T> inline static void allInOne(Stream& stream, T m)
    {
      stream.next(m.tau);
    }

    ROS_DECLARE_ALLINONE_SERIALIZER
  }; // struct getStaticTorquesResponse_

} // namespace serialization
} // namespace ros

namespace ros
{
namespace message_operations
{

template<class ContainerAllocator>
struct Printer< ::dynamics::getStaticTorquesResponse_<ContainerAllocator> >
{
  template<typename Stream> static void stream(Stream& s, const std::string& indent, const ::dynamics::getStaticTorquesResponse_<ContainerAllocator>& v)
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

#endif // DYNAMICS_MESSAGE_GETSTATICTORQUESRESPONSE_H
