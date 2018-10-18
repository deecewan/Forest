module Forest.ConsoleWriter

open Texta

type ConsoleWriter() =
  let writer name level message =
    let color = match level with
                | Forest.Log -> Texta.blue
                | Forest.Warn -> Texta.yellow
                | Forest.Error -> Texta.red
    let level = level.ToString().ToUpper()
    printfn "%s" (color("[%s] %s: %s") name level message)

  interface Forest.ITarget with
    member this.Write name level message = writer(name)(level)(message)
