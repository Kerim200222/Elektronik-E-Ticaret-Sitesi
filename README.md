# E-Ticaret (ASP.NET MVC Projesi)

Bu proje ASP.NET MVC teknolojisi kullanılarak geliştirilmiş basit bir e-ticaret web uygulamasıdır. Kullanıcılar ürünleri görüntüleyebilir, sepete ekleyebilir ve sipariş verebilir. Yönetici paneli üzerinden ise ürün ve kategori yönetimi yapılabilir.

## 🚀 Özellikler

- Kullanıcı kaydı ve girişi
- Ürün listeleme ve detay sayfası
- Sepete ekleme ve sipariş verme
- Yönetici paneli üzerinden:
  - Ürün/Kategori CRUD işlemleri
  - Satış raporları görüntüleme

## 🧱 Proje Katmanları

- `EntityLayer22` → Veri modelleri
- `DataAccessLayer` → EF Core ile veri erişim işlemleri
- `BusinessLayer` → İş mantığı
- `E-Ticaret` → UI (MVC Controllers + Views)

## 🛠️ Kurulum

1. Visual Studio ile `E-Ticaret.sln` dosyasını açın
2. `appsettings.json` veya `Web.config` dosyasında veritabanı bağlantı ayarlarını yapın
3. Migration ve DB oluşturmak için:
   ```bash
   Update-Database
