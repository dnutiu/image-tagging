publish-all:
	dotnet publish -c release --os win
	dotnet publish -c release --os linux
	dotnet publish -c release --os osx