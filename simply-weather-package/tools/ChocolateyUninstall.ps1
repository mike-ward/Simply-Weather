$productcode = (gwmi win32_product | ? { $_.Name -Like "Simply Weather*" } | % { $_.IdentifyingNumber } | Select-Object -First 1)
Uninstall-ChocolateyPackage "simply-weather" "msi" "$productcode /qb"
