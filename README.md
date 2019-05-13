# ElearningWebsite

## Core Structure

1. Backend ElearningWebsite.API
    - api
        - DotNet Core 2.1
        - PORT 5000
        - localhost:5000/api/
    - database
        - PostgreSql
        - PORT 5432
2. Frontend ElearningWebsite-SPA
    - app
        - web
            - AngularJs
            - Single page application
            - PORT: 4200
            - localhost:4200/
3. README.md (you are here)

### Backend
  - API
    - Technologies
        - DotNet Core
  - Database
    - Technologies
        - PostgreSql

View API in detail at [API](./ElearningWebsite.API/API.md)

### Frontend
  - Web
    - Technologies
        - AngularJs
        - Bootstrap 4.x

## Setup and Running
1. Prerequisites
    - DotNet Core SDK 2.1.x
    - NodeJs v10.x
    - Angular 6.0.8
2. Clone repo 
3. API
    - cd into ElearningWebsite.API folder
    - config appsettings.json ConnectionStrings with your PostgreSql config
    - run following command
    ```shell
    dotnet ef database update
    dotnet restore
    dotnet run
    ```
4. Frontend
    - cd into ElearningWebsite-SPA folder
    - run following command 
    ```shell
    npm install
    ng serve
    ```

## Author
Kien Nguyen Duc


