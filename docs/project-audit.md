# Project Audit — EventApi

## 1. Përshkrimi i shkurtër i projektit

**Çka bën sistemi?**  
`EventApi` është një Web API në ASP.NET Core që menaxhon një listë **eventesh** dhe i ruan në një file **`events.csv`** (lexim/shkrim përmes Repository layer).

**Kush janë përdoruesit kryesorë?**  
- **Klientët e API-s** (p.sh. Postman/Swagger, një frontend në të ardhmen, ose një shërbim tjetër që konsumon JSON).  
- **Zhvilluesit** që testojnë endpoint-et përmes Swagger.

**Cili është funksionaliteti kryesor?**  
- **CRUD** për `Event` (GET/POST/PUT/DELETE)  
- **Statistika** mbi çmimet (`/api/event/stats`)  
- **Sortim** i listës (`/api/event/sort`)

---

## 2. Çka funksionon mirë? (minimum 3)

- **Arkitektura e ndarë në shtresa**: `Controllers` → `Services` → `Data` (Repository) → CSV.  
- **Swagger** është aktiv, që e bën testimin manual të shpejtë.  
- **CSV storage** është e thjeshtë për demonstrim dhe nuk kërkon DB setup.

---

## 3. Dobësitë e projektit (minimum 5)

1. **DI (Dependency Injection) mungonte**: `EventController` krijonte manualisht `FileRepository` dhe `EventService`, që e bën testimin dhe zgjerimin më të vështirë.  
2. **Validimi nuk ishte i njëtrajtshëm**: `Create` kishte validime, por `Update` nuk ishte po aq strikt (rrezik i të dhënave të pavlefshme).  
3. **Repository interface nuk ishte “i plotë”**: `Update/Delete` ishin të lidhura me cast në `FileRepository`, që thyen encapsulation.  
4. **Dokumentimi për zhvillues ishte i dobët**: `README.md` ishte bosh, ndërsa `architecture.md` ishte i përgjithshëm pa lidhje të qartë me flow aktual + DI.  
5. **Test coverage është ende i kufizuar**: ka teste për raste kryesore, por mungojnë teste për endpoint behavior (integration) dhe edge cases të tjera (p.sh. CSV i korruptuar).  
6. **Siguria bazike**: API është publik pa auth; për një sistem real kjo është dobësi.

---

## 4. 3 përmirësime që do t’i implementosh

### Përmirësimi A — Dependency Injection (strukturë)
- **Problemi**: instancim manual i repository/service në controller.  
- **Zgjidhja**: regjistrim në `Program.cs` me `AddSingleton<IRepository<Event>, FileRepository>()` + `AddSingleton<EventService>()` dhe constructor injection në `EventController`.  
- **Pse ka rëndësi**: kod më i testueshëm, më pak coupling, më i lehtë për zgjerim (p.sh. DB repository).

### Përmirësimi B — Validim më i fortë për `Update` (reliability)
- **Problemi**: `Update` mund të pranonte `Id` invalid ose fusha bosh pa një mesazh të qartë për klientin.  
- **Zgjidhja**: validime në `EventService.UpdateEvent` për `Id`, `Title`, `Price`, `CategoryId`, `OrganizerId`.  
- **Pse ka rëndësi**: më pak “të dhëna të pista” në CSV dhe përgjigje më të parashikueshme nga API.

### Përmirësimi C — Dokumentim (shpjegueshmëri)
- **Problemi**: `README` bosh; e vështirë për dikë të ri ta nisë projektin.  
- **Zgjidhja**: `README.md` me hapat `restore/build/run/test` + lista endpoint-esh; përditësim i `docs/architecture.md` me DI dhe flow aktual.  
- **Pse ka rëndësi**: onboarding më i shpejtë dhe komunikim më i mirë me vlerësuesit/kolegët.

---

## 5. Një pjesë që ende nuk e kupton plotësisht

Ende jam kurioz për **strategjinë ideale të “file concurrency”** kur dy kërkesa njëkohësisht shkruajnë në `events.csv` (locking/atomic writes). Për një demo lokale është OK, por për prod duhet model më i fortë (DB ose file locking).
