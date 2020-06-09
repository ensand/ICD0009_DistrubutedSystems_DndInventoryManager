
MVC controllers:
~~~
dotnet aspnet-codegenerator controller -name QuizController                   -actions -m Domain.Quiz                   -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TextEntryQuestionController      -actions -m Domain.TextEntryQuestion      -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MultipleChoiceQuestionController -actions -m Domain.MultipleChoiceQuestion -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AnswerController                 -actions -m Domain.Answer                 -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

REST API controllers:
~~~
dotnet aspnet-codegenerator controller -name QuizController                   -actions -m Quiz                   -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TextEntryQuestionController      -actions -m TextEntryQuestion      -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MultipleChoiceQuestionController -actions -m MultipleChoiceQuestion -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TextEntryAnswerController        -actions -m TextEntryAnswer        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~
