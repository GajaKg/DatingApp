IN API INSTALL
<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.4" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.4" />
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0-preview.3.efcore.9.0.0" />
</ItemGroup>

add reference to DATA project
<ItemGroup>
    <ProjectReference Include="..\stockmarket.Data\stockmarket.Data.csproj" />
</ItemGroup>

IN DATA INSTALL
<ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.4" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" Version="9.0.4" />
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0-preview.3.efcore.9.0.0" />
</ItemGroup>



START MIGRATIONS 

dotnet ef migrations add NameOfMigration \
 --project ../stockmarket.Data \
 --startup-project . \
 --output-dir Migrations

# apply it to the database
dotnet ef database update \
 --project ../stockmarket.Data \
 --startup-project .



# Start project
dotnet run --launch-profile https 







# JWT DATABSE
1. Double-check your migrations list
From the stockmarket.Data folder run:

dotnet ef migrations list \
  --project . \
  --startup-project ../stockmarket.Api \
  --context ApplicationDbContext

2. Scaffold a single “everything” migration
If your migrations are out of order or missing the Identity schema, delete the bad ones (via dotnet ef migrations remove) until you’re back to your very first (domain-only) migration. Then re-create one that covers both your IdentityDbContext and your domain entities:

cd stockmarket.Api

dotnet ef migrations add InitialSchema \
  --project ../stockmarket.Data \
  --startup-project . \
  --context ApplicationDbContext \
  --output-dir Migrations

3. Still in stockmarket.Api:

# Drop & re-create your MySQL database if you’re in dev and can wipe it:
# (Or manually drop it in your client, then recreate it.)
# Then run:

dotnet ef database update \
  --project ../stockmarket.Data \
  --startup-project . \
  --context ApplicationDbContext