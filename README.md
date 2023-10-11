# GymFinder - AI-based Gym Information Web Crawler

Welcome to the GymFinder project! This is a C# .NET 2.0 application running on Azure App Services that utilizes ASP.NET and the Google Search API to find gyms in a given area, collect information about gym amenities, and store the data in a Cosmos DB database. The application allows users to search for gyms within a specified radius of a location.

## Features

- Uses the Google Search API to search for gyms in a specified area.
- Visits each gym's website to extract information about available amenities.
- Stores gym and amenity data in a Cosmos DB database.
- Allows users to search for gyms based on location and radius.
- Provides detailed information about gyms, including amenities offered.

## Prerequisites

Before you begin, ensure you have the following dependencies installed and set up:

- Visual Studio or Visual Studio Code for C# .NET development.
- An Azure account with an Azure App Service and a Cosmos DB instance was created.
- Access to the Google Search API for web scraping.

## Setup Instructions

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/your-username/GymFinder.git
   ```bash
   git clone https://github.com/your-username/GymFinder.git
   
Open the solution in Visual Studio or Visual Studio Code.
Configure your Azure App Service and Cosmos DB connection settings in the appsettings.json file.
Configure access to the Google Search API by obtaining an API key and adding it to the project settings.
Build and run the application.
The web crawler will start searching for gyms based on your location preferences.
Usage

Access the web application through the Azure App Service URL.
Enter a location and a search radius to find gyms in that area.
View the details of each gym, including the amenities they offer.
Contributing

Contributions are welcome! If you'd like to contribute to this project, please follow these guidelines:

Fork the repository.
Create a new branch for your feature or bug fix.
Make your changes and test them thoroughly.
Submit a pull request to the main repository.
License


Acknowledgments
Thanks to the Azure and Google APIs for enabling this project.
