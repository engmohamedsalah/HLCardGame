# Problem

We want to have a web service that allows us to play a game called “HIGHER OR LOWER”, without a
deck of cards.

# Rules of the game
The dealer shuffles a deck of 52 cards, draws the first card, and places it on the table.
The first player guesses if the next card will be higher or lower than the one on the table. The player
wins if he guessed correctly (if the card had the same face value, it counts as a win).
Move on to the next player, clockwise, until you’ve gone through the deck.
# Additional rules:
• No need to know the players, everyone is using the same UI (to simplify the API).
• It should support multiple games at the same time.


# Scope 
• Design REST service written in asp.net core, no UI required.
• Add Unit Tests, the application should have good test coverage.
• Follow the SOLID principles as much as possible.
• Use a real database(inmemory data structure)

# Project Technology Stack
- .Net Core
- Entity Framework Core (using InMemory Database)
- Docker for containerization the project
- The API is documented using swagger.

# Main Endpoints
![image](https://user-images.githubusercontent.com/4210492/116212010-b3bc2a00-a744-11eb-9bda-76750afc40c8.png)


 Method | Endpoint | Description |
--- | --- |  --- | 
POST|/Decks/new/shuffle/{numberOfPlayers} | Creates the new deck with shuffeld 52 cards | 
GET|/Decks/{deckId}| Gets the deck info by identifier|
GET|/Decks/{deckId}/DrawCard|Draws the card from the deck if possible|
GET|/Decks/{deckId}/GuessNextIs/{guessDirection}| Guesses the next card|
DELETE|/Decks/{deckId}|Deletes the Deck|


# How To Play
by using the previous endpoint you can simulate the game

### 1- use Decks/new/shuffle/{numberOfPlayers} for create new game by adding number of palyer greater than 1 

response will be 
```json
{
  "deckId": "a8ea40f0-5be4-4cd7-9238-186510c867d7",
  "shuffled": true,
  "playerTurn": 0,
  "nPlayer": 5,
  "remaining": 52
}
```
## please note that playerTurn means there is no card on the deck yet to you need to draw a card to start the game

### 2- use Decks/{deckId}/DrawCard
a card will be drawn from the list of remaining cards and this card will be the table card
now the playerTurn will be 1 

the result example as following
```json
{
  "color": "Black",
  "suit": "Clubs",
  "value": 5,
  "displayName": "5C"
}
```

## at any time that the draw a card will be called  it will not increase the player turn, only the first time to start the game


### 3-/Decks/{deckId}/GuessNextIs/{guessDirection}
this endpoint is used to guess the next card and the result will be as following
while the deck a card is the card on the table now
and the card is the one which is drawn recently
and if the result is correct, the game will be ended and the deck will be deleted

```json
{
  "sucess": true,
  "deckCard": {
    "color": "Black",
    "suit": "Clubs",
    "value": 5,
    "displayName": "5C"
  },
  "card": {
    "color": "Red",
    "suit": "Hearts",
    "value": 6,
    "displayName": "6H"
  }
}
```
### 4-GET/Decks/{deckId} 
this is being use for get deck info

```json
{
  "deckId": "1030ff60-9ee5-4676-9560-0ae2920c48c0",
  "shuffled": true,
  "playerTurn": 2,
  "nPlayer": 5,
  "remaining": 50
}
```
### 5-DELETE/Decks/{deckId}
if success it will return 200 Ok 
