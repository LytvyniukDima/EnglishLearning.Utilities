version="1.0.5"
outputFolder="nupkg"
forProjectOutputFolder="../$outputFolder"
apiKey=""

projects=(
    'EnglishLearning.Utilities.All'
    'EnglishLearning.Utilities.Enums'
    'EnglishLearning.Utilities.Expressions'
    'EnglishLearning.Utilities.Linq'
    'EnglishLearning.Utilities.Configurations'
    'EnglishLearning.Utilities.Persistence'
    'EnglishLearning.Utilities.Persistence.Mongo'
)

rm -rf $outputFolder

for project in ${projects[*]}
do
    dotnet pack ./$project/$project.csproj -p:PackageVersion=$version --output $forProjectOutputFolder
    dotnet nuget push ./$outputFolder/$project.$version.nupkg -k $apiKey -s https://api.nuget.org/v3/index.json
done
