Aby ogarnąć migracje, to trzeba pobrać ef-a

dotnet tool install --global dotnet-ef
i dodać go do Patha, ogólnie narzędzia z dotneta

export PATH=$PATH:/home/USER/.dotnet
export PATH=$PATH:/home/USER/.dotnet/tools

Oczywiście konsola nie widzi .NETa z Riddera, bo czemu miałaby widzieć, jak zawsze XD
Więc trzeba zainstalować

sudo apt-get update
sudo apt-get install -y dotnet-runtime-8.0

I tera możemy se robić migracje z konsoli, bo wpudowany w riddera nie działa XD

dotnet ef database update


