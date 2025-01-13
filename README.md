# How to run the app

## Prerequisites
The be able to build & run the project, you need to have the following set up on your machine:

1. [.NET 9 SDK](https://dotnet.microsoft.com/download)
2. [Node.js / npm](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)

## Steps

1. Navigate to the directory where the web app is located
```sh
cd TripPlanner.Web
```

2. Install npm dependencies
```sh
npm install
```

3. Compile the CSS file using Tailwind CSS
```sh
npx tailwindcss -i wwwroot/css/app.css -o wwwroot/css/app.min.css
```
*Use `--watch` to watch for changes and rebuild as needed*

4. Run the web app
```sh
dotnet run
```