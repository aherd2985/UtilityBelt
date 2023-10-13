# UtilityBelt - Fun Console Application

UtilityBelt is a console application designed to help you learn the basics of Git and C# while participating in Hacktoberfest. This project incorporates User Secrets to securely manage sensitive information. It's a great opportunity to enhance your coding skills and contribute to open-source development.

## User Secrets

User Secrets provide a secure way to manage sensitive information in your application. In UtilityBelt, you can store your secret data in a `secrets.json` file. Here's an example of what the `secrets.json` file might look like:

```json
{
  "SecretsModel": {
    "Email": "yourEmail@gmail.com",
    "EmailPassword": "yourPassword",
    "OpenWeatherMapApiKey": "yourApiKey",
    "DiscordWebhook": "yourWebhook"
  }
}
```

You can use these secrets to protect sensitive data, such as email credentials, API keys, or webhooks, without exposing them in your code.

## Getting Started

1. Get your Open Weather API Key: To use the Open Weather functionality, you can generate a free API key from the [OpenWeatherMap website](https://openweathermap.org/api).

2. Configure User Secrets: Add your secrets to the `secrets.json` file as shown in the example above.

3. Have Fun Coding: Dive into the world of Git and C#, participate in Hacktoberfest, and contribute to the project.

## Resources

- Learn more about User Secrets in ASP.NET Core: [User Secrets Documentation](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows)

- Find the Open Weather API Key: [OpenWeatherMap API](https://openweathermap.org/api)

## Get Involved

Feel free to reach out if you have any questions or concerns. Your contributions are greatly appreciated. Happy coding and happy Hacktoberfest!

[Contribute to UtilityBelt](https://technoherder.com/fredAPIsecret.php)
