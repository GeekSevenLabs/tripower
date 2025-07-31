# TriPower


# EF Commands

### Identity

- Add Migration
   - `dotnet ef migrations add Initial -s .\presentation\web\server\TriPower.Presentation.Web.csproj -p .\services\identity\infrastructure\TriPower.Identity.Infrastructure.csproj -c TriIdentityDbContext`
- Update Database
   -  `dotnet ef database update -s .\presentation\web\server\TriPower.Presentation.Web.csproj -p .\services\identity\infrastructure\TriPower.Identity.Infrastructure.csproj -c TriIdentityDbContext`

### Electrical

- Add Migration
   - `dotnet ef migrations add Initial -s .\presentation\web\server\TriPower.Presentation.Web.csproj -p .\services\electrical\infrastructure\TriPower.Electrical.Infrastructure.csproj -c TriElectricalDbContext` 
- Update Database
   - `dotnet ef database update -s .\presentation\web\server\TriPower.Presentation.Web.csproj -p .\services\electrical\infrastructure\TriPower.Electrical.Infrastructure.csproj -c TriElectricalDbContext`