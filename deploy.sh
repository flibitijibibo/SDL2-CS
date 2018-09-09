#!/bin/bash

nuget setApiKey $NUGET_API_KEY -verbosity quiet

for package in `find *.nupkg`; do
  nuget push $package -source https://nuget.org/
done

