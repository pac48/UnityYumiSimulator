# CMake generated Testfile for 
# Source directory: C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro
# Build directory: C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-debug/xacro
# 
# This file includes the relevant testing commands required for 
# testing this directory and lists subdirectories to be tested as well.
add_test(_ctest_xacro_nosetests_test "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-debug/catkin_generated/env_cached.bat" "C:/opt/ros/noetic/x64/python.exe" "C:/opt/ros/noetic/x64/share/catkin/cmake/test/run_tests.py" "C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-debug/test_results/xacro/nosetests-test.xml" "--return-code" "\"D:/Program Files/JetBrains/CLion 2020.2.3/bin/cmake/win/bin/cmake.exe\" -E make_directory C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-debug/test_results/xacro" "C:/opt/ros/noetic/x64/Scripts/nosetests.exe -P --process-timeout=60 --where=C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/test --with-xunit --xunit-file=C:/Users/paulg/UnityProjects/project/Assets/ROS/src/cmake-build-debug/test_results/xacro/nosetests-test.xml")
set_tests_properties(_ctest_xacro_nosetests_test PROPERTIES  _BACKTRACE_TRIPLES "C:/opt/ros/noetic/x64/share/catkin/cmake/test/tests.cmake;160;add_test;C:/opt/ros/noetic/x64/share/catkin/cmake/test/nosetests.cmake;83;catkin_run_tests_target;C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/CMakeLists.txt;20;catkin_add_nosetests;C:/Users/paulg/UnityProjects/project/Assets/ROS/src/xacro/CMakeLists.txt;0;")
subdirs("test")
