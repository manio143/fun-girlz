(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
Potrzebne oprogramowanie
========================

Będziemy głównie pracować w Visual Studio Code pisząc pliki `.fsx`, czyli skrypty w języku F#.
Na ten moment interpreter F# nie działa na platformie .NET Core, w związku z czym będziemy pracować na platformie .NET Framework v4.5 (lub kompatybilnej).

Można korzystać ze swojego ulubionego edytora kodu, ale polecam <u>VS Code z dodatkiem Ionide</u>, który usprawnia pisanie kodu w F#.

Windows
---------

Zainstaluj [Build Tools for Visual Studio 2017](https://www.visualstudio.com/pl/thank-you-downloading-visual-studio/?sku=BuildTools&rel=15) (m.in. MSBuild) zaznaczając w trakcie instalacji moduł F#.

Następnie zainstaluj [F# 4.1 Compiler SDK](http://download.microsoft.com/download/F/3/D/F3D6045E-4040-4058-ADAD-2698F1793CBC/Microsoft.FSharp.SDK.Core.msi) (najnowszą wersję kompilatora).

Narzędzia nas interesujące powinny znaleźć się w

    C:\Program Files (x86)\Microsoft SDKs\F#\4.1\Framework\v4.0

Którą to ścieżkę można dodać do globalnej zmiennej środowiskowej `PATH`.

Alternatywą jest korzystanie z Visual Studio z zainstalowanymi narzędziami do F#.

Linux
----------

Dodaj repozytorium Mono zgodnie z instrukcjami [na tej stronie](http://www.mono-project.com/download/stable/).

Debian/Ubuntu

    apt install fsharp

CentOS/RHEL/Fedora

    sudo yum install mono-complete fsharp

Arch/Manjaro

    aurman install fsharp


Inne
-------

Odwiedź stronę [organizacji F#](https://fsharp.org/) i zapoznaj się z odpowiednią zakładką ze strony 'Use'.


Wszystko co trzeba zainstalowane? Jeśli tak to przechodzimy do [pierwszego modułu](./lets.html)
*)
