[**HttpClient**](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0 "HttpClient") ve [**HttpClientFactory**](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests "HttpClientFactory") kullan�m �rne�i;


Powershellden projenin oldu�u klas�re cd komutu ile girdim.
Daha sonra kulland���m apinin url adresine ping komutu istek g�nderdim

![](https://www.linkpicture.com/q/Ads�z_68.png)

ve bu �ekilde kulland���m apinin ip adresini ald�m
daha sonra netstat -ano komudunu �al��t�r�yorum.
netstat aktif tcp ba�lant� portlar�n� ve a�la ilgili istatistikleri g�r�nt�ler. Kaynak i�in [t�klay�n�z.](https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/netstat)

-----

Fakat bu http client kendisi bir socket olu�turup onun �zerinden ba�lan�yor. Bu a�amada da s�k kar��la��lan problem socketin t�kenmesi yeni requestlere cevap verememesi oluyor. 
Bunun ��z�m� i�in
Singleton hale getiriyorum. ard�ndan tekrar program� �al��t�rd���m zaman sadece bir ba�lant�m�n oldu�unu ve s�rekli ayn� kald���n� g�r�yorum.
Fakat dnsim de�i�ti�inde veya api ile ilgili bir de�i�iklik oldu�unda ba�lanmakta problem ya�ayabilirim Bu durumda s�rekli program� yeniden ba�latma gibi istenmeyen durumlar olabilir.

------------

HttpClient .net taraf�ndan desteklenir ve Bunun ��z�m� i�in IHttpClientFactory ad�nda bir interface tan�mlanm��t�r. 
Bu noktada bu interfaceyi kullanarak sorunumu halledebilirim. 
Controller�ma injekt ettikten sonra startup class�nda AddHttpClient diyerek client factorynin bir insantacesinin program�n ilk �al��t�r�lma an�nda olu�turulmas�n� sa�l�yorum.
Uygulamam� �al��t�rd���mda aradaki fark �u �ekilde g�r�n�yor.

![](https://www.linkpicture.com/q/Ads%C4%B1z2.png)


> Not:
TIME_WAIT 0 a��k ba�lant�n�n kalmad���n� ifade eder.
ESTABLISHED  ise request bitmesine ra�men kurulu ve a��k oldu�u ifade eder.