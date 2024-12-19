namespace DataGridFuncUI

open System
open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts
open Avalonia.Input
open Avalonia.Controls
open Avalonia.Layout
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Styling
open Avalonia.Markup.Xaml.Styling

module Main =
    type Person = {FirstName : string; LastName : string; Age : int; Occupation : string }

    let people = [
        { FirstName = "Jim"; LastName = "Smith"; Age = 35; Occupation = "Printed Circuit Board Drafter" }
        { FirstName = "Charlotte"; LastName = "O'Shaughnessy-Alejandro"; Age = 30; Occupation = "Librarian" }
        { FirstName = "Ryan"; LastName = "Cullen"; Age = 40; Occupation = "Ceramics Instructor" }
        { FirstName = "Valentina"; LastName = "Levine"; Age = 38; Occupation = "Oceanologist" }
    ]

    let create () =
        Component (fun ctx ->
            StackPanel.create [
                Grid.isSharedSizeScope true

                StackPanel.children [
                    ListBox.create [
                        ListBox.dataItems people
                        ListBox.itemTemplate (
                            DataTemplateView<Person>.create (
                                fun person ->
                                    Grid.create [
                                        Grid.rowDefinitions "Auto, Auto"
                                        Grid.showGridLines true
                                        Grid.columnDefinitions [
                                            ColumnDefinition(SharedSizeGroup = "A")
                                            ColumnDefinition(SharedSizeGroup = "B")
                                            ColumnDefinition(GridLength.Star)
                                            ColumnDefinition(SharedSizeGroup = "C")
                                        ]

                                        Grid.children [
                                            TextBlock.create [
                                                TextBlock.text person.FirstName
                                                Grid.column 0
                                                TextBlock.margin (6,0)
                                            ]
                                            TextBlock.create [
                                                TextBlock.text person.LastName
                                                Grid.column 1
                                                TextBlock.margin (6,0)
                                            ]
                                            TextBlock.create [
                                                TextBlock.text (string person.Age)
                                                Grid.column 2
                                                TextBlock.margin (6,0)
                                            ]
                                            TextBlock.create [
                                                TextBlock.text person.Occupation
                                                Grid.column 3
                                                TextBlock.margin (6,0)
                                            ]
                                        ]
                                    ]
                            )
                        )
                    ]

                    Separator.create []

                    Grid.create [
                        Grid.columnDefinitions [
                            ColumnDefinition(SharedSizeGroup = "A")
                            ColumnDefinition(SharedSizeGroup = "B")
                            ColumnDefinition(GridLength.Star)
                            ColumnDefinition(SharedSizeGroup = "C")
                        ]

                        Grid.children [

                            Button.create [
                                Button.content "This is the First Name"
                                Button.horizontalAlignment HorizontalAlignment.Stretch
                                Grid.column 0
                            ]

                            Button.create [
                                Button.content "Last"
                                Button.horizontalAlignment HorizontalAlignment.Stretch
                                Grid.column 1
                            ]

                            Button.create [
                                Button.content "Age"
                                Button.horizontalAlignment HorizontalAlignment.Stretch
                                Grid.column 2
                            ]

                            Button.create [
                                Button.content "Occupation"
                                Button.horizontalAlignment HorizontalAlignment.Stretch
                                Grid.column 3
                            ]
                        ]
                    ]
                ]
            ]
        )

type MainWindow() as this =
    inherit HostWindow()
    do
        base.Title <- "FuncUIGrids"
        base.Width <- 1000.0
        base.Height <- 450.0
        base.Content <- Main.create()
#if DEBUG
        this.AttachDevTools(KeyGesture(Key.F12))
#endif

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentTheme())
        this.Styles.Add (StyleInclude(baseUri = null, Source = Uri("avares://FuncUIGrids/Styles.axaml")))
        this.Styles.Load "avares://Avalonia.Controls.DataGrid/Themes/Fluent.xaml"

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop ->
             desktop.MainWindow <- MainWindow()
        | _ -> ()

        base.OnFrameworkInitializationCompleted()

module Program =

    [<CompiledName "BuildAvaloniaApp">]
    let buildAvaloniaApp () =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace(areas = Array.empty)

    [<EntryPoint; STAThread>]
    let main argv =
        buildAvaloniaApp().StartWithClassicDesktopLifetime(argv)
