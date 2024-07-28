# WhatsUp API Aggregator

This project implements an API Gateway using Ocelot to combine weather data, random cat facts, and news headlines into a
single endpoint. Official documentation for Ocelot can be found [here](https://ocelot.readthedocs.io/en/latest/).

## Overview

This API Gateway provides the following functionalities:

1. **Weather Data**: Fetches weather data for a specified city using the OpenWeatherMap API.
2. **Cat Facts**: Retrieves a random cat fact from the Cat Facts API.
3. **News Headlines**: Fetches top news headlines for a specified country using the NewsAPI.

### MainAggregator Class

The `MainAggregator` class aggregates the responses from the weather, cat facts, and news APIs. It uses a switch
statement to determine the key for each response based on the downstream path and includes the response content in the
aggregated result.

### Response Aggregation Logic

- **Weather Data**: Fetched from the OpenWeatherMap API.
- **Cat Facts**: Fetched from the Cat Facts API.
- **News Headlines**: Fetched from the NewsAPI.

## Running the Application

1. **Clone the Repository**: Clone this repository to your local machine.
2. **Configure Environment Variables**:
    - Set the environment variables for the API keys:
        - `WEATHER_API_KEY`: Your OpenWeatherMap API key.
        - `NEWSAPI_API_KEY`: Your NewsAPI key.

   Example (Windows):
   ```sh
   set WEATHER_API_KEY=your_weather_api_key
   set NEWSAPI_API_KEY=your_newsapi_key
   ```
   Example (Linux):
   ```sh
    export WEATHER_API_KEY=your_weather_api_key
    export NEWSAPI_API_KEY=your_newsapi_key
    ```
3. Navigate to the project directory:
    ```sh
    cd WhatsUp.Aggregator
        
    ```
4. Run the application:
    ```sh
    dotnet run
    ```
5. The application will start on `https://localhost:5003` by default. You can test the API by using the
   `WhatsUp.http` file int the project directory.

## Checking request speed

There is a stats controller that can be used to check the speed of the requests. The controller has one endpoint:

- **/stats.{key}**: Returns the average time taken to process requests for the specified downstream service.

## Adding New Endpoints

You can follow ocelot documentation
to [add new endpoints](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html) to the API Gateway.
The `MainAggregator` class can be extended
to include additional endpoints, by adding a new switch case for the new downstream path. If custom delegation logic is
required, you can create a new class that implements the `IDelegatingHandler` interface and use the existing logic as a
template.
   

