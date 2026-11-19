# DECISIONS

Answer **yourself**, in your own words, in 3–5 sentences per question.
Refer to concrete files and classes from your own repo (for example `Features/Bookings/CreateBooking/...`).
An unanswered question gives 0 points.

## 1\. A rule you moved

Pick one business rule (R1–R8). In which file did it live in the starter and in which file does it live now? Why there?

*Am mutat regula R7 din Booking service in Booking in metodele OverlapsWith. Aceasta verifica daca 2 rezervari se suprapun. Am facut asta pt ca regulile sa fie in clasa Booking*

## 2\. Result instead of an exception

Why is `Result` a better choice than an exception for "the room is occupied"? What happens in your code when an `Error` is returned?

*Am folosit result pt erorile normale, cum ar fi camera ocupata. In loc sa arunc o exceptie returnez o eroare, iar endpoint ul raspunde cu 409*

## 3\. Who knows about EF Core?

Which classes in your project know about EF Core and which do not? What do you gain and what do you pay with Repository + Unit of Work?

*EF CORE e folosit in Infrastructure/Persistence si configurat in Program.cs. Clasele de domeniu si handlerele nu depind direct de EF Core. Avantajul e ca logica aplicatiei e separata de baza de date, dezavantajul e ca am mai multe clase si interfete*

## 4\. The flow of a request

Draw in text (ASCII arrows) the path of a `POST /api/rooms/{roomId}/bookings` request through your project, with the **real** names of files and classes, from the endpoint to the database.

POST /api/rooms/{roomId}/bookings

CreateBookingEndpoint primeste cererea

CreateBookingHandler verifica datele si regulile

Booking.Create creeaza rezervarea

BookingRepository adauga rezervarea

UnitOfWork salveaza modificarile prin AppDbContext

Datele ajung in SQLite

## 5\. What you rejected from what the AI gave you

Give a concrete example of code or an idea proposed by the AI that you corrected or rejected, and say why.

*Am cerut ai-ului sa implementeze proiectul in 3 etape separate, nu tot deodata. Am vrut sa verific compilarea si sa fac commit dupa fiecare etapa. Am pastrat o implementare simpla*

## 6\. If you had 30 more minutes

What would you improve and what risk did you leave in the project?

*As fi adaugat testele automate pt regulile rezervarilor.*

