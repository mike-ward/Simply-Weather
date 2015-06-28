if EXIST out\*.msi del /Q out\*.msi
@"c:\Program Files (x86)\WiX Toolset v3.9\bin\candle.exe" Product.wxs -dSourceFiles="..\Simply Weather\bin\Release" -out out\
@if NOT ERRORLEVEL 1 "c:\Program Files (x86)\WiX Toolset v3.9\bin\light.exe" -ext WiXNetFxExtension -ext WixUIExtension out\Product.wixobj -out out\SimplyWeatherSetup.msi -sice:ICE61
exit /b errorlevel