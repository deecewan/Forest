#!/usr/bin/env zsh


dotnet clean
rm ./**/*.nupkg

dotnet build -c Debug
dotnet build -c Release

for dir in src/*; do
  echo "Going into $dir"
  pushd $dir
  nuget pack
  popd
done

for i in ./**/*.nupkg; do
  echo "Pushing $i"
  nuget push $i -Source nuget.org
done
