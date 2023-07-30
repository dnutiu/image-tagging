publish-all:
	dotnet publish -c release --os win
	dotnet publish -c release --os linux
	dotnet publish -c release --os osx -a x64
	dotnet publish -c release --os osx -a arm64