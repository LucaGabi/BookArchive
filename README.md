# BookArchive - gestiune carti cu autori

De stiut:
1. apiul este pe dotnet core 3.1 EF core 5
2. patternuri folosite: repository pattern & unit of work, cqrs
3. maparea pentru dto <-> model fara AutoMapper - varianta cu automapper are branch separat (adaugat recent)
4. post / update pentru o carte foloseste un custom model binder care extinde [FromForm]
5. baza de date configurata partial cu fluent api
