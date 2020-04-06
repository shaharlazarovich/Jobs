Add-Type -AssemblyName System.IO.Compression.FileSystem
cd C:\Repos\edrm
git checkout Development
git pull origin Development

cd C:\Repos\ensuredr-runner
git checkout Development
git pull origin Development

cd C:\Repos\ensuredr-dependencies
git checkout Development
git pull origin Development
git lfs install
git lfs pull origin Development

cd C:\Repos\ensuredr-setup
git checkout Development
git pull origin Development

Remove-Item -Path C:\Repos\edrm\API\wwwroot -Force -Recurse
cd C:\Repos\edrm\client-app
npm install
npm run build
cd C:\Repos\edrm\API
dotnet publish -c Release -r win-x64 --self-contained -o C:\Repos\ensuredr-setup\EDRM
Remove-Item -Path C:\Repos\ensuredr-setup\EDRM.zip -Force

[System.IO.Compression.ZipFile]::CreateFromDirectory('C:\Repos\ensuredr-setup\EDRM', 'C:\Repos\ensuredr-setup\EDRM.zip',[System.IO.Compression.CompressionLevel]::Optimal,$true)

cd C:\Repos\ensuredr-runner
dotnet publish EDR.Runner.csproj -c Release -r win-x64 --self-contained -o C:\Repos\ensuredr-setup\EDRRunner
Remove-Item -Path C:\Repos\ensuredr-setup\EDRRunner.zip -Force
[System.IO.Compression.ZipFile]::CreateFromDirectory('C:\Repos\ensuredr-setup\EDRRunner', 'C:\Repos\ensuredr-setup\EDRRunner.zip',[System.IO.Compression.CompressionLevel]::Optimal,$true)

cd 'C:\Program Files (x86)\Inno Setup 6'
.\ISCC.exe C:\Repos\ensuredr-setup\Setup\EDR-Setup.iss

