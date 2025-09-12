EventX Full API - Eventos, Usuarios, Inscricoes (demo)

Steps:
1. unzip and open folder EventX_FullApi
2. dotnet restore
3. (optional) dotnet tool install --global dotnet-ef
4. dotnet ef migrations add InitialCreate
5. dotnet ef database update
6. dotnet run
7. Open http://localhost:5000/index.html

Seeded users:
- admin@eventx.com / 123456 (admin)
- joao@eventx.com / 123456 (user)
- maria@eventx.com / 123456 (user)
