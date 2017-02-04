module ParseOptions

open System


type ParseOptions() = 
    member this.StartIndex = "_pageData = \""
    member this.EndIndex = "\";</script>"
    member this.TypeDivider = "zIhLJ_"
    member this.SubtypeDivider = "[[\\\"Название RU\\\",[\\\""
    member this.Url = 
        Uri("https://www.google.com/maps/d/viewer?mid=19rjPjXRI3UWFhhWmr1FRzC6cdmM&hl=en&ll=55.729495970579094%2C37.6837816213374&z=12")