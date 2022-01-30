using System;
using System.Collections.Generic;
using System.Text;

namespace MN_Sciaga.QuestionsMocks
{
    public class QM_MN2022
    {
		public static string data = @"• kod binarny, kod U2, działania na liczbach w formacie binarnym
   + Kod binarny
- Kod 0,1
- Spełnia algebrę boole’a
- Zakres 0 do (2^n)-1
- Pierwszy bit może być bitem znaku
- Działania jak pisemnie
- Stratny (0 jest kodowane 2 razy)
   + Kod U2
- Dodatnie takie same
- Odejmowanie A-B = A + inv(B)
- inv(B) = ~B + 1 [negacja bitów i dodanie 1]
• format stałoprzecinkowy i zmiennoprzecinkowy (działania, różnice, formaty pojedynczej i podwojnej precyzji)
   + Format stałoprzecinkowy
- Działa jak zwyczajna liczba binarna, w tym z U2
- Stała dokładność
- Prostota i niskie wymagania sprzętowe
- Niska dokładność
- W działaniach liczba odpowiednich bitów musi być równa
- Mnożąć wynik dzielimy przez 2^n
   + Format zmiennoprzecinkowy
- Norma IEEE
- Większa dokładność
- Reprezentacja: x = S * M * B^E 
[ S = znak, M = mantysa, B = podstawa (2,10), E = wykładnik]
- Mantysa odpowiada za ułamkową część zapisu, zakres [1,2)
- Wykładnik zazwyczaj w kodzie U2
- Dodawanie to suma mantys pomnożonych przez B^E
- Mnożenie to iloczyn znaków * iloczyn mantys * B^(E1+E2)
- Dzielenie to iloczyn znaków * iloraz mantys * B^(E1-E2)
- Różne algorytmy dzielenia [Restoring/Non-restoring division, SRT, Newtona-Raphsona, Goldshmidta]
- W IEE Mantysę koduje się bez liczby przed przecinkiem, która w założeniu = 1
- IEE Single [float32/float]: E=**8b**, M=**23b**
- IEE Double [float64/double]: E=**11b**, M=**52b**
• rodzaje i źródła błędów numerycznych, zaokrąglanie i cyfry znaczące
   + Rodzaje błędów
- Błędy względne ( e = |y-y’| ) oraz bezwzględne ( e = |y-y’| / |y| )
   + Źródła błędów
- Formułowania (pomiaru, przyjęcie przybliżeń)
- Obliczeń (grube, metody / obcięcia – do uzyskania dokładnego wyniku potrzeba nieskończonośći obliczeń)
- Zaokrągleń
   + Zaokrąglanie
- Warunek zaokrąglenia: e = |y-y’| <= ½ * 10^(-d)
   + Cyfry znaczące
- k-ta cyfra dziesiętną liczby y’ to liczba znacząca, gdy |y-y’| <= ½ * 10^(-k) i |y’| >= 10^(-k)
• liczby maszynowe i epsilon maszynowy, błąd reprezentacji
   + Liczba maszynowa
- Liczba maszynowa, to liczbą którą możemy przedstawić w komputerze
- Zbiór liczb maszynowych to A
- Dokładność maszynowa (epsilon maszynowy) to min. Liczba, którą można dodać do 1, aby otrzymać liczbę większą od 1, zależy on od liczby bitów
   + Błąd reprezentacji
- Dla każdej liczby rzecz. x, istnieje e, taki że |e| < |em|, tak że fl(x) = x(1+e)
- Oznacza to, że błąd względny między liczbą, a jej reprezentacją zmiennoprzecinkową jest zawsze mniejszy od e[m]
• przenoszenie się błędów, błąd nieunikniony
   + Przenoszenie się błędów
- Wzór wzięty z różniczkowej analizy błędów
- Jeśli y = f(x1, x2, ..., xn), a x[i] są zaokrąglone przy pomocy e[x[i]], to błąd względny jest:
Sumą i=0->n z **x[i] / f(x) * df(x) / dx[i] * e[x[i]] **
   + Błąd nieunikniony
- Błąd składający się z **błędu wyliczania wartości** (przenoszenie błędów) i **błędu zaokrąglania** dy/y = e[y] + epsilon
• Złożoność obliczeniowa i „O” duże
   + Złożoność obliczeniowa
- Liczba zasobów potrzebnych do wyliczenia algorytmu
- Funkcja pewnych wielkości (np. liczby zmiennych / liczby ograniczeń)
- Różne rodzaje: 
	-- **Arytmetyczna**: Liczba operacji potrzebnych do rozwiązania problemu, 
	-- **Czasowa**: Czas potrzebny do rozwiązania problemu
	-- **Pamięciowa**: Ilośc pamięci potrzebnej do rozwiązania problemu
   + „O” duże:
- Dla parametru n, F(n) = O(G(n)), gdy dla stałej C i przy n dążącym do nieskończoności, prawdą jest: F(n) <= CG(n)
- Dla O(x), mówimy że błąd jest **rzędu** x
• Uwarunkowanie problemu, stała uwarunkowania
   + Uwarunkowanie problemu
- Problem jest **dobrze** uwarunkowany, jeśli **mała** zmiana x powoduje **małą** zmianę f(x)
- Źle uwarunkowany w odwrotnej sytuacji
   + Stała miara uwarunkowania
- Miarą uwarunkowania jest k (kappa), która (nieformalnie) określa max iloraz zaburzeń f(x) wywołanych przez min. zaburzenia x 
- Można wyliczyć tylko w niektórych problemach
• Dokładność, stabilność i stabilność wsteczna algorytmu
   + Dokładność algorytmu
- Jest dokładny, jeśli || f’(x) - f(x) || / || f(x) || = O(e[m])
- Czyli jeśli błąd względny jest równy O(em)
- Zagwarantowanie dokładności wg definicji jest bardzo trudne, szczególnie dla źle uwarunkowanych problemów
   + Stabilność algorytmu
- Stabilny algorytm daje **prawie dobrą** odpowiedź na **prawie dobre** pytanie
- Stabilny algorytm: || f’(x) – f(x’) || / || f(x’) || = O(em), dla x’ takich że || x – x’ || / || x || = O(em)
   + Stabilność wsteczna algorytmu
- Stabilny wstecznie algorytm daje **prawidłową** odpowiedź na **prawie dobre** pytanie
- Błąd względny pogarsza się proporcjonalnie do stałej uwarunkowania – O(K*e[m])
- Jeśli f’(x) = f(x’), dla takich x’ że || x – x’ || / || x || = O(e[m])
• Interpolacja, interpolacja wielomianowa i jej jednoznaczność
   + Twierdzenie Weierstrass’a: 
- Każdą funkcję ciągłą można przybliżyć jednostajnie z dowolną dokładnością wielomianami
   + Aproksymacja funkcji:
 - Znalezienie funkcji f’, która przybliża funkcję f
   + Interpolacja
- Znalezienie dla zestawu węzłów (x[i], y[i]) funkcji f(x[i]) = y[i]
- Interpolacja wielomianowa: znalezienie współczynników wielomianu p stopnia n, gdy mamy n+1 węzłów
   + Jednoznaczność interpolacji:
- Istnieje tylko jeden „najlepszy” w interpolacji wielomian stopnia n
• Wzór Lagrange’a (zasada działania)
   + Wzór Lagrange’a
- Klasyczny wzór na określenie wielomianu interpolacyjnego
- Najbardziej uniwersalny
- Najprostszy pojęciowo
- Wielomian jako suma prostszych wielomianów [suma i=0->n] ( y[i] * l[i](x) ), gdzie l[i](x) = 1 dla x=x[i], 0 dla x=x[k] gdzie k != i
- Każdy z wielomianów odpowiada za interpolację w 1 punkcie, a w pozostałych ma wartość 0
- Wysoka złożoność obliczeniowa wyliczenia wielomianu w punkcie x – O(n^2)
• Wzór barycentryczny (obie formy, zasada działania)
   + Wzór barycentryczny
- Modyfikacja interpolacji Lagrange’a
- Wielomian stopnia n+1, który zeruje się we wszystkich węzłach
- l(x) = ( x – x[0] )( x – x[1] ) ... ( x – x[n] )
- Pierwsza forma wzoru: 
   Wyciągnięcie l(x) przed nawias: L(x) = l(x) * suma [ j=0 -> n ] z ( y[j] * w[j] / (x-x[j]) )
- O(n)
- Konieczność wyliczenia l(x), które może być trudne
- Druga forma:
L(x) = suma [ j = 0 -> n ] z ( y[j] * w[j] / (x – x[j]) ) / suma [ j = 0 -> n ] z ( w[j] / (x – x[j]) )
- Mała złożoność obliczeniowa i brak konieczności liczenia l(x)
- Węzły interpolacji dla [-1,1]:  x[i] = -1 + 2i/n
- Wagi barycentryczne: w[j] = (-1)^(n-j) / (h^n * n!) * (n po j)
• Efekt Rungego,
   + Efekt Rungego
- Efekt przy węzłach równoodległych
- Pogorszenie się interpolacji wielomianowej na krańcach przedziału pomimo zwiększenia ilości węzłów
- Interpolacja zachowuje się dobrze, jeśli węzły zagęszczają się przy brzegach przedziału – np. węzły Legendre’a
• Węzły czebyszewa, Interpolacja czebyszewa, zbieżność interpolacji Czebyszewa dla różnych funkcji (rząd zbieżności)
   + Węzły Czebyszewa
- Części rzeczywiste punktów rozmieszczonych równomiernie na okręgu
   X[j] = Re(z[j]) = ½ * ( z[j] + z[j]^-1 ) = cos( j*Pi / n ), 0 <= j <= n
- Wartości zagęszczają się na brzegach przedziału przez wagi węzłów
   + Zbieżność interpolacji Czebyszewa
- Dla funkcji **różniczkowalnych** błąd maleje podczas zwiększania rzędu
• Szeregi wielomianów i wielomiany ortogonalne
   + Szeregi wielomianów
- Zbiór wielomianów stopnia n jest podprzestrzenią przestrzeni funkcyjnej V
- Baza wielomianów V jest bazą przestrzeni wielomianów
- Aproksymacja funkcji za pomocą skończonego szeregu wielomianów bazy jest rzutowaniem funkcji na podprzestrzeń wielomianów stopnia n
   + Wielomiany ortogonalne
- Znając iloczyn skalarny można sprawdzić ortogonalność wielomianów
- Jeśli funkcja wagowa f(x):
F(x) = 1 -> wielomiany Legendre’a
F(x) = 1 / sqrt(1-x^2) -> wielomiany Czebyszewa
• Wyliczanie szeregu Czebyszewa z wielomianu interpolacyjnego oraz dobór stopnia interpolacji
   + Wyliczanie szeregu Czebyszewa
- Każdy wielomian stopnia n można przedstawić jako sumę wielomianów Czebyszewa stopni 0 do n
- f(x) ~= suma [ i = 0 -> n ] z ( a[i] * T[i](x) ), gdzie T[i](x) to funkcje bazy
- Wyliczanie współczynników jest kłopotliwe
- Zbieżność jest taka sama jak w przypadku wielomianów (ale szybsza)
   + Dobór stopnia interpolacji
1) Wyliaczamy wartość funkcji N w węzłach Czebyszewa
2) Wyznaczamy szereg Czebyszewa
3) Sprawdzamy czy współczynniki maleją do poziomu zera maszynowego
a. Jeśli nie, to N = 2N i powrót do 1)
b. Jeśli tak, to znajdujemy największe n’, takie że Cn’ > em’ i N = n’
• Najlepsza aproksymacja wielomianowa
   + Najlepsza aproksymacja wielomianowa
- **Ekwioscylacja** - funckja f(x) wraz ze wzrostem argumentu oscyluje między wartościami +- || f(x) ||
1) Szukamy wielomianu, dla którego błąd E będzie ekwioscylował
2) Wyznaczamy wsp. Wielomianu i wartości E w przyjętym zestawie punktów, E niekoniecznie jest max. Błędu
3) Znajdujemy miejsca zerowe krzywej błędu, wyznaczamy maksima i minima pomiędzy miejscami zerowymi jako nowe punkty
4) Przestajemy, gdy wartości minimów i maksimów są sobie bliskie co do modułu
• Stała Lebesgue’a i jakość aproksymacji
   + Stała Lebesgue’a
- Określa jak duży może być wielomian interpolacyjny między punktami
- Jak aproksymacja zwiększa normę aproksymowanej funkcji
- Dla każdej aproksymacji wartości stałej dąży do nieskończonośći wraz ze wzrostem n 
- Przykłady:
	-- Węzły równoodległe: L[n] = O( 2^(n+1) )
	-- Interpolacja Czebyszewa: L[n] = O( 2/Pi * log(n) )
	-- Aproksymacja Czebyszewa: L[n] = O( 4/Pi^2 * log(n) )
	-- Ze względu na logarytmiczną naturę, dokładność nie rośnie zbyt długo
• Aproksymacja Caratheodory’ego–Fejer’a
   + Aproksymacja Caratheodory’ego–Fejer’a
- Jeśli mamy wielomian interpolacyjny wysokiego rzędu N dla f i chcemy znaleźć dobrą aproksymację rzędu n
- Za pomocą operacji macierzowych na współczynnikach szerego Czebyszewa i obcięcia szeregu Laurenta dostajemy bardzo dobre przybliżenie krzywej ekwioscylacji
- Taka aproksymacja różni się znacznie mniej od aproksymacji najlepszej niż wynosi jej błąd 
- O( (N-n)^3 )
- Nie wymaga iteracji jak algorytm Remeza
• Geometryczna interpretacja układu równań
   + Geometryczna interpretacja układu równań
- Narysowanie prostych tworzących układ równań i znalezienie ich przecięcia
- Mnożenie Ax to kombinacja liniowa kolumn macierzy A o współczynnikach wektora x
- Rozwiązując układ równań poszukujemy współczynników, które pozwolą nam zrekonstruować wektor b za pomocą kolumn macierzy A
• Normy wektorów i macierzy, normy indukowane, równoważność norm
   + Norma wektora
- Wszystkie normy w przestrzeniach skończenie wymiarowych są równoważne
- **Norma wektora** to funkcja, która przyporządkowuje wektorowi liczbę nieujemną
- Jest uogólnieniem długości wektora
- Aksjomaty normy:
	-- Norm = 0 -> wektor zerowy
	-- Skalowanie wektora skaluje normę ( || a*x || = |a| * ||x|| )
	-- Nierówność trójkąta ( || x + y || <= ||x|| + ||y|| )
   + Norma macierzy
- Indukowana (norma wskazująca, jak silnie macierz wpływa na wektor, np. Euklidesowa)
- Elementowa [ogólna] (norma definiowana w oparciu o parametry macierzy, np. Frobeniusa)
• Rozkład na wartości singularne
   + Rozkład na wartości singularne
- Rozkład zachodzi dla każdej macierzy
- Rząd macierzy to liczba niezerowych wartości singularnych
- Wyznacznik macierzy kwadratowej to iloczyn wartości singularnych
- Wartości singularne macierzy odwrotnej są odwrotnościami wart singularne
- **Nie** istnieją w ogólnym przypadku wzory analityczne na wartości singularne
- Rozkład:
	-- Zapis macierzy A m*n w postaci:
		A*Q = P*E 	-> 	A = P * E * Q’
	-- Gdzie P i Q są macierzami ortogonalnymi (Q^-1 = Q^T), P wymiaru m*m, Q wymiaru n*n, a E macierzą diagonalną m*n
	-- Rozkład niewrażliwy na błędy w przypadku macierzy prawie osobliwych
	-- Wartości singularne to nieujemne uporządkowane liczby rzeczywiste z macierzy E
• Rozkład LU (z przestawieniami i bez), uwarunkowanie
   + Rozkład LU
- Najpopularniejszy sposób przedstawiania układów równań liniowych
- A = LU, 	gdzie:
	-- L jest trójkątna dolna (z 1 na przekątnych)
	-- U jest trójkątna górna
- Rozkład LU konstruuje się przez eliminację Gaussa
- **Pivoting** to wybranie wiersza zaczynającego się od największego elementu i zamiany go z bieżącym
- LU z Pivotingiem jest **stabilna wstecznie** dla wszystkich praktycznych macierzy
- Niestabilność:
	-- Jeśli w miacierzy są 0 w niewłaściwych miejscach, to eliminacja jest niemożliwa
	-- Jeśli zamiast 0 są bardzo małe liczby to występuje duży potencjał na błędy numeryczne
- O( 2/3 * m^3 )
• Rozkłąd Choleskiego
   + Rozkład Choleskiego
- Każda macierz dodatnio określona ma jednoznacznie określony rozkład Choleskiego
- Specyficzny rozkład LU, w którym L = U^T
- O( 1/3 * m^3 ), czyli 2x szybciej niż Gauss
- Stabilny wstecznie
- Wszystkie problemy stabilności występujące przy LU go nie dotyczą, ze względu na dodatnio-określoność
• Rozkład QR
   + Rozkład QR
- Każda macierz ma rozkład QR
- Rozkład macierzy A na iloczyn A = Q*R, gdzie:
	-- Q jest ortogonalna ( Q*Q*T = I ),
	-- R jest trójkątna górna
- Rozkład poprzez faktoryzację, czyli liczenie prawej strony i rozwiązanie układu równań
- Istnieją 2 algorytmy: **Gramma-Schmidtta** i **triangulacja Householdera**
- Stabilny wstecznie
- Głównie do rozwiązywania problemu najmniejszych kwadratów, ale działa też dla prostokątnych
- Jeśli m = n, to O ( 4/3 * m^3 ), czyli 2x wolniejszy od LU
- Nie ma wymagań do specyficznych macierzy jak LU
• Ortogonalizacja Grama-Schmidta (ogólna zasada)
   + Ortogonalizacja Grama-Schmidta
- Możliwe jest przekształcenie metody z wykorzystaniem koncepcji rzutów ortogonalnych
- Bardziej stabilne numerycznie od rzutów na kolumny jest dokonywanie rzutów na komplementarne podprzestrzenie
- Wzory analityczne są równoważne, ale bardziej stabilne numerycznie
- Wersja zmodyfikowana generuje mniej błędów zaokrągleń
- O( 2 * m * n^2 )
- Można traktować jako sekwencję operacji mnożenia przez macierze trójkątne tak, aby uzyskać macierz z ortogonalnymi kolumnami
• Triangularyzacja Hausholdera (ogólna zasada)
   + Triangularyzacja Hausholdera
- Sekwencja mnożenia przez macierze unitarne w celu uzyskania macierzy trójkątnej
- O( 2*m*n^2 – 2/3 * n^3 )
• Problem najmniejszych kwadratów (metody rozwiązywania)
   + Problem najmniejszych kwadratów
- Układ równań z macierzą prostokątną jest zazwyczaj sprzeczny
- Można go rozwiązać przez minimalizację normy residuum
- Tzw **problem najmniejszych kwadratów**, bo minimalizuje sumę kwadratów

   + Interpretacja geometryczna
- Dla układu równań Ax = b
- Geometrycznie konstruujemy wektor b:
	-- Przedstawienie za pomocą kolumn macierzy A oraz residuum 
	-- Jeśli b nie jest liniowo zależne od kolumn A to **nie** jest to możliwe

   + Intepretacja optymalizacyjna
- Szukanie minimum ze względu na min ||r||^2
- Funkcja wielu zmiennych ma min. gdy jej wektor pochodnych cząstkowych (gradient) = 0 i jej macierz drugich pochodnych (Hesjan) jest dodatnio określona

   + Praktyczne wyznaczanie x
- Rozkład Choleskiego: 	A*Ax = A*b
- Rozkład QR:	A = Q*R  ->  Rx = Q * b
- SVD
• Macierze rzadkie
   + Macierze rzadkie
- Sparse matrix
- Macierze, w których większość elementów ma wartość zero
- Sposoby zapisu macierzy rzadkich:
	-- Dictionary of keys: 	słownik (wiersz kolumna) -> wartość	[dobry tylko do tworzenia macierzy]
	-- Lista list (LIL)	[np. kolumnami]
	-- Współrzędne i wartość (COO):	sortowane po wierszach, a następnie kolumnach
	-- Format Yale (Compressed Sparse Row): 	3 macierze jednowymiarowe
- Wykorzystuje się także np. symetrię macierzy (wystarczy połowa danych), lub struktury jak macierze Hassenberga czy Pasmowe
• Klasyczne algorytmy iteracyjne
   + Klasyczne algorytmy iteracyjne
- Metody wywodzące się z lat 50-tych, ale wciąż często używane
- Wynik dzięki iteracji
- Stosowane obecnie jako **preconditionery**, czyli zrobienie iteracji metodą klasyczną celem ulepszenia bardziej zaawansowanych metod
   + Układ równań dyskretnych
- G * x[k+1] = H * x[k] + g
- Przy odpowiednich założeniach ciąg x[n] zbieżny do granicy: (G – H)^-1 * g
   + Metoda Jacobiego
- Pozwala obliczyć układ **n** równań z **n** niewiadomymi Ax = b
- Należy zamienić kolejność równań tak, aby na głównej przekątnej były elementy różne od 0
- Wektor x0 jest początkowym przybliżeniem rozwiązania, zwykle przyjmuje się same 0
- Macierz A należy zdekomponować: 	A = L + D + U, 	gdzie:
	-- L:  macierz trójkątna dolna z 0 na przekątnej
	-- D: macierz diagonalna z elementami tylko na przekątnej
	-- U: macierz trójkątna górna z 0 na przekątnej
- Rozwiązanie iteracyjne:	D*x[k+1] = b – (L + U) * x[k]
   + Metoda Geussa-Seidla
- Prawie jak Jacobiego:		A = L’ + U,	gdzie
	-- L: macierz trójkątna dolna
	-- U: macierz trójkątna górna z 0 na przekątnej
- Warunki zbieżności:
	-- Diagonalna dominacja
	-- Dodatnia określoność macierzy
- Rozwiązanie iteracyjne:	L’ * x[k+1] = b – U * x[k]
   + Metoda sukcesywnej nadrelaksacji
- Tak jak w Jacobiego:	A = D + L + U
- Wprowadzamy współczynnik nadrelaksacji w ( z przediału [1,2] )
	( D + w*L) * x = w * b – [ w * U + (w-1) * D] * x
- Rozwiązanie iteracyjne:	(D + w*L) * x[k+1] = Lw * x[k] + c
- Nadrelaksacja ma na celu przyspieszenie zbieżności
• Algorytm GMRES, przestrzenie Kryłowa
   + GMRES
- Generalized Minimal RESiduals
- Przeformułowanie problemu najmniejszych kwadratów
- W n-tym kroku szukamy aproksymacji reozwiązania za pomocą wektora x[n], minimalizując normę residuum r[n] = b – A*x[n]
- Realizacja przy pomocy **Iteracji Arnoldiego** i w każdym kroku sprowadza się do rozwiązania układu równań o wymiarze (n + 1) * n

   + Podprzestrzeń Kryłowa
- Odgyrwają ważną rolę w konstrukcji metod iteracyjnych
- Rozwiązanie jako konstrukcja wektorów postaci b, A*b, A^2*b, A^3*b, ...
- Podprzestrzeń skonstruowana z takich wektorów to **przestrzeń Kryłowa**
• Metoda gradientu sprzężonego (zasada działania), zastosowania
   + Metoda gradientu sprzężonego
- Służy do rozwiązywania układów równań liniowych, w których macierz jest dodatnio określona i symetryczna
- Jest metodą iteracyjną
- Może być zastosowana do rozwiązywania układów o macierzach rzadkich, z którymi nie radzą sobie algorytmy bezpośrednie jak rozkład Choleskiego
- Metoda polega na dobieraniu kierunków przeszukiwania tak, aby zbliżać się do rozwiązania przy macierzy diagonalnej. Najlepiej jest to zrobić wykorzystując podprzestrzenie Kryłowa
• Metoda iteracji prostej w rozwiązywaniu równań nieliniowych (zasada zbieżności, przykład) i zasada odwzorowań zwężających
   + Metoda iteracji prostej
- Przeznaczona dla równań, dla których funkcja ma postać f(x) = g(x) – x
- Każdą funkcję można przekształcić do takiej postaci
- Według zasady zbieżności:
	-- Zapisujemy funkcję w odpowiedniej formie
	-- Podstawiamy punkt początkowy
	-- Wyliczamy wartość i podstawiamy do kolejnego kroku
	-- Powtarzamy aż do uzyskania żądanej dokładności
- Wartość bezwzględna pochodnej w punkcie wyliczonym analitycznie jako miesjce zerowe musi być mniejsza od 1 (warunek Lipschitza)
   + Zasada odwzorowań zwężających
- Szybkość zmierzania do rozwiązania można oszacować
- Twierdzenie Banacha o punkcie stałym
- ***Jeśli rozłożony plan miasta upuścimy na jedną z ulic w tym mieście, to dokładnie jeden punkt planu znalazł się dokładnie w miejscu, które przedstawia***
- Funkcja g spełnia warunek **Lipshitza**:
	|| g(x[1]) – g(x[2]) || <= L * || x[1] – x[2] || 	dla każdego x[1] i x[2] należących do X ze stałą L < 1
- Wtedy odwzorowanie x -> g(x) ma tylko jeden punkt stały w zbiorze X (równanie x=g(x) ma tylko jedno rozwiązanie)
- Rozwiązanie można wyznaczyć w następujący sposób:
	x * k + 1 = g(x*k), 	gdzie x0 należy do X
- Rozwiązaniem będzie granica ciągu rekurencyjnego, mającego zagwarantowaną zbieżność
- Równanie rekurencyjne zmierza do rozwiązania
• Metody bisekcji, siecznych, regula falsi, metoda Newtona
   + Metoda bisekcji
- Metoda równego podziału / metoda połowienia
	-- Szukamy miejsca zerowego poprzez zawężanie przedziału
	-- Opiera się to na twierdzeniu Darboux:
		--- Jeśli funkcja ciągła f(x) na końcach przedziału domkniętego ma wartości różnych znaków, to wewnątrz tego przedziału istnieje co najmniej jeden pierwiastek równania f(x) = 0
	-- Dzielimy przedział na połowę, jeśli środek przedziału nie jest szukanym miejscem zerowym, to dzielimy znowu ten podprzedział, którego wartości są na krańcach przeciwnych znaków

   + Metoda siecznych
- Funkcję ciągłą na dostatecznie małym odcinku można uznać za liniową
- Wyliczamy prostą przechodzącą przez 2 punkty o różnych znakach
- Rozwiązaniem jest punkt przecięcia z osią OX
- Do użycia metody nie jest potrzebna ani pochodna ani założenie różniczkowalności
   + Reguła falsi (fałszywa linia prosta, fałszywa pozycja)
- Połączenie metod siecznych i przedziałowych
- Ograniczenia funkcji:
	-- W przedziale [a,b] znajduje się dokładnie jeden, pojedynczy pierwiastek
	-- Na końcach przedziału funkcja ma różne znaki f(a) * f(b) < 0
	-- Pierwsza i druga pochodna istnieją i mają w tym przedziale stałe znaki
- Dzielimy przedział w taki sposób, aby na krańcach zawsze były przeciwne znaki, punkt przecięcia z osią OX to pierwsze przybliżenie pierwiastka
- Jeśli nie jest wystarczająco dobre, wybierany jest punkt, którego rzędna ma znak przeciwny do f(x[1]) i algorytm powtarza się
   + Metoda Newtona / stycznych
- Wykorzystuje pochodną, aby poszukiwać rozwiązania
- Ograniczenia jak w **regule falsi**
- Rząd zbieżności równy 2
- Zasada działania:
	-- Wybieramy punkt startowy x[1] (zazwyczaj wartość a, b, 0 lub 1)
	-- Wyprowadzamy styczną z f(x[1]), miejsce przecięcia z OX jest pierwszym przybliżeniem rozwiązania (x[2])
	-- Jeśli przybliżenie nie jest satysfakcjonujące, to obieramy x[2] jako punkt startowy i powtarzamy czynności
• Twierdzenie Abela
   + Twierdzenie Abela
- Dla każdego m >= 5 istnieje wielomian p(x) stopnia m o wymiernych współczynnikach, taki że pierwiastek rzeczywisty p(r) = 0, taki że nie może on zostać zapisany w formie wyrażenia zawierającego liczby wymierne, dodawanie, odejmowanie, mnożenie, dzielenie i pierwiastki różnych stopni
- Konsekwencja jest taka, że pierwiastki wielomianu można wyliczyć TYLKO iteracyjnie
• Wartości własne i ich związek z pierwiastkami, macierz Frobeniusa
   + Wartości własne
- Niech A będzie zespoloną macierzą kwadratową, niezerowy wektor zespolony w jest wektorem własnym A, a l należące do C jest odpowiadającą mu wartością własną, jeśli A*w = l*w
- Poszukiwanie pierwiastków wielomianu jest równoważne z poszukiwaniem wartości własnych macierzy
- Wielomian charakterystyczny macierzy A o wymiarach m*m to wielomian stopnia m o postaci:	p(x) – det(z*I – A)
- Wartości własne są pierwiastkami wieomianu charakterystycznego macierzy A
- Wartości własne macierzy trójkątnej leżą na jej przekątnej
- Ślad macierzy jest równy sumie wartości własnych
- Wyznacznik macierzy jest równy iloczynowi wartości własnych
- Dla każdej macierzy nieosobliwej P, wartości własne macierzy A i P*A*P^-1 są sobie równe
   + Macierz Frobeniusa
- Kwadratowa macierz m*m z samymi zerami, poza przekątną „nad główną” gdzie są 1-ki oraz ostatnim wierszem, w którym znajdują się współczynniki wielomianu
- Wielomian charakterystyczny takiej macierzy jest równy:
	P(z) = z^n + c[n-1] * z^(n-1) + ... + c[1] * z + c[0]
- Poszukiwanie pierwiastków wielomianu jest równoważne poszukiwaniu wartości własnych macierzy
• Dekompozycje macierzy i postać Schura
   + Dekompozycje macierzy
- Każdą macierz, która ma **rózne wartości własne**, można przedstawić w postaci:
	A = X * D * X^-1,	gdzie
	-- X: macierz wektorów własnych
	-- D: macierz diagonalna wartości własnych
- Każdą **macierz normalną** (A*A’ = A’*A) można przedstawić w postaci:
	A = Q * D * Q’,		gdzie
	-- Q: macierz unitarna
- Każdą **macierz kwadratową** można przedstawić w formie rozkładu Schura:
	A = Q * T * Q’,		gdzie
	-- Q: macierz unitarna
	-- T: macierz trójkątna górna
   + Rozkład Schura
- Podstawa wszystkich algorytmów wyliczania wartości własnych
- Konstruowany za pomocą macierzy unitarnych (z obu stron), tak aby iloczyn
	Q’[j] * ... * Q’[2] * Q’[1] * A * Q[1] * Q[2] * … * Q[j] 
   Zmierzał do macierzy trójkątnej T, gdy j dąży do nieskończoności
• Macierz Hessenberga i przekształcanie do niej
   + Macierz Hessenberga
- Wszystkie elementy poniżej pierwszej subdiagonali równe zero
- Dla macierzy symetrycznych macierz trójprzekątniowa
- Każda macierz jest ortogonalnie podobna do macierzy Hessenberga
	A = Q * H * Q’, 		gdzie
	-- H ma postać:
	[ h[1,1] h[1,2] h[1,3] ... h[1,m]
	 h[2,1] h[2,2] h[2,3] ... h[2,m]
	         h[3,2] h[3,3] ... h[3,m]
		       h[m] h[m,m] ]
- Transformację do macierzy Hessenberga można wyznaczyć nieiteracyjnie poprzez m przekształceń unitarnych (z lewej i prawej strony)
- Złożoność obliczeniowa dla dowolnej macierzy to O(10/3 * m^3)
- Złożoność obliczeniowa dla macierzy symetrycznych może być zmniejszona do O(4/3 * m^3)
- Algorytm transformacji, podobnie jak rozkład QR jest **stabilny wstecznie**
• Iteracja potęgowa, iteracja odwrotna i iloraz Rayleigha
   + Iteracja potęgowa
- Niech macierz A ma wektory własne q[1], q[2], ..., qm i m różnych wartości własnych,
Wtedy ciąg wektorów: v[0] / ||v[0]||, A*v[0] / ||A*v[0]||, A^2*v[0] / ||A^2*v[0]||, ...
Jest zbieżny do wektora własnego odpowiadającego największej co do modułu wartości własnej
- Jest tak, ponieważ wektor v[0] można przedstawić jako kombinację wektorów własnych
- Ma zbieżność liniową, z kroku na krok poprawa następuje proporcjonalnie do ilorazu największej wartości własnej i kolejnej
- Zbieżność **jest powolna**, gdy wartości własne są **bliskie sobie**
   + Iteracja odwrotna
- Dla każdej liczby rzeczywistej r, która **nie jest** wartością własną macierzy A, wektory własne macierzy (A – r*I)^-1 są takie same, jak wektory własne macierzy A, zaś odpowiadające im wartości własne to (lj – r)^-1, gdzie l[j] to wartości własne macierzy A
- Pozwala to na ominięcie podstawowego problemu iteracji potęgowej. Jesli r jest bliskie l[j], to (l[j] – r)^-1 będzie dużo większe od (l[i] – r)^-1 dla wszystkich i != j
- r nie może być wartością własną macierzy, bo to pierwiastki wielomianu charakterystycznego. Jeśli r byłoby wartością własną, to det(A – r*I) = 0 (więc odwrotność nie istnieje)

   + Iloraz Rayleigha
- Jest dokładną kwadratowo estymatą wartości własnej
- Pozwala na wyznaczenie wartości własnej, znając wektor własny, a iteracja odwrotna pozwala na wyznaczenie wektora własnego znając estymatę
	r(x) = x’ * A * x / (x’ * x)
- Jeśli x jest wektorem własnym, to iloraz ten jest równy odpowiadającej mu wartości własnej
	Jeśli ||x|| = 1 to r(x) = x^T*A*x
- Iteracja ilorazu Rayleigha polega na naprzemiennym stosowaniu obydwu algorytmów
- Jeden z najszybciej zbieżnych algorytmów numerycznych (zbieżny w sposób sześcienny)
• Algorytm QR wyznacznia wartości własnych (Zasada działania i rząd zbieżności)
   + Algorytm QR
- Algorytm Rayleigha, ale szukający w róznych kierunkach jednocześnie – najlepiej ortogonalnych
- Stosuje się rozkład QR macierzy A:
	A[0] = A
	Q[k]*R[k] = A[k-1]
	A[k] = R[k]*Q[k]
	-- Iteracyjnie rozkładamy macierz na rozkład QR, a następnie składamy w odwrotnej kolejności
- Algorytm zbieżny do postaci Schura, ale powoli
- Praktyczny algorytm QR:
	-- Rozpoczynanie od macierzy Hessenberga
	-- Wykorzystanie iteracji ilorazu Rayleigha dla przyspieszenia zbieżności
	-- Gdy pojawią się wartości bliskie zera pod przekątną macierz rozdzielamy na podproblemy
- Przesunięcia:
	-- W każdej iteracji używamy przybliżenia wartości własnej z ilorazu Rayleigha r[k]:
		Q[k]*R[k] = A[k-1] – r[k]*I
		A[k] = R[k]*Q[k] + r[k]*I
	-- To samo co w klasycznym QR, ale przesuwamy wartości własne macierzy przed faktoryzacją i z powrotem do złożenia macierzy
	-- Macierze Q zbiegają do macierzy wektorów własnych, zaś przekątna R do ilorazów Rayleigha. Dzięki temu jako element r[k] wybieramy element z przekątnej  
• Algorytm divide and conquer
   + Algorytm Divide And Conquer
- Wyznaczanie wektorów i wartości własnych
- 2x szybszy od QR
- Ogólna zasada polega na podzieleniu problemu na 2 mniejsze problemy – każdy z nich rozwiązywany jest rekurencyjnie, a następnie wartości własne są znajdywane przy użyciu mniejszych problemów
- Ogółem dzielimy dużą macierz na mniejsze
• Aproksymacja wielomianowa, szereg Czebyszewa, interpolacyjne wyznaczanie pierwiastków funkcji
   + Aproksymacja wielomianowa i szereg Czebyszewa
- Wielomiany Czebyszewa, to takie które spełaniają:
	Tn(cos(th)) = cos(n*th)
- Tworzą bazę ortogonalną dla iloczynu skalarnego:
	p * q = całka z [-1, 1] p(x)*q(x) / sqrt(q-x^2) dx
	-- Te wielomiany oznaczamy jako T[i](x)
- Można zdefiniować również rekurencyjnie:
	T[k+1](x) = 2*x*T[k](x) – T[k-1](x)
- Każdą funkcję całkowalną z kwadratem na przedziale [-1,1] można przedstawić w formie szeregu Czebyszewa:
	F(x) = suma [i=0 -> inf] (a[i]*T[i](x)), 	którego współczynniki są określone wzorem:
	-- a[i] = 2/Pi * całka [-1, 1] z f(x) * T[i](x) / sqrt(q-x^2) dx
- Dla i >= 0 wzór należy podzielić przez 2
- Od pewnego i, ciąg współczynników jest oszacowany z góry przez ciąg malejący
   + Interpolacyjne wyznaczenie pierwiastków funkcji
- Wyznaczyć stopień wielomianu interpolacyjnego
- Wyznaczyć współczynniki szeregu Czebyszewa
- Wyznaczyć pierwiastki wielomianu, które są wartościami własnymi macierzy koleżeńskiej
	-- C = A – 1/(2*C[n]) * B, 		gdzie
	-- A: macierz z 0 na przekątnej i ½ pod i nad nią
	-- B: Macierz zerowa, gdzie ostatni wiersz to c[0], c[1], c[2], ..., c[n-2], c[n-1]
- Wybrać wartości, które są liczbami z przedziału interpolacji
• Metoda Newtona rozwiązywania układów równań i warianty, metoda Broydena
   + Metoda Newtona
- Znalezienie takiego p, że r*(x*k + p) = 0, czyli J[k] * p = x[k]
- J[k] powinno być macierzą pochodnych cząstkowych (macierzą Jacobiego) w punkcie xk lub jej aproksymacją
- Jeśli blisko rozwiązania funkcja f jest różniczkowalna, a pochodna spełnia warunek Lipshitza to mamy **zbieżność kwadratową**
- Niedokładne metody Newtona:
	-- Klasa metod, która wyznacza kolejny krok rozwiązania p jako:
		|| r * k + J * k * p * k || <= u * k * || r * k ||, 		gdzie u[k] < u [0, 1]	to tzw ciąg wymuszający
   + Metoda Broydena
- Uogólnienie metody siecznych
- Niech sk = x[k+1] – xk, yk = r( x[k+1] ) – r( x[k] )
- Konstruujemy aproksymację macierzy Jacobiego B[k], która ma spełniać warunek siecznej:
   y[k] = B[k+1]*S[k]
- Najpopularniejsza aproksymacja to wzór Broydena:
	B[k+1] = B[k] + ( y[k] – B[k]*s[k] ) * s[Tk] / s[Tk]*s[k]
- Najmniejsza możliwa zmiana w aproksymacji przy spełnieniu warunku siecznej
- Różnica między wartościami residuum ma być równa iloczynowi wektora różnicy argumentów * jakaś macierz
• Funkcja celu i poszukiwanie kierunkowe
   + Funkcja celu
- Metody Newtonowskie i quasi-Newtonowskie są zbieżne jedynie lokalnie
- Aby uniknąć tego problemu zaproponowano aby korygować długość między iteracjami:
	X[k+1] = x[k] + e*p
- Długość kroku dobieramy tak, aby poprawić wartość funkcji celu
- Najpopularniejsza jest suma kwadratów residuów
- Pierwiastek równania jest minimum funkcji celu
   + Poszukiwanie kierunkowe
- Kolejne kroki rozwiązania zmniejszają wartość funkcji celu
- Nie jest konieczne poszukiwanie dokładnie minimum, wystarczy spełnienie warunków Wolfe’a:
	-- Nie robimy długich kroków, chyba że to uzasadnione
	-- Jeżeli funkcja po naszym kroku dalej silnie maleje, to trzeba zrobić dłuższy krok
• Metody homotopii (ogólna idea)
   + Metody homotopii
- Stopniowe przechodzenie od problemu prostszego do trudniejszego:
	H(x, l) = l*r(x) + ( 1 – l ) * ( x – a ), 	gdzie
	-- l [0, 1]: zaczynamy od prostego problemu dla l = 0 i stopniowo zmierzamy do rozwiązania
- W praktyce wyznaczmy wektor styczny do homotopi i w oparciu o niego konstruujemy równania różniczkowe albo algebraiczne, które trzymają nas dalej na ścieżce
• Całkowanie w czasie rzeczywistym, Metody Eulera, trapezów
   + Całkowanie w czasie rzeczywistym
- Podstawowe zastosowanie w automatyce – regulacja Pl i PID
- Kolejne wartości funkcji dostajemy w określonych odstępach czasu
- Nie możemy wybierać punktów
- Wartości są obarczone błędem pomiaru
- Po pojawieniu się nowego pomiaru wartość całki musi być zaktualizowana
   + Metoda Eulera
- Korzystanie z definicji całki Riemanna, w której wartość całki jest interpretowana jako suma pól obszarów pod wykresem krzywej w przedziale całkowania
- Sumę przybliżamy przy pomocy sumy pól prostokątów
- Bardzo prosta do wyliczenia
- Da się przenieść na układy nieliniowe
- Nie gwarantuje stabilności
- Mało dokładna
   + Metoda trapezów
- Tak jak metoda Eulera, ale trapezy zamiast prostokątów
- Powszechnie stosowana
- Gwarantuje stabilność
- Trudniejsza w obliczeniach analitycznych
- Nie przenosi się łatwo na obliczenia nieliniowe
- Większa złożoność obliczeniowa
• Kwadratury interpolacyjne, Rząd wielomianowy kwadratury
   + Kwadratury interpolacyjne
- Kwadraturą nazywamy numeryczne wyliczenie całki
- Przedstawienie wartości całki w przedziale za pomocą ważonej sumy jej wartości w n punktach
- Interpolujemy funkcję wielomianem i liczymy jego całkę
- Rodzaje kwadratur:
	-- Newtona-Cotesa: Interpolacja węzłów równoodległych, używalne tylko dla niskich rzędów
	-- Gaussa: Wykorzystująca własności wielomianów ortogonalnych
		--- Gauusa-Legendre’a: 
      ---- Węzły Legendre’a
      ---- Najdokładniejsza
      ---- O( n^3 )
		--- Clenshawa-Curtisa: 
			---- Węzły Czebyszewa
			---- Najprostsza
			---- O( n * log(n) )
   + Rząd wielomianowy kwadratury
- Dla każdego n >= 0 kwadratura interpolacyjna na n+1 węzłach ma rząd wielomianowy n
- Np. Gaussa-Legendre’a ma rząd 2n+1 (czyli jest 2x dokładniejsza)
• Warianty kwadratury Gaussa
   + Warianty kwadratury Gaussa
- Gauusa-Legendre’a:
      -- Dla liniowej funkcji wagowej
	-- Najdokładniejsza dla wielomianów
	-- Węzły i wagi można wyliczyć *tylko* numerycznie
	-- O( n^3 )
- Gaussa-Czebyszewa:
      -- Dla funkcji wagowej	w(x) = 1 / sqrt(1-x^2)
- Gaussa-Hermite’a:
	-- Dla funkcji wagowej	w(x) = e^(-x^2)
- Gaussa-Laguerre’a:
	-- Dla funkcji wagowej:	w(x)=e^(-x)
- Gaussa-Jacobiego:
	-- Dla funkcji wagowych będących wielomianami
- Gaussa-Lobatto:
	-- Modyfikacja GL, ale
		--- Pierwszy i ostatni węzeł to krańce przedziału
		--- Pozostałe węzły to pierwiastki wielomianu P’[n-1]
		--- Dokładna dla wielomianów stopnia 2n-3
		--- Wagi:
			---- Skrajne:	2 / n*(n-1)
			---- Pozostałe:	2 / n*(n-1)*(P[n-1]*x[i])^2
	
• Wyznaczanie wag i węzłów kwadratury Clenshawa-Curtisa
   + Metoda Clenshawa-Curtisa
- Najprostsza metoda
- Szybka obliczeniowo
- Może mieć problemy z dokładnością
   + Metoda
1) Wyliczenie wielomianu interpolacyjnego
2) Za pomocą FFT zamiana wielomianu na szereg Czebyszewa
3) Całki z poszczególnych wielomianów Czebyszewa są znane, więc obliczenie całki to suma iloczynów współczynników o odpowiadających im wartościach całek
• Wyznaczanie wag i węzłów kwadratury Gaussa-Legendre’a
   + Metoda Gaussa-Legendre’a
- Tylko numerycznie
- Algorytm Goluba-Welscha
- Istnieje szybszy algorytm Hale’a-Towsenda
	-- Wykorzystuje wzory asymptotyczne, a „krok Newtona” wykonuje się oddzielnie dla każdego węzła
• Kwadratury adaptacyjne (zasada działania)
   + Kwadratury adaptacyjne
- Liczenie kwadratur w podprzedziałach
- Jednoczesne liczenie kwadratur różnych rzędów, mających węzły na brzegach przedziału
- Jeśli różnica między kwadraturami jest duża, to stosujemy każdą z kwadratur na podprzedziałach pomiędzy wyliczonymi już węzłami
• Istnienie i jednoznaczność rozwiązania równania różniczkowego, warunek Lipschitza
   + Warunek Lipschitza
- Równanie różniczkowe ma rozwiązanie i jest ono jednoznaczne wtedy i tylko wtedy, gdy funkcja f spełnia warunek Lipschitza (ze względu na drugą zmienną):
	-- | f(x,z) – f(x,y) | <= L * | z – y |
• Metoda eulera w przód, dokładność, błąd, łamana Eulera
   + Metoda Eulera w przód
- Szukamy rozwiązania problemu na przedziale całkowania
	-- y’ = f(x,y)
	-- y(x[0]) = y[0]
	-- y(X) = ?
	-- x[0], x[1], ..., x[n-1], x[n] = X
- Na każdym podprzedziale stosujemy pierwszy wyraz szeregu Taylora
	-- y[1] – y[0] = ( x[1] – x[0] ) * f( x[0], y[0] )
	-- y[2] – y[1] = ( x[2] – x[1] ) * f( x[1], y[1] )
	-- ...
	-- y[n] – y[n-1] = ( x[n] – x[n-1] ) * f( x[n-1], y[n-1] )

	-- h: różnice między kolejnymi wyrazami x lub inaczej krok
	-- h = ( h[0], h[1], …, h[n-1] )
	-- h[i] = x[i+1] – x[i]
- Zakładamy, że funkcja w każdym podprzedziale jest dobrze reprezentowana przez styczną
- Uzupełnieniem punków y[1], y[2], y[3], ... jest **łamana Eulera**
- Skracając długość kroku *h* łamana Eulera zmierza jednostajnie do ciągłej funkcji fi(x)
- Jeśli funkcja oraz jej pochodne ze względu na x i y są ograniczone, to dla dostatecznie małych *h* globalny błąd metody Eulera jest proporcjonalny do długości maksymalnego kroku (czyli np. dla 3 miejsc po przecinku potrzeba 1000 kroków)
• Idea metod Rungego-Kutty, przykład metody punktu środkowego
   + Idea Rungego-Kutty
- Usprawnienie metody Eulera
- Metoda punktu środkowego – relatywnie prosta, mówi tylko, że wartość całki / równania różniczkowego zależeć będzie od wartości w połowie przedziału
- W porównaniu do Eulera – niestety mamy wartości nie w konkretnym punkcie, ale przesuniętą o pół,
	-- Można to rozwiązać wyliczająć wartość w punkcie, który nie jest wielokrotnością naszego kroku, robiąc mały krok metodą Eulera
• Ogólna postać metod Rungego-Kutty, tablice Butchera, rząd metody, błąd globalny
   + Postać Rungego-Kutty
- Składa się z s-etapów 
- Rozwiązanie każdego z etapów zależy od poprzedniego
- s-etapowa otwarta metoda RK ma postać:
	-- Wyliczamy pochodną w punkcie
	-- Wyliczamy pochodną w kolejnym punkcie, zależnym od poprzedniego x[0] i y[0]
	-- [Wykonujemy kroki Eulerem]
	-- **Rozwiązanie** jest pewną kombinacją wszystkich etapów z wagami pomnożoną razy krok
	k[1] = f( x[0], y[0] )
	k[2] = f( x[0] + c[2]*h, y[0] + h*a[2,1] * k[1] )
	k[3] = f( x[0] + c[3] * h, y[0] + h * ( a[3,1] * k[1] + a[3,2] * k[2] ) )
		…
	k[s] = f( x[0] + c[s] * h, y[0] + h * ( a[s,1] * k[1] + … + a[s, s-1] * k[s-1] ) )
	y[1] = y[0] + h * ( b[1] * k[1] + … + b[s] * k[s] )

	Gdzie:
		--- c[i] = suma [j=1 -> i-1] z a[i, j]
	-- Metoda Eulera czy punktu środkowego mieści się w tym jako szczególny przypadek
	-- Metoda RK ma rząd p, jeśli dla odpowiednio gładkich problemów (muszą istnieć wszystkie odpowiednie pochodne) zachodzi relacja, że błąd między rozwiązaniem analitycznym i numerycznym na jednym kroku da się oszacować przez jakąś stałą h do p+1
		-- || y( x[0] + h ) – y[1] || <= K * h^(p+1)
   + Tablice Butchera
- Sposób macierzowego zapisu metody Rungego-Kutty, w którym jasno widać które punkty i etapy są wykorzystywane w którym kroku i jak się konstruuje rozwiązanie
- Macierz trójkątna dolna, bez elementów na przekątnej (macierz podprzekątniowa) – dla metod otwartych
   + Rząd metody
- Najbardziej znane: metody RK 4-go rzędu 
	-- 4-ro etapowe
	-- Dokładność 4-rzędu
	-- Dużo zer w macierzy
	-- Tylko jedna popdprzekatna
   + Błąd globalny
- Jest rzędu K * h^n ze stałą zależną od długości przedziału
- Im dłuższy przedział tym dłuższy błąd
• Kontrola długości kroku, ogólna zasada, metody wbudowane, bariery Butchera
   + Kontrola długości kroku
- Pozwala na optymalne znalezienie rozwiązania w punkcie X
- Najstarszy sposób  - wykonać ponownie obliczenia z o połowę mniejszym krokiem – cyfry rozwiązania które się *nie* zmieniły *powinny* być poprawne (mało wydajna metoda)
- Lepszy sposób - **metody wbudowane** - rozwiązywać dwoma różnymi metodami jednocześnie, zmieniając tylko współczynniki b, co da nam rozwiązanie różnych rzędów – różnica między rozwiązaniami, jednym mniej a drugim bardziej dokładnym da nam estymatę błędu
- Problemem jest to, że trzeba „dołożyć” kilka dodatkowych etapów
   + Automatyczna kontrola długości kroku
- Małe kroki na obszarach, na których dużo się zmienia
- Duże kroki na obszarach, w których mało się zmienia
- Nakładamy pewną tolerancję na każdą zmienną – sc, która składa się z 2 komponentów:
	-- Tolerancji bezwzględnej ( max. dopuszczalny błąd )
	-- Tolerancji względnej ( procentowo )
- Wprowadzamy całkowitą miarę błędu err i porównujemy do 1
- Wynik jest wzorem na krok optymalny
- Finalnie, na zasadzie lokalnej ekstrapolacji z 2 metod bierze się rozwiązanie wyższego rzędu
   + Bariery Butchera
- Nie da się skonstruować pewnych metod, ponieważ nie da się spełnić wszystkich warunków algebraicznych
- Nie istnieją metody p etapowe rzędu p dla p > 4
- Nie istnieją metody p+1 etapowe rzędu p dla p > 6
- Nie istnieją metody p+2 etapowe rzędu p dla p > 7
• Gęste wyjście, ogólna idea, interpolacja Hermite’a, bootstraping, zastosowania
   + Gęste wyjście
- Połączenie rozwiązywania równania różniczkowego z interpolacją
- Konstruujemy wielomian interpolacyjny, który będzie łączył punkty między krokami które uzyskaliśmy
- „Tańsza” aproksymacja
- Zamiast stałych b[i] dajemy wielomiany b[i]
- Nieco więcej etapów
	-- y[1] = y( x[0] + h )	- rozwiązanie z RK
	-- Szukamy „taniej” aproksymacji y( x[0] + fi * h ) dla fi w (0, 1)
	-- Generalnie u(0) = y[0] + suma [i=1 -> s’] z b[i]( fi ) * k[i], 	gdzie
		b[i]( fi ) to wielomiany, takie że
			u( fi ) – y( x[0] + fi * h ) = O( h^(p’ + 1) )
- Chcemy, aby w każdym punkcie z przedziału, różnica między rozwiązaniem dokładnym, a naszym wielomianem była rzędu zależnego od długości kroku – czyli aby nie pogarszało za bardzo w stosunku do rozwiązania dokładnego
   + Interpolacja Hermite’a
- Interpolacja za pomocą wartości funkcji jak i jej pochodnej
- Najprostsza jest 3 stopnia – ponieważ wartości w punktach są już policzone, wartość pochodnej też już mamy (bo pierwszy krok metody Rungego-Kutty zawsze potrzebuje wartości na skraju przedziału)
- Dla wyższych rzędów musimy gdzieś wyliczyć pochodną w dodatkowych punktach pośrodku

   + Bootstraping
- „Dociągnięcie” wielomianu na wyższy rząd
	-- Konstruujemy wielomian 3 rzędu z Hermite’a
	-- Wyliczamy w jakimś punkcie wartość tego wielomianu, czyli znajdujemy jakiś punkt pośredniego rozwiązania, które będzie rzędu niższego
	-- Wyliczamy pochodną w tym punkcie
	-- Używając wartości poprzedniego wielomianu oraz nowej wartości dodatkowej otrzymujemy nowy wielomian
- Operację można powtarzać i podnosić rząd dokładności (z pewną stratą na stałej obliczeń)
   + Zastosowania
- **Event location** - sytuacja, w której równanie różniczkowe funkcjonuje na pewnych założeniach, albo interesuje nas trafienie / pojawienie się trajektorii w pewnym konkretnym miejscu – np. kiedy rozwiązanie będzie osiągało wartość dokładnie 0 (np. analizowanie odbijania się piłeczki od podłogi)
- W automatyce – w szczególności sterowanie przekaźnikowe (np. zmiana biegów w samochodzie czy włączenie ABS’u)
• Metody odwrotne Rungego-Kutty, ogólna idea, rodzaje, zalety, sposób implementacji
   + Metody odwrotne Rungego-Kutty
- Formalnie równanie różniczkowe jest równoważne równaniu całkowemu
	-- Formalne:
		y( x[1] ) = y( x[0] ) + całka [x[0] -> x[1]] z f( x, y(x) ) dx
	-- Metoda punktu środkowego:
		k[1] = f( x[0] + h / 2, y[0] + (h / 2) * k1 )
		y[1] = y[0] + h * k[1]
	-- Wzór trapezów:
		Y[1] = y[0] + (h/2) * ( f( x[0], y[0] ) + f( x[1], y[1] ) )
- Dla równań liniowych wszystko da się policzyć analitycznie
- Dla równań nieliniowych dla różniczek musmy zastosować schemat iteracyjny
   + Rodzaje metod RK:
	-- Jeśli a[i, j] = 0 dla i <= j – metoda otwarta (explicit RK)
	-- Jeśli a[i, j] = 0 dla i < j oraz co najmniej 1 a[i, i] = 0 – metoda diagonalnie odwrotna (diagonally implicit KR)
	-- W pozostałych przypadkach metoda odwrotna (implicit RK)
- Zaleta: rząd metod odwrotnych jest wyższy dla takich samych s
   + Systemy Hamiltonowskie
- Bazują na funkcji zwanej Hamiltonianem
- Opisuje ona relacje energetyczną w układzie
- W oparciu o niego możemy skonstruować układ równań różniczkowych, który ma bardzo szczegółową postać:
	-- p’[i] = dH/dq[i] ( p, q )
	-- q’[i] = dH/dp[i] ( p, q )
- Hamiltonian jest stały na rozwiązaniach (zasada zachowania energii)
- Strumień fazowy zachowuje objętość 
- Układy astronomiczne mają opis hamiltonowski
	
• Metody symplektyczne, ogólna idea, zastosowanie, zalety
   + Metody symplektyczne
- Stosowane do systemów z równaniami Hamiltona
- *Nie zachowują* Hamiltonianu, ale *zachowują* formę różniczkową i całki pierwsze równania
- Otwarte metody RK *nie będą* symplektyczne
- Metody odwrotne operate o kwadratury Gaussa *są* symplektyczne
   + Zastosowania
- Obliczenia astronomiczne
- Nowoczesne Hamiltoniany Monte Carlo i pokrewne
• Metody wielokrokowe, metody Adamsa, predyktor-korektor, różnic wstecznych
   + Metody wielokrokowe
- Metody rozwiązania równań różniczkowych (nie są tak dobre jak Rungego-Kutty)
- Metody wielokrotne, bazują na równaniu całkowym
- Przed erą komputeryzacji byly to jedyne używane metody, ponieważ w ówczesnym czasie obliczeń wydawały się najlepsze
- Idea – znając wartości z poprzednich kroków funkcji f, możemy zastąpić całkę wielomianem interpolacyjnym, tak działa m.in. metoda Adamsa:
   + Metody Adamsa
- Metoda *otwarta* k-tego rzędu
      -- Mamy wyliczone k-kroków do tyłu wartości pochodnych
      -- Liczymy wielomian interpolacyjny
      -- Liczymy jego całkę
      -- W oparciu o to wyliczamy wartość funkcji do przodu
- Szczególnym przypadkiem jest metoda Eulera w przód
- Metody odwrotne – dokładają dodatkowy punkt jeszcze dalej
- Wady:
	-- Wyliczanie prawej strony nie jest tak kosztowne jak przechowywanie jej w pamięci
	-- Wymagana duża regularność rozwiązania
	-- Zmiana długości kroku jest dużym problemem
   + Predyktor-korektor
- Połączenie obu metod Adamsa (odwrotnych i zamkniętych)
- Powstała w celu rozwiązywania równań algebraicznych potrzebnych do metod odwrotnych
- Idea:
	-- Liczymy Adamsem otwartym do przodu wartość rozwiązania – to jest **predyktor**
	-- Z tej metody wyliczamy wartość prawej strony równania różniczkowego (czyli mamy wartość „gorszej jakości” bo z metody otwartej
	-- Podstawiamy do metody zamkniętej (czyli wstecznej = odwrotnej)
	-- Wyliczamy wartość z dodatkową wartością pochodnej z metody odwrotnej
	-- Wyliczamy nową pochodną
   + Metody różnic wstecznych
- Metod wielokrokowych raczej się już nie stosuje – wyjątkiem są metody różnic wstecznych
- Wykorzystanie wielu wartości rozwiązania, tylko do wyliczenia wartości pochodnych w tym punkcie
- Zamiast całkowania wykorzystuje się wielomian interpolacyjny do estymacji pochodnej
• Zachowanie stabilności rozwiązania równania różniczkowego, równanie sztywne, A-stabilność
   + Zachowanie stabilności
- Rozwiązania równań powinny się zachowywać tak jak równania (czyli jeśli układ jest stabilny, to rozwiązanie też powinno być)
- **Stabilność w sensie Lapunowa** - „trochę mocniejsza ciągłość”, która mówi nam że mała zmiana warunku początkowego mało zmienia wyjście
- Jeśli mamy rozwiązanie i chcemy sprawdzić jego stabilność, to jeśli wystartujemy blisko rozwiązania to pozostaniemy blisko
- **Asymptotyczna stabilność** - układ nie tylko pozostaje blisko, ale nawet zmierza do ustalonego rozwiązania
   + Równanie sztywne
- Równania, w których dynamika układu potrafi zmieniać swoją sztywność
-* Wykrywanie sztywności:
	-- Specjalny estymator błędu
	-- Estymacja wartości własnej linearyzacji
	-- Wykrycie sztywności sugeruje przejście do metod dla równań sztywnych
   +* Funkcja stabilności
- Dane jest równanie różniczkowe
	y’ = l * y, 	y[0] = 1
- Jego rozwiązanie numeryczne ma postać:
	y[m+1] = R(h * l) * y[m]
- Niech z = h * l, wtedy R(z) nazywamy **funkcją stabilności**
   + A-Stabilność
- Jeśli l jest w lewej półpłaszczyźnie, to funkcja stabilności jest mniejsza od 1
- Odwrotne metody RK mają funkcję stabilności w postaci funkcji wymiernych
   +* L-stabilność i A(a) stabilność
- L stabilność jest mocniejsza, bo wymaga żeby R(inf) = 0
- A(a) stabilność jest słabsza, ponieważa wymaga aby sektor stabilności zawierał
	s[a] = { z;  | arg(-z) | < a, z != 0 }
";
	}
}
