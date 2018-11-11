version="1.0.1"
outputFolder="../nupkg"

dotnet pack ./EnglishLearning.Utilities.All/EnglishLearning.Utilities.All.csproj -p:PackageVersion=$version --output $outputFolder

dotnet pack ./EnglishLearning.Utilities.Enums/EnglishLearning.Utilities.Enums.csproj -p:PackageVersion=$version --output $outputFolder
dotnet pack ./EnglishLearning.Utilities.Expressions/EnglishLearning.Utilities.Expressions.csproj -p:PackageVersion=$version --output $outputFolder
dotnet pack ./EnglishLearning.Utilities.Linq/EnglishLearning.Utilities.Linq.csproj -p:PackageVersion=$version --output $outputFolder
dotnet pack ./EnglishLearning.Utilities.Configurations/EnglishLearning.Utilities.Configurations.csproj -p:PackageVersion=$version --output $outputFolder

dotnet pack ./EnglishLearning.Utilities.Persistence/EnglishLearning.Utilities.Persistence.csproj -p:PackageVersion=$version --output $outputFolder
dotnet pack ./EnglishLearning.Utilities.Persistence.Mongo/EnglishLearning.Utilities.Persistence.Mongo.csproj -p:PackageVersion=$version --output $outputFolder
