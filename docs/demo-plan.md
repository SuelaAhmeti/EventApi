# Demo Plan — EventApi

## 1. Titulli i projektit

**EventApi** — Web API për menaxhimin e eventeve.

## 2. Problemi që zgjidh

Projekti zgjidh problemin e menaxhimit të thjeshtë të eventeve përmes një API-je. Në vend që eventet të ruhen dhe ndryshohen manualisht në një file, sistemi ofron endpoint-e për listim, kërkim sipas ID-së, krijim, përditësim, fshirje, sortim dhe statistika mbi çmimet.

Për demo, ruajtja bëhet në `events.csv`, që e bën projektin të lehtë për t'u hapur dhe testuar pa konfigurim databaze.

## 3. Përdoruesit kryesorë

- Zhvilluesit që duan të konsumojnë API-n nga një frontend ose sistem tjetër.
- Administratorët/organizatorët që në të ardhmen mund të menaxhojnë evente përmes një paneli.
- Testuesit ose vlerësuesit që mund të provojnë funksionalitetin përmes Swagger ose `EventApi.http`.

## 4. Flow-i që do ta demonstroj

Flow-i kryesor për demo do të jetë:

1. Hapja e projektit dhe Swagger UI.
2. `GET /api/event` për të treguar listën ekzistuese të eventeve.
3. `GET /api/event/sort?by=price&dir=asc` për të treguar sortimin sipas çmimit.
4. `GET /api/event/stats` për të treguar statistikat e çmimeve.
5. `POST /api/event` për të krijuar një event të ri.
6. `GET /api/event` përsëri për të verifikuar që eventi u ruajt në `events.csv`.
7. Një shembull validimi, p.sh. krijim me `Price <= 0`, për të treguar përgjigjen `BadRequest`.

E zgjodha këtë flow sepse tregon pjesët më të rëndësishme të projektit: API live, ruajtje reale në file, logjikë biznesi në service layer, validim dhe funksione shtesë si sortimi/statistikat.

## 5. Një problem real që e kam zgjidhur

Problemi ishte që controller-i dhe logjika e të dhënave ishin të lidhura shumë fort me njëra-tjetrën, sepse objekti `FileRepository` krijohej manualisht. Kjo e bënte kodin më pak fleksibil dhe më të vështirë për testim ose zgjerim.

Problemi ishte në strukturën e aplikacionit, sidomos në mënyrën si `EventController` lidhej me `EventService` dhe `FileRepository`.

Zgjidhja ishte përdorimi i Dependency Injection në `Program.cs`:

- `IRepository<Event>` regjistrohet me `FileRepository`.
- `EventService` regjistrohet si service.
- `EventController` e merr `EventService` përmes konstruktorit.

Kjo e bën projektin më të pastër, më të testueshëm dhe më të lehtë për t'u zgjeruar në të ardhmen, për shembull nëse `events.csv` zëvendësohet me databazë.

Një problem tjetër real ishte validimi jo i plotë gjatë përditësimit të eventeve. U shtuan kontrolle për `Id`, `Title`, `Price`, `CategoryId` dhe `OrganizerId`, që të mos ruhen të dhëna të pavlefshme.

## 6. Çka mbetet ende e dobët

Pjesa më e dobët është ruajtja në CSV. Për demo është praktike, sepse nuk kërkon setup shtesë, por për një aplikacion real ka kufizime:

- nuk ka mbrojtje të fortë për shumë kërkesa që shkruajnë njëkohësisht;
- nuk ka query të avancuara si në databazë;
- nuk ka authentication/authorization;
- testet janë ende kryesisht unit tests dhe mungojnë integration tests për endpoint-et.

Për përmirësim në të ardhmen, projekti duhet të kalojë në databazë reale dhe të shtojë autentikim për përdorues/admin.

## 7. Struktura e prezantimit (5-7 min)

**Hyrja (1 min)**  
Do të prezantoj shkurt problemin: nevoja për një API që menaxhon evente në mënyrë të strukturuar. Do të shpjegoj që projekti është ASP.NET Core Web API me ruajtje në `events.csv`.

**Demo live (2-3 min)**  
Do të hap Swagger dhe do të demonstroj flow-in kryesor: listim eventesh, sortim sipas çmimit, statistika, krijim eventi dhe verifikim në listë.

**Shpjegimi teknik (1 min)**  
Do të shpjegoj arkitekturën: `Controller -> Service -> Repository -> CSV`. Do të përmend Dependency Injection dhe pse e bën strukturën më të mirë.

**Problemi + zgjidhja (1 min)**  
Do të shpjegoj problemin real të coupling-ut dhe validimit të dobët, pastaj zgjidhjen me DI dhe validime në `EventService`.

**Mbyllja (30 sek)**  
Do të përmend çka funksionon tani live dhe çka mbetet për përmirësim: databazë reale, auth dhe më shumë teste.

## Demo readiness dhe plan B

Para prezantimit do të kontrolloj:

- `dotnet restore`
- `dotnet build`
- `dotnet test EventApi.Tests/EventApi.Tests.csproj`
- `dotnet run`
- Swagger UI dhe endpoint-et kryesore

Nëse Swagger nuk hapet ose demo live ka problem, plani B është:

- të përdor `EventApi.http` me request-et e përgatitura;
- të tregoj `README.md` për hapat e nisjes dhe endpoint-et;
- të tregoj `docs/architecture.md` për arkitekturën;
- të përdor screenshot-et në `docs/` si dëshmi të funksionalitetit;
- të tregoj `events.csv` për të dëshmuar ruajtjen reale të të dhënave.

Rendi i sigurt për demo:

1. Nis API-n me `dotnet run`.
2. Hap Swagger.
3. Bëj `GET /api/event`.
4. Bëj `GET /api/event/sort?by=price&dir=asc`.
5. Bëj `GET /api/event/stats`.
6. Bëj `POST /api/event` me një event valid.
7. Bëj `POST /api/event` me çmim invalid për të treguar validimin.
8. Mbyll me përmirësimet e ardhshme.
