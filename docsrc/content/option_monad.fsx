(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Monady
======================

Monada to pewien obiekt matematyczny z określonymi operacjami, który stosowany jest w programowaniu funkcyjnym w kilku różnych celach, w zależności od tego jak ta monada jest określona.

Pierwszy moment, kiedy spotykamy się z monadą w **czystym** języku programowania (np. Haskell) to kiedy mamy do czynienia z kontaktem ze światem. W Haskellu jedyny sposób aby otworzyć plik, zapisać coś na konsoli, itp. to użyć wbudowanej monady `IO`.

W F# takiej potrzeby nie ma, więc zaczniemy od monad, które ułatwiają życie. Pierwszą z nich będzie monada `Option`.

Jeśli pamiętamy to typ `Option` był taki:

*)

type Option<'a> = None | Some of 'a

(*** hide ***)
let doSomething _ = None

(**

Podczas jego używania będziemy bardzo często spotykać się takim wzorcem:

*)

let func opt =
    match opt with
    | None -> None
    | Some x -> doSomething x

(**

Z jednej strony mamy funkcję `Option.map`, ale jej użycie może nie zawsze być wygodne w porównaniu do monady `Option`, szczególnie jeśli połączymy ją z wyrażeniami komputacyjnymi F#.

Ale po kolei.

Monada w najprostszej postaci do pewien generyczny typ danych z jednym argumentem oraz dwie funkcje: `bind` i `return`. Zadaniem `return` jest opakować pewną wartość w daną monadę. Zadaniem `bind` jest umożliwić operacje na opakowanej wartości, w środowisku monady.

Najlepiej jest zrozumieć to na przykładzie.

*)

let optReturn x = Some x
let optFail = None

//optBind : Option<'a> -> ('a -> Option<'b>) -> Option<'b>
let optBind opt f =
    match opt with
    | None -> None
    | Some x -> f x

(**

Tak wygląda implementacja funkcji `return` oraz `bind` dla monady `Option`. Dorzuciłem jeszcze `fail`, żeby zaraz pokazać jak tego będziemy używać.

*)

let optDivide (a,b) =
    if b = 0 then optFail
    else optReturn (a / b)

let test1 = optBind (optReturn (4,2)) optDivide
let test2 = optBind (optReturn (4,0)) optDivide

(**

Ale prawdziwa moc pokaże się dopiero za chwilę. Bo dla jednej operacji oczywiście nie opłaca się tworzyć skomplikowanego wzorca. Nie przejmując się wcześniejszym możliwym błędem dodajemy 5 do wyniku.

*)

let optAdd5 x = optReturn (x + 5)

let test3 = optBind test1 optAdd5
let test4 = optBind test2 optAdd5

(**

Więc jeśli jest sukces to idziemy ścieżką sukcesu. Jeśli nie to propagujemy `None`.

A teraz wspomniane wyrażenia komputacyjne. To jest jedyny moment gdzie podczas tego warsztatu dotkniemy klas, bo jest to interfejs wykorzystywany przez kompilator F#.

*)

type OptionBuilder() =
    member this.Return(x) = optReturn x
    member this.Bind(m, f) = optBind m f

let option = OptionBuilder()

(**

Zdefiniowaliśmy pewnien typ i utworzyliśmy jego instancję. Pozwala nam to na coś takiego:

*)

let test5 b = 
    option {
        let! d = optDivide (7, b)
        let! e = optAdd5 d
        return e
    }

(**

Dostajemy tę super składnię to pracy z monadami. `return` jest oczywiste, a `let!` to w rzeczywistości `bind`. Wszystkie operacje są wykonywane w środowisku monady `Option` i jeśli w dowolnym momencie wystąpi błąd to na sam koniec dostaniemy `None`, ale nie musimy sie tym martwić i wielkrotnie sprawdzać.

Pozbyliśmy się wartości `null`, zastępując ją wartością `None`, a teraz nie musimy jej nawet sprawdzać.

Zadanie
---------

Napisz monadę na typie: *)

type Result<'TSuccess, 'TError> = Success of 'TSuccess | Failure of 'TError

(**

Ta monada jest ciut lepsza od monady `Option` w tym, że pozwala nam dodać informację co poszło nie tak, a nie tylko, że coś poszło nie tak.

Kompilator może mieć wąty kiedy używamy tylko jednego z typów, więc trzeba mu wtedy jawnie dodać typ deklarowanej wartości np:

*)

let succ10 : Result<int, 'e> = Success 10

(**

Ale zazwyczaj będzie się właściwie domyślał.

Pytania? Jeśli wszystko jasne, to przechodzimy do [następnego modułu](./state_monad.html)

*)