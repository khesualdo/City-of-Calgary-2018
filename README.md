# ART++ :phone: :movie_camera: :art:
Augmented Reality application to show art in real-time in real-size.

![Demo GIF](https://github.com/00111000/City-of-Calgary-2018/blob/master/demo.gif)

> You can find more details about the pedestrian comfort theme in our FAQs, but in a nutshell, we’re looking for participants to create something that falls into one of the three categories of pedestrian comfort: play, safety and health.

- **Play: Create something that helps people use our streets in a new ways and encourages community.**
- Safety: Create something that helps increase pedestrian safety on Calgary’s streets.
- Health: Create something that helps encourage Calgarians to get outside and be active.

We had to use a at least one dataset from the City of Calgary.

## My contribution

I worked on determining in which communities our app is being used (the green text in the upper-right corner of the GIF). For this I used the Community Boundaries dataset from the City of Calgary to get the polygon coordinates of each community. Given a single geo-coordinate I was able to determine the exact community using the Point Inclusion algorithm - [code](https://github.com/00111000/City-of-Calgary-2018/blob/52ca1eacf524b655dbf27bed5421025bac90ea9e/AR%20Pedestrian/Assets/GoogleARCore/Examples/AugmentedImage/Scripts/Boundaries.cs). The idea behind this was to record in which of the communities the people are the most active, as well as send data back to the City of Calgary, such as which art piece is the most famous, where the art piece is located, etc...

## Inspiration

<!q>

We decided to create a solution that will make sure our city steps it's game up with public art. The following are some benefits our solution introduces:

- Citizens do not have a voice
- Citizens engagement is limited
- Finished art usually does not fit in
- Time and money are not well spent

## What it does

- Encourages citizens to explore their city
- Everyone can be heard and participate in choosing an art peice in their community
- Shows the project before it is built, which allows the city to plan ahead without huge costs

## How we built it

- Used C# programming language
- Used Unity along side with ARCore
- Used [Community Boundaries dataset](https://data.calgary.ca/Base-Maps/Community-Boundaries/ab7m-fwn6)
- Used [Point Inclusion algorithm](https://www.geeksforgeeks.org/how-to-check-if-a-given-point-lies-inside-a-polygon/)

## Challenges we ran into

<!>

## Accomplishments that we're proud of

This was mostly a learning experience for most members of the group to expose them to Augmented Reality  implementation, as well as data processing and data visualization. ART++ is fully functional, and we hope to squish bugs, add features, and improve its performance in the future.

## What we learned

- Augmented Reality technologies
- Computational geometry algorithms
- Data processing
- Data visualization

## What's next for ART++
- Add new features
  - Donations
  - Extend to not just art (buildings, parks, bridges, etc...)
- Create a portal to upload art models
