# Tupla Web Store
How to run these thing?!
- Open project file 'Tupla_Web_Store.sln'
- In toolbar tab on the top, click Tools
- Go to 'NuGet Package Manager'
- Open 'Package Manager Console'
- Find the Default project, that's at the top in command-line window.
- select Default project to 'Tupla_Web_Store'
- In command-line, type "Drop-Database -Context DataContext"
- In command-line, type "Update-Database -Context DataContext"
- Find the Default project, that's at the top in command-line window.
- select Default project to 'Tupla.Data.Context'
- In command-line, type "Update-Database -Context TuplaContext"
- Run the web server by pressing F5 or CTRL +  F5
