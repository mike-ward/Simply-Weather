$u = "${Env:ProgramFiles(x86)}" + "\Simply Weather\unins000.exe"
Uninstall-ChocolateyPackage "simply-weather" "exe" "/verysilent" "$u"
