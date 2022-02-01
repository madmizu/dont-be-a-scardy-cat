# Unity App - Don't Be a Scardy Cat

## Project Pitch:
The aim of this web application is to provide the user with a cat-themed game that is fun to play! This is a 2d platform, endless runner game. When the game begins, a cat character will begin running across the screen (from left to right). The player’s goal is to keep the game going for as long as possible while collecting fishes for extra points. The game ends when the cat hits an item that the cat is scared of (i.e. vaccum, blender, noise makers, water, spoiled food). 

### Framework/Mockup (frontend):
![frontend1](https://i.ibb.co/mt2J3ch/Game-View-Mock-up.jpg)
![frontend2](https://i.ibb.co/xf63SKY/Background-1.jpg)
![frontend3](https://i.ibb.co/D8PbrzB/Background-2.jpg)

## User Stories:
As a user, I should be able to:
- See the game rendered in the web browser
- Click ‘start’ to begin a game
- Press space bar to jump
- See points accumulating in the top corner of the screen
- Have points accumulate over time
- Have the character collide with (collect) fishes to gain extra points
- Have the character collide with 'bad things' (i.e. things cat is scared of) to end the game
- See 'Game Over' with total points earned when game has ended



## Project Requirements:
For this project, you must:

    ☑ Use a Rails API backend with a React frontend.
    ☑ Have at least two models with a one-to-many relationship on the backend, with full CRUD actions for at least one resource. (More than two related models is also fine — if you need three models and a many-to-many relationship, go for it!).
      Relationships:
        - One-to-many: Vacation ⇒ Lodging Options
        - One-to-many: Vacation ⇒ Food Options
        - One-to-many: Vacation ⇒ Activity Options
        - Many-to-many: Vacations ⇒ Users
      CRUD Actions:
        - Vacations: CREATE, READ, UPDATE, DESTROY
        - Lodgings: CREATE, READ, UPDATE, DESTROY
        - Foods: CREATE, READ, UPDATE, DESTROY
        - Activities: CREATE, READ, UPDATE, DESTROY
        - Users: CREATE, UPDATE
        - Vacation_Users (join table): CREATE
    ☑ Have at least two different client-side routes using React Router
        - See frontend mockup
    ☑ Build a separate React frontend application that interacts with the API to perform CRUD actions.
    ☑ Use good OO design patterns. You should have separate classes for each of your models, and create instance - and class methods as necessary.


## Bonus Deliverables:
As a user, it would be nice to be able to:
- See option to enter score with user's name into the high score list
- View high score list
- Invite friends to play
- Add friends to friend list
- See list of high scores amongst friends only

-----