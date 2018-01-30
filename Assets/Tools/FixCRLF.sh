#/bin/bash

# fix line ending behavior for git
# and fix files that have wrong line ending

git config --global core.autocrlf false
git config core.autocrlf false
git config --system core.autocrlf false

git config --global core.eol lf
git config --system core.eol lf
git config core.eol lf

find . -name "*.cs" -exec dos2unix {} \;
find . -name "*.meta" -exec dos2unix {} \;
find . -name "*.unity" -exec dos2unix {} \;

