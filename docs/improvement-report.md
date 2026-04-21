# Improvement Report — EventApi

## Cilat 3 përmirësime i realizova

1. **Dependency Injection (DI) në `Program.cs` + constructor injection në `EventController`**
2. **Validim më i fortë për `UpdateEvent` në `EventService`**
3. **Dokumentim: `README.md` + përditësim i `docs/architecture.md`**

---

## Përmirësimi 1 — Dependency Injection (strukturë)

### Çka ishte më parë problem
`EventController` krijonte manualisht:

- `new FileRepository()`
- `new EventService(repo)`

Kjo e bën controller-in të lidhur fort me implementimin konkret dhe e vështirëson testimin me “fakes”.

### Çfarë ndryshova
- Regjistrova shërbimet në `Program.cs`:

```csharp
builder.Services.AddSingleton<IRepository<Event>, FileRepository>();
builder.Services.AddSingleton<EventService>();
```

- `EventController` tani merr `EventService` përmes konstruktorit.

### Pse versioni i ri është më i mirë
- **Më pak coupling** midis UI dhe implementimit të repository.
- **Më i lehtë për testim** (mund të injektohet një repo alternative në të ardhmen).
- **Më i pastër** sipas praktikave të ASP.NET Core.

---

## Përmirësimi 2 — Validim më i fortë për `Update` (reliability)

### Çka ishte më parë problem
`UpdateEvent` nuk validonte në të njëjtën mënyrë si `CreateEvent`, prandaj mund të futen të dhëna të pavlefshme në CSV (p.sh. `Id` invalid, `Title` bosh, `CategoryId` invalid).

### Çfarë ndryshova
Në `EventService.UpdateEvent` shtova validime për:

- `Id > 0`
- `Title` jo bosh
- `Price > 0`
- `CategoryId > 0`
- `OrganizerId > 0`

Gjithashtu, `IRepository<Event>` tani përfshin `Update` dhe `Delete`, që të mos ketë nevojë për `cast` në `FileRepository`.

### Pse versioni i ri është më i mirë
- **Më pak korruptim të të dhënave** në file.
- **Mesazhe më të qarta** për klientin (400) kur input-i është i gabuar.
- **Më pak surpriza runtime** nga të dhëna të papritura.

---

## Përmirësimi 3 — Dokumentim (shpjegueshmëri)

### Çka ishte më parë problem
`README.md` ishte bosh, ndërsa dokumentimi nuk lidhej qartë me flow aktual (sidomos DI dhe endpoint-et).

### Çfarë ndryshova
- Plotësova `README.md` me udhëzime `build/run/test` dhe lista endpoint-esh kryesore.
- Përditësova `docs/architecture.md` me seksion për **DI** dhe flow aktual.

### Pse versioni i ri është më i mirë
- **Onboarding më i shpejtë** për dikë që hap repo për herë të parë.
- **Më pak pyetje** gjatë vlerësimit/dorëzimit.

---

## Testet

Shtova një test shtesë për validimin e `Update` me `Id` invalid:

- `UpdateEvent_InvalidId_ReturnsError`

Tani projekti i testeve ka **4 teste** që kalojnë (`dotnet test`).

---

## Çka mbetet ende e dobët në projekt

- **Concurrency** në shkrimin e CSV (dy request njëkohësisht).  
- **Nuk ka auth** (API është publike).  
- **Test coverage** ende nuk është i plotë për të gjitha endpoint-et (sidomos integration tests).  
- **Model binding errors** (JSON invalid) ende nuk janë trajtuar me një middleware të dedikuar (mund të kthejnë 400 default nga framework).
