module Forest.Test.ConsoleWriter.Library

open NUnit.Framework
open FsUnit
open System
open System.IO

let withConsole setup assertion =
  use writer = new StringWriter()
  Console.SetOut(writer);
  setup()
  assertion(writer.ToString())

[<Test>]
let ``The writer should be able to handle simple outputs``() =
  let cw = (new Forest.ConsoleWriter.ConsoleWriter() :> Forest.ITarget)
  withConsole(fun _ ->
    cw.Write "LogName" Forest.Log "Test Log"
  )(fun output -> output |> should startWith "\u001b[0;34m[LogName] LOG: Test Log\u001b[0m")
  withConsole(fun _ ->
    cw.Write "LogName" Forest.Warn "Test Warn"
  )(fun output -> output |> should startWith "\u001b[0;33m[LogName] WARN: Test Warn\u001b[0m")
  withConsole(fun _ ->
    cw.Write "LogName" Forest.Error "Test Error"
  )(fun output -> output |> should startWith "\u001b[0;31m[LogName] ERROR: Test Error\u001b[0m")
