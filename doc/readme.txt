Celem aplikacji jest umo¿liwienie zamawiania posi³ków on-line. W za³o¿eniach, ma byæ na tyle elastyczna, aby da³o siê wdro¿yæ j¹ w ka¿dej restauracji, bez wzglêdu na specyfikê danej kuchni.
Podstawowym za³o¿eniem jest fakt, i¿ ka¿dy posi³ek sk³ada siê z pewnej zawartoœci bazowej oraz sk³adników dodatkowych. Administrator systemu, którym mo¿e byæ np. sam restaurator ma mo¿liwoœæ komponowania posi³ków gotowych do sprzeda¿y. Zdefiniowaæ on mo¿e cenê zawartoœci bazowej oraz dodaæ do niej sk³adniki w okreœlonych iloœciach po wczeœniejszym wprowadzeniu do systemu tych sk³adników, gdzie mo¿e wprowadziæ ich cenê za sztukê.

Przyk³ad: Pizza Xyz.
Jako bazê traktujemy ciasto w cenie x z³otych.
Dodajemy sk³adniki typu miêso i warzywa w okreœlonych iloœciach.

Administrator ma mo¿liwoœæ zdefiniowania posi³ku bez sk³adników tzn. sama baza jest uznawana jako posi³ek do zamówienia. Inaczej mówi¹c, posi³ek taki sam w sobie stanowi kompletn¹ ca³oœæ gotow¹ do zamówienia.

Klient wybiera posi³ki do zamówienia oraz ma mo¿liwoœæ dodania do nich sk³adników w po¿¹danej przez siebie iloœci. Jeœli wybierze on posi³ek nie zmieniaj¹c sk³adników co do wyst¹pienia oraz iloœci wzglêdem tego co jest wprowadzone przez administratora do bazy danych, wówczas do zamówienia dodawane jest id tego posi³ku. Gdy klient po wybraniu posi³ków zmieni w którymœ z nich zawartoœæ sk³adników, wówczas w bazie tworzony jest nowy posi³ek, a id tego nowego posi³ku jest dodawane do zamówienia. Nowy posi³ek w kolumnie originId ma wpisywane id zmodyfikowanego posi³ku.
Dla celów testowych wprowadzono w aplikacji u¿ytkownika administracyjnego o danych logowania:
login: admin@admin.pl
has³o: admin1AD!
