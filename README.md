# Labb-4

## Varje bok måste ha ett ISBN-nummer
* Filnamn: [LibrarySystem](Labb%204/LibrarySystem.cs#L21)
* Metod: AddBook
* Rad: 21
* Problem: Det går att lägga till en bok men en tom sträng i ISBN-nummerfältet.
* Lösning: Kontrollera att ISBN-nummer inte är tomt innan boken läggs till i systemet med [string.IsNullOrEmpty](Labb%204/LibrarySystem.cs#L25-28).

## Systemet ska inte tillåta dubbletter av ISBN-nummer
* Filnamn: [LibrarySystem](Labb%204/LibrarySystem.cs#L21)
* Metod: AddBook
* Rad: 21
* Problem: Det går att lägga till en bok med samma ISBN-nummer som en annan bok.
* Lösning: Kontrollera att ISBN-nummer inte redan finns i systemet innan boken läggs till med att använda metoden [SearchByISBN](Labb%204/LibrarySystem.cs#L51) för att söka efter boken med det angivna ISBN-numret. Om boken redan finns, skriv ut ett felmeddelande och returnera utan att lägga till boken.

## Böcker ska kunna tas bort ur systemet
* Filnamn: [LibrarySystem](Labb%204/LibrarySystem.cs#L40)
* Metod: RemoveBook
* Rad: 40
* Problem: Finns inga problemer med att ta bort en bok ur systemet.

## Böcker som är utlånade ska inte kunna tas bort från systemet
* Filnamn: [LibrarySystem](Labb%204/LibrarySystem.cs#L40)
* Metod: RemoveBook
* Rad: 40
* Problem: Det går att ta bort en bok som är utlånad.
* Lösning: Kontrollera att boken inte är utlånad med att använda propertyn `IsBorrowed` i klassen [Book](Labb%204/Book.cs#L9) innan boken tas bort. 

## Systemet ska erbjuda flera sökfunktioner för att hitta böcker (ISBN, Titel eller författare)
* Filnamn: [LibrarySystem](Labb%204/LibrarySystem.cs#L51)
* Metod: SearchByISBN, SearchByTitle, SearchByAuthor
* Rad: 46, 51, 56
* Problem: Det gåt att söka efter böcker med ISBN-nummer och författare men inte med titel.
* Lösning: Fixade så att det bryr sig om stor och liten bokstäver i sökningen. med hjälp av [string.Equals](Labb%204/LibrarySystem.cs#L46-47) och `StringComparison.OrdinalIgnoreCase` i metoden [SearchByTitle](Labb%204/LibrarySystem.cs#L51-54). 

## Sökningar ska kunna hitta böcker på delmatchningar (inte bara exakta matchningar)
* Filnamn: [LibrarySystem](Labb%204/LibrarySystem.cs#L51)
* Metod: SearchByISBN, SearchByTitle, SearchByAuthor
* Rad: 48, 57, 62
* Problem: Det går att söka efter böcker med ISBN-nummer, titel och författare
