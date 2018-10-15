module Forest.FileWriter

open System.IO
open System

type FileWriter(directory: string, combinedLog: bool) =
  // TODO: Is it better to just keep 1 stream open always and write to it?
  do()
    Directory.CreateDirectory(directory) |> ignore
  let getFileName level =
    Path.Combine(directory, level.ToString().ToLower()) + ".log"
  // separate these so we don't have to do the if check on every log message
  // It'd be awesome if F# supports short-hand destructuring like JS
  let writeStandard logName level message =
    File.AppendAllLines(getFileName(level), [|sprintf "%A -- [%s] {%s}" DateTime.Now logName message|])
  let writeCombined logName level message =
    writeStandard(logName)(level)(message) // write to the standard log, too
    File.AppendAllLines(
      getFileName("combined"),
      [|sprintf("%A -- [%s] %s: %s")(DateTime.Now)(level.ToString().ToUpper())(logName)(message)|]
    )

  let writer = if combinedLog then writeCombined else writeStandard
  interface Forest.ITarget with
    member this.Write a b c = writer(a)(b)(c)
  new(directory: string) = FileWriter(directory, true)
