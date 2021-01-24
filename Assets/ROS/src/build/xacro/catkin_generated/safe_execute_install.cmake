execute_process(COMMAND "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/build/xacro/catkin_generated/python_distutils_install.bat" RESULT_VARIABLE res)

if(NOT res EQUAL 0)
  message(FATAL_ERROR "execute_process(C:/Users/paulg/UnityProjects/project/Assets/ROS/src/build/xacro/catkin_generated/python_distutils_install.bat) returned error code ")
endif()
