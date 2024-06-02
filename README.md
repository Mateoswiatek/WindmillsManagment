# DEMO
Demo prezentacyjne:
[link](https://youtu.be/cTLI487_VlI)


Zewnętrzne API z: [openweathermap](https://openweathermap.org/current#multi)

# Przemyślenia / TODO / Rozwój
Zamienić na CQRSa, ale dużo roboty przy IOC będzie i wstrzykiwaniu zależności.
W Javie byłoby raz dwa. W tedy, rzeczy do faktycznej modyfikacji encji miałby admin,


Pracownicy tylko by zgłaszali poprawki / modyfikacje, i one musiałby być zatwierdzone przez któregoś z adminów.


Niby spoko się robi, ale jednak Java jest przyjemniejsza, bardziej widać co sie dzieje, bardziej zrozumiała, Nie ma babrania sie we frontend i backend.
Bardziej przejrzyste.


Niemniej, C# jest spoko dla małych aplikacji / gdy trzeba zrobić i backend i frontend, jakis szybki crud bez skomplikowanych operacji i relaci.
Boo Relacje i zarządzanie bazą danych równiez jest problematyczne, każdą metode trzeba robić, definiować, zabezpieczać, nie ma jednego typowego flow.


Dodać do windparku oopcję, aby dodać wiatrak, iw tedy już automatycznie windpark ustawi się na tego konkretnego?

# Wymagania
Spełnione wymagania:
- Podstawowe operacje CRUDowe
- Podejście MVC,
- Stronnicowanie,
- Wstrzykiwanie zależności,
- Logowanie + Ukrywanie / Zabezpieczenie API
- Customowe projekcje (Pobieranie tylko tego co potrzebuje - np Guidy i Nazwy / ShortDtos itp)
- Nawigacja między widokami
- Wyświetlanie danych z encji na widoki
- Wyświetlanie "stalych danych" (nie z encji)
- Formularze
- Kilka featurów jest
- Jest Logowanie  = rejestracja, logowanie, wylogowywanie, zabezpieczenie dostępu, 


Dodatkowe rzeczy / funkcjonalności / dodatki
- Użycie ORMa (Co nie zawsze się zdarza, niektórzy niskopoziomowo zarządzają bazką, SQLki lecą)
- O ile byłot możliwe, wykonywanie Requestów zgodnie ze sztuką (a przynajmniej tak mi się wydaje)
- Stosunkowo przemyślana struktura programu
- Użycie AspNetCore.Identity do ogarnięcia userów (co również nie zawsze się zdarza, inni przykładowo z palca hashowali md5-tką)
- Wartości domyślne w Formularzach
- Dodanie Service,
- Rozbudowana nawigacja między widokami, wiele różnych opcji,
- Logiczny interface (co nie zawsze się zdarza)
- Połączenie z bazą Postgresql w Dockerze
- Stworzenie Migracji
- Bardziej skomplikowane wyświetlanie danych w widokach (Wybieranie odpowiednich propertisów)
- Wyświetlanie złożonych encji bazodanowych na widokach (wybór WindParków przy dodawaniu Wiatraka)
- Bardziej Optymalne zapytnia do bazy danych (pobieram tylko to co potrzebuję)
- Połączenie z zewnętrznym API (Pogoda)
- Konwertowanie JSONów odpowiedzi na Obiekty w Programie.

# SS z aplikacji
![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/4e3d673e-f359-4307-abce-fb92798c2c27)


Działa sobie ładnie poprawnie wszystko:


![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/ddee5933-65ad-48a4-8922-49898ce1b95e)

![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/bf9a9a57-ede5-4583-af59-9cbddc0af325)
![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/8f492b51-a30f-4443-836b-79dceb3340f0)


Dizała dodawanie WindParków oraz przekierowanie


![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/ac21e463-76f4-4b99-8795-8946fb94e06e)


Edycja


![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/99839a65-06f9-4ac2-819a-0b1560a5993c)
![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/27cbcda6-795d-4340-8f16-03bec48a8914)


Usuwanie (z Potwierdzeniem)


![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/326a38a8-ad2c-463c-bf53-db3a5d3cf5d2)
![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/1c518023-977b-4031-9c08-0be7c173109c)


Wyświetlanie wszystkich Windparków


![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/362a7c7d-059e-4027-bb2a-d5816510d915)


Stronicowanie


![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/91ee97f1-4229-4561-aa3e-255ef940ec0b)


I wiele wiele innych, wszystko jest na demo.

Bazka
![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/e48c64a3-ec9f-4b17-be5d-4f1c73663253)

![obraz](https://github.com/Mateoswiatek/WindmillsManagment/assets/115046087/dd0c730a-39df-4b19-824e-092ce5ad16aa)
