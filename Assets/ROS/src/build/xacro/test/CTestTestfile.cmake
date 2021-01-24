# CMake generated Testfile for 
# Source directory: C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test
# Build directory: C:/Users/paulg/UnityProjects/project/Assets/ROS/src/build/xacro/test
# 
# This file includes the relevant testing commands required for 
# testing this directory and lists subdirectories to be tested as well.
if("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
  add_test(xacro_cmake "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-cmake.sh" "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-xacro-cmake")
  set_tests_properties(xacro_cmake PROPERTIES  _BACKTRACE_TRIPLES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;1;add_test;C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee])$")
  add_test(xacro_cmake "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-cmake.sh" "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-xacro-cmake")
  set_tests_properties(xacro_cmake PROPERTIES  _BACKTRACE_TRIPLES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;1;add_test;C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
  add_test(xacro_cmake "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-cmake.sh" "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-xacro-cmake")
  set_tests_properties(xacro_cmake PROPERTIES  _BACKTRACE_TRIPLES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;1;add_test;C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;0;")
elseif("${CTEST_CONFIGURATION_TYPE}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
  add_test(xacro_cmake "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-cmake.sh" "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/test-xacro-cmake")
  set_tests_properties(xacro_cmake PROPERTIES  _BACKTRACE_TRIPLES "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;1;add_test;C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test/CMakeLists.txt;0;")
else()
  add_test(xacro_cmake NOT_AVAILABLE)
endif()
