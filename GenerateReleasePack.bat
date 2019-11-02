@echo off
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
zip -r Landtory\bin\Debug\ LandtoryRelease-%0.zip
echo Proceed to delete SHDN development file manually