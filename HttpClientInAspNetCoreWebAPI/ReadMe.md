[**HttpClient**](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0 "HttpClient") ve [**HttpClientFactory**](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests "HttpClientFactory") kullaným örneði;


Powershellden projenin olduðu klasöre cd komutu ile girdim.
Daha sonra kullandýðým apinin url adresine ping komutu istek gönderdim

![](https://www.linkpicture.com/q/Adsýz_68.png)

ve bu þekilde kullandýðým apinin ip adresini aldým
daha sonra netstat -ano komudunu çalýþtýrýyorum.
netstat aktif tcp baðlantý portlarýný ve aðla ilgili istatistikleri görüntüler. Kaynak için [týklayýnýz.](https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/netstat)

-----

Fakat bu http client kendisi bir socket oluþturup onun üzerinden baðlanýyor. Bu aþamada da sýk karþýlaþýlan problem socketin tükenmesi yeni requestlere cevap verememesi oluyor. 
Bunun çözümü için
Singleton hale getiriyorum. ardýndan tekrar programý çalýþtýrdýðým zaman sadece bir baðlantýmýn olduðunu ve sürekli ayný kaldýðýný görüyorum.
Fakat dnsim deðiþtiðinde veya api ile ilgili bir deðiþiklik olduðunda baðlanmakta problem yaþayabilirim Bu durumda sürekli programý yeniden baþlatma gibi istenmeyen durumlar olabilir.

------------

HttpClient .net tarafýndan desteklenir ve Bunun çözümü için IHttpClientFactory adýnda bir interface tanýmlanmýþtýr. 
Bu noktada bu interfaceyi kullanarak sorunumu halledebilirim. 
Controllerýma injekt ettikten sonra startup classýnda AddHttpClient diyerek client factorynin bir insantacesinin programýn ilk çalýþtýrýlma anýnda oluþturulmasýný saðlýyorum.
Uygulamamý çalýþtýrdýðýmda aradaki fark þu þekilde görünüyor.

![](https://www.linkpicture.com/q/Ads%C4%B1z2.png)


> Not:
TIME_WAIT 0 açýk baðlantýnýn kalmadýðýný ifade eder.
ESTABLISHED  ise request bitmesine raðmen kurulu ve açýk olduðu ifade eder.