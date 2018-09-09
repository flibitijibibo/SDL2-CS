#!/bin/bash

set -e

timestamp=`date +"%Y%m%d%H%M%S"`

nuspec="gitlab-ci/SDL2-CS.nuspec"

sed -i -e "s/%timestamp%/$timestamp/g" $nuspec

nuget pack $nuspec

