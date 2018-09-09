#!/bin/bash

csproj=SDL2-CS.csproj
nuspec="${csproj}.nuspec"

timestamp=`date +"%Y%m%d%H%M%S"`

sed -i -e "s/%timestamp%/$timestamp/g" $nuspec

msbuild /p:Configuration=Debug $csproj

nuget pack $nuspec

