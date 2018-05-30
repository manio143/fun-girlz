(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Operacja na funkcjach
======================

Skoro funkcje są wartościami, to nic nie stoi nam na przeszkodzie żeby używać ich z operatorami. Podstawową operacją na funkcjach jest ich złożenie:

*)

let (<<) f g = fun x -> f (g x)

let add2mul5 = (*) 5 << (+) 2
let add2mul5' x = (x + 2) * 5

(**

Wiadomo, które jest bardziej czytelne, ale w przypadku niektórych złożonych wyrażeń, pozbywamy się sporej ilości nawiasów stosując operator złożenia.

Kolejnym operatorem jest to z czego znany jest F#, czyli `|>` (pipe), który służy do pompowania wyników jednej funkcji do drugiej.

*)

let (|>) x g = g x

let add2mul5'' x = x + 2 |> (*) 5

(**

Widzicie różnicę? Pipe bierze wartość, a nie funkcję, więc będzie miał nieco inne zastosowania. Zazwyczaj składamy funkcje zanim mamy wartość do przesłania przez nie, a używamy pipe'a kiedy tą wartość od razu mamy.

    (f << g) x  <=>  f <| g x

*)

(**

Przełożenie funkcji krotki na funkcje argumentowe, jest rzadszym ale również spotykanym przypadkiem operacji na funkcjach. Polega ono na rozwijaniu parametrów z krotki.

*)

let uncurry f = fun x -> fun y -> f (x,y)
let curry f = fun (x,y) -> f x y

(**

Nie mam dobrego pomysłu na zadanie, które nie będzie trywialne, więc przejdźmy do [następnego modułu](./algeb.html)

*)