# Technical Assessment - .NET Web API

A clean, production-ready .NET Web API that integrates with NewsAPI.org to fetch and return UAE news articles.

## Requirements

- **.NET SDK 9.0** or higher

## Architecture

This solution follows clean architecture principles with the following structure:

- **TechnicalAssessment** - Web API project (Controllers, Program.cs)
- **TechnicalAssessmentApplication** - Application layer (Handlers, Queries, DTOs, Mappers)
- **TechnicalAssessmentDomain** - Domain layer (Interfaces, Entities)

### Key Technologies

- **ASP.NET Core Web API** (.NET 9.0)
- **MediatR** - For CQRS pattern implementation
- **HttpClientFactory** - For typed HTTP client management
- **Swagger/OpenAPI** - API documentation

## Configuration

### API Key Setup

The API key can be configured in two ways:

#### Option 1: Environment Variable (Preferred for Production)

Set the environment variable:

```bash
NEWSAPI_API_KEY=7036b09db7e64f24891a22c6e5ab54b9
```

#### Option 2: appsettings.json (Local Development Only)

Update `appsettings.json`:

```json
{
  "NewsApi": {
    "ApiKey": "7036b09db7e64f24891a22c6e5ab54b9",
    "BaseUrl": "https://newsapi.org/v2"
  }
}
```

**Note:** Never commit API keys to version control. Use User Secrets or environment variables in production.

## How to Run

1. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

2. **Build the solution:**
   ```bash
   dotnet build
   ```

3. **Run the application:**
   ```bash
   dotnet run --project TechnicalAssessment
   ```

   Or use the solution file:
   ```bash
   dotnet run --project TechnicalAssessment/TechnicalAssessment.csproj
   ```

4. **Access the API:**
   - API Base URL: `https://localhost:5001` or `http://localhost:5000`
   - Swagger UI: `https://localhost:5001/swagger` (in Development mode)

## API Endpoints

### GET /api/v1/news

Fetches UAE news articles with pagination support.

#### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `page` | int | No | 1 | Page number (must be >= 1) |

#### Example Request

```http
GET /api/v1/news?page=1
```

#### Success Response (200 OK)

```json
{
  "page": 1,
  "pageSize": 20,
  "totalResults": 1234,
  "items": [
    {
      "source": "BBC News",
      "title": "Article Title",
      "summary": "Article description",
      "url": "https://example.com/article",
      "image": "https://example.com/image.jpg",
      "publishedAt": "2024-01-01T12:00:00Z"
    }
  ]
}
```

#### Error Responses

**400 Bad Request** - Invalid page parameter:
```json
{
  "code": "InvalidPage",
  "message": "Page must be greater than or equal to 1",
  "details": "Provided page value: 0"
}
```

**500 Internal Server Error** - External API error:
```json
{
  "code": "ExternalApiError",
  "message": "Unable to fetch news",
  "details": "400"
}
```

## Features Implemented

### Core Requirements ✅

- ✅ Public endpoint: `GET /api/v1/news?page=1`
- ✅ Page validation (must be >= 1)
- ✅ External API integration with NewsAPI.org
- ✅ Query parameter fixed to "uae"
- ✅ API key from configuration
- ✅ Clean response mapping

### Architecture Requirements ✅

- ✅ Thin Controller (uses MediatR)
- ✅ MediatR Query + Handler pattern
- ✅ Typed HttpClient using IHttpClientFactory
- ✅ Mapping in dedicated mapper class

### Enhancements Implemented ✅

#### Option B - Pagination Wrapper
The API returns a pagination wrapper with:
- `page`: Current page number
- `pageSize`: Fixed at 20 articles per page
- `totalResults`: Total number of results from external API
- `items`: Array of news articles

#### Option C - Custom Error Model
Custom error responses with:
- `code`: Error code identifier
- `message`: Human-readable error message
- `details`: Additional error details (status code or reason)
- No stack traces exposed to clients

## Project Structure

```
TechnicalAssessment/
├── Controllers/
│   └── NewsController.cs          # Thin controller using MediatR
├── Program.cs                      # Application startup and configuration
├── appsettings.json               # Configuration file
└── README.md                      # This file

TechnicalAssessmentApplication/
├── Handlers/
│   └── GetNewsQueryHandler.cs     # MediatR handler for news query
├── Queries/
│   └── GetNewsQuery.cs            # MediatR query definition
├── DTOs/
│   ├── NewsResponseDto.cs         # Pagination wrapper response
│   ├── NewsArticleResponseDto.cs  # Article response DTO
│   ├── ErrorResponseDto.cs        # Error response DTO
│   └── NewsArticleReponse.cs      # External API response model
└── Mappers/
    └── NewsMapper.cs              # Mapping logic

TechnicalAssessmentDomain/
└── Interfaces/                    # Domain interfaces
```

## Testing

### Using Swagger UI

1. Navigate to `https://localhost:5001/swagger` in your browser
2. Find the `GET /api/v1/news` endpoint
3. Click "Try it out"
4. Enter a page number (or leave default)
5. Click "Execute"

### Using cURL

```bash
curl -X GET "https://localhost:5001/api/v1/news?page=1" -k
```

### Using PowerShell

```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/v1/news?page=1" -Method Get
```

## Validation Rules

- **Page Parameter:**
  - Must be an integer
  - Must be >= 1
  - Defaults to 1 if not provided
  - Returns 400 Bad Request if invalid

## Error Handling

The API handles errors gracefully:

1. **Invalid Input (400)**: Returns clear validation messages
2. **External API Errors (500)**: Returns custom error model without exposing stack traces
3. **Internal Errors (500)**: Returns generic error message without exposing implementation details

## Notes

- The query parameter is hardcoded to "uae" as per requirements
- Page size is fixed at 20 articles per page
- All source.name values are mapped to the "source" field (empty string if missing)
- The API uses typed HttpClient via HttpClientFactory for better resource management
- MediatR is used for clean separation of concerns (CQRS pattern)

## License

This is a technical assessment project.


