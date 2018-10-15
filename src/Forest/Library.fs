module Forest

type Level = | Log | Warn | Error

type ITarget =
  abstract member Write: string -> Level -> string -> unit

type Logger = { name: string; mutable targets: ITarget list } with
  member private this.Write (level: Level, message) =
    this.targets |> List.map(fun t -> t.Write(this.name)(level)(message)) |> ignore
  member this.Log message =
    Printf.ksprintf(fun res -> this.Write(Log, res)) message
  member this.Warn message =
    Printf.ksprintf(fun res -> this.Write(Warn, res)) message
  member this.Error message =
    Printf.ksprintf(fun res -> this.Write(Error, res)) message
  member this.AddTarget (target: ITarget) =
    this.targets <- target :: this.targets

let create (name: string) (targets: ITarget list) : Logger =
    { name = name; targets = targets }

let createChild (logger: Logger) (name: string) : Logger =
    create (logger.name + ":" + name) logger.targets
