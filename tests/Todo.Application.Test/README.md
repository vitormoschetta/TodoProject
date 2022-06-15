### Running tests

```
dotnet test
```

### Running tests with coverage verification

```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage.opencover.xml
```

Install report generator:
```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Report generate:
```
reportgenerator -reports:**/coverage.opencover.xml -targetdir:coverage_report
```

Open `index.html`

