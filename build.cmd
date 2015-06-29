echo off
nuget restore "Simply Weather.sln"
if ERRORLEVEL 1 goto END
msbuild "Simply Weather.sln" /p:configuration=release
:END
