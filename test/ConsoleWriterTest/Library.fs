module Forest.Test.ConsoleWriter.Library

open NUnit.Framework
open FsUnit

[<Test>]
let ``Hope and pray``() =
  let mutable thing = ""
  let print msg = thing <- (sprintf msg)
  let cw = (new Forest.ConsoleWriter.ConsoleWriter("nothing", false, print) :> Forest.ITarget)
  thing |> should equal ""
  cw.Write "LogName" Forest.Log "Test Log"
  thing |> should equal "LogName"

