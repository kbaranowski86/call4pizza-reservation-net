Celem aplikacji jest umo�liwienie zamawiania posi�k�w on-line. W za�o�eniach, ma by� na tyle elastyczna, aby da�o si� wdro�y� j� w ka�dej restauracji, bez wzgl�du na specyfik� danej kuchni.
Podstawowym za�o�eniem jest fakt, i� ka�dy posi�ek sk�ada si� z pewnej zawarto�ci bazowej oraz sk�adnik�w dodatkowych. Administrator systemu, kt�rym mo�e by� np. sam restaurator ma mo�liwo�� komponowania posi�k�w gotowych do sprzeda�y. Zdefiniowa� on mo�e cen� zawarto�ci bazowej oraz doda� do niej sk�adniki w okre�lonych ilo�ciach po wcze�niejszym wprowadzeniu do systemu tych sk�adnik�w, gdzie mo�e wprowadzi� ich cen� za sztuk�.

Przyk�ad: Pizza Xyz.
Jako baz� traktujemy ciasto w cenie x z�otych.
Dodajemy sk�adniki typu mi�so i warzywa w okre�lonych ilo�ciach.

Administrator ma mo�liwo�� zdefiniowania posi�ku bez sk�adnik�w tzn. sama baza jest uznawana jako posi�ek do zam�wienia. Inaczej m�wi�c, posi�ek taki sam w sobie stanowi kompletn� ca�o�� gotow� do zam�wienia.

Klient wybiera posi�ki do zam�wienia oraz ma mo�liwo�� dodania do nich sk�adnik�w w po��danej przez siebie ilo�ci. Je�li wybierze on posi�ek nie zmieniaj�c sk�adnik�w co do wyst�pienia oraz ilo�ci wzgl�dem tego co jest wprowadzone przez administratora do bazy danych, w�wczas do zam�wienia dodawane jest id tego posi�ku. Gdy klient po wybraniu posi�k�w zmieni w kt�rym� z nich zawarto�� sk�adnik�w, w�wczas w bazie tworzony jest nowy posi�ek, a id tego nowego posi�ku jest dodawane do zam�wienia. Nowy posi�ek w kolumnie originId ma wpisywane id zmodyfikowanego posi�ku.
Dla cel�w testowych wprowadzono w aplikacji u�ytkownika administracyjnego o danych logowania:
login: admin@admin.pl
has�o: admin1AD!
