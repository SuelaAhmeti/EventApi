

## 1. Përmbledhje e Sprintit
Ky sprint fokusohet në implementimin e plotë të funksionalitetit CRUD për menaxhimin e eventeve në aplikacionin Event App. Qëllimi kryesor është që sistemi të bëhet funksional nga aspekti i backend-it duke mundësuar ruajtjen, modifikimin dhe fshirjen e të dhënave në mënyrë të qëndrueshme duke përdorur file CSV.

---

## 2. Gjendja Aktuale e Projektit

### 2.1 Çka funksionon
- Projekti është krijuar në ASP.NET Core dhe kompajlohet pa errore kritike
- Ekziston modeli **Event** me atributet kryesore (ID, Title, Date, Location, Price, etj.)
- Arkitektura bazë është e implementuar:
  - Controller Layer
  - Service Layer
  - Repository Layer
- Ekziston endpoint-i **GET** për marrjen e listës së eventeve

### 2.2 Çka nuk funksionon
- CRUD operacionet nuk janë të implementuara plotësisht
- Validimi i input-it është i munguar ose i paplotë
- Integrimi me file CSV nuk është i testuar plotësisht
- Disa endpoint-e mungojnë ose nuk kthejnë përgjigje korrekte

### 2.3 Statusi i ekzekutimit
- Aplikacioni kompajlohet dhe ekzekutohet
- Funksionaliteti është pjesërisht i përfunduar

---

## 3. Qëllimet e Sprintit (Sprint Goals)
- Implementimi i plotë i CRUD operacioneve për Event
- Shtimi i validimit të input-it
- Implementimi i error handling
- Integrimi korrekt me CSV
- Testimi përmes Swagger

---

## 4. Funksionalitetet Kryesore (Features)

### 4.1 CRUD për Event

#### Get All Events
- `GET /api/events`
- Kthen listën e eventeve

#### Get Event by ID
- `GET /api/events/{id}`
- Kthen një event specifik

#### Create Event
- `POST /api/events`
- Shton një event të ri

#### Update Event
- `PUT /api/events/{id}`
- Përditëson një event ekzistues

#### Delete Event
- `DELETE /api/events/{id}`
- Fshin një event

### 4.2 Ruajtja e të Dhënave
- Përdorim i file CSV
- Repository menaxhon:
  - Lexim
  - Shkrim
  - Mapping në objekte

---

## 5. Validimi dhe Error Handling

### Validimi
- Title: nuk duhet të jetë bosh
- Price: > 0
- Date: format valid

### Error Handling
- 404 Not Found → kur nuk ekziston event
- 400 Bad Request → input i gabuar
- 500 Internal Server Error → gabime serveri

### CSV Handling
- Krijohet file nëse nuk ekziston
- `try-catch` për lexim/shkrim

---

## 6. Testimi

### Metodat
- `GetAllEvents()`
- `GetById(id)`
- `CreateEvent(event)`
- `UpdateEvent(event)`
- `DeleteEvent(id)`

### Edge Cases
- ID jo ekzistuese
- Title bosh
- Price ≤ 0
- Lista bosh
- Input invalid

### Swagger
- Testim manual
- Kontroll i responses
- Kontroll i JSON

---

## 7. Deliverables
- CRUD i implementuar
- API funksionale
- CSV storage funksional
- Swagger dokumentim

---

## 8. Rreziqet (Risks)
- Probleme me CSV
- Parsing errors
- ID conflicts
- Mungesë kohe për testim

### Mitigimi
- Testim i hershëm
- Debugging i vazhdueshëm
- Ndërtim gradual

---

## 9. Metrics (Matja e Suksesit)
- Endpoint-et funksionojnë
- Nuk ka errore runtime
- CRUD punon korrekt
- Testet kalojnë

---

## 10. Task Breakdown

| Task | Përshkrimi | Prioriteti |
|------|-----------|-----------|
| GetById | Marrja sipas ID | High |
| Create | Shtimi | High |
| Update | Përditësimi | High |
| Delete | Fshirja | High |
| Validation | Kontroll input | Medium |
| CSV Integration | File handling | High |
| Testing | Testim Swagger | High |

---

## 11. Përfundim
Ky sprint është thelbësor për funksionalitetin bazë të aplikacionit. Pas përfundimit, sistemi do të jetë plotësisht funksional për menaxhimin e eventeve dhe i gatshëm për zgjerime të ardhshme.
