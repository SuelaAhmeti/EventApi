
---

# 📄 docs/implementation.md

```md
# Implementation (Web API)

## 📌 Overview

Ky projekt implementon një sistem të plotë CRUD për menaxhimin e eventeve duke përdorur ASP.NET Core Web API dhe arkitekturë të ndarë në shtresa.

---

## ✅ Features Implemented

- GET all events (lexim nga CSV)
- GET event by ID
- POST create event (me validim)
- PUT update event
- DELETE event
- CSV file storage

---

## 🔄 Flow i të dhënave

Rrjedha e ekzekutimit është:

Client (Swagger UI)  
→ Controller  
→ Service  
→ Repository  
→ CSV file  

---

## 🧠 Business Logic

Logjika e biznesit implementohet në Service layer:

- Kontrollohet që emri i eventit të mos jetë bosh
- Kontrollohet që çmimi të jetë > 0
- Validimi bëhet para ruajtjes së të dhënave

---

## 📁 Repository Layer

Repository përdor CSV file për ruajtje:

- `GetAll()` → lexon nga CSV
- `GetById()` → kërkon event sipas ID
- `Add()` → shton event të ri
- `Save()` → ruan në CSV

---

## 🧪 Testing

API është testuar përmes Swagger UI:

- GET → kthen listën e eventeve
- POST → shton event të ri
- PUT → përditëson event
- DELETE → fshin event

---

## 📸 Example Output

Shembull i rezultateve:

- Lista e eventeve shfaqet në Swagger
- Eventet shtohen dhe ruhen në CSV
- Ndryshimet reflektohen menjëherë

---

## 💡 Notes

- Nuk përdoret databazë reale (CSV për thjeshtësi)
- Arkitektura e ndarë e bën projektin të mirëmbajtshëm
- Mund të zgjerohet lehtësisht me database në të ardhmen