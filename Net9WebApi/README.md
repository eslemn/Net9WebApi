# .NET 9 Web API - E-Ticaret Backend Projesi

Bu proje, **.NET 9**, **Entity Framework Core** ve **PostgreSQL** kullanılarak geliştirilmiş, katmanlı mimariye sahip sağlam bir Web API çözümüdür. Kapsamlı CRUD işlemleri, güvenli kimlik doğrulama ve modern yazılım mimarisi desenleri ile E-Ticaret sitemülasyonu sunar.

## 🚀 Özellikler ve Öne Çıkanlar

### 🌟 Bonus Geliştirmeler (Hedef: 120/120 Puan)
- **JWT Kimlik Doğrulama**: `Bearer` token kullanarak güvenli ve stateless (durumsuz) oturum yönetimi.
- **Rol Tabanlı Erişim (RBAC)**: `Admin` ve `User` rolleri ile yetkilendirme kontrolü.
- **Soft Delete (Yumuşak Silme)**: Veriler veritabanından tamamen silinmez; `IsDeleted` bayrağı ve Global Query Filter ile yönetilir.
- **Otomatik Veri Ekleme (Seeding)**: Uygulama ayağa kalkarken Admin kullanıcısı ve örnek veriler otomatik oluşturulur.
- **Otomatik Migration**: Kod-First yaklaşımı ile migration'lar çalışma anında otomatik uygulanır.

### 🏗 Mimari
Proje, temiz ve yönetilebilir bir **Katmanlı Mimari** (Layered Architecture) izler:
- **Presentation Layer (Sunum)**: 
  - **Controllers**: Standart RESTful endpoint'ler.
  - **Minimal API**: Hızlı ve hafif endpoint'ler (`/minimal/...`).
- **Service Layer (Servis)**: İş mantığının kapsüllendiği katman (`AuthService`, `ProductService` vb.).
- **Data Access Layer (Veri Erişim)**: EF Core `DbContext` ve Repository desenleri.
- **Core/Domain Layer**: Varlıklar (`User`, `Product`, `Review`, `Category`) ve DTO nesneleri.

### 🛠 Teknoloji Yığını
- **Framework**: .NET 9.0
- **Dil**: C# 13
- **Veritabanı**: PostgreSQL (Npgsql)
- **ORM**: Entity Framework Core 9
- **Dokümantasyon**: Swagger / OpenAPI
- **Güvenlik**: JWT Bearer Authentication
- **Diğer**: BCrypt (Şifreleme), Global Exception Handler (Hata Yönetimi)

---

## ⚙️ Kurulum ve Çalıştırma

### 1. Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### 2. Yapılandırma
`appsettings.json` dosyasını açın ve veritabanı bağlantı cümlenizi güncelleyin:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=net9webapi_db;Username=postgres;Password=SIFRENIZ"
}
```

### 3. Projeyi Çalıştırma
Uygulama, veritabanını oluşturma ve veri ekleme işlemlerini otomatik yapar.
```bash
dotnet restore
dotnet run
```

---

## 📖 API Dokümantasyonu (Swagger)

Proje çalıştıktan sonra interaktif dokümantasyona şu adresten erişebilirsiniz:
👉 **URL**: `https://localhost:7251/swagger`

### 🔐 Kimlik Doğrulama (Login) Akışı
Ürün ekleme/silme gibi bazı işlemler korumalıdır (Protected).

1.  **Giriş Yap (Login)**: Otomatik oluşturulan Admin hesabını kullanın.
    - **Endpoint**: `POST /api/auth/login`
    - **Bilgiler**:
      ```json
      {
        "username": "admin",
        "password": "admin123"
      }
      ```
2.  **Token Al**: Yanıttan gelen `token` değerini kopyalayın.
3.  **Yetkilendir (Authorize)**:
    - Swagger'da sağ üstteki **Authorize** butonuna tıklayın.
    - Kutucuğa: `Bearer <KOPYALADIGINIZ_TOKEN>` yazın.
    - **Authorize** ve ardından **Close** butonuna basın.
4.  **Erişim**: Artık kilitli endpoint'leri çalıştırabilirsiniz.

---

## 📡 API Endpoint Özeti

| Metot | Endpoint | Açıklama | Yetki |
| :--- | :--- | :--- | :--- |
| **AUTH** | | | |
| POST | `/api/auth/login` | Giriş yap & Token al | 🔓 |
| **PRODUCTS** | | | |
| GET | `/api/product` | Tüm ürünleri listele | 🔓 |
| GET | `/api/product/{id}` | Ürün detayını getir | 🔓 |
| POST | `/api/product` | Yeni ürün ekle | 🔒 |
| PUT | `/api/product/{id}` | Ürünü güncelle | 🔒 |
| DELETE | `/api/product/{id}` | Ürünü sil (Soft Delete) | 🔒 |
| **CATEGORIES** | | | |
| GET / POST | `/api/category` | Kategori işlemleri | 🔓 |
| **USERS** | | | |
| GET / POST | `/api/user` | Kullanıcı işlemleri | 🔓 |
| **REVIEWS** | | | |
| GET / POST | `/api/review` | Yorum işlemleri | 🔓 |

*Not: Minimal API endpoint'leri `/minimal/*` altında ayrıca mevcuttur.*

---

## 🧪 Test Kontrol Listesi
- **200 vs 201**: `POST` isteklerinin başarılı olduğunda `201 Created` döndüğü doğrulanmıştır.
- **Kilit İkonu**: Swagger'da korumalı endpoint'lerin yanında kilit ikonu görünmektedir.
- **Soft Delete**: Bir kayıt silindiğinde (`DELETE`) veritabanında `IsDeleted=true` olarak işaretlenir, tamamen silinmez ama listelerde görünmez.

---
**Öğrenci**: [Adınız Soyadınız]
**Ders**: [Ders Adı]
