# EventApi

Web API për menaxhimin e **eventeve** me ruajtje në **`events.csv`**.

## Kërkesat

- .NET SDK **8.x** (rekomanduar)

## Si ta nisësh

Nga root i repo-s:

```bash
dotnet restore
dotnet build
dotnet run
```

Swagger UI zakonisht hapet automatikisht në development (sipas `launchSettings.json`).

## Testet

```bash
dotnet test EventApi.Tests/EventApi.Tests.csproj
```

## Endpoint-et kryesore (shembuj)

- `GET /api/event` — lista e eventeve
- `GET /api/event/{id}` — event sipas ID
- `POST /api/event` — krijim
- `PUT /api/event` — përditësim
- `DELETE /api/event/{id}` — fshirje
- `GET /api/event/stats` — statistika mbi çmimet
- `GET /api/event/sort?by=price&dir=asc` — sortim

## Dokumentacion

- `docs/architecture.md` — arkitektura dhe flow
- `docs/project-audit.md` — audit i projektit
- `docs/improvement-report.md` — raporti i përmirësimeve