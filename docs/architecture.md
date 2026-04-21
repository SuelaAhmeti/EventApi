# Architecture (Web API)

## 📌 Overview

Projekti është ndërtuar duke përdorur Layered Architecture për të ndarë përgjegjësitë dhe për të përmirësuar mirëmbajtjen dhe shkallëzimin e sistemit.

---

## 🏗 Layers

### 1. Controllers
- Menaxhojnë HTTP kërkesat
- Komunikojnë me Service layer
- Kthejnë përgjigje për klientin

---

### 2. Services
- Përmbajnë logjikën e biznesit
- Kryejnë validimin e të dhënave
- Ndërmjetësojnë midis Controller dhe Repository

---

### 3. Data (Repository)
- Menaxhon qasjen në të dhëna
- Lexon dhe shkruan në CSV file
- Implementon Repository Pattern

---

### 4. Models
- Përfaqësojnë strukturat e të dhënave
- Përdoren në të gjitha shtresat

---

## 🔄 Data Flow

Client  
→ Controller  
→ Service  
→ Repository  
→ CSV File  

---

## 🧠 Design Decisions

- U përdor Repository Pattern për ndarjen e logjikës së të dhënave
- CSV file për ruajtje të thjeshtë dhe demonstrim
- Swagger për testim të API
- Layered Architecture për strukturë të pastër
- **Dependency Injection (DI)**: `FileRepository` dhe `EventService` regjistrohen në `Program.cs`, dhe injektohen në `EventController` përmes konstruktorit (më pak coupling dhe më i lehtë për testim)

---

## 🔌 Dependency Injection (DI)

Në `Program.cs`:

- `IRepository<Event>` → `FileRepository`
- `EventService` regjistrohet si singleton

Kjo e bën `Controller` të varet nga abstraksioni (`IRepository<Event>`) dhe jo nga `new FileRepository()` brenda klasës.

---

## 💡 Advantages

- Kod i organizuar dhe i lexueshëm
- Lehtë për mirëmbajtje
- Mund të zgjerohet me databazë reale
- Testim i lehtë përmes Swagger

---

## 🔮 Future Improvements

- Integrimi me databazë (SQL Server)
- Authentication / Authorization
- Frontend (React / Angular)