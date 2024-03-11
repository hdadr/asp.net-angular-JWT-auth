Backend: ASP.NET Core 5 Web API. Entityframework core 5. Identity. Issuing and validating AccessToken and using RefreshToken.  
Frontend: Angular v11 served by nginx  
Database: Postgres v10.15  
Reverse Proxy: Nginx  

To run the application: (you will need docker)

1. Download repository.
2. Run ***docker-compose up -d database*** in the dotnetCore_angular_auth directory.
3. Go to the backend directory and run (EF Core version 5 needed) ***dotnet ef database update --connection "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=appdb;"*** it will create the database in the postgres service. 
4. Run ***docker-compose up*** in the dotnetCore_angular_auth directory.
5. Go to http://localhost:80 which will show the client app and you can register and login.

There are two initial users (username:password). user:password (with user role) and admin:password (with admin role).

Disclaimer: Do not use it in production.
