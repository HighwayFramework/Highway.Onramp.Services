 Get-Item .\build\*.nupkg | % { src\.nuget\NuGet.exe push $_ }