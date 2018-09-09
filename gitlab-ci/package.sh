#!/bin/bash

set -e

version=`date +"%Y.%m.%d"`

nuspec="gitlab-ci/SDL2-CS.nuspec"

sed -i -e "s/%version%/$version/g" $nuspec

nuget pack $nuspec

