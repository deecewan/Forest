module Forest.ConsoleWriter

open Texta

type ConsoleWriter(directory: string, combinedLog: bool) =
  let writer name level message =
    let color = match level with
                | Log -> Texta.blue
                | Warn -> Texta.yellow
                | Error -> Texta.red
    let level = level.ToString().ToUpper()
    let out = sprintf "[%s] %s: %s" name level message
    printfn "%s" (color(out))

  interface Forest.ITarget with
    member this.Write name level message = writer(name)(level)(message)
