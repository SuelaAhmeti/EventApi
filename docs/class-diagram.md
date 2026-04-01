# UML Class Diagram – Event API

## 📦 Classes

### Event
- Id: int
- Title: string
- Price: double
- Date: DateTime
- CategoryId: int
- OrganizerId: int

---

### EventService
+ GetById(int id)
+ GetByCategory(int categoryId)
+ CreateEvent(Event ev)
+ UpdateEvent(Event ev)
+ DeleteEvent(int id)

---

### IRepository<T>
+ GetAll()
+ GetById(int id)
+ Add(T entity)
+ Save()

---

### FileRepository
+ GetAll()
+ GetById(int id)
+ Add(Event entity)
+ Save()

---

### EventController
+ GetAll()
+ GetById(int id)
+ Create(Event ev)
+ Update(Event ev)
+ Delete(int id)

---

## 🔗 Relationships

- EventController → EventService
- EventService → IRepository<Event>
- FileRepository → implements IRepository<Event>
- Repository → CSV file

---

## 🧠 Architecture Pattern

- Layered Architecture
- Repository Pattern
- Dependency Injection

---

## 🔄 Flow

Controller → Service → Repository → File