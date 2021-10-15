# Finmaks Mülakat Projesi
Projeyi Hazırlayan: Afra Canan Koç

# Kullanılan Araçlar
- Visual Studio Community 2019
- SQL Server 2019 Express
- SQL Server Management Studio (SSMS) 18.10

# Kullanılan Paketler
- System.Data.SqlClient 4.8.3

# Projeyi Çalıştırma Hazırlıkları
- MSSQL üzerinden "TCMB2" isimli bir database oluşturulmalıdır.
- Bu database içerisine "currencies2" isimli bir tablo oluşturulmalıdır. (Tablo detayları: DB_TABLE.png)
	- "id" sütunu primary key olarak atanmalı ve identity değerleri (YES, 1, 1) olarak seçilmelidir.
- appsettings.json içerisindeki "DBConnection" değeri düzenlenmelidir.

# Proje Özeti
- Proje TCMB web sitesinde yer alan güncel kurların takip edilmesi amacıyla tasarlanmıştır.
- Projenin anasayfası, yanlızca kullanıcıyı yönlendirmek üzere bulunmaktadır.
- Güncel Kurlar sayfası, veritabanına en son kaydedilen kurların gösterildiği sayfadır.
- Güncel Kurlar sayfasında yer alan butona basıldığında veritabanında yer alan kur bilgileri sıfırlanmakta, TCMB'nin sunduğu XML dosyası okunarak/işlenerek veritabanına kaydedilmektedir. Eğer bu işlem başarıyla gerçekleşirse kullanıcının sayfası yenilenmekte ve veritabanına en son kaydedilen kur bilgileri kullanıcıya gösterilmektedir.
- Butona basıldığında jQuery kullanarak dinamik olarak bir GET sorgusu atılmakta ve cevaba göre işlem yapılmaktadır.
-Verilerin düzgün görüntülenebilmesi, sıralanabilmesi için jQuery DataTable kütüphanesi basitçe kullanılmıştır.   