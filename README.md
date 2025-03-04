# 🚀 CRM-MicroServices

## 📌 Opis projekta
CRM-MicroServices je **skalabilni CRM sistem** razvijen u **.NET** tehnologiji koristeći **mikroservisnu arhitekturu**.  
Ovaj sistem omogućava upravljanje korisnicima, klijentima, proizvodima, prodajom, AI asistentima, obaveštenjima i plaćanjima.

🔹 **Glavne funkcionalnosti**:
- ✅ **Autentifikacija** (JWT tokeni, autorizacija korisnika)
- ✅ **Upravljanje klijentima i leadovima**
- ✅ **Upravljanje proizvodima i prodajom**
- ✅ **AI servisi** (ChatGPT, Gemini, DeepSeek) za analizu i odgovore
- ✅ **Notifikacije putem emaila**
- ✅ **Plaćanja integrisana sa Stripe API-jem**
- ✅ **Skalabilna arhitektura uz Docker i Kubernetes podršku**

---

## ⚙️ **Tehnologije**
🔧 Ovaj projekat koristi sledeće tehnologije:
- **C# .NET 7**
- **Entity Framework Core** (EF Core) – ORM za rad sa bazama podataka
- **SQL Server** – Relaciona baza podataka
- **Docker & Kubernetes** – Deployment i skaliranje mikroservisa
- **Swagger** – API dokumentacija
- **Stripe API** – Procesiranje plaćanja
- **Azure Storage** – Čuvanje slika proizvoda

---

## 🏗️ **Mikroservisi**
CRM-MicroServices je podelejen na **7 mikroservisa**, svaki sa specifičnom funkcionalnošću:

| Mikroservis                | Opis funkcionalnosti |
|----------------------------|----------------------|
| **CRM.AuthenticationService** | Upravljanje autentifikacijom korisnika (JWT) |
| **CRM.CustomerService** | Upravljanje klijentima i leadovima |
| **CRM.ProductService** | Kreiranje, uređivanje i brisanje proizvoda |
| **CRM.SalesService** | Kreiranje i upravljanje narudžbinama |
| **CRM.AIService** | AI asistenti (ChatGPT, Gemini, DeepSeek) |
| **CRM.NotificationService** | Slanje email notifikacija |
| **CRM.PaymentService** | Stripe integracija za plaćanja |

---

## 📥 **Instalacija i pokretanje**
Da biste pokrenuli projekat, pratite sledeće korake:

### **1️⃣ Klonirajte repozitorijum**
```sh
git clone https://github.com/05vukasin/CRM-MicroServices.git
cd CRM-MicroServices
