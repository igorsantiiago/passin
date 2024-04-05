## 🔑 PassIn Api

Essa API foi desenvolvida durante o evento da Rocketseat nlw-unite, onde a trilha que resolvi seguir foi a de C#/.Net

Durante o desenvolvimento dessa API, foram aplicados importantes conceitos do desenvolvimento backend, sendo alguns deles:

- Conceitos básicos de camadas do DDD
- Exceptions personalizadas utilizando filtro
- Organização de projeto

## 💻 Tecnologias

<div style="display: inline_block">
    <img align="center" alt="tecnologias" src="https://skillicons.dev/icons?i=dotnet,cs,sqlite,postman">
</div>


## ⚙️ Funcionalidades

- Evento
    - Cadastro de Eventos
    - Buscar Evento
    - Listagem de participantes a um evento

- Participante
    - Cadastro de participante a um evento
    - CheckIn do participante a um evento

## 📌 Endpoints

***🟢 PARTICIPANTE - Criar um participante***
```
https://localhost:7056/api/attendees/event/register/{eventId}
```
```
{
  "name": "string",
  "email": "string"
}
```
OBS: Substituir {eventId} por um guid de evento cadastrado.

***🔵 PARTICIPANTE - Listar participantes em um evento***
```
https://localhost:7056/api/attendees/{eventId}
```
```
{
  "name": "string",
  "email": "string"
}
```
OBS: Substituir {eventId} por um guid de evento cadastrado.


***🟢 EVENTO - Criar um Evento***
```
https://localhost:7056/api/event
```
```
{
  "title": "string",
  "details": "string",
  "maximumAttendees": 0
}
```

***🔵 EVENTO - Buscar um Evento***
```
https://localhost:7056/api/event/{id}
```
OBS: Substituir {id} por um guid de evento cadastrado.

***🟢 CHECKIN - Realizar um CheckIn***
```
https://localhost:7056/api/checkin/{attendeeId}
```
OBS: Substituir {attendeeId} por um guid de usuario(attendee) cadastrado.
