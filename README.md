# Görev Dağıtım Uygulaması

Bu projeyi Task Distribution Case Study kapsamında geliştirdim. Amaç, görevleri işlem türünün zorluk seviyesine göre uygun personele otomatik olarak atamak.

Uygulamada personel yönetimi, görev oluşturma ve görev listeleme ekranları bulunuyor.

## Kullanılan Teknolojiler

- C#
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Bootstrap

## Proje Yapısı

Projeyi dört katmana ayırdım:

- **Domain:** Personel, görev ve işlem türü modelleri bulunuyor.
- **Application:** Servisler, repository arayüzleri ve veri taşıyan sınıflar bulunuyor. Görev dağıtım kuralları burada uygulanıyor.
- **Infrastructure:** Veritabanı bağlantısı, repository sınıfları, migration dosyaları ve başlangıç verileri bulunuyor.
- **Web:** Controller, ViewModel ve ekranlar bulunuyor.

Controller'lar servisleri kullanıyor. Servisler de repository arayüzleri üzerinden verilere ulaşıyor.

## Yapılan İşlemler

- Personel ekleme, listeleme, düzenleme ve silme
- Personel listesinden aktif/pasif durumunu değiştirme
- Görev oluşturma ve görevleri listeleme
- İşlem türü ve zorluk seviyesine göre otomatik personel seçme
- Uygun personel bulunamadığında görevi atanmamış olarak kaydetme
- Üzerinde görev bulunan personelin silinmesini engelleme

## İşlem Türleri

Uygulama ilk açıldığında işlem türleri tablosu boşsa aşağıdaki kayıtlar eklenir:

| İşlem Türü | Zorluk |
|---|---|
| Metin güncelleme | 1 |
| Görünüm düzenleme | 2 |
| Form geliştirme | 3 |
| Rapor hazırlama | 4 |
| İş kuralı geliştirme | 5 |
| Servis entegrasyonu | 6 |
| Performans iyileştirme | 7 |
| Veri taşıma | 8 |

İşlem adlarını ve 1–8 zorluk ölçeğini örnek olarak belirledim.

Tabloda kayıt varsa başlangıç verileri tekrar eklenmez. Bu işlem, kısmen dolu bir tablodaki eksik kayıtları tamamlamaz.

## Görev Dağıtımı Nasıl Çalışıyor?

Görev oluşturulurken işlem türü seçiliyor. Sistem, seçilen işlem türünün zorluk seviyesini kullanarak personeli belirliyor.

Seçim sırası şöyle:

1. Yalnızca aktif personeller değerlendirilir.
2. Son atanan görevi ile yeni görev arasındaki zorluk farkı 1 olan personeller elenir.
3. Kalanlar arasından, yeni görevin zorluk seviyesinde en az görev almış personel seçilir.
4. Eşitlik varsa toplam görev zorluk puanı daha düşük olan seçilir.
5. Eşitlik devam ederse Id değeri küçük olan seçilir.

Toplam zorluk puanı, personelin geçmişte aldığı bütün görevlerin zorluklarının toplamıdır. Uygulamada görev tamamlanma durumu tutulmadığı için bu değer yalnızca devam eden işleri göstermez.

### Ardışık Zorluk Yorumu

Case içindeki ardışık zorluk şartını, aynı personele peş peşe verilen görevlerin zorluk farkının 1 olmaması şeklinde yorumladım.

Örneğin personelin son görevi zorluk 4 ise sonraki görev zorluk 3 veya 5 olamaz. Aynı zorlukta tekrar görev almasına bu kural engel olmaz.

Son görevi belirlerken oluşturulma zamanı yerine atanma zamanı kullanılır.

### Uygun Personel Bulunamazsa

Görev kaydedilir ancak personele atanmaz. Kullanıcıya açıklama gösterilir ve listede “Henüz atanmadı” yazar.

Personeller daha sonra aktif yapılsa bile bekleyen görevler otomatik olarak tekrar dağıtılmaz. Mevcut dağıtım yalnızca yeni görev oluşturulurken çalışır.

## Kurulum

### Gereksinimler

- .NET 8 SDK
- SQL Server
- .NET 8 destekleyen Visual Studio ve ASP.NET web geliştirme bileşenleri

### Çalıştırma Adımları

1. Projeyi bilgisayarınıza indirin.
2. `TaskDistribution.sln` dosyasını Visual Studio ile açın.
3. `TaskDistribution.Web` projesini başlangıç projesi olarak seçin.
4. `TaskDistribution.Web/appsettings.json` içindeki `DefaultConnection` bağlantısını kendi SQL Server sunucunuza göre düzenleyin.

Windows kimlik doğrulaması için örnek bağlantı:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SUNUCU_ADINIZ;Database=TaskDistributionDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Bu bağlantı yerel geliştirme içindir.

5. Visual Studio'da Package Manager Console üzerinden migration'ları uygulayın:

```powershell
Update-Database -Project TaskDistribution.Infrastructure -StartupProject TaskDistribution.Web
```

6. Uygulamayı çalıştırın. İşlem türleri tablosu boşsa 8 işlem türü otomatik eklenir.
7. Personeller ekranından 8 personel oluşturun ve aktif olduklarından emin olun.
8. Görev Oluştur ekranından yeni görev ekleyin. Atama sonucunu Görevler ekranında görebilirsiniz.

Personel kayıtları başlangıç verisi olarak eklenmiyor. Başka bir bilgisayarda çalıştırırken bu kayıtların ekran üzerinden oluşturulması gerekiyor.

## Denediğim Senaryolar

- Personel ekleme, listeleme, güncelleme ve silme
- İşlem türlerinin uygulama tekrar açıldığında çoğalmaması
- Görev oluşturulduktan sonra listede görünmesi
- Yeni görevin otomatik olarak personele atanması
- Tek aktif personelin son görevi zorluk 1 iken zorluk 2 görevin atanmaması
- Aynı durumda zorluk 3 görevin atanabilmesi
- Personeller tekrar aktif yapıldığında yeni görevlerin dağıtılması

Bu kontrolleri uygulama ekranları üzerinden manuel olarak yaptım.

## Mevcut Sınırlar

- Aynı anda gelen görev oluşturma istekleri için ek bir eşzamanlılık önlemi bulunmuyor. İstekler aynı görev geçmişini okuyarak seçim yapabilir.
- Atanmamış görevler için sonradan tekrar atama işlemi bulunmuyor.
- Ayrı kullanıcı girişi ve analist rolü ekranı bulunmuyor. Analistin zorluk belirlemesi, görev oluştururken işlem türünün seçilmesiyle temsil ediliyor.
- Personeller developer olarak kabul ediliyor. Personel sayısı kodda 8 ile sınırlandırılmıyor; case senaryosu 8 aktif personelle deneniyor.
- Tarihler UTC olarak tutuluyor ve görev listesinde UTC olarak gösteriliyor.
