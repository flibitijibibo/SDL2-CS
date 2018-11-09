#!/bin/bash

set -e

version=`date +"%Y.%m.%d"`

nuspec="gitlab-ci/SDL2-CS.nuspec"

nuget pack $nuspec -Version $version
