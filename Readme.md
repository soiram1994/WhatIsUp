# WhatsUp API Gateway

This project implements an API Gateway using Ocelot to aggregate weather data, random cat facts, and news headlines into a single endpoint.

## Overview

This API Gateway provides the following functionalities:

1. **Weather Data**: Fetches weather data for a specified city using the OpenWeatherMap API.
2. **Cat Facts**: Retrieves a random cat fact from the Cat Facts API.
3. **News Headlines**: Fetches top news headlines for a specified country using the NewsAPI.


## Aggregator

The `MainAggregator` class is used to aggregate responses from the different downstream services. It tracks the response time for each service and includes it in the final aggregated response.

### MainAggregator Class

The `MainAggregator` class aggregates the responses from the weather, cat facts, and news APIs. It uses a switch statement to determine the key for each response based on the downstream path and includes the response content in the aggregated result.

### Response Aggregation Logic

- **Weather Data**: Fetched from the OpenWeatherMap API.
- **Cat Facts**: Fetched from the Cat Facts API.
- **News Headlines**: Fetched from the NewsAPI.


## Running the Application

1. **Clone the Repository**: Clone this repository to your local machine.
2. **Configure Environment Variables**:
   - Set the environment variables for the API keys:
      - `OPENWEATHERMAP_API_KEY`: Your OpenWeatherMap API key.
      - `NEWSAPI_API_KEY`: Your NewsAPI key.

   Example (Windows):
   ```sh
   set OPENWEATHERMAP_API_KEY=your_openweathermap_api_key
   set NEWSAPI_API_KEY=your_newsapi_key

