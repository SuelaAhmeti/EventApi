## Sprint 2 Report — Suela Ahmeti

## Çka Përfundova
- **Feature e re: Statistika**
  - `GET /api/event/stats`
  - Kthen: `Count`, `TotalPrice`, `AveragePrice`, `MinPrice`, `MaxPrice`
- **Feature e re: Sortim**
  - `GET /api/event/sort?by=price&dir=asc`
  - `by`: `title|date|price` (default `id`)
  - `dir`: `asc|desc` (default `asc`)
- **Error handling (pa crash)**
  - `FileRepository`: nëse mungon `events.csv` shfaq “File nuk u gjet, po krijoj file të ri...” dhe e krijon file-in; rreshtat e prishur në CSV i anashkalon pa crash.
  - `EventService` + `EventController`: mesazhe të qarta për validime, 404 kur s’gjendet ID.
- **Unit Tests**
  - `EventApi.Tests` me minimum 3 teste që kalojnë.

## Screenshot / Output (dëshmi)
- `dotnet test EventApi.Tests/EventApi.Tests.csproj -f net8.0` → **Passed: 3**

## Çka Mbeti
- Opsionale: me shtu “export raport” në file (p.sh. `report.txt`) si feature shtesë.

## Çka Mësova
- Si me implementu logjikë në `Service` dhe me bo `Repository` safe me `try-catch` + `TryParse`, dhe si me shkru unit tests me izolim të `events.csv`.

## Sprint 2 Report — EventApi

### Çka Përfundova
- **Feature e re: Statistika**
  - Endpoint: `GET /api/event/stats`
  - Kthen: `Count`, `TotalPrice`, `AveragePrice`, `MinPrice`, `MaxPrice`
- **Feature e re: Sortim**
  - Endpoint: `GET /api/event/sort?by=price&dir=asc`
  - `by`: `title|date|price` (default `id`)
  - `dir`: `asc|desc` (default `asc`)
- **Error Handling (pa crash)**
  - Në `FileRepository`: nëse mungon `events.csv` shfaq mesazh dhe e krijon automatikisht; rreshtat e prishur në CSV i “skip” pa e rrëzuar programin.
  - Në `EventService`: create/update/delete kthejnë `(Success, Message)` me validime dhe `Itemi nuk u gjet` kur s’ka ID.
  - Në `EventController`: kthen `400/404/200` me mesazhe të qarta (jo exceptions në ekran).
- **Unit Tests**
  - `EventApi.Tests` u rregullua dhe u shtuan 3 teste:
    - `CreateEvent_EmptyTitle_ReturnsError`
    - `GetPriceStats_EmptyList_ReturnsZeroCount`
    - `Sort_ByPriceAsc_ReturnsCorrectOrder`

### Dëshmi (output)
- `dotnet test EventApi.Tests/EventApi.Tests.csproj -f net10.0` → **Passed: 3**

### Çka Mbeti
- Opsionale: refactor për të shtyrë “search/filter/sort” edhe më thellë në Repository me metoda të dedikuara (tani funksionon përmes `GetAll()`).

### Çka Mësova
- Si të bëj **error handling të sigurt** me file IO dhe parsime (`TryParse`) dhe si të shkruaj **unit tests** me izolim të `events.csv` duke përdorur working directory të përkohshme.

