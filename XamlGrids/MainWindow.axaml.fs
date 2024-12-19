namespace XamlGrids

open System.Collections.Generic
open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml

type Person = {FirstName : string; LastName : string; Age : int; Occupation : string }

type ViewModel = { People : IReadOnlyCollection<Person> }

type MainWindow () as this =
    inherit Window ()

    let vm = {
        People = [
            { FirstName = "Jim"; LastName = "Smith"; Age = 35; Occupation = "Printed Circuit Board Drafter" }
            { FirstName = "Charlotte"; LastName = "O'Shaughnessy-Alejandro"; Age = 30; Occupation = "Librarian" }
            { FirstName = "Ryan"; LastName = "Cullen"; Age = 40; Occupation = "Ceramics Instructor" }
            { FirstName = "Valentina"; LastName = "Levine"; Age = 38; Occupation = "Oceanologist" }
        ]
    }

    do
        this.InitializeComponent()
        this.DataContext <- vm

    member private this.InitializeComponent() =
#if DEBUG
        this.AttachDevTools()
#endif
        AvaloniaXamlLoader.Load(this)
