# BeGiftee Desktop

Welcome to **BeGiftee Desktop**! Begiftee Desktop is a desktop application designed to simplify gift-giving by allowing users to create personalized wish lists. Family and friends can easily view these lists, ensuring they always give the perfect gift.

## Features

- **Gift Lists:** Get your familiar gift lists so you can .
- **Gift Suggestions:** Browse through a curated list of gifts for different occasions.
- **Cross-Platform:** Available on Windows, macOS, Android, and iOS.

### Code architecture

In the course of developing the solution, I have applied various design patterns and adhered to industry best practices to delineate file responsibilities, ensure compliance with SOLID principles, enhance testability, and facilitate scalability.

The solution comprises two primary projects: `BeGiftee`, which encapsulates the core implementation, and `BeGifteeTest`, dedicated to housing the unit tests for the application.

Within BeGiftee, the source code is organized under the `Source` directory, covering a range of crucial elements:

- `ContentPages`: This directory contains the application's various pages, designed solely for information display without bearing additional responsibilities.
- `ContentViews`: This section stores partial views that enable the reuse of shared XAML interface code, mirroring the functionality scope of ContentPages.
- `ViewModels`: This segment hosts the data layer for views, tasked with managing user events, operating with services, and refreshing the interface accordingly.
- `Helpers`: These classes offer methods to simplify interactions with other software components and enhance code readability.
- `Models`: Here are defined the data models for objects manipulated within the application, serving as the structural foundation.
- `Services`: Each class within this category fulfills a distinct purpose, such as interfacing with the backend or operating with specific entities like Gifts. Notably:
  - `HttpService`: A specialized service that executes HTTP requests to the backend REST API, providing standard HTTP operations while automatically injecting the user token into the Authorization header upon user authentication.
  - `Clients`: Implementations in this area utilize HttpService for backend interactions, adhering to interfaces defined in the Api folder to ensure contract compliance.
  - `Dto`: Data Transfer Objects (DTOs) encapsulate the backend's response format for each entity, which may diverge from the application's internal model representation.
  - `Mappers`: Because the server's returned data may not directly align with the format of our project's data models, we've implemented mappers. These specialized tools are responsible for reformatting DTOs into models, providing a tailored mapping structure for individual entities.

The project includes sample unit tests and integrates GitHub Actions to automate their execution, thus safeguarding the integrity of commits merged into the master branch by preventing the incorporation of failing code.

Of course there's a lot to do (see the TODO section of this README), but this application could serve as the skeleton for one desktop application that consumes REST services.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Before you begin, ensure you have the following tools installed:

- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with the Mobile development with .NET workload

### Installation

1. Clone the repository:
   ```bash
   git clone git@github.com:manuman94/begiftee-desktop.git
   # OR use HTTPS
   git clone https://github.com/manuman94/begiftee-desktop.git
   ```
2. Open the solution in Visual Studio.
3. Build and run the application

# TODO

- Include the labels CRUD.
- Make Friendship available.
- Create some localization system. Translating strings, date formatting, etc.
- Increase unit test coverage.
- Use Windows App Driver to implement some E2E tests.
- Code documentation (code comments, docs).

## License

This project has all rights reserved Jos� Manuel Blasco 2024 - see the [LICENSE.md](LICENSE) file for details.

## Acknowledgments

- Hat tip to anyone whose code was used
- Inspiration
- etc

## Contact

For any queries or further information, you can reach us at:

- Email: jm_bg@hotmail.com
