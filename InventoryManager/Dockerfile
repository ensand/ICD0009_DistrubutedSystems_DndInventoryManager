FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .

COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/

COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL/DAL.App.DTO/*.csproj ./DAL/DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/

COPY Domain.App/*.csproj ./Domain.App/
COPY Extensions/*.csproj ./Extensions/
COPY PublicApi.DTO.V1/*.csproj ./PublicApi.DTO.V1/
COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore


# copy everything else and build app
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/

COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL/DAL.App.DTO/. ./DAL/DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY DAL.Base.EF/. ./DAL.Base.EF/

COPY Domain.App/. ./Domain.App/
COPY Extensions/. ./Extensions/
COPY PublicApi.DTO.V1/. ./PublicApi.DTO.V1/
COPY WebApp/. ./WebApp/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:SqlServerConnection="Server=alpha.akaver.com,1533;User Id=student;Password=Student.Bad.password.0;Database=ensand_dist_courseProj;MultipleActiveResultSets=true"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]