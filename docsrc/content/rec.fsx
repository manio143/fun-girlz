(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Rekordy
======================

Aby modelować dane złożone z pól (np. dane z bazy danych) zamiast posługiwać się krotkami to użyjemy rekordów. Rekordy aby były funkcyjną strukturą danych są niemutowalne.

Na początku musimy zdefiniować typ rekordu:

*)
type SkinColor = Caucasian | Asian | Black

type Person = {
    Name : string;
    Age  : int;
    Skin : SkinColor;
}

(**

Aby utworzyć wartość tego typu posłużymy się bardzo podobną składnią, ale do przypisania wartości pól użyjemy `=`.

*)

let jim = { Name = "Jim"; Age = 1; Skin = Caucasian }

(**

Aby dokonywać modyfikacji stanu, będziemy tworzyli funkcje, które przyjmują rekord i zwracają nowy zaktualizowany.

*)

let makeOlder p = { p with Age = p.Age + 1 }
let jim' = makeOlder jim

(**

Powyżej widzimy składnię aktualizacji rekordów oraz składnię odwołania się do konkretnego pola.

*)

(**

Zadanie
========

Opracuj obiekt `Random`, który będzie zawierał stan (stałe oraz seed) służący do generowania kolejnej liczby pseudolosowej. Do tego napisz funkcję `newRandom`, która przyjmuje wartość początkową seeda, oraz funkcję `nextRandom` o sygnaturze `Random -> Random * int`. Przetestuj działanie generatora liczb pseudolosowych tworząc funkcję `randomArray`, która przyjmuje początkowy seed i liczbę elementów do wygenerowania.

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./option_monad.html)

*)